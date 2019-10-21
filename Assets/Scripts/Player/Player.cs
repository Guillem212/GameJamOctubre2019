using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerID;
    private Vector3 spawnPoint;

    /*public void GetNewSpawn(Vector3 newSpawn)
    {
        spawnPoint = newSpawn;
    }*/


    public void GoToSpawn()
    {
        transform.position = spawnPoint;
    }

    private void Awake()
    {
        string toSearch = "InitialSpawnP" + (playerID);
        //Debug.Log(toSearch);
        GameObject initialSpawn = GameObject.Find(toSearch);
        spawnPoint = initialSpawn.transform.position;
        GoToSpawn();
    }

    public string GetPlayerId()
    {
        return playerID.ToString();
    }
}
