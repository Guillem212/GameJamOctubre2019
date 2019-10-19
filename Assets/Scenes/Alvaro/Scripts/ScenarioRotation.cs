using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJamOctubre.Inputs;

public class ScenarioRotation : MonoBehaviour
{
    PlayerInput inputs;
    [Header("Control Variable")]
    [SerializeField] bool canInteract = true;
    [SerializeField] bool rotationIsRequested;
    bool p1R, p2R, p1L, p2L = false;
    enum Turn { Player1Right, Player2Right, Player1Left, Player2Left };
    Turn requestedRotation;

    float rightCounter, leftCounter;
    float cooldown = 3f;

    // Start is called before the first frame update
    void Start()
    {
        inputs = new PlayerInput();
    }

    // Update is called once per frame
    void Update()
    {
        if (canInteract) 
        {
            if (inputs.RBumper("1") || inputs.RTriggerAxis("1") > 0)
            {
                //player 1 request L turn
            }
            if (inputs.RBumper("2") || inputs.RTriggerAxis("2") > 0)
            {
                //player 2 request L turn
            }
            if (inputs.LBumper("1") || inputs.LTriggerAxis("1") > 0)
            {
                //player 1 request R turn
            }
            if (inputs.LBumper("2") || inputs.LTriggerAxis("2") > 0)
            {
                //player 1 request R turn
            }
        }
    }

    void CheckPlayerInteraction(int player)
    {
        if (player == 1)
        {
            if (inputs.RBumper("1") || inputs.RTriggerAxis("1") > 0) //player 1 Right
            {
                rotationIsRequested = true;                
                rightCounter = (requestedRotation == Turn.Player1Right) ? cooldown : (requestedRotation == Turn.Player1Left) ? cooldown : 0f;

            }
            else if (inputs.LBumper("1") || inputs.LTriggerAxis("1") > 0)
            {

            }
        }
        else
        {
            if (inputs.RBumper("2") || inputs.RTriggerAxis("2") > 0)
            {
                rotationIsRequested = true;
            }
            else if (inputs.LBumper("2") || inputs.LTriggerAxis("2") > 0)
            {

            }
        }
        

    }

    IEnumerator RequestTurn(int turn)
    {
        //1 -> clockwise
        //-1 -> unclockwise
        rotationIsRequested = true;
        yield return null;
    }

    
}
