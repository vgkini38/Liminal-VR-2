using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lightintens : MonoBehaviour
{   public static UnityAction<bool> onHasController =null;
    public static UnityAction onTriggerUp=null;
	public static UnityAction onTriggerDown=null;
	public static UnityAction onTouchpadUp=null;public static UnityAction onTouchpadDown=null;
	private bool hasController=false;
	private bool inputActive=true;
	public float lightstep=1.0f;
	public GameObject fireLight;
	private void Awake(){
		OVRManager.HMDMounted+=PlayerFound;
		OVRManager.HMDUnmounted+=PlayerLost;
		
	}
    void Start()
    {
		
	}
	private void OnDestroy(){
	OVRManager.HMDMounted-=PlayerFound;
	OVRManager.HMDUnmounted-=PlayerLost; 
	}
    void Update()
    {
	if (!inputActive)
		return;
		
		hasController= CheckForController(hasController);
		bool isPressingTouchpad = OVRInput.Get(OVRInput.Button.PrimaryTouchpad, OVRInput.Controller.LTrackedRemote) || OVRInput.Get(OVRInput.Button.PrimaryTouchpad, OVRInput.Controller.RTrackedRemote);
		if(isPressingTouchpad==true&&fireLight.GetComponent<Light>().intensity <12){	
				fireLight.GetComponent<Light>().intensity += lightstep*Time.deltaTime*6;	

		}
		float triggerValue=OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
		if(triggerValue>0.5f&&fireLight.GetComponent<Light>().intensity >1){	
		fireLight.GetComponent<Light>().intensity -= lightstep*Time.deltaTime*6;	
			}
			
    }
		/*if (Input.GetMouseButtonDown(0) && fireLight.GetComponent<Light>().intensity < 11)
		fireLight.GetComponent<Light>().intensity += lightstep;*/
		
	
private bool CheckForController(bool currentValue)
{
	bool controllerCheck=OVRInput.IsControllerConnected(OVRInput.Controller.RTrackedRemote)||OVRInput.IsControllerConnected(OVRInput.Controller.LTrackedRemote);
	if(currentValue==controllerCheck)
	return currentValue;
if(onHasController!=null)
	onHasController(controllerCheck);
	return controllerCheck;
}
	private void PlayerFound(){
		inputActive= true;
	}
	private void PlayerLost(){
		inputActive= false;
	}
   
}
