using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomberman_Enemy : MonoBehaviour {
    string moveto;
    public float timetomove = 1;
    Vector3 pos;
    Boomberman_gameController temp;
    private void Start()
    {
        moveto = "";
        temp = Boomberman_gameController.ins;
    }
    // Update is called once per frame
    void Update () {
        pos = transform.position;
        int index = -1;
        Dictionary<string, bool> allow = new Dictionary<string, bool>();
        if (gameObject.name == "orangeEnemy(Clone)")
        {
            allow = temp.allowMoves("enemy2");
            index = 1;
        }
        else if (gameObject.name == "enemyRed(Clone)")
        {
            allow = temp.allowMoves("enemy1");
            index = 0;
        }
        else if (gameObject.name == "purpleEnemy(Clone)") {
            allow = temp.allowMoves("enemy3");
            index = 2;
        }
        else{
            Destroy(gameObject);
        }
        KeyValuePair<int,int>enemyPos= temp.getenemyPos(index);
        int k = enemyPos.Key, v = enemyPos.Value;
        if(moveto==""){
            {
                List<string> moves = new List<string>();
                if (allow["right"])
                    moves.Add("right");
                if (allow["up"])
                    moves.Add("up");
                if (allow["left"])
                    moves.Add("left");
                if (allow["down"])
                    moves.Add("down");
                int i = Random.Range(0, moves.Count);
                moveto = moves[i];
            }
        }
        else if (!allow[moveto])
        {
            List<string> moves = new List<string>();
            if (allow["right"])
                moves.Add("right");
            if (allow["up"])
                moves.Add("up");
            if (allow["left"])
                moves.Add("left");
            if (allow["down"])
                moves.Add("down");
            int i = Random.Range(0, moves.Count);
            moveto = moves[i];
        }
        if (timetomove < 0)
        {
            timetomove = 1;
            if (moveto == "up")
            {
                gameObject.transform.position = new Vector3(pos.x, pos.y, pos.z + 1f);
                temp.setEnemyPos(index,k - 1, v);
            }
            if (moveto == "down")
            {
                gameObject.transform.position = new Vector3(pos.x, pos.y, pos.z - 1f);
                temp.setEnemyPos(index,k + 1, v);
            }
            if (moveto == "right")
            {
                gameObject.transform.position = new Vector3(pos.x + 1f, pos.y, pos.z);
                temp.setEnemyPos(index,k, v + 1);
            }
            if (moveto == "left")
            {
                gameObject.transform.position = new Vector3(pos.x - 1f, pos.y, pos.z);
                temp.setEnemyPos(index,k, v - 1);
            }
        }
        else{
            timetomove -= 0.01f;
        }
       
	}
}
