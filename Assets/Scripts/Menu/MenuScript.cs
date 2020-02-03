using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuScript : MonoBehaviour
{

    private Animator animator;

    [Header("Menu's Canvas")]
    public GameObject firstCanvas;
    public GameObject secondCanvas;

    [Header("Button's Image")]
    public Sprite player_1;
    public Sprite player_2;
    public Sprite bothPlayers;

    [Header("Positions of the Buttons")]
    public Transform[] placeHolders;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    public void onExit(){
        Application.Quit();
    }

    public void onStart(){
        //Activar el trigger que hace que se inicie la animación de salida.
    }

    public void pressAtoStart(){

    }
}
