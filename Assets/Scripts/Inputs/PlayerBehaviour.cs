using UnityEngine;

namespace GameJamOctubre.Input_System
{
    public class PlayerBehaviour : MonoBehaviour
    {
        [HideInInspector] public CharacterController m_PlayerController;
        [HideInInspector] public PlayerInteraction m_PlayerInteraction;

        private Vector3 m_MoveDirection;

        public GameObject m_PlayerModel;

        private Inputs_Sys inputs;

        public float radius = 10f;

        [Header("Control Variable")]
        [SerializeField] private float m_MoveSpeed = 1f;
        [SerializeField] private float m_GrabMoveSpeed = 0.7f;
        [SerializeField] private float m_RotateSpeed = 25f;
        public float m_GravityFactor = 2f;

        private Animator anim;

        [HideInInspector] public ParticleSystem ps;

        private Vector3 desiredMoveDirection;

        private Camera cam;
        private Vector3 forward;
        private Vector3 right;

        [Header("Interaction Prefabs")]
        public GameObject holder;
        public GameObject canvas;
        public GameObject logPrefab;
        PlayerCanvasBehavior playerCanvas;

        Rigidbody logRigidBody;

        [HideInInspector] public bool canGrab = false;
        [HideInInspector] public bool canChop = false;
        [HideInInspector] public bool canBuild = false;
        [HideInInspector] public bool grabbingAnObject = false;

        public bool interacting = false;
        public float interactTime;
        private float currentInteractTime = 0;

        public float canvasHeight = 2f;
        public float throwForce = 20f;

        GameObject objectToGrab;
        GameObject objectToInteract;
        GrabTrigger trigger;

        Transform grabHolder;
        Transform parentableTransform;

        void Start()
        {
            //GETTERS
            anim = GetComponentInChildren<Animator>();
            m_PlayerController = GetComponent<CharacterController>();
            inputs = GetComponent<Inputs_Sys>();
            ps = GetComponentInChildren<ParticleSystem>();
            playerCanvas = GetComponentInChildren<PlayerCanvasBehavior>();
            trigger = GetComponentInChildren<GrabTrigger>();

            cam = Camera.main;
            grabHolder = holder.transform;
        }


        void Update()
        {
            Move();

            anim.SetBool("grabbing", grabbingAnObject);

            playerCanvas.transform.position = transform.position + Vector3.up * canvasHeight;
            if (inputs.southButton) //grab
            {
                inputs.southButton = false;
                Item();
            }
            else if (inputs.westButton)
            {
                inputs.westButton = false;
                Action();
            }
            if (interacting)
            {
                currentInteractTime += Time.deltaTime;
            }
            if (currentInteractTime >= interactTime)
            {
                EndInteracting();
            }

            if (inputs.northButton)
            {
                inputs.northButton = false;
                FindObjectOfType<AudioManager>().Play("Cuak");
            }
        }

        private void Move()
        {
            m_MoveDirection = Vector3.zero;
            if (inputs.moveAxis != Vector2.zero) //if there is some move
            {
                forward = cam.transform.forward;
                right = cam.transform.right;

                forward.y = 0f;
                right.y = 0f;

                //Normalize the vectors.
                forward.Normalize();
                right.Normalize();
                desiredMoveDirection = forward * inputs.moveAxis.y + right * inputs.moveAxis.x;
                desiredMoveDirection.Normalize();


                m_MoveDirection = desiredMoveDirection;
                if (!interacting)
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), Time.deltaTime * m_RotateSpeed);

                anim.SetBool("moving", true);

