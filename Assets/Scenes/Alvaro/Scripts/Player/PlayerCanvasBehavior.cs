using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvasBehavior : MonoBehaviour
{
    public GameObject AButton;
    Vector3 targetPos;    
    public float damping = 10f;

    private void Start()
    {
        AButton.SetActive(false);        
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
}
