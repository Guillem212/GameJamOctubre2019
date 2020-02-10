using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GameJamOctubre.Inputs;

public class MenuScript : MonoBehaviour
{

    public enum MenuState{
        START_MENU,
        PLAY_MENU,
        LEVEL_MENU
    }

    private MenuState menuState;

    private Animator animator;

    private PlayerInput inputs;
    private float timer;


    [Header("Camera Rotation")]
    public CameraRotation cameraRotation;

    [Header("Level selector")]
    public GameObject levelSelector;

    [Header("Menu's Canvas")]
    public GameObject firstCanvas;
    public GameObject secondCanvas;

    [Header("Button's Image")]
    public Sprite player_1;
    public Sprite player_2;
    public Sprite bothPlayers;

    [Header("Positions of the Buttons")]
    public GameObject[] placeHolders;
    private int actualHolderP1; //de 0 a las opciones que haya.
    private int actualHolderP2;


    private void Awake() {
        animator = GetComponent<Animator>();
        menuState = MenuState.START_MENU;

        inputs = new PlayerInput();
        timer = 0.12f;

        actualHolderP1 = 0;
        actualHolderP2 = actualHolderP1;

        Camera.main.GetComponent<CameraMovement>().minSize = 20f;
    }

    private void LateUpdate() {
        cameraRotation.rotateMenu();

        if (menuState == MenuState.START_MENU){
            if(inputs.Item("1") || inputs.Item("2")){
                pressAtoStart();
            }
        }
        else if(menuState == MenuState.PLAY_MENU){
            if (timer <= 0)
            {
                timer = 0.12f;
                optionSelector();
            }
            else
            {
                timer -= Time.deltaTime;
            }

            if ((inputs.Item("1") || inputs.Item("2")) && (actualHolderP1 == actualHolderP2 && actualHolderP1 == 0))
            {
                onStart();
            }
            else if ((inputs.Item("1") || inputs.Item("2")) && (actualHolderP1 == actualHolderP2 && actualHolderP1 == 2))
            {
                onExit();
            }
        }
    }

    private void onExit(){
        Application.Quit();
    }

    private void onStart(){
        //Activar el trigger que hace que se inicie la animación de salida.
        animator.SetTrigger("ChangeToLevelSelection");
    }
    private void onOption(){
        //Ir al menú de opciones.
    }

    private void pressAtoStart(){
        //Cambia al segundo menú activando el trigger.
        animator.SetTrigger("ChangeToSecondMenu");
    }

    public void changeMenuState(MenuState state){
        menuState = state;
    }

    public void activateLevelSelector()
    {
        levelSelector.SetActive(true);
    }

    //Se llama desde el animador.
    public void playMenuInitialize(){
        Image image = placeHolders[actualHolderP1].GetComponent<Image>();
        image.color = Color.white;
        image.sprite = bothPlayers;
    }

    private void optionSelector()
    {
        int movmentInputP1 = (int)inputs.GetMovementAxis("1").y;
        int movmentInputP2 = (int)inputs.GetMovementAxis("2").y;

        if (movmentInputP1 != 0)
        {
            if(movmentInputP1 > 0)
            {
                if(actualHolderP1 < placeHolders.Length - 1)
                {
                    Image image = placeHolders[actualHolderP1].GetComponent<Image>();
                    image.color = Color.clear;
                    image.sprite = null;
                    actualHolderP1++;
                }

                if(actualHolderP1 == actualHolderP2)
                {
                    Image image = placeHolders[actualHolderP1].GetComponent<Image>();
                    image.color = Color.white;
                    image.sprite = bothPlayers;
                }
                else
                {
                    Image image = placeHolders[actualHolderP1].GetComponent<Image>();
                    image.color = Color.white;
                    image.sprite = player_1;

                    image = placeHolders[actualHolderP2].GetComponent<Image>();
                    image.color = Color.white;
                    image.sprite = player_2;
                }
            }
            else if (movmentInputP1 < 0)
            {
                if (actualHolderP1 > 0)
                {
                    Image image = placeHolders[actualHolderP1].GetComponent<Image>();
                    image.color = Color.clear;
                    image.sprite = null;
                    actualHolderP1--;
                }

                if (actualHolderP1 == actualHolderP2)
                {
                    Image image = placeHolders[actualHolderP1].GetComponent<Image>();
                    image.color = Color.white;
                    image.sprite = bothPlayers;
                }
                else
                {
                    Image image = placeHolders[actualHolderP1].GetComponent<Image>();
                    image.color = Color.white;
                    image.sprite = player_1;

                    image = placeHolders[actualHolderP2].GetComponent<Image>();
                    image.color = Color.white;
                    image.sprite = player_2;
                }
            }
        }

        if(movmentInputP2 != 0)
        {
            if (movmentInputP2 > 0)
            {
                if (actualHolderP2 < placeHolders.Length - 1)
                {
                    Image image = placeHolders[actualHolderP2].GetComponent<Image>();
                    image.color = Color.clear;
                    image.sprite = null;
                    actualHolderP2++;
                }

                if (actualHolderP1 == actualHolderP2)
                {
                    Image image = placeHolders[actualHolderP2].GetComponent<Image>();
                    image.color = Color.white;
                    image.sprite = bothPlayers;
                }
                else
                {
                    Image image = placeHolders[actualHolderP2].GetComponent<Image>();
                    image.color = Color.white;
                    image.sprite = player_2;

                    image = placeHolders[actualHolderP1].GetComponent<Image>();
                    image.color = Color.white;
                    image.sprite = player_1;
                }
            }
            else if (movmentInputP2 < 0)
            {
                if (actualHolderP2 > 0)
                {
                    Image image = placeHolders[actualHolderP2].GetComponent<Image>();
                    image.color = Color.clear;
                    image.sprite = null;
                    actualHolderP2--;
                }

                if (actualHolderP1 == actualHolderP2)
                {
                    Image image = placeHolders[actualHolderP2].GetComponent<Image>();
                    image.color = Color.white;
                    image.sprite = bothPlayers;
                }
                else
                {
                    Image image = placeHolders[actualHolderP2].GetComponent<Image>();
                    image.color = Color.white;
                    image.sprite = player_2;

                    image = placeHolders[actualHolderP1].GetComponent<Image>();
                    image.color = Color.white;
                    image.sprite = player_1;
                }
            }
        }
    }
}