                if (!ps.isPlaying)
                    ps.Play();
            }
            else
            {
                anim.SetBool("moving", false);

                if (ps.isPlaying)
                    ps.Stop();
            }
            if (m_PlayerController.isGrounded)
            {
                m_MoveDirection.y = 0f;
            }
            else
            {
                m_MoveDirection.y = -m_GravityFactor;
            }
            if (grabbingAnObject && !interacting) m_PlayerController.Move(m_MoveDirection * Time.deltaTime * m_GrabMoveSpeed);
            else if (!interacting) m_PlayerController.Move(m_MoveDirection * Time.deltaTime * m_MoveSpeed);
        }

        public void CanGrab(bool state, GameObject obj)
        {
            //print("canGrab" + state + obj);
            playerCanvas.ShowAButtonOnCanvas(state); //shows A hint button
            canGrab = state;
            if (state)
            {
                objectToGrab = obj;
            }
        }

        public void CanChop(bool state, GameObject obj)
        {
            //print("canChop" + state + obj);
            playerCanvas.ShowXButtonOnCanvas(state, canGrab); //shows X hint button
            canChop = state;
            if (state)
            {
                objectToInteract = obj;
            }
        }

        public void CanBuild(bool state, GameObject obj)
        {
            //print("canBuild" + state + obj);
            if (!canChop) playerCanvas.ShowXButtonOnCanvas(state, canGrab); //shows A hint button
            canBuild = state;
            if (state)
            {
                objectToInteract = obj;
            }
        }

        private void Item() //A BUTTON
        {
            if (canGrab)//grab
            {
                logRigidBody = objectToGrab.GetComponent<Rigidbody>();
                FindObjectOfType<AudioManager>().Play("Grab");
                CanGrab(false, null); //desactiva botón
                grabbingAnObject = true;
                objectToGrab.transform.position = grabHolder.position;
                objectToGrab.transform.rotation = grabHolder.rotation;
                //robar tronco
                if (objectToGrab.transform.parent != parentableTransform)
                {
                    objectToGrab.transform.parent.gameObject.GetComponentInParent<PlayerBehaviour>().Item();
                }
                logRigidBody.useGravity = false;
                logRigidBody.isKinematic = true;
                objectToGrab.transform.SetParent(grabHolder);

            }
            else if (grabbingAnObject) //drop
            {
                FindObjectOfType<AudioManager>().Play("Drop");
                logRigidBody = objectToGrab.GetComponent<Rigidbody>();

                grabbingAnObject = false;
                trigger.SetActiveTrigger(true);
                logRigidBody.useGravity = true;
                logRigidBody.isKinematic = false;
                objectToGrab.transform.SetParent(parentableTransform);
                if (anim.GetBool("moving"))
                {
                    logRigidBody.AddForce(transform.forward * throwForce, ForceMode.Impulse);
                }
            }
        }

        private void Action() //X BUTTON
        {
            if (canChop)
            {
                //CanChop(false, null);             
                //bloquear arbol para el otro jugador
                objectToInteract.GetComponentInParent<Tree>().locked = true;
                interacting = true; //para el animador
                objectToInteract.GetComponent<Animator>().SetTrigger("Chop");
                anim.SetBool("chopping", true);
            }
            else if (canBuild)
            {
                interacting = true; //para el animador
                Destroy(objectToGrab);
                objectToGrab = null;
                grabbingAnObject = false;
                //bloquear constructor            
                //si es el ultimo tronco, animar la rampa
                anim.SetBool("chopping", true);

            }
        }

        private void EndInteracting()
        {
            anim.SetBool("chopping", false);
            currentInteractTime = 0;
            interacting = false;
            if (objectToInteract != null && objectToInteract.CompareTag("Tree"))
            {
                //sonido
                FindObjectOfType<AudioManager>().PlaySoundWithRandomPitch(2); //CHOP               
                                                                              //cortar arbol
                Tree tree = objectToInteract.gameObject.GetComponentInParent<Tree>();
                tree.Fall();
                //cogemos tronco
                grabbingAnObject = true;
                GameObject log = Instantiate(logPrefab, grabHolder.position, grabHolder.rotation);
                Rigidbody objectRb = log.GetComponent<Rigidbody>();
                objectRb.useGravity = false;
                objectRb.isKinematic = true;
                log.transform.SetParent(grabHolder);
                objectToGrab = log;
                objectToInteract = null;
            }

            if (objectToInteract != null && objectToInteract.CompareTag("Buildable"))
            {
                FindObjectOfType<AudioManager>().Play("Build");

                Buildable buildable = objectToInteract.gameObject.GetComponent<Buildable>();
                buildable.Build();
                print(objectToInteract.name);
                objectToInteract = null;
                //playerCanvas.ShowXButtonOnCanvas(false, false);
            }
        }
    }
}