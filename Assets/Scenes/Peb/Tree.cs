using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    
    public Mesh[] meshes;
    public Mesh saplingMesh;
    public bool isGrown;
    public float minRespawnTime;
    public float maxRespawnTime;

    private float respawnTime;
    private float currentRespawnTime;

    private MeshFilter meshFilter;
    // Start is called before the first frame update

    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        Grow();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGrown)
        {
            currentRespawnTime += Time.deltaTime;
            if(currentRespawnTime >= respawnTime)
            {
                currentRespawnTime = 0;
                Grow();
            }
        }
    }

    public void Fall()
    {
        respawnTime = Random.Range(minRespawnTime, maxRespawnTime);
        isGrown = false;
        meshFilter.mesh = saplingMesh;
    }

    private void Grow()
    {
        meshFilter.mesh = meshes[Mathf.FloorToInt(Random.Range(0, meshes.Length - 0.001f))];
        isGrown = true;
    }

}
