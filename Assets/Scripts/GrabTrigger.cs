using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabTrigger : MonoBehaviour
{
    PlayerInteraction player;
    BoxCollider collider;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider>();
        player = GetComponentInParent<PlayerInteraction>();
    }    
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Wood"))
        {
            player.CanGrab(false, null);
        }
        if (other.CompareTag("Tree"))
        {
            player.CanInteract(false, null);
        }
        if (other.CompareTag("Buildable"))
        {
            player.CanInteract(false, null);
        }
    }

    private void OnTriggerStay(Collider other) //con ontrigger enter petaba
    {        
        if (other.CompareTag("Wood"))
        {
            if (!player.grabbingAnObject) player.CanGrab(true, other.gameObject);            
        }
        if (other.CompareTag("Tree"))
        {
            if (!player.grabbingAnObject) player.CanInteract(true, other.gameObject);
        }
        if (other.CompareTag("Buildable"))
        {
            if (player.grabbingAnObject) player.CanInteract(true, other.gameObject);
        }
    }

    public void SetActiveTrigger(bool state)
    {
        collider.enabled = state;
    }
}
