
using UnityEngine;

public class CameraRotation : MonoBehaviour
{

    public Transform player;
    public GameObject cam;

    public GameObject[] canvas;

    public static bool inMenu;

    private void Awake() {
    }

    private void Update() {
        if(inMenu){
            rotateMenu();
        }
        else{
            rotateSelector();
        }
    }

    private void rotateSelector(){
        transform.LookAt(-player.position, Vector3.up);
        foreach(GameObject c in canvas){
            c.transform.LookAt(-Camera.main.transform.position);
        }
        cam.GetComponent<CameraMovement>().targets[0] = player;
        cam.GetComponent<CameraMovement>().minSize = 10f;
    }

    private void rotateMenu(){
        transform.Rotate(0f, -0.2f, 0f, Space.Self);
        
        cam.GetComponent<CameraMovement>().targets[0] = this.transform;
        cam.GetComponent<CameraMovement>().minSize = 20f;
    }
}
