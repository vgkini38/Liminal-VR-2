using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputAcceptor : MonoBehaviour
{
 private void Awake(){
	 Lightintens.onTriggerDown+=TriggerDown;
	 Lightintens.onTriggerUp+=TriggerUp;
	 }
	 private void TriggerDown(){
		 print("Trigger Down");
		 }
		 private void TriggerUp(){
			 }
}
