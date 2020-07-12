using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastStageAudioManager : MonoBehaviour
{
    public AudioClip finalMusic;
    // Start is called before the first frame update
    void Start()
    {
        GameObject globalAudio = GameObject.Find("Audio");
        if (globalAudio)
        {
            AudioSource source = globalAudio.GetComponent<AudioSource>();
            source.clip = finalMusic;
            source.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
