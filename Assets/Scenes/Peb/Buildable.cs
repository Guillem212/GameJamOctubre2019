using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildable : MonoBehaviour
{
    public int neededWood;
    public int currentWood;

    public GameObject building;

    // Start is called before the first frame update
    void Start()
    {
        building.gameObject.SetActive(false);
    }

    public void Build()
    {
        currentWood++;
        if (currentWood >= neededWood)
        {
            building.gameObject.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
