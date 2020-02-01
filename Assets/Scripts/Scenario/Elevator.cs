using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Transform endTransform;
    private Vector3 start;
    private Vector3 end;
    public float speed;
    public bool security;
    private int state; // 0: Idle (up), 1: Descending, 2: Idle (down), 3: Ascending

    // Start is called before the first frame update
    void Awake()
    {
        start = transform.position;
        end = endTransform.position;
        security = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (state == 1)
        {
            if(transform.position.y > end.y)
            {
        
                if (security) transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);

            }
            else
            {
                state++;
            }
        }
        else if (state == 3)
        {
            if (transform.position.y < start.y)
            {
                transform.Translate(Vector3.up * speed * Time.deltaTime, Space.World);
            }
            else
            {
                state = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = this.gameObject.transform;
            if (state == 0 || state == 2) state++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = GameObject.Find("ParentableObject").transform;
        }
    }
}
