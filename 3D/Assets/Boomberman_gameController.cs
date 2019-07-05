using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Boomberman_gameController : MonoBehaviour
{

    public GameObject floar;
    public GameObject floarParent;
    public GameObject border;
    public GameObject borderParent;
    public List<GameObject> pickups;
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject player;
    public GameObject box;
    public GameObject boxParent;
    public Image bigBombImage;
    public Image doubleBombImage;
    GameObject[][] thegame;

    public static Boomberman_gameController ins = null;
    KeyValuePair<int, int> playerPos;
    List<KeyValuePair<int, int>> enemyPos;
    GameObject doorBox;
    const int size = 17;
    Vector3 Pos;
    List<KeyValuePair<int, int>> boxPos;
    float bigBombTimer = 0,doubleBombTimer=0;
    int bombCount = 0,bombcapicity = 1, boxNum = 20;
    bool bigBombIsActive = false;
    // Use this for initialization
    private void Awake()
    {
        ins = this;
        bigBombImage.gameObject.SetActive(false);
        doubleBombImage.gameObject.SetActive(false);
    }
    void Start()
    {
        //genarate indexs for the boxs
        boxPos = new List<KeyValuePair<int, int>>();
        enemyPos = new List<KeyValuePair<int, int>>();
        while (boxNum > 0)
        {
            int a, b;
            a = Random.Range(2, size - 1);
            b = Random.Range(2, size - 1);
            if ((a % 2 != 0 ^ b % 2 != 0) && boxPos.IndexOf(new KeyValuePair<int, int>(a, b)) < 0)
            {
                boxPos.Add(new KeyValuePair<int, int>(a, b));
                boxNum--;
            }
        }
        //instantiate a 2d array of gameobjects to hold the gameobjects like {borders,players,enemys,...}
        thegame = new GameObject[size][];
        for (int i = 0; i < size; i++)
        {
            thegame[i] = new GameObject[size];
        }

        //the point to start build the scene from
        float startX = -8.5f, startY = -8.1f, startZ = -4.1f;
        Pos = new Vector3(startX, startY, startZ);

        //instantiate the groundfloar
        GameObject temp=null;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                temp=Instantiate(floar, Pos, Quaternion.identity);
                temp.transform.parent=floarParent.transform;
                Pos = new Vector3(Pos.x, Pos.y, Pos.z - 1f);
            }
            Pos = new Vector3(Pos.x + 1f, Pos.y, -4.1f);
        }
        //free the object temp to use it in other places
        temp=null;
        //instantiate the borders and boxs
        Pos = new Vector3(startX, startY + 1, startZ);
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (i == 0 || j == 0 || i == size - 1 || j == size - 1 || (i % 2 == 0 && j % 2 == 0))
                {
                    thegame[i][j] = Instantiate(border, Pos, Quaternion.identity);
                    thegame[i][j].transform.parent=borderParent.transform;
                }
                else
                {
                    if (boxPos.Contains(new KeyValuePair<int, int>(i, j)))
                    {
                        thegame[j][i] = Instantiate(box, Pos, Quaternion.identity);
                          thegame[j][i].transform.parent=boxParent.transform;
                    }
                }
                Pos = new Vector3(Pos.x, Pos.y, Pos.z - 1f);
            }
            Pos = new Vector3(Pos.x + 1f, Pos.y, -4.1f);
        }
        //instantiate pickups inside boxs as there child
        while(pickups.Count>0)
        {
        int boxIndex=Random.Range(0,boxParent.transform.childCount);
        temp=boxParent.transform.GetChild(boxIndex).gameObject;
        if(temp.transform.childCount==0)
            {
            int pickupIndex=Random.Range(0,pickups.Count);
            GameObject pickup=pickups[pickupIndex];
            Pos=boxParent.transform.GetChild(boxIndex).position;
            pickup= Instantiate(pickups[pickupIndex],Pos,Quaternion.identity);
            pickup.transform.parent=temp.transform;
            pickup.gameObject.SetActive(false);
            if(pickup.gameObject.tag=="key")
                {
                pickup.transform.rotation = new Quaternion(0f, 200f, 200f, 20f);
                pickup.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                pickup.transform.position=new Vector3(Pos.x-0.5f,-7.5f,Pos.z-0.5f);
                }
            pickups.RemoveAt(pickupIndex);
            }
        }
        //instantiate the player and the enemy 
        thegame[1][1] = Instantiate(player, new Vector3(startX + 1, startY + 1, startZ - 1), Quaternion.identity);
        playerPos = new KeyValuePair<int, int>(1, 1);
        enemy1 = Instantiate(enemy1, new Vector3(startX + 1, startY + 1, startZ - 15), new Quaternion(0f, 100f, 100f, 0f));
        enemyPos.Add(new KeyValuePair<int, int>(15, 1));
        enemy2 = Instantiate(enemy2, new Vector3(startX + 15, startY + 1, startZ - 15), new Quaternion(0f, 100f, 100f, 0f));
        enemyPos.Add( new KeyValuePair<int, int>(15, 15));
        enemy3 = Instantiate(enemy3, new Vector3(startX + 15, startY + 1, startZ - 1), new Quaternion(0f, 100f, 100f, 0f));
        enemyPos.Add(new KeyValuePair<int, int>(1, 15));



    }
    //function that look for directiones that the object can move to it
    public Dictionary<string,bool> allowMoves(string name)
    {
        int k=0, v=0;
        //store the player index in thegame array
        if (name == "player")
        {
            k = playerPos.Key;
            v = playerPos.Value;
        }
        else if(name=="enemy1"){
            k = enemyPos[0].Key;
            v = enemyPos[0].Value;
        }
        else if(name=="enemy2"){
            k= enemyPos[1].Key;
            v= enemyPos[1].Value;
        }
        else if(name=="enemy3"){
            k = enemyPos[2].Key;
            v = enemyPos[2].Value;
        }

        //List of bool false=can't move there true=can moce there can't move if there a border or box in the index
        Dictionary<string,bool> allowed = new Dictionary<string, bool>();
        if (thegame[k - 1][v] != null)
            if (thegame[k - 1][v].tag == "border" || thegame[k - 1][v].tag == "box")
                allowed.Add("up",false);
            else
            {
                allowed.Add("up",true);

            }
        else
        {
            allowed.Add("up",true);

        }
        if (thegame[k][v - 1] != null)
            if (thegame[k][v - 1].tag == "border" || thegame[k][v - 1].tag == "box")
                allowed.Add("left",false);
            else
                allowed.Add("left",true);
        else
            allowed.Add("left",true);
        if (thegame[k + 1][v] != null)
            if (thegame[k + 1][v].tag == "border" || thegame[k + 1][v].tag == "box")
                allowed.Add("down",false);
            else
                allowed.Add("down",true);
        else
            allowed.Add("down",true);
        if (thegame[k][v + 1] != null)
            if (thegame[k][v + 1].tag == "border" || thegame[k][v + 1].tag == "box")
                allowed.Add("right",false);
            else
                allowed.Add("right",true);
        else
            allowed.Add("right",true);


        return allowed;
    }
    // Update is called once per frame
    void Update()
    {
        if(bigBombTimer<=0&&bigBombIsActive){
            disactiveBigBomb();
        }
        else{
            bigBombTimer -= Time.deltaTime;
            bigBombImage.fillAmount=bigBombTimer / 60;
        }
        if(doubleBombTimer<=0&&bombCount>1){
            disactiveDoubleBomb();
        }
        else{
            doubleBombTimer -= Time.deltaTime;
            doubleBombImage.fillAmount = doubleBombTimer / 60;
        }
    }
    //called to chick if the player can put a bomb or not
    public bool canPutBomb()
    {
        return bombCount < bombcapicity;
    }
    //called whene player put a bomb
    public void addBomb()
    {
        bombCount++;
    }
    //called whene the bomb explode
    public void removeBomb()
    {
        bombCount--;
    }
    //called whene the player moved to set his new position
    public void setPlayerPos(int k, int v)
    {
        playerPos = new KeyValuePair<int, int>(k, v);
    }
    //called whene the player pickup the double bomb bonus
    public void activeDuobleBomb()
    {
        bombcapicity = 2;
        doubleBombImage.gameObject.SetActive(true);
        doubleBombTimer = 60;
    }
    //called whene double bomb timer is over
    public void disactiveDoubleBomb(){
        bombcapicity = 1;
        doubleBombImage.gameObject.SetActive(false);
    }
    //called whene the player pickup the big bomb bouns
    public void activeBigBomb(){
        bigBombIsActive = true;
        bigBombImage.gameObject.SetActive(true);
        bigBombTimer = 60;

    }
    //called whene big Bomb Timer is over
    public void disactiveBigBomb(){
        bigBombIsActive = false;
        bigBombImage.gameObject.SetActive(false);
       // bigBombImage.transform.GetChild(0).gameObject.SetActive(false);
    }
    //called to chick if the player own the big bomb bouns
    public bool getBigBombActive()
    {
        return bigBombIsActive;

    }
    //return the player indexs on thegame array
    public KeyValuePair<int, int> getPlayerPos()
    {
        return playerPos;

    }
    //return the enemy indexs on thegame array
    public KeyValuePair<int,int> getenemyPos(int i){
        return enemyPos[i];
    }
    //called whene the enemy moved to set his new position
    public void setEnemyPos(int i,int k,int v){
        enemyPos[i] = new KeyValuePair<int, int>(k, v);
    }
    //called to step the game , status it win or lost
    public void gameOver(string status)
    {
        #if UnityEditor 
        if(status=="win"){
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else{
            UnityEditor.EditorApplication.isPlaying = false;
        }
        #endif
    }
}
