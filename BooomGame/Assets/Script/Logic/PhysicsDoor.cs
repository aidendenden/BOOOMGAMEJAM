using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsDoor : MonoBehaviour
{



    public AudioClip collisionSound;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = collisionSound;
    }



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
    void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.tag == "Player")
        {
            audioSource.PlayOneShot(audioSource.clip);
        }
    }
}
