using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AlertnessLogic : MonoBehaviour, IPointerClickHandler
{
    public int 每次增加的数值;
    public bool OnTrigger;
    public bool OnClick;

    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (OnClick)
        {
            GameManager.Instance.ChangeAlertnessValue(每次增加的数值);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (OnTrigger)
        {
            GameManager.Instance.ChangeAlertnessValue(每次增加的数值);
        }
    }
}
