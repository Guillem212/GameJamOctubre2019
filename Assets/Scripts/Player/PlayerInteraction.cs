﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameJamOctubre.Inputs;

public class PlayerInteraction : MonoBehaviour
{
    [Header("References")]
    public GameObject holder;
    public GameObject canvas;
    public GameObject logPrefab;
    PlayerCanvasBehavior playerCanvas;

    Rigidbody logRigidBody;
    [Header("State")]
    public bool canGrab = false; //todo esto lo debería asignar el gamemanager al comienzo de cada nivel
    public bool canChop = false;
    public bool canBuild = false;
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
        anim.SetBool("grabbing", grabbingAnObject);

        playerCanvas.transform.position = this.transform.position + Vector3.up * canvasHeight;
        if(inputs.Item(ID)) //grab
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
        //print("canGrab" + state + obj);
        playerCanvas.ShowAButtonOnCanvas(state); //shows A hint button
        canGrab = state;
        if (state)
        {
            objectToGrab = obj;
        }
    }

    public void CanChop(bool state, GameObject obj)
    {
        //print("canChop" + state + obj);
        playerCanvas.ShowXButtonOnCanvas(state, canGrab); //shows X hint button
        canChop = state;
        if (state)
        {
            objectToInteract = obj;
        }
    }

    public void CanBuild(bool state, GameObject obj)
    {
        //print("canBuild" + state + obj);
        if (!canChop) playerCanvas.ShowXButtonOnCanvas(state, canGrab); //shows A hint button
        canBuild = state;
        if (state)
        {
            objectToInteract = obj;
        }
    }

    private void Item() //A BUTTON
    {            
        if (canGrab)//grab
        {
            logRigidBody = objectToGrab.GetComponent<Rigidbody>();
            FindObjectOfType<AudioManager>().Play("Grab");            
            CanGrab(false, null); //desactiva botón
            grabbingAnObject = true;
            objectToGrab.transform.position = grabHolder.position;
            objectToGrab.transform.rotation = grabHolder.rotation;
            //robar tronco
            if (objectToGrab.transform.parent != parentableTransform)
            {
                string other = ID == "1" ? "2" : "1";
                GameObject.Find("Player" + other).GetComponent<PlayerInteraction>().Item();
            }
            logRigidBody.useGravity = false;
            logRigidBody.isKinematic = true;
            objectToGrab.transform.SetParent(grabHolder);            
            
        }
        else if (grabbingAnObject) //drop
        {
            FindObjectOfType<AudioManager>().Play("Drop");
            logRigidBody = objectToGrab.GetComponent<Rigidbody>();

            grabbingAnObject = false;
            trigger.SetActiveTrigger(true);
            logRigidBody.useGravity = true;
            logRigidBody.isKinematic = false;
            objectToGrab.transform.SetParent(parentableTransform);
            if (anim.GetBool("moving"))
            {
                logRigidBody.AddForce(transform.forward * throwForce, ForceMode.Impulse);
            }
        }
    }

    private void Action() //X BUTTON
    {
        if(canChop)
        {
            //CanChop(false, null);             
            //bloquear arbol para el otro jugador
            objectToInteract.GetComponentInParent<Tree>().locked = true;
            interacting = true; //para el animador
            objectToInteract.GetComponent<Animator>().SetTrigger("Chop");
            anim.SetBool("chopping", true);
        }
        else if(canBuild)
        {
            interacting = true; //para el animador
            Destroy(objectToGrab);
            objectToGrab = null;
            grabbingAnObject = false;            
            //bloquear constructor            
            //si es el ultimo tronco, animar la rampa
            anim.SetBool("chopping", true);

        }
    }

    private void EndInteracting()
    {
        anim.SetBool("chopping", false);
        currentInteractTime = 0;
        interacting = false;
        if (objectToInteract != null && objectToInteract.CompareTag("Tree"))
        {            
            //sonido
            FindObjectOfType<AudioManager>().PlaySoundWithRandomPitch(2); //CHOP               
            //cortar arbol
            Tree tree = objectToInteract.gameObject.GetComponentInParent<Tree>();
            tree.Fall();
            //cogemos tronco
            grabbingAnObject = true;
            GameObject log = Instantiate(logPrefab, grabHolder.position, grabHolder.rotation);
            Rigidbody objectRb = log.GetComponent<Rigidbody>();
            objectRb.useGravity = false;
            objectRb.isKinematic = true;
            log.transform.SetParent(grabHolder);        
            objectToGrab = log;
            objectToInteract = null;
        }

        if (objectToInteract != null && objectToInteract.CompareTag("Buildable"))
        {
            FindObjectOfType<AudioManager>().Play("Build");

            Buildable buildable = objectToInteract.gameObject.GetComponent<Buildable>();
            buildable.Build();
            print(objectToInteract.name);
            objectToInteract = null;
            //playerCanvas.ShowXButtonOnCanvas(false, false);
        }
    }
}
