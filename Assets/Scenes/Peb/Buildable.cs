using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buildable : MonoBehaviour
{
    public int neededWood;
    public int currentWood;

    public GameObject building;
    public float textTime;
    private Text text;
    private float currentTime = -1;

    // Start is called before the first frame update
    void Start()
    {
        building.gameObject.SetActive(false);
        text = GetComponentInChildren<Text>();
        text.text = currentWood + "/" + neededWood;
        text.enabled = false;
    }

    private void Update()
    {
        if(currentTime>=0) currentTime += Time.deltaTime;
        if (currentTime >= textTime)
        {
            text.enabled = false;
            currentTime = -1;

        }
        else if (currentTime >= textTime * 0.6)
        {
            text.color = new Vector4(1,1,1, Mathf.Lerp(text.color.a, 0, Time.deltaTime * 2 * textTime));

        }
    }

    public void Build()
    {
        currentWood++;
        text.text = currentWood + "/" + neededWood;
        if (currentWood >= neededWood)
        {
            building.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        currentTime = -1;
        if (other.CompareTag("Player"))
        {
            text.enabled = true;
            text.color = Color.white;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            currentTime = 0;
        }
    }
}
