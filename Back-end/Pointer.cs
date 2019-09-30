using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Pointer : MonoBehaviour
{
    
    public float m_Distance = 10.0f;
    public LineRenderer m_LineRenderer = null;
    public LayerMask m_EverythingMask = 0;
    private float time = 0f;
    public GameObject fpsText;
    
    public LayerMask m_InteractableMask =   0;
    public UnityAction<Vector3, GameObject> OnPointerUpdate = null;
    

    public Transform m_CurrentOrigin = null;
    private GameObject m_CurrentObject = null;
    private GameObject gb = null;
    private GameObject previousObject = null;
    public LanternMovement LM;

    private void Awake()
    {
        PlayerEvent.OnControllerSource += UpdateOrigin;
        // PlayerEvent.OnTouchpadDown += ProcessTouchpadDown;
    }

    private void Start()
    {
        SetLineColor();
        LM = new LanternMovement();
    }

    private void OnDestroy()
    {
        PlayerEvent.OnControllerSource -= UpdateOrigin;
        // PlayerEvent.OnTouchpadDown -= ProcessTouchpadDown;
    }
   

    private void Update()
    {
        Vector3 hitpoint = UpdateLine();
        // m_CurrentObject = UpdatePointerStatus();

        // Create ray
        RaycastHit hit = CreateRaycast(m_InteractableMask);

        //fpsText.GetComponent<TextMesh>().text = hit.collider.gameObject.name;

       // check hit
        if (hit.collider)
        {
            gb = hit.collider.gameObject;

            if (gb.name == previousObject.name && gb.tag == "lantern")
            {
                time += Time.deltaTime;
                gb.GetComponent<Interactable>().StartFlare();
                if (time >= 5)
                {
                    fpsText.GetComponent<TextMesh>().text = hit.collider.gameObject.name;
                    time = 0f;
                    // gb.GetComponent<Interactable>().StopFlare();
                    gb.GetComponent<Interactable>().LitItUp();
                    Myfunction(gb.name);
                }
            }
            else
            {
                time = 0f;
                // previousObject.GetComponent<Interactable>().StopFlare();
            }

            // m_CurrentObject = hit.collider.gameObject;
            fpsText.GetComponent<TextMesh>().text = "Lantern Hit..";
        }
        else
        {
            // m_CurrentObject = null;
            // previousObject.GetComponent<Interactable>().StopFlare();
            fpsText.GetComponent<TextMesh>().text = "Current object null..";
            gb.GetComponent<Interactable>().StopFlare();
        }

        previousObject = gb;
    }

    public void Myfunction(string name)
    {
        var lantern = GameObject.Find(name);
        lantern.GetComponent<Interactable>().MoveSpeed = LanternMovementSpeed;
        lantern.GetComponent<Interactable>().TargetP = LanternPosition;
        lantern.GetComponent<Interactable>().TargetAvailable = true;
        Debug.Log("Lantern " + name + " Done...");
    }

    public Vector3 LanternPosition { get { return LM.GetLaternTargetPosition(); } }
    public float LanternMovementSpeed { get { return LM.GetLanternMovementSpeed(); } }

    private Vector3 UpdateLine()
    {
        // create hit
        RaycastHit hit = CreateRaycast(m_EverythingMask);

        // Default end
        Vector3 endPosition = m_CurrentOrigin.position + (m_CurrentOrigin.forward * m_Distance);

        // Check hit
        if (hit.collider != null)
            endPosition = hit.point;

        // Set position
        m_LineRenderer.SetPosition(0, m_CurrentOrigin.position);
        m_LineRenderer.SetPosition(1, endPosition);

        return endPosition;
    }

    private void UpdateOrigin(OVRInput.Controller controller, GameObject controllerObject)
    {
        // set origin of pointer
        m_CurrentOrigin = controllerObject.transform;

        // is laser visible
        if (controller == OVRInput.Controller.Touchpad)
        {
            m_LineRenderer.enabled = false;
        }
        else
        {
            m_LineRenderer.enabled = true;
        }
    }

    private GameObject UpdatePointerStatus()
    {
        // Create ray
        RaycastHit hit = CreateRaycast(m_InteractableMask);

        // check hit
        if (hit.collider)
        {
            return hit.collider.gameObject;
        }
       
        return null;
    }
   
    private RaycastHit CreateRaycast(int layer)
    {
        RaycastHit hit;
        Ray ray = new Ray(m_CurrentOrigin.position, m_CurrentOrigin.forward);
        Physics.Raycast(ray, out hit, m_Distance, layer);
        return hit;
    }

    private void SetLineColor()
    {
        if (!m_LineRenderer)
            return;

        Color endColor = Color.white;
        endColor.a = 0.0f;
        m_LineRenderer.endColor = endColor;
    }

    private void ProcessTouchpadDown()
    {
        
    }
}
