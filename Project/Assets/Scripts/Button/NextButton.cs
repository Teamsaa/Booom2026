using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class NextButton : MonoBehaviour
{
    [SerializeField] private Image Menu01;
    [SerializeField] private GameObject Menu02;
    [SerializeField] private GameObject button01;
    [SerializeField] private GameObject button02;

    public void Next()
    {
        Menu01.enabled = false;
        button01.SetActive(false);
        Menu02.SetActive(true);
        button02.SetActive(true);
    }

    public void Return()
    {
        Menu01.enabled = true;
        button01.SetActive(true);
        Menu02.SetActive(false);
        button02.SetActive(false);
    }
}
