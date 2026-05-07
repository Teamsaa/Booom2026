using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Operate : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject mask;
    [SerializeField] private GameObject operate;
    [SerializeField] private GameObject customer;
    [SerializeField] private GameObject order;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("点击的物体名称是: " + gameObject.name);
        mask.SetActive(true);
        operate.SetActive(false);
        customer.SetActive(true);
        order.SetActive(true);
    }
}
