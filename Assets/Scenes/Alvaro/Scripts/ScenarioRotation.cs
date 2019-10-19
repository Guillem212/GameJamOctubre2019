using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJamOctubre.Inputs;

public class ScenarioRotation : MonoBehaviour
{
    PlayerInput inputs;
    [Header("Control Variable")]
    [SerializeField] bool canInteract = true;
    [SerializeField] bool turnRequested;

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

    IEnumerator RequestTurn(int turn)
    {
        //1 -> clockwise
        //-1 -> unclockwise
        turnRequested = true;
        yield return null;
    }

    
}
