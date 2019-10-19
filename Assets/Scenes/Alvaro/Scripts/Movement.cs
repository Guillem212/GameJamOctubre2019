using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamOctubre.Inputs
{
    public class Movement : MonoBehaviour
    {
        public string ID;
        private float speed = 2f;
        PlayerInput inputs;
        // Start is called before the first frame update
        void Start()
        {
            inputs = new PlayerInput();
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 moveDirection = new Vector3(inputs.GetMovementAxis(ID).x, 0, inputs.GetMovementAxis(ID).y * -1);
            transform.position += moveDirection * Time.deltaTime * speed;
        }
    }
}