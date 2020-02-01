using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityBox : MonoBehaviour
{
    Elevator elevator;

    private void Awake()
    {
        elevator = GetComponentInParent<Elevator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            elevator.security = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            elevator.security = true;
        }
    }
}
