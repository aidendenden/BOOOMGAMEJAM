using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameItem : MonoBehaviour
{
    [SerializeField]
    public EGameObj gameItem;
    
    void OnTriggerEnter(Collider other) {
        PlayerManager triggerEvent = other.GetComponent<PlayerManager>();
        if (triggerEvent != null) {
            triggerEvent.Triggered("Hello, World!",gameItem);
        }
    }
    
}
