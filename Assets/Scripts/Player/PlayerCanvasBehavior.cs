using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvasBehavior : MonoBehaviour
{
    public GameObject AButton;
    public GameObject XButton;
    Vector3 targetPos;    
    public float damping = 10f;
    //Vector3 XButtonPosition;

    private void Start()
    {
        AButton.SetActive(false);
        XButton.SetActive(false);
        //XButtonPosition = XButton.GetComponent<RectTransform>().localPosition;
    }
    
    private void Update()
    {        
        /*targetPos = Camera.main.transform.position;
        Vector3 targetPostition = new Vector3(targetPos.x, transform.position.y, transform.position.z);
        this.transform.LookAt(targetPostition);*/
    }

    public void ShowAButtonOnCanvas(bool state)
    {
        AButton.SetActive(state);
    }

    public void ShowXButtonOnCanvas(bool state, bool canGrab)
    {        
        XButton.SetActive(state);
        //XButtonPosition = XButton.GetComponent<RectTransform>().localPosition;
        if (canGrab)
        {
            XButton.GetComponent<RectTransform>().localPosition = new Vector3(0, 180, 0); //si lo pongo como variable justo antes no funciona :D
        }
        else
        {
            XButton.GetComponent<RectTransform>().localPosition = Vector3.zero;
        }
    }
}
