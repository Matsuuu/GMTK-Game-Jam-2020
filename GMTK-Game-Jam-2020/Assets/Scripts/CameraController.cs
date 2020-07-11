using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public bool followTarget = true;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FollowTarget();
        
    }

    private void FollowTarget()
    {
        if (!followTarget) return;

        Vector3 targetVec = target.position;
        Vector3 targetPos = new Vector3(targetVec.x + 4, targetVec.y + 1, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, speed * Time.deltaTime);
    }
}
