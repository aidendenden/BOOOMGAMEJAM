using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsDoor : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            Rigidbody rigidbody = other.GetComponent<Rigidbody>();

            rigidbody.drag = 500;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            Rigidbody rigidbody = other.GetComponent<Rigidbody>();

            rigidbody.drag = 1;
        }
    }
}
