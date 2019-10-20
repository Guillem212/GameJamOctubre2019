using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJamOctubre.Inputs;

public class PlayerInteraction : MonoBehaviour
{
    [Header("References")]
    public GameObject holder;
    public GameObject canvas;
    public GameObject logPrefab;
    PlayerCanvasBehavior playerCanvas;
    [Header("State")]
    public bool canGrab = false;
    public bool canInteract = false;
    public bool grabbingAnObject = false;
    public bool interacting = false;
    public float interactTime;
    private float currentInteractTime = 0;
    public float canvasHeight = 2f;
    public float throwForce = 20f;

    string ID;
    PlayerInput inputs;
    GameObject objectToGrab;
    GameObject objectToInteract;
    GrabTrigger trigger;
    
    Transform grabHolder;
    Transform parentableTransform;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        inputs = new PlayerInput();
        ID = GetComponent<Player>().GetPlayerId();
        playerCanvas = canvas.GetComponent<PlayerCanvasBehavior>();
        grabHolder = holder.transform;
        trigger = GetComponentInChildren<GrabTrigger>();

        parentableTransform = GameObject.Find("ParentableObject").transform;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("chopping", interacting);
        anim.SetBool("grabbing", grabbingAnObject);

        playerCanvas.transform.position = this.transform.position + Vector3.up * canvasHeight;
        if(inputs.Item(ID))
        {
            Item();
        }
        else if (inputs.Action(ID))
        {
            Action();
        }
        if (interacting)
        {
            currentInteractTime += Time.deltaTime;
        }
        if(currentInteractTime >= interactTime)
        {
            EndInteracting();
        }

        if (inputs.Cuak(ID))
        {
            FindObjectOfType<AudioManager>().Play("Cuak");            
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

    public void CanInteract(bool state, GameObject obj)
    {
        playerCanvas.ShowXButtonOnCanvas(state); //shows B hint button
        canInteract = state;
        if (state)
        {
            objectToInteract = obj;
        }
    }

    private void Item()
    {
        Rigidbody objectRb = objectToGrab.GetComponent<Rigidbody>();
        if (canGrab && !grabbingAnObject)//grab
        {
            FindObjectOfType<AudioManager>().Play("Grab");            

            CanGrab(false, null); //avoid enter here again
            grabbingAnObject = true;
            objectToGrab.transform.position = grabHolder.position;
            objectToGrab.transform.rotation = grabHolder.rotation;
            //player already has the same object -> told him to drop it
            if (objectToGrab.transform.parent != parentableTransform)
            {
                string other = ID == "1" ? "2" : "1";
                GameObject.Find("Player" + other).GetComponent<PlayerInteraction>().Item();
            }
            objectRb.useGravity = false;
            objectRb.isKinematic = true;
            objectToGrab.transform.SetParent(grabHolder);            
            
        }
        else if (grabbingAnObject)//drop
        {
            FindObjectOfType<AudioManager>().Play("Drop");            

            grabbingAnObject = false;
            trigger.SetActiveTrigger(true);
            objectRb.useGravity = true;
            objectRb.isKinematic = false;
            objectToGrab.transform.SetParent(parentableTransform);
            if (anim.GetBool("moving"))
            {
                objectRb.AddForce(transform.forward * throwForce, ForceMode.Impulse);
            }
        }
    }

    private void Action()
    {
        if(canInteract && !interacting)
        {
            CanInteract(false, null);
            interacting = true;
        }
    }

    private void EndInteracting()
    {
        currentInteractTime = 0;
        interacting = false;
        if (objectToInteract != null && objectToInteract.CompareTag("Tree"))
        {
            //FindObjectOfType<AudioManager>().Play("Chop");
            FindObjectOfType<AudioManager>().PlaySoundWithRandomPitch(2); //CHOP

            grabbingAnObject = true;
            GameObject log = Instantiate(logPrefab, grabHolder.position, grabHolder.rotation);
            Rigidbody objectRb = log.GetComponent<Rigidbody>();
            objectRb.useGravity = false;
            objectRb.isKinematic = true;
            log.transform.SetParent(grabHolder);
            Tree tree = objectToInteract.gameObject.GetComponent<Tree>();
            tree.Fall();
            objectToGrab = log;
            objectToInteract = null;
        }

        if (objectToInteract != null && objectToInteract.CompareTag("Buildable"))
        {
            FindObjectOfType<AudioManager>().Play("Build");

            Buildable buildable = objectToInteract.gameObject.GetComponent<Buildable>();
            buildable.Build();
            Destroy(objectToGrab);
            objectToGrab = null;
            objectToInteract = null;
            grabbingAnObject = false;
            playerCanvas.ShowXButtonOnCanvas(false);

        }
    }
}
