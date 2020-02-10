using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJamOctubre.Input_System;

public class ScenarioRotation : MonoBehaviour
{

    enum Turn {Right, Left, None};

    struct inputPlayerInfo
    {
        public Turn direction { get; }
        public int playerIndex { get; }

        public inputPlayerInfo(Turn direction, int playerIndex)
        {
            this.direction = direction;
            this.playerIndex = playerIndex;
        }
    }

    public GameObject[] objectsToRotate;

    List<Inputs_Sys> playerInputs;
    List<inputPlayerInfo> playersRotationActions;

    Animator anim;

    bool isMoving = false;

    [Header("Control Variable")]
    public bool canInteract = true;

    public float cooldown = 3f;

    float rightCounter, leftCounter = 0f;
    
    public GameObject leftArrow;
    public GameObject rightArrow;

    [HideInInspector] public bool onePlayerMode;

    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        playerInputs = new List<Inputs_Sys>();
        playersRotationActions = new List<inputPlayerInfo>();

        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {             
        foreach(GameObject obj in objectsToRotate){
            Vector3 v = cam.transform.position - obj.transform.position;
            v.x = v.z = 0.0f;
            obj.transform.LookAt( cam.transform.position - v ); 
            //obj.transform.Rotate(0,180,0);
            obj.transform.rotation =(cam.transform.rotation);
        }

        leftArrow.SetActive(leftCounter > 0);
        rightArrow.SetActive(rightCounter > 0);

        if (canInteract)
        {
            CheckPlayerInteraction();
        }

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
            playersRotationActions.Clear();      
        }
    }

    public void addPlayer(Inputs_Sys player)
    {
        playerInputs.Add(player);
    }

    private void requestRotation(Turn dir)
    {
        if(dir == Turn.Right)
        {
            rightCounter = cooldown;
        }
        else if(dir == Turn.Left)
        {
            leftCounter = cooldown;
        }
    }

    void CheckPlayerInteraction()
    {

        foreach (Inputs_Sys player in playerInputs)
        {
            if(player.rightJoystick != 0)
            {
                playersRotationActions.Add(new inputPlayerInfo(player.rightJoystick < 0 ? Turn.Left : Turn.Right, player.playerIndex));
                player.rightJoystick = 0;
            }
        }

        inputPlayerInfo lastInfo = new inputPlayerInfo(Turn.None, -1);
        foreach (inputPlayerInfo item in playersRotationActions)
        {
            if(lastInfo.playerIndex != -1)
            {
                if(lastInfo.playerIndex == item.playerIndex && lastInfo.direction == item.direction)
                {
                    playersRotationActions.Remove(item);
                    break;
                }
                else if(lastInfo.playerIndex == item.playerIndex && lastInfo.direction != item.direction)
                {
                    playersRotationActions.Remove(lastInfo);
                    break;
                }
            }
            lastInfo = item;
        }

        if(playersRotationActions.Count == 1)
        {
            foreach (inputPlayerInfo item in playersRotationActions)
            {
                leftCounter = 0;
                rightCounter = 0;
                requestRotation(item.direction);
            }
        }
        else if(playersRotationActions.Count > 1)
        {
            lastInfo = new inputPlayerInfo(Turn.None, -1);
            foreach (inputPlayerInfo item in playersRotationActions)
            {
                if(lastInfo.playerIndex != -1)
                {
                    if(lastInfo.direction == item.direction && lastInfo.playerIndex != item.playerIndex)
                    {
                        RequestTurn(item.direction);
                        playersRotationActions.Clear();
                        break;
                    }
                    else
                    {
                        leftCounter = 0;
                        rightCounter = 0;
                        requestRotation(item.direction);
                        playersRotationActions.Remove(lastInfo);
                        break;
                    }
                }
                lastInfo = item;
            }
        }
    }
    
    private void RequestTurn(Turn turn)
    {
        leftCounter = 0;
        rightCounter = 0;

        FindObjectOfType<AudioManager>().Play("CameraRotation");

        cam.gameObject.transform.SetParent(transform);

        canInteract = false;
        isMoving = true;
        
        if (turn == Turn.Right) {
            anim.SetTrigger("RightRotation");

        }
        else if (turn == Turn.Left){ 
            anim.SetTrigger("LeftRotation");
 
        }              
        anim.SetBool("IsMoving", true);  
    }
    public void endRotation()
    {
        cam.gameObject.transform.SetParent(null);
        anim.SetBool("IsMoving", false);
        canInteract = true;
    }
}

