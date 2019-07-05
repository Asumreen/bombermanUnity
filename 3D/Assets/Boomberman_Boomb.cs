using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomberman_Boomb : MonoBehaviour {
    public GameObject fier;
    Vector3 pos;
	// Use this for initialization
	void Start () {
        pos = gameObject.transform.position;
	}

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 3.0f);
    }
    private void OnDestroy()
    {
        Boomberman_gameController.ins.removeBomb();
        Instantiate(fier, pos, Quaternion.identity);
        Instantiate(fier,new Vector3(pos.x-1,pos.y,pos.z), Quaternion.identity);
        Instantiate(fier, new Vector3(pos.x + 1, pos.y, pos.z), Quaternion.identity);
        Instantiate(fier, new Vector3(pos.x, pos.y, pos.z-1), Quaternion.identity);
        Instantiate(fier, new Vector3(pos.x , pos.y, pos.z+1), Quaternion.identity);
        if(Boomberman_gameController.ins.getBigBombActive()){
            Instantiate(fier, new Vector3(pos.x - 1, pos.y, pos.z-1), Quaternion.identity);
            Instantiate(fier, new Vector3(pos.x - 1, pos.y, pos.z+1), Quaternion.identity);
            Instantiate(fier, new Vector3(pos.x +1, pos.y, pos.z-1), Quaternion.identity);
            Instantiate(fier, new Vector3(pos.x + 1, pos.y, pos.z+1), Quaternion.identity);
            Instantiate(fier, new Vector3(pos.x + 1, pos.y, pos.z + 2), Quaternion.identity);
            Instantiate(fier, new Vector3(pos.x , pos.y, pos.z + 2), Quaternion.identity);
            Instantiate(fier, new Vector3(pos.x - 1, pos.y, pos.z + 2), Quaternion.identity);

        }
    }
}
