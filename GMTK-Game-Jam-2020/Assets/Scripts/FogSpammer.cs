using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogSpammer : MonoBehaviour
{
    public GameObject fog;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnFog());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpawnFog()
    {
        while (true)
        {
            GameObject f = GameObject.Instantiate(fog, transform.position, transform.rotation);
            yield return new WaitForSeconds(20);
        }
    }
}
