using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    private bool boosting = false;
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
            if(boosting != true)
			{
                boosting = true;
                StartCoroutine(BoostPlayer(collision.gameObject));
                boosting = false;
            }
        }
     }

    private IEnumerator BoostPlayer(GameObject player)
    {
        audioSource.Play();
        yield return new WaitForSeconds(0.15f);
        player.GetComponent<PlayerController>().HandleBoost();
    }
}
