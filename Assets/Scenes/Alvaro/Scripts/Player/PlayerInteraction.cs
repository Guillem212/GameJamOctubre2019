using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJamOctubre.Inputs;

public class PlayerInteraction : MonoBehaviour
{
    [Header("References")]
    public GameObject holder;
    public GameObject canvas;
    PlayerCanvasBehavior playerCanvas;
    [Header("State")]
    public bool canGrab = false;
    public bool grabbingAnObject = false;
    public float canvasHeight = 2f;

    string ID;
    PlayerInput inputs;
    GameObject objectToGrab;
    GrabTrigger trigger;
    
    Transform grabHolder;

    // Start is called before the first frame update
    void Start()
    {
        inputs = new PlayerInput();
        ID = GetComponent<Player>().GetPlayerId();
        playerCanvas = canvas.GetComponent<PlayerCanvasBehavior>();
        grabHolder = holder.transform;
        trigger = GetComponentInChildren<GrabTrigger>();
    }

    // Update is called once per frame
    void Update()
    {
        playerCanvas.transform.position = this.transform.position + Vector3.up * canvasHeight;
        if(inputs.Item(ID))
        {            
            if (canGrab && !grabbingAnObject && canGrab)
            {
                CanGrab(false, null); //avoid enter here again
                grabbingAnObject = true;
                trigger.SetActiveTrigger(false); //deactivates detection trigger
                objectToGrab.transform.position = grabHolder.position;
                objectToGrab.transform.SetParent(grabHolder);
            }
            else if (grabbingAnObject)
            {
                grabbingAnObject = false;
                trigger.SetActiveTrigger(true);
                objectToGrab.transform.SetParent(null);
            }
        }
    }

    public void CanGrab(bool state, GameObject obj)
    {
        playerCanvas.ShowAButtonOnCanvas(state); //shows A hint button
        canGrab = state;
        if (state)
        {
            objectToGrab = obj;
        }
    }
}
