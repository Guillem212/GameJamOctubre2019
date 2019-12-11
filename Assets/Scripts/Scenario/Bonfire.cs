using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bonfire : MonoBehaviour
{
    public float fire;
    public float maxFire;
    public float extinguishFrequency;
    public GameObject emissive;

    public Light[] lights;

    public float maxLampIntensity;
    public float maxBonfireIntensity;

    public Image filler;
    public Image background;


    public float UITime;
    public AudioManager audioManager;
    private float currentTime = -1;

    private void Awake()
    {
        filler.enabled = background.enabled = false;
        currentTime = -1;
    }

    void Update()
    {
        if(fire > 0)
            fire -= Time.deltaTime * extinguishFrequency;
        UpdateLights();

        if (currentTime >= 0) currentTime += Time.deltaTime;
        if (currentTime >= UITime)
        {
            filler.enabled = background.enabled = false;
            currentTime = -1;
        }
        else if (currentTime >= UITime * 0.6)
        {
            filler.color = new Vector4(1, 1, 1, Mathf.Lerp(filler.color.a, 0, Time.deltaTime * 2 * UITime));
            background.color = new Vector4(0, 0, 0, Mathf.Lerp(background.color.a, 0, Time.deltaTime * 4 * UITime));

        }
    }

    public void Feed()
    {
        fire +=2;
    }

    private void UpdateLights()
    {
        lights[0].intensity = fire / maxFire * maxBonfireIntensity;
        for (int i = 1; i < lights.Length; i++)
        {
            lights[i].intensity = fire / maxFire * maxLampIntensity;
        }
        emissive.transform.localScale = Vector3.one * fire / maxFire;
        filler.fillAmount = fire / maxFire;
        audioManager.UpdateSoundtrack(Mathf.Clamp01(fire / maxFire));
            
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wood"))
        {
            FindObjectOfType<AudioManager>().Play("Lit");
            Feed();
            Destroy(collision.gameObject);
        }
        PlayerInteraction player1 = GameObject.Find("Player1").GetComponent<PlayerInteraction>();
        PlayerInteraction player2 = GameObject.Find("Player2").GetComponent<PlayerInteraction>();
        player1.CanGrab(false, null);
        player2.CanGrab(false, null);
    }

    private void OnTriggerStay(Collider other)
    {
        currentTime = -1;
        if (other.CompareTag("Player"))
        {
            filler.enabled = true;
            filler.color = Color.white;
            background.enabled = true;
            background.color = new Vector4(0, 0, 0, 0.5f);
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
