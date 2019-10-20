using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    public float cycleDuration;
    public float targetSpeed;
    public float twilight;
    public float night;
    public GameObject target;
    public float temperature;
    private float currentTime;
    private Light light;
    // Start is called before the first frame update
    void Start()
    {

        light = GetComponent<Light>();
        light.colorTemperature = 12500f;
        transform.rotation = Quaternion.Euler(170, -30, 0);
    }

    // Update is called once per frame
    void Update()
    {
        target.transform.position += Vector3.forward * Time.deltaTime * targetSpeed;
        transform.rotation = Quaternion.LookRotation(target.transform.position - transform.position);
        currentTime += Time.deltaTime;
        temperature = Mathf.Lerp(light.colorTemperature, twilight, 0.07f * Time.deltaTime);
        light.colorTemperature = temperature;
        if (currentTime > cycleDuration)
        {
            light.color = Vector4.Lerp(light.color, Color.black, Time.deltaTime * 0.05f);
        }
    }
}
