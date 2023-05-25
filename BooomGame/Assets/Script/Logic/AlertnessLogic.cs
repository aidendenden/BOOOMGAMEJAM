using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AlertnessLogic : MonoBehaviour, IPointerClickHandler
{
    public int 每次增加的数值;
    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Instance.ChangeAlertnessValue(每次增加的数值);
    }

    void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.ChangeAlertnessValue(每次增加的数值);
    }
}
