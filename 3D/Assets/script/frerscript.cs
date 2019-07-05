using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frerscript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
      Destroy(gameObject, 0.8f);
	}
    private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.tag!="border"&&other.gameObject.tag!="Finish"&&other.gameObject.tag!="pickup"&&other.gameObject.tag!="key"){
      
           Destroy(other.gameObject);
        }
    }
  
}
