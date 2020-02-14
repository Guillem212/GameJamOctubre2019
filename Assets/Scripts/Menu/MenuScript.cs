using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class MenuScript : MonoBehaviour
{

    public enum MenuState{
        START_MENU,
        PLAY_MENU,
        PLAYER_SELECTOR,
        LEVEL_MENU
    }

    private MenuState menuState;

    private Animator animator;
    private GameManager gameManager;
    private PlayerInputManager inputManager;


    [Header("Camera Rotation")]
    public CameraRotation cameraRotation;

    [Header("Level selector")]
    public GameObject levelSelector;

    [Header("Menu's Canvas")]
    public GameObject firstCanvas;
    public GameObject secondCanvas;
    public GameObject playerSelector;

    public GameObject[] players;

    [Header("Button's Image")]
    public Sprite bothPlayers;

    [Header("Positions of the Buttons")]
    public GameObject[] placeHolders;
    private int actualHolder; //de 0 a las opciones que haya.

    private void Start() {
        animator = GetComponent<Animator>();
        menuState = MenuState.START_MENU;

        actualHolder = 0;

        Camera.main.GetComponent<CameraMovement>().minSize = 20f;

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        inputManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerInputManager>();
        inputManager.DisableJoining();
    }

    private void LateUpdate() {
        cameraRotation.rotateMenu();


    }

    public void onExit(){
        Application.Quit();
    }

    public void onStart(){
        //Activar el trigger que hace que se inicie la animación de salida.
        animator.SetTrigger("ChangeToLevelSelection");
    }
    public void onOption(){
        //Ir al menú de opciones.
    }

    public void pressAtoStart(){
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

    public void activatePlayerSelector()
    {
        playerSelector.SetActive(true);
    }

    //Se llama desde el animador.
    public void playMenuInitialize(){
        Image image = placeHolders[actualHolder].GetComponent<Image>();
        image.color = Color.white;
        image.sprite = bothPlayers;
    }

    public void OnNavigate(InputValue data)
    {
        Vector2 value = data.Get<Vector2>();

        if(value.y > 0)
        {
            if(actualHolder > 0)
            {
                Image image = placeHolders[actualHolder].GetComponent<Image>();
                image.color = Color.clear;
                image.sprite = null;

                actualHolder--;
                image = placeHolders[actualHolder].GetComponent<Image>();
                image.color = Color.white;
                image.sprite = bothPlayers;

            }
        }
        else if(value.y < 0)
        {
            if (actualHolder < placeHolders.Length-1)
            {
                Image image = placeHolders[actualHolder].GetComponent<Image>();
                image.color = Color.clear;
                image.sprite = null;

                actualHolder++;
                image = placeHolders[actualHolder].GetComponent<Image>();
                image.color = Color.white;
                image.sprite = bothPlayers;

            }
        }
    }

    public void OnSubmit()
    {
        if(menuState == MenuState.START_MENU)
        {
            pressAtoStart();
        }
        else if (menuState == MenuState.PLAY_MENU)
        {
            if (actualHolder == 0)
            {
                onStart();
            }
            else if (actualHolder == 1)
            {
                onOption();
            }
            else
            {
                onExit();
            }
        }
    }
}
