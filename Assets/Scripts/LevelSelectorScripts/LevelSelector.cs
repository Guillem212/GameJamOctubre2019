
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ParticleSystemJobs;
using UnityEngine.UIElements;

public class LevelSelector : MonoBehaviour
{
    public GameObject[] levels;

    public NavMeshAgent agent;

    public ParticleSystem particleSystem;

    private GameObject levelSelected;
    private int indexLevelSelected;

    private float timer;

    private static LevelManager levelInstance;

    private void Start() {
        levels = GameObject.FindGameObjectsWithTag("levelPoint");
        foreach(GameObject level in levels){
            level.GetComponentInChildren<Canvas>().enabled = false;
        }

        indexLevelSelected = 0;
        levelSelected = levels[indexLevelSelected];
        levelSelected.GetComponentInChildren<Canvas>().enabled = true;
        timer = 0;
        levelInstance = GameObject.FindGameObjectWithTag("GameManager").GetComponent<LevelManager>();

        particleSystem.Stop();
    }

    private void Update() {
        if(!CameraRotation.inMenu){
            changeDestination();
            reachedDestination();
        }
    }

    private void changeDestination(){
        float axis = Input.GetAxisRaw("Horizontal");
        if( axis != 0 && timer <= 0){

            timer = .2f;
            
            //Seleccion de nivel hacia la derecha
            if(axis > 0 && indexLevelSelected < levels.Length-1){
                activateCanvas(levelSelected);
                indexLevelSelected++;
                levelSelected = levels[indexLevelSelected];
                activateCanvas(levelSelected);
            }//Seleccion de nivel hacia la izquierda
            else if(axis < 0 && indexLevelSelected > 0){
                activateCanvas(levelSelected);
                indexLevelSelected--;
                levelSelected = levels[indexLevelSelected];
                activateCanvas(levelSelected);
            }

        }
        else if (timer >= 0){
            timer -= Time.deltaTime;
        }

        agent.SetDestination(levelSelected.transform.position);
    }

    private void activateCanvas(GameObject level){
        level.GetComponentInChildren<Canvas>().enabled = !level.GetComponentInChildren<Canvas>().enabled;
    }

    private void reachedDestination(){
        if(agent.remainingDistance <= 0){
            //Ha llegado a su destino.
            if(particleSystem.isPlaying) particleSystem.Stop();
            
            if(Input.GetButtonDown("Submit")){
                /*GameObject[] objs = levelSelected.GetComponentsInChildren<getGameObjectScript>();
                objs[0] = levelSelected.transform.GetChild(1).gameObject;
                objs[1] = levelSelected.transform.GetChild(2).gameObject;
                foreach(GameObject o in objs){
                    o.SetActive(true);
                }*/
            } //levelInstance.loadScene(1);
        }
        else{
            if(!particleSystem.isPlaying) particleSystem.Play();   
        }
    }


}
