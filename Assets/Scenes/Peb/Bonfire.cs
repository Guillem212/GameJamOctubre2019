using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bonfire : MonoBehaviour
{
    public float fire;
    public float maxFire;
    public float extinguishFrequency;

    public Light[] lights;

    public float maxLampIntensity;
    public float maxBonfireIntensity;

    public Image filler;


    void Update()
    {
        fire -= Time.deltaTime * extinguishFrequency;
        UpdateLights();
    }

    public void Feed()
    {
        fire = Mathf.Max(fire + 1, maxFire);
    }

    private void UpdateLights()
    {
        lights[0].intensity = fire / maxFire * maxBonfireIntensity;
        for (int i = 1; i < lights.Length; i++)
        {
            lights[i].intensity = fire / maxFire * maxLampIntensity;
        }

        //filler.fillAmount = fire / maxFire;
    }
}
