using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AlertnessLogic : MonoBehaviour, IPointerClickHandler
{
    public int 每次增加的数值;
    public bool OnTrigger;
    public bool OnClick;
    public Transform _ThisT;
    public AudioClip collisionSound;

    private AudioSource audioSource;
    


    private void Start()
    {
        _ThisT = gameObject.transform;
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = collisionSound;

    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (OnClick)
        {

            GameManager.ChangeAlertnessValue(每次增加的数值, _ThisT);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (OnTrigger)
        {
            if (other.tag == "HuanJin")
            {
                audioSource.PlayOneShot(audioSource.clip);
                GameManager.ChangeAlertnessValue(每次增加的数值, _ThisT);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
       
    }
}
