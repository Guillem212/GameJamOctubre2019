using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{

    private Canvas canvas;

    private void Start() {
        CameraRotation.inMenu = true;
        canvas = GetComponent<Canvas>();
    }
    public void onExit(){
        Application.Quit();
    }

    public void onStart(){
        CameraRotation.inMenu = false;
        canvas.enabled = false;
    }
}
