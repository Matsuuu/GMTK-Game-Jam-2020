using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RedBarrel : MonoBehaviour
{
    private AudioSource barrelAudioSource;
    public AudioClip explodeSound;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        barrelAudioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            Destroy(GetComponent<Rigidbody2D>());
            barrelAudioSource.clip = explodeSound;
            StartCoroutine(ExplodeBarrel());
        }
    }

    public IEnumerator ExplodeBarrel()
    {
        barrelAudioSource.Play();
        animator.SetTrigger("Explosion");
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
