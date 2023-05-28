using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GouTou : MonoBehaviour
{
    Transform T;//这里放玩家的transform
    Vector3 newT;
    // Start is called before the first frame update
    void Start()
    {
        T = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        newT = gameObject.transform.position;
        newT.x = T.position.x;
        gameObject.transform.position = newT;
        ;
    }
}
