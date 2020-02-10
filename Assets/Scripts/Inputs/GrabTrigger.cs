using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamOctubre.Input_System
{
    public class GrabTrigger : MonoBehaviour
    {
        public PlayerBehaviour player;
        public BoxCollider coll;

        // Start is called before the first frame update
        void Start()
        {
            coll = GetComponent<BoxCollider>();
            player = GetComponentInParent<PlayerBehaviour>();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Wood"))
            {
                player.CanGrab(false, null);
            }
            if (other.CompareTag("Tree"))
            {
                player.CanChop(false, null);
            }
            if (other.CompareTag("Buildable"))
            {
                player.CanBuild(false, null);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Wood"))
            {
                if (!player.grabbingAnObject) player.CanGrab(true, other.gameObject);
                else player.CanGrab(false, null);
            }
            if (other.CompareTag("Tree")) //chequear el estado de tree, el otro jugador puede bloquearlo
            {
                if (!player.grabbingAnObject && !other.GetComponentInParent<Tree>().locked) //and arbol no bloqueado
                    player.CanChop(true, other.gameObject);
                else //frente a un arbol con una madera o lo está talando otro jugador
                {
                    //ignorar arbol
                    player.CanChop(false, null);
                }
            }
            if (other.CompareTag("Buildable"))
            {
                if (player.grabbingAnObject)
                    player.CanBuild(true, other.gameObject);
                else player.CanBuild(false, null); //hay que llevar madera para construir
            }
        }

        public void SetActiveTrigger(bool state)
        {
            coll.enabled = state;
        }
    }
}