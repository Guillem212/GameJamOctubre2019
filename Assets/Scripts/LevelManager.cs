using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [HideInInspector]
    public int actualScene;
    AsyncOperation asyncLoad;

    [Header("Loading Canvas"), SerializeField]
    private GameObject canvas;

    private void Start() {
        actualScene = SceneManager.GetActiveScene().buildIndex;
    }

    public void loadScene(int index)
    {
        canvas.GetComponent<Animator>().SetTrigger("StartLoading");
        StartCoroutine(StartLoading(index));
    }

    public void resetScene(){
        canvas.GetComponent<Animator>().SetTrigger("StartLoading");
        StartCoroutine(StartLoading(SceneManager.GetActiveScene().buildIndex));
    }

    IEnumerator LoadAsyncScene(){
        while(!asyncLoad.isDone){
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        canvas.GetComponent<Animator>().SetTrigger("Load");
    }

    IEnumerator StartLoading(int index){
        while(!canvas.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("StateEnd")){
            yield return null;
        }
        asyncLoad = SceneManager.LoadSceneAsync(index);
        StartCoroutine(LoadAsyncScene());

    }

    public void onExit(){
        Application.Quit();
    }
}
