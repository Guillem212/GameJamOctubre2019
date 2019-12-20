using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_1_Script : MonoBehaviour
{
    public Text text;

    private float time;

    private void Start() {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if(time > 5){
            text.text = "In this tutorial we will show you how to build and complete the level.";
        }
        if(time > 10){
            text.text = "If you aproach to a Tree and press the X button you will cut it down.";
        }
        if(time > 15){
            text.text = "With this log that you collect you can build, go to the construction point and press X.";
        }
        if(time > 25){
            text.text = "Do not forget to reaching the trash cans! These littles Raccoons love trash.";
        }
        if(time > 30){
            text.text = "";
        }
    }
}
