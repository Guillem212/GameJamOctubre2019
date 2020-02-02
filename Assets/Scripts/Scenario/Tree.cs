using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    GameObject grown, sapling;

    Animator grownAnim, saplingAnim;

    public bool locked = false;


    //public Mesh[] meshes;
    //public Mesh saplingMesh;
    public bool isGrown;
    public float minRespawnTime;
    public float maxRespawnTime;

    private float currentRespawnTime, respawnTime, halfRespawnTime;
    bool saplingHalfGrown = false;

    private MeshFilter meshFilter;
    // Start is called before the first frame update

    void Start()
    {
        grown = this.transform.GetChild(0).gameObject;
        sapling = this.transform.GetChild(1).gameObject;

        sapling.SetActive(false);        
        grown.SetActive(true);

        grownAnim = grown.GetComponent<Animator>();
        saplingAnim = sapling.GetComponent<Animator>();

        //meshFilter = GetComponent<MeshFilter>();
        Grow();
        grownAnim.SetBool("Loaded", true);
    }

    /*// Update is called once per frame
    void Update()
    {
        if (!isGrown)
        {
            currentRespawnTime += Time.deltaTime;
            if (currentRespawnTime >= halfRespawnTime && !saplingHalfGrown)
            {
                saplingHalfGrown = true;
                saplingAnim.SetTrigger("GrowHalf");                
            }
            if(currentRespawnTime >= respawnTime)
            {
                currentRespawnTime = 0;
                Grow();                
            }
        }
    }*/

    public void Fall()
    {/*
        //activa el sapling
        sapling.SetActive(true);
        //desactiva el arbol
        grown.SetActive(false);

        respawnTime = Random.Range(minRespawnTime, maxRespawnTime);
        halfRespawnTime = respawnTime / 2;*/
        isGrown = false;
    }

    private void Grow()
    {
        this.locked = false;
        //meshFilter.mesh = meshes[Mathf.FloorToInt(Random.Range(0, meshes.Length - 0.001f))];
        isGrown = true;
        saplingHalfGrown = false;

        //desactiva el sapling
        sapling.transform.localScale = Vector3.one;
        sapling.SetActive(false);
        //restablece lo que sea
        //activa el arbol
        grown.SetActive(true);
    }

}
