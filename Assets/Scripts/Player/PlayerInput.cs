using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamOctubre.Inputs
{
    public class PlayerInput
    {
        /// <summary>
        /// Returns Vector 2 with Left Joystick axis
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Vector2 GetMovementAxis(string index)
        {
            Vector2 movementAxis = Vector2.zero;
            if (Input.GetAxis("Horizontal" + index) < -0.5f || Input.GetAxis("Horizontal" + index) > 0.5f)
            {
                movementAxis.x = (Input.GetAxis("Horizontal" + index));
            }
            if (Input.GetAxis("Vertical" + index) < -0.5f || Input.GetAxis("Vertical" + index) > 0.5f)
            {
                movementAxis.y = Input.GetAxis("Vertical" + index);
            }
            //Vector2 movementAxis = new Vector2(Input.GetAxis("Horizontal" + index), Input.GetAxis("Vertical" + index));
            return movementAxis;
        }

        /// <summary>
        /// Return Right Trigger value 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public float RTriggerAxis(string index)
        {
            //Debug.Log("RTrigger");

            return Input.GetAxis("RTrigger" + index);
        }

        /// <summary>
        /// Return Left Trigger value 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public float LTriggerAxis(string index)
        {
            //Debug.Log("LTrigger");

            return Input.GetAxis("LTrigger" + index);
        }

        /// <summary>
        /// Return Right Bumper Down
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool RBumper(string index)
        {
            //Debug.Log(Input.GetButtonDown("RBumper" + index));
            return (Input.GetButtonDown("RBumper" + index));
        }

        /// <summary>
        /// Return Left Bumper Down
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool LBumper(string index)
        {
            //Debug.Log(Input.GetButtonDown("LBumper" + index));
            return (Input.GetButtonDown("LBumper" + index));
        }

        /// <summary>
        /// Return Button X Down, interact with enviroment and items
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool Action(string index)
        {
            //Debug.Log(Input.GetButtonDown("Action" + index));
            return (Input.GetButtonDown("Action" + index));
        }

        /// <summary>
        /// Return Button A Down, catch and drop objects
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool Item(string index)
        {
            //Debug.Log(Input.GetButtonDown("Item" + index));
            return (Input.GetButtonDown("Item" + index));
        }

        /// <summary>
        /// Pause menu
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool Pause(string index)
        {
            //Debug.Log(Input.GetButtonDown("Item" + index));
            return (Input.GetButtonDown("Pause" + index));
        }

        /// <summary>
        /// Just cuaks, but in raccoon dialect
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool Cuak(string index)
        {
            //Debug.Log(Input.GetButtonDown("Item" + index));
            return (Input.GetButtonDown("Cuak" + index));
        }
    }
}

