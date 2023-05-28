using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCount : MonoBehaviour
{
    public static int sceneLoadCount = 0;

    void Start()
    {
        sceneLoadCount++;
        Debug.Log("Scene loaded " + sceneLoadCount + " times.");
    }
    
}
