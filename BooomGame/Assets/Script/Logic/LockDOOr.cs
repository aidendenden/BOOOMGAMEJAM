using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockDOOr : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager._unLockState==1)
        {
            Destroy(gameObject);
        }
    }
}
