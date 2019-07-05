using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomberman_playerMovement : MonoBehaviour
{
    Vector3 pos;
    Dictionary<string,bool> canMove;
    public GameObject boomb;
    // Update is called once per frame
    void Update()
    {
        //refrence for the object ins in gameController class so the playerMovement can deal with the gameController 
        Boomberman_gameController temp = Boomberman_gameController.ins;
        //call the allowMoves function to know what dirction can the player go 
        canMove = temp.allowMoves("player");
        //call the getPlayerPos function to know the indexs of player in thegame array in gameController class 
        KeyValuePair<int, int> playerPos = temp.getPlayerPos();
        int k = playerPos.Key, v = playerPos.Value;
        pos = gameObject.transform.position;
        //check the  input and the canMove List to move the player
        if (Input.GetKeyDown(KeyCode.UpArrow)&&canMove["up"])
        {
            gameObject.transform.position = new Vector3(pos.x, pos.y, pos.z + 1f);
            temp.setPlayerPos(k - 1, v);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && canMove["down"])
        {
            gameObject.transform.position = new Vector3(pos.x, pos.y, pos.z - 1f);
            temp.setPlayerPos(k + 1, v);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)&&canMove["left"]){
            gameObject.transform.position = new Vector3(pos.x-1f, pos.y, pos.z);
            temp.setPlayerPos(k , v-1);
        }
        if(Input.GetKeyDown(KeyCode.RightArrow)&&canMove["right"]){
            gameObject.transform.position = new Vector3(pos.x +1f, pos.y, pos.z);
            temp.setPlayerPos(k, v+1);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (temp.canPutBomb())
            {
                GameObject b = Instantiate(boomb, pos, Quaternion.identity);
                if(b!=null){
                    temp.addBomb();
                }

            }
        }

        }
    private void OnTriggerEnter(Collider other)
    {
        Boomberman_gameController temp = Boomberman_gameController.ins;
        switch (other.gameObject.tag)
        {
            case "doubleBomb":
                temp.activeDuobleBomb();
                Destroy(other.gameObject);
                break;
            case "bigBomb":
                temp.activeBigBomb();
                Destroy(other.gameObject);
                break;
            case "key":
                doorScript.ins.openDoor();
                Destroy(other.gameObject);
                break;
            case "Finish":
                temp.gameOver("win");
                break;
            case "enemy":
                Destroy(gameObject);
                break;
        }
    }
    private void OnDestroy()
    {
        Boomberman_gameController.ins.gameOver("lost");
    }

}
