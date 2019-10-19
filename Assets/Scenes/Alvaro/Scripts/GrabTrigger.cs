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
        player = this.GetComponentInParent<PlayerInteraction>();
    }    
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Wood"))
        {
            player.CanGrab(false, null);
        }
    }

    private void OnTriggerStay(Collider other) //con ontrigger enter petaba
    {        
        if (other.CompareTag("Wood"))
        {
            player.CanGrab(true, other.gameObject);            
        }
    }

    public void SetActiveTrigger(bool state)
    {
        collider.enabled = state;
    }
}
