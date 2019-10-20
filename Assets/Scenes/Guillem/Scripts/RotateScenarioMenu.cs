using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScenarioMenu : MonoBehaviour
{
    void LateUpdate()
    {
        transform.Rotate(0f, 0.2f, 0f, Space.Self);
    }
}
