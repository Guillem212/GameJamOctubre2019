using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamOctubre.Inputs
{
    public class PlayerController : MonoBehaviour
    {
        [HideInInspector]
        public CharacterController m_PlayerController;
        public PlayerInteraction m_PlayerInteraction;
        [SerializeField] Vector3 m_MoveDirection;        
        public GameObject m_PlayerModel; 
        PlayerInput inputs;
        string ID;
        public float radius = 10f;

        [Header("Inputs")]
        public string m_VerticalAxis = "Vertical";
        public string m_HorizontalAxis = "Horizontal";

        [Header("Control Variable")]
        [SerializeField] float m_MoveSpeed = 1f;
        [SerializeField] float m_GrabMoveSpeed = 0.7f;
        [SerializeField] float m_RotateSpeed = 10f;
        [SerializeField] float m_GravityFactor = 2f;

        Animator anim;


        void Start()
        {
            anim = GetComponentInChildren<Animator>();
            ID = this.GetComponent<Player>().GetPlayerId();
            m_PlayerController = GetComponent<CharacterController>();
            m_PlayerInteraction = GetComponent<PlayerInteraction>();
            inputs = new PlayerInput();
        }


        void Update() 
        {            
            //print(m_PlayerController.isGrounded);
            m_MoveDirection = new Vector3();
            if (inputs.GetMovementAxis(ID) != Vector2.zero) //if there is some move
            {
                if(!m_PlayerInteraction.interacting) RotatePlayerModel();
                m_MoveDirection = this.transform.forward;
                anim.SetBool("moving", true);
            }
            else
            {
                anim.SetBool("moving", false);
            }
            if (m_PlayerController.isGrounded) 
            {
                m_MoveDirection.y = 0f; 
            }
            else
            {
                m_MoveDirection.y = -m_GravityFactor;
            }
            if (m_PlayerInteraction.grabbingAnObject && !m_PlayerInteraction.interacting) m_PlayerController.Move(m_MoveDirection * Time.deltaTime * m_GrabMoveSpeed);
            else if (!m_PlayerInteraction.interacting) m_PlayerController.Move(m_MoveDirection * Time.deltaTime * m_MoveSpeed);
        }

        private void RotatePlayerModel()
        {
            Vector3 direction = new Vector3(inputs.GetMovementAxis(ID).x, 0f, 0f) + new Vector3(0f, 0f, -inputs.GetMovementAxis(ID).y);
            //print(direction);            
            Vector3 desiredPosition = new Vector3(m_MoveDirection.x, 0f, m_MoveDirection.z); 
            Quaternion newRotation = Quaternion.LookRotation(direction); 
            m_PlayerModel.transform.rotation = Quaternion.Slerp(m_PlayerModel.transform.rotation, newRotation, m_RotateSpeed * Time.deltaTime); 
        }
    }
}