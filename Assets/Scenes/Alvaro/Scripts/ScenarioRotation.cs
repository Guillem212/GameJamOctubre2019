using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameJamOctubre.Inputs;


public class ScenarioRotation : MonoBehaviour
{
    public GameObject[] objectsToRotate;
    PlayerInput inputs;
    Animator anim;
    bool isMoving = false;
    public GameObject parentable;

    [Header("Control Variable")]
    public bool canInteract = true;
    public float canvasTurnSpeed = 20f;
    public float cooldown = 3f;

    enum Turn { Player1Right, Player2Right, Player1Left, Player2Left, None};
    Turn requestedRotation = Turn.None;

    float rightCounter, leftCounter = 0f;
    float angle = 0f;

    //DEBUG
    /* 
    GameObject rc;
    GameObject lc;
    GameObject actual;
    */
    //DEBUG

    // Start is called before the first frame update
    void Start()
    {
        inputs = new PlayerInput();
        anim = GetComponent<Animator>();

        //DEBUG
        /* 
        rc = GameObject.Find("RightC");
        lc = GameObject.Find("LeftC");
        actual = GameObject.Find("Actual");*/
    }

    // Update is called once per frame
    void Update()
    {
        //DEBUG
        /* 
        rc.GetComponent<Text>().text = "RC " + rightCounter;
        lc.GetComponent<Text>().text = "LC " + leftCounter;
        actual.GetComponent<Text>().text = "ACTUAl " + requestedRotation;*/
        //DEBUG
                

        if (leftCounter > 0f)
        {
            leftCounter -= Time.deltaTime;
        }        
        if (rightCounter > 0f)
        {
            rightCounter -= Time.deltaTime;
        }
        if (leftCounter <= 0 && rightCounter <= 0)
        {
            requestedRotation = Turn.None;
        }

        if (canInteract) 
        {
            CheckPlayerInteraction();            
        }
    }

    void CheckPlayerInteraction()
    {
        if (inputs.RBumper("1") || inputs.RTriggerAxis("1") > 0) //player 1 Right
        {
            //print("1R");            
            //update counter
            if (requestedRotation == Turn.Player1Right) rightCounter = cooldown;
            //same player change rotation request
            else if (requestedRotation == Turn.Player1Left || requestedRotation == Turn.None)
            {
                requestedRotation = Turn.Player1Right;
                rightCounter = cooldown;
                leftCounter = 0f;
            } 
            //conflict
            else if (requestedRotation == Turn.Player2Left)
            {                
                requestedRotation = Turn.None;
                rightCounter = 0f;
                leftCounter = 0f;
            }
            //turn request
            else if (requestedRotation == Turn.Player2Right)
            {
                //girate coño    
                StartCoroutine(RequestTurn(1));
                rightCounter = 0f;
                leftCounter = 0f;                
            }            
        }
        else if (inputs.LBumper("1") || inputs.LTriggerAxis("1") > 0) //player 1 Left
        {
            //print("1L");
            //update counter
            if (requestedRotation == Turn.Player1Left) leftCounter = cooldown;
            //same player change rotation request
            else if (requestedRotation == Turn.Player1Right || requestedRotation == Turn.None)
            {
                requestedRotation = Turn.Player1Left;
                leftCounter = cooldown;
                rightCounter = 0f;
            } 
            else if (requestedRotation == Turn.Player2Right)
            {                
                requestedRotation = Turn.None;
                leftCounter = 0f;
                rightCounter = 0f;
            }
            else if (requestedRotation == Turn.Player2Left)
            {
                //girate coño                
                StartCoroutine(RequestTurn(2));
                leftCounter = 0f;
                rightCounter = 0f;                
            }
        }
        //PLAYER 2
        else if (inputs.RBumper("2") || inputs.RTriggerAxis("2") > 0) //player 2 Right
        {
            //print("2R");            
            //update counter
            if (requestedRotation == Turn.Player2Right) rightCounter = cooldown;
            //same player change rotation request
            else if (requestedRotation == Turn.Player2Left || requestedRotation == Turn.None)
            {
                requestedRotation = Turn.Player2Right;
                rightCounter = cooldown;
                leftCounter = 0f;
            }
            //conflict
            else if (requestedRotation == Turn.Player1Left)
            {                
                requestedRotation = Turn.None;
                rightCounter = 0f;
                leftCounter = 0f;
            }
            //turn request
            else if (requestedRotation == Turn.Player1Right)
            {
                //girate coño    
                StartCoroutine(RequestTurn(1));
                rightCounter = 0f;
                leftCounter = 0f;                
            }
        }
        else if (inputs.LBumper("2") || inputs.LTriggerAxis("2") > 0) //player 2 Left
        {
            //print("2L");
            //update counter
            if (requestedRotation == Turn.Player2Left) leftCounter = cooldown;
            //same player change rotation request
            else if (requestedRotation == Turn.Player2Right || requestedRotation == Turn.None)
            {
                requestedRotation = Turn.Player2Left;
                leftCounter = cooldown;
                rightCounter = 0f;
            }
            else if (requestedRotation == Turn.Player1Right)
            {
                requestedRotation = Turn.None;
                leftCounter = 0f;
                rightCounter = 0f;
            }
            else if (requestedRotation == Turn.Player1Left)
            {
                //girate coño     
                StartCoroutine(RequestTurn(2));
                leftCounter = 0f;
                rightCounter = 0f;                
            }
        }        

    }
    
    IEnumerator RequestTurn(int turn) //1 or 2
    {        
        canInteract = false;
        isMoving = true;
        
        if (turn == 1) { anim.SetTrigger("RightRotation");
            foreach (GameObject i in objectsToRotate)
            {                
                i.transform.rotation = Quaternion.AngleAxis(90, i.transform.up);
                print(i.transform.rotation.eulerAngles.y);
            }
        }
        else { anim.SetTrigger("LeftRotation");
            foreach (GameObject i in objectsToRotate)
            {
                i.transform.rotation = Quaternion.AngleAxis(-90, i.transform.up);
                print(i.transform.rotation.eulerAngles.y);
            }
        }              

        anim.SetBool("IsMoving", true);
        yield return new WaitUntil(() => isMoving == false); //se llama a false cuando termina el animador
        //turn all canvas after rotation
        
        yield return new WaitForEndOfFrame();                

        anim.SetBool("IsMoving", false);
        canInteract = true;        
    }

    public void PlugElements()
    {
        //set parent
        parentable.transform.SetParent(this.transform);
    }

    public void UnPlugElements()
    {
        //quitar parent
        parentable.transform.SetParent(null);
        isMoving = false; //finaliza corrutina
    }
}
