using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEvent : MonoBehaviour
{
    Pointer p = new Pointer();
    #region Events
    public static UnityAction OnTouchpadUp = null;
    public static UnityAction OnTouchpadDown = null;
    public static UnityAction<OVRInput.Controller, GameObject> OnControllerSource = null;
    #endregion


    #region Anchors
    public GameObject m_LeftAnchor;
    public GameObject m_RightAnchor;
    public GameObject m_HeadAnchor;
    #endregion

    #region Input
    private Dictionary<OVRInput.Controller, GameObject> m_ControllerSets = null;
    private OVRInput.Controller m_InputSource = OVRInput.Controller.None;
    private OVRInput.Controller m_Controller = OVRInput.Controller.None;
    private bool m_InputActive = true;
    #endregion

    private void Awake()
    {
        OVRManager.HMDMounted += PlayerFound;
        OVRManager.HMDUnmounted += PlayerLost;

        m_ControllerSets = CreateControllerSets();
    }

    private void OnDestroy()
    {
        OVRManager.HMDMounted -= PlayerFound;
        OVRManager.HMDUnmounted -= PlayerLost;

    }
    private void Update()
    {

        // Check for Active Input
        if (!m_InputActive)
            return;
        // Check if controller exist
        CheckForController();
        // Checking input source
        CheckInputSource();
        // Checking Actual Input
        Input();
    }

    private void CheckForController()
    {
        OVRInput.Controller controllerCheck = m_Controller;

        // Right Controller
        if (OVRInput.IsControllerConnected(OVRInput.Controller.RTrackedRemote))
            controllerCheck = OVRInput.Controller.RTrackedRemote;

        // Left Controller
        if (OVRInput.IsControllerConnected(OVRInput.Controller.LTrackedRemote))
            controllerCheck = OVRInput.Controller.LTrackedRemote;

        // if no controller, headset
        if (!OVRInput.IsControllerConnected(OVRInput.Controller.LTrackedRemote) &&
            !OVRInput.IsControllerConnected(OVRInput.Controller.RTrackedRemote))
            controllerCheck = OVRInput.Controller.Touchpad;

        // Update
        m_Controller = UpdateSource(controllerCheck, m_Controller);
    }

    private void CheckInputSource()
    {
        // Left
        if (OVRInput.GetDown(OVRInput.Button.Any, OVRInput.Controller.LTrackedRemote))
        {
            Debug.Log("Controller Check: " + m_Controller);
        }

        // Right
        if (OVRInput.GetDown(OVRInput.Button.Any, OVRInput.Controller.RTrackedRemote))
        {
            Debug.Log("Controller Check: " + m_Controller);
        }

        // Headset
        if (OVRInput.GetDown(OVRInput.Button.Any, OVRInput.Controller.Touchpad))
        {
            Debug.Log("Controller Check: " + m_Controller);
        }

        // Update
        m_InputSource = UpdateSource(OVRInput.GetActiveController(), m_InputSource);
    }

    private void Input()
    {
        if (OnTouchpadDown != null)
            OnTouchpadDown();
    }

    private OVRInput.Controller UpdateSource(OVRInput.Controller check, OVRInput.Controller previous)
    {
        // if values are same
        if (check == previous)
            return previous;

        // get the controller object
        GameObject controllerObject = null;
        m_ControllerSets.TryGetValue(check, out controllerObject);

        // if no controller, set to head
        if (controllerObject == null)
            controllerObject = m_HeadAnchor;

        // send out the event
        if (OnControllerSource != null)
            OnControllerSource(check, controllerObject);

        // return new value
        return check;
    }


    private void PlayerFound() { m_InputActive = true; }

    private void PlayerLost() { m_InputActive = false; }

    private Dictionary<OVRInput.Controller, GameObject> CreateControllerSets()
    {
        Dictionary<OVRInput.Controller, GameObject> newsets = new Dictionary<OVRInput.Controller, GameObject>()
    {
        { OVRInput.Controller.LTrackedRemote, m_LeftAnchor},
        { OVRInput.Controller.RTrackedRemote, m_RightAnchor},
        { OVRInput.Controller.Touchpad, m_HeadAnchor}
    };
        return newsets;
    }
}
