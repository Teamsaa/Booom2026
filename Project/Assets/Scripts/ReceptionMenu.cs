using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ReceptionMenu : MonoBehaviour
{
    [SerializeField] private GameObject mask;
    [SerializeField] private GameObject operate;
    [SerializeField] private GameObject customer;
    [SerializeField] private GameObject order;

    private void Update()
    {
        Detection();
    }

    public void Detection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mask.SetActive(true);
            operate.SetActive(false);
            customer.SetActive(true);
            order.SetActive(true);
        }
    }
}
