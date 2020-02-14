using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{

    private const int ONE = 1;
    private static GameManager instance;
    public static bool endOfLevel;
    private static LevelManager levelInstance;

    private List<GameObject> players;

    void Awake()
    {
        // Create the singleton
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        levelInstance = GetComponent<LevelManager>();
        players = new List<GameObject>();
    }


    private void LateUpdate()
    {
        //if(Input.GetKeyDown(KeyCode.R))
        //levelInstance.resetScene();
        if (endOfLevel == true)
        {
            levelInstance.loadScene(levelInstance.actualScene + ONE);
        }
    }
}
