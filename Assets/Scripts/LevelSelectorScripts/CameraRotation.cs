
using UnityEngine;

public class CameraRotation : MonoBehaviour
{

    public Transform player;
    public GameObject cam;

    public GameObject[] canvas;

    public void rotateSelector(){
        transform.LookAt(-player.position, Vector3.up);
        foreach(GameObject c in canvas){
            c.transform.LookAt(-Camera.main.transform.position);
        }
        cam.GetComponent<CameraMovement>().targets[0] = player;
    }

    public void rotateMenu(){
        transform.Rotate(0f, -0.2f, 0f, Space.Self);
        
        cam.GetComponent<CameraMovement>().targets[0] = this.transform;
    }
}
