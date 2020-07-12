using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    private AudioSource playerAudioSource;
    public AudioClip failureSound;

    // Start is called before the first frame update
    void Start()
    {
        playerAudioSource = GameObject.Find("PlayerAudio").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            playerAudioSource.clip = failureSound;
            playerAudioSource.Play();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
