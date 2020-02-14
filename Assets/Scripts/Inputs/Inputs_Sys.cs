using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameJamOctubre.Input_System
{
    public class Inputs_Sys : MonoBehaviour
    {

        private PlayerInput input;

        [HideInInspector]
        public int playerIndex;

        //Gameplay Inputs
        [HideInInspector]
        public bool southButton, northButton, westButton, startButton;
        [HideInInspector]
        public Vector2 moveAxis;
        public float rightJoystick;

        //Menu Inputs
        [HideInInspector]
        public bool submitButton, denyButton;
        [HideInInspector]
        public Vector2 moveMenu;

        private ScenarioRotation sc;

        private CameraMovement camMovment;

        private void Awake()
        {
            input = GetComponent<PlayerInput>();
            camMovment =GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>();
            camMovment.addToTargets(this.transform);
            playerIndex = input.playerIndex;

            //Know the type of device that this player have assigned.
            for (int i = 0; i < input.devices.Count; i++)
            {
                print(input.devices[i].layout);
            }

            sc = GameObject.Find("RotationPivot").GetComponent<ScenarioRotation>();
            sc.addPlayer(this);
        }

        //------------------------------
        //Gameplay functions
        //------------------------------
        public void OnMove(InputValue data)
        {
            moveAxis = data.Get<Vector2>();
        }

        public void OnA_Interaction(InputValue data)
        {
            southButton = data.Get<float>() != 0;
        }

        public void OnX_Interaction(InputValue data)
        {
            westButton = data.Get<float>() != 0;
        }

        public void OnY_Interaction(InputValue data)
        {
             northButton = data.Get<float>() != 0;
        }

        public void OnStart(InputValue data)
        {
            startButton = data.Get<float>() != 0;
        }

        public void OnRotateMap(InputValue data)
        {
            rightJoystick = data.Get<Vector2>().x;
        }
    }
}