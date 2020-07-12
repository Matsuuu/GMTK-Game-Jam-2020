using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            StartCoroutine(BoostPlayer(collision.gameObject));
        }
     }

    private IEnumerator BoostPlayer(GameObject player)
    {
        audioSource.Play();
        yield return new WaitForSeconds(0.05f);
        audioSource.Play();
        yield return new WaitForSeconds(0.05f);
        audioSource.Play();
        yield return new WaitForSeconds(0.05f);
        player.GetComponent<PlayerController>().HandleBoost();
    }
}
