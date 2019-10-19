using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvasBehavior : MonoBehaviour
{
    [SerializeField] GameObject AButton;

    void ShowAButtonOnCanvas(bool state)
    {
        AButton.SetActive(state);
    }
}
