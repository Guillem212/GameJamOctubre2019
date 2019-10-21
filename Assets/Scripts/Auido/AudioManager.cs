using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour {

    //llamar con FindObjectOfType<AudioManager>().Play("X");
	public Sound[] sounds;

	//public AudioMixerGroup audioMixerMaster;

	public static AudioManager instance;
    Sound soft;
    Sound hard;
    Sound bonfire;
    [Range(0.5f,1f)]
    public float minPitchRange = 0.85f;
    [Range(1, 1.5f)]
    public float maxPitchRange = 1.22f;

    // Use this for initialization
    void Awake () {
        
		if (instance == null)
			instance = this;
		else {
			Destroy (gameObject);
			return;
		}

		DontDestroyOnLoad (gameObject);

		foreach (Sound s in sounds) 
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.loop = s.loop;	
			//s.source.outputAudioMixerGroup = audioMixerMaster; //to control volume
		}
			
		Play("SoftSoundtrack");
        Play("HardSoundtrack");
        Play("Bonfire");
    }

    private void Start()
    {        
        soft = sounds[0];
        hard = sounds[1];
        bonfire = sounds[8];
    }

    public void Play (string name)
	{
		Sound s = Array.Find(sounds, sound => sound.name == name);
		if (s == null)
			return;
		s.source.Play();
	}

	public void Stop (string name)
	{
		Sound s = Array.Find(sounds, sound => sound.name == name);
		if (s == null)
			return;
		s.source.Stop();
	}		

    public void UpdateSoundtrack(float value)
    {
        //value depende de la vida de la hoguera (0 - > 1)
        //si es 1 -> soft volume = max / hard volume = 0;
        soft.source.volume = value;
        //si es 0 -> soft volume = 0 / hard volume = 1
        hard.source.volume = 1 - value;
        bonfire.source.volume = value * 0.8f;
    }
			    

    public void PlaySoundWithRandomPitch(int index) 
	{
        float rdmPitch = UnityEngine.Random.Range(minPitchRange, maxPitchRange); //pitch range
        Sound s = sounds[index];
        s.source.pitch = rdmPitch;
        Play(s.name);            
    }








    /*
        if (index == 0) //chop
		{
			float rdmPitch = UnityEngine.Random.Range(0.85f, 1.22f); //pitch range
			Sound example = Array.Find(sounds, sound => sound.name == "soundName");
			example.source.pitch = rdmPitch;
			Play("soundName");
		}*/

    /*
	private IEnumerator TemporalSilence(float timeToWait) //stops soundtrack for x time and restore it again with a fade in
	{
		Sound backgroundSound = sounds [0];
		float originalVolume = backgroundSound.source.volume;//stores background music volume
		backgroundSound.source.volume = 0;
		yield return new WaitForSeconds (timeToWait);
		while (backgroundSound.source.volume < originalVolume)
		{
			//print("iteration");
			backgroundSound.source.volume += 0.005f;  
			yield return null;
		}
	}

    public void ReduceMusicVolume(float time) //only background
    {
        StartCoroutine(TemporalSilence(time));
    }*/
}
