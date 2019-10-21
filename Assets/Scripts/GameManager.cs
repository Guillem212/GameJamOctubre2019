using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static GameManager instance;
    private static LevelManager levelInstance;

    private void LateUpdate()
    {
        if(Input.GetKeyDown(KeyCode.R))
            ResetScene();
        /*if(endOfLevel == true){
            NextScene();
        }*/
    }

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
        }
        DontDestroyOnLoad(gameObject);

        levelInstance = new LevelManager();
    }
    
    public static void Pause()
    {

    }

    public static void Resume()
    {

    }

    public void NextScene(){
        
    }

    public void ResetScene(){

    }

    public void ReturnToMenu(){

    }


}
