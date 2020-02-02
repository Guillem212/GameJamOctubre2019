using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameJamOctubre.Inputs;

public class DemocracyMenu : MonoBehaviour
{
    [Header("Images")]
    public Image[] images;

    [Header("Timing")]
    public float ticTime;
    private float timer;
    private int index1;
    private int index2; 

    [Header("Sprites")]
    public Sprite cursor1;
    public Sprite cursor2;
    public Sprite cursorBoth;
    public Sprite cursorBoth1;
    public Sprite cursorBoth2;
    public Sprite[] selectedButtons;

    private PlayerInput inputs;
    public GameObject panel;
    private bool paused;
    private LevelManager levelManager;
    private int optionConfirmed; // 0: No confirmada, 1: Jugador 1 me lo confirmó, 2: Jugador 2 me lo confirmó

    private void Awake()
    {
        panel.SetActive(false);
        inputs = new PlayerInput();
        levelManager = GameObject.Find("GameManager").GetComponent<LevelManager>();
        for(int i = 0; i < images.Length; i++)
        {
            UpdateCanvas(i);
        }
    }

    private void Update()
    {


        if (optionConfirmed == 0)
        {
            if (inputs.Pause("1") || inputs.Pause("2"))
            {
                if (paused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }

            if (timer <= 0)
            {
                MoveCursor(1);
                MoveCursor(2);
            }
            else
            {
                timer -= Time.fixedDeltaTime;
            }


            if (index1 == index2) //Confirmar
            {
                if (inputs.Item("1"))
                {
                    optionConfirmed = 1;
                    Select(index1);
                }
                else if (inputs.Item("2"))
                {
                    optionConfirmed = 2;
                    Select(index1);
                }

            }
        }
    }

    private void Pause()
    {
        paused = true;
        panel.SetActive(true);
        Time.timeScale = 0f;
    }

    private void Resume()
    {
        paused = false;
        optionConfirmed = 0;
        panel.SetActive(false);
        Time.timeScale = 1f;
    }

    private void MoveCursor(int playerID)
    {
        float axis = Input.GetAxisRaw("Vertical" + playerID);
        if (Mathf.Abs(axis) > 0.3)
        {
            print(axis);
            int lastIndex = playerID == 1? index1 : index2; //anterior 
            if (axis > 0 && lastIndex < images.Length - 1) //Abajo
            {
                if (playerID == 1) index1++; else index2++;
            }
            else if (axis < 0 && lastIndex > 0) //Arriba
            {
                if (playerID == 1) index1--; else index2--;
            }
            UpdateCanvas(lastIndex);                                            //Update de la anterior opción del menú
            if (playerID == 1) UpdateCanvas(index1); else UpdateCanvas(index2); //Update de la actual opción del menú
            timer = ticTime;
        }

    }

    private void Select(int index)
    {
        

        if (index == 0) //RESUME
        {
            Resume();
        }
        else if(index == 1) //RETRY
        {
            Resume();
            levelManager.resetScene();
        }
        else if (index == 2) //OPTIONS
        {
            //ABRIR MENÚ DE OPCIONES
        }
        else if (index == 3)
        {
            //MENÚ PRINCIPAL
            Resume();
            levelManager.loadScene(0);
        }
        UpdateCanvas(index);

    }

    private void UpdateCanvas(int index)
    {
        Image cursorImage = images[index].GetComponentsInChildren<Image>()[1];

        if(optionConfirmed == 0)
        {
            if (index1 == index || index2 == index) //Tiene algún cursor
            {
                cursorImage.color = Color.white;
                if (index1 == index)
                {
                    if (index2 == index)
                    {
                        cursorImage.sprite = cursorBoth;
                        //ACTIVAR BOOLEANO ANIMACIÓN
                    }
                    else
                    {
                        cursorImage.sprite = cursor1;
                    }
                }
                else
                {
                    cursorImage.sprite = cursor2;
                }
            }
            else
            {
                cursorImage.color = Color.clear;
            }
        }
        else
        {
            if(optionConfirmed == 1)
            {
                cursorImage.sprite = cursorBoth1;
            }
            else
            {
                cursorImage.sprite = cursorBoth2;
            }
        }



        //igual ponerle una animación de pingo al pingo del aceptar y no usar botones distintos para los seleccionados
        // además, quien pulse pa aceptar se pinta el go de ese color (el pingo de ese color y las letras blancas)
    }
}
