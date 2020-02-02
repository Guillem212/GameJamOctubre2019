using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo : MonoBehaviour
{
    public GameObject treeTest;
    Animator anim;
    Animator tAnim;
    int aux = 0;
    // Start is called before the first frame update
    void Start()
    {
        aux = 0;
        anim = GetComponent<Animator>();
        tAnim = treeTest.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            anim.SetBool("TEST", true);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            aux = 0;
            tAnim.SetTrigger("TEST");
            
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            anim.SetBool("TEST", true);
        }
        if (aux == 4)
        {
            tAnim.SetTrigger("Chop");
            anim.SetBool("TEST", false);
            aux = 0;
        }
    }

    public void AnimEvent()
    {
        tAnim.SetTrigger("Hit");
        aux += 1;
    }
}
