using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxScript : MonoBehaviour {

    private void OnDestroy()
    {
        GameObject temp=null;
        if(this.transform.childCount>0){
            temp=this.transform.GetChild(0).gameObject;
            temp.transform.parent=null;
            temp.gameObject.SetActive(true);
    }
    }
}