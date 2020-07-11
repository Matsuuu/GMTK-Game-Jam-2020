using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalZone : MonoBehaviour
{
    private PersistentDataManager persistentDataManager;
    // Start is called before the first frame update
    void Start()
    {
        persistentDataManager = GameObject.Find("PersistentDataManager").GetComponent<PersistentDataManager>();
        persistentDataManager.StartTime();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        persistentDataManager.EndTime();
    }
}
