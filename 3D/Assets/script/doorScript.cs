using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorScript : MonoBehaviour {

	public Texture opendoor;
	public static doorScript ins;
void Awake(){
	//singlize the object door so i can call the same door from any class
	ins=this;
	//set the door angel to face the main camera
	this.transform.Rotate(new Vector3(0,180,0));
}
//this function called when the player take the key 
 public void openDoor(){
	this.GetComponent<MeshCollider>().enabled=true;
	this.GetComponent<MeshRenderer>().material.SetTexture("_MainTex",opendoor);
	
 }
}
