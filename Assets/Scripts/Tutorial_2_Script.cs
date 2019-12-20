using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_2_Script : MonoBehaviour
{
    public Text text;

    private float time;
    private bool aux;

    private void Start() {
        time = 0;
        aux = false;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            text.text = "That's interestng, the trash is not here... Try to flip the camera!";
            aux = true;
            time = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(aux){
            if(time > 10){
                text.text = "To flip the camera both players must press RT/RB to rotate right";
            }
            if(time > 15){
                text.text = "Or LT/LB to rotate left the camera. Your choice camarades!";
            }
        }
        else{
            if(time > 5){
            text.text = "Now the bonfire... This will turn off over time, so you have to feed it with wood!";
            }
            if(time > 15){
            text.text = "To feed it you just have to approach it with wood and press the A.";
            }

        }        
    }
}
