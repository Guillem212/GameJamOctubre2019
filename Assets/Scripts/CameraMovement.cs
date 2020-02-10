using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
        public float dampTime = 0.5f;   
    public float sizeMargin = 5f;           
    public float minSize = 5f;
    public List<Transform> targets;                  

    private new Camera camera;
    public Camera cameraCanvas;
    private float zoomVelocity;
    private Vector3 moveVelocity;

    private Vector3 targetPosition;
    private float targetSize;

    public int distanceToMap;


    private void Awake ()
    {
        camera = GetComponentInChildren<Camera> ();
    }

    public void CenterCamera()
    {
        FindTargetPosition();
        transform.position = targetPosition;

        FindTargetSize();
        camera.orthographicSize = targetSize;
        cameraCanvas.orthographicSize = camera.orthographicSize;
    }

    private void LateUpdate()
    {
        if(targets.Count > 0)
        {
            Move();
            Zoom();
        }
    }


    private void Move ()
    {
        FindTargetPosition();
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition - transform.forward * distanceToMap, ref moveVelocity, dampTime);
    }


    private void FindTargetPosition ()
    {
        Vector3 averagePosition = new Vector3 ();
        int targetCount = 0;

        foreach (Transform target in targets)
        {
            if (!target.gameObject.activeSelf)
            {
                continue;
            }

            averagePosition += target.position;
            targetCount++;
        }

        if (targetCount > 0)
        {
            averagePosition /= targetCount;
        }

        // averagePosition.y = transform.position.y
        targetPosition = averagePosition;
    }


    private void Zoom ()
    {
        FindTargetSize();
        camera.orthographicSize = Mathf.SmoothDamp (camera.orthographicSize, targetSize, ref zoomVelocity, dampTime);
        cameraCanvas.orthographicSize = camera.orthographicSize;
    }


    private void FindTargetSize ()
    {
        Vector3 screenCenterLocalSpace = camera.transform.InverseTransformPoint(targetPosition);
        targetSize = 0f;

       
        foreach (Transform target in targets)
        {
            
            if (!target.gameObject.activeSelf)
            {
                continue;
            }

            Vector3 targetPositionLocalSpace = camera.transform.InverseTransformPoint(target.position);
            Vector3 localPosition = targetPositionLocalSpace - screenCenterLocalSpace;

            targetSize = Mathf.Max(targetSize, Mathf.Abs(localPosition.y));
            targetSize = Mathf.Max(targetSize, Mathf.Abs(localPosition.x) / camera.aspect);
        }

        targetSize += sizeMargin;
        targetSize = Mathf.Max (targetSize, minSize);
    }

    public void addToTargets(Transform obj)
    {
        targets.Add(obj);
    }

}
