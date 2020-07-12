using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameButton : MonoBehaviour
{
    private PersistentDataManager persistentDataManager;
    // Start is called before the first frame update
    void Start()
    {
        persistentDataManager = GameObject.Find("PersistentDataManager").GetComponent<PersistentDataManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToFirstStage()
    {
        persistentDataManager.GoToStage(1);
    }
}
