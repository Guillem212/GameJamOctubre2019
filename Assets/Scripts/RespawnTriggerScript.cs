using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnTriggerScript : MonoBehaviour
{
   /* public Player[] playerScript;

    private void Start() {
        playerScript = new Player[2];
    }

    private void OnTriggerStay(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<Player>().GoToSpawn();
            if(playerScript[0] != null){
                playerScript[1] = other.gameObject.GetComponent<Player>();
                playerScript[1].GoToSpawn();
                playerScript[1] = null;
            }
            else{
                playerScript[0] = other.gameObject.GetComponent<Player>();
                playerScript[0].GoToSpawn();
                playerScript[0] = null;
            }
        }
    }*/
}
