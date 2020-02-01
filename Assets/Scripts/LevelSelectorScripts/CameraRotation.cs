
using UnityEngine;

public class CameraRotation : MonoBehaviour
{

    public Transform player;

    public GameObject[] canvas;

    private void Update() {
        transform.LookAt(-player.position, Vector3.up);
        foreach(GameObject c in canvas){
            c.transform.LookAt(Camera.main.transform.position);
        }
    }
}
