using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvasBehavior : MonoBehaviour
{
    [SerializeField] GameObject AButton;

    private void Start()
    {
        AButton.SetActive(false);
    }
    private void Update()
    {
        this.transform.LookAt(Camera.main.transform.position, Vector3.up);
    }

    public void ShowAButtonOnCanvas(bool state)
    {
        AButton.SetActive(state);
    }
}
