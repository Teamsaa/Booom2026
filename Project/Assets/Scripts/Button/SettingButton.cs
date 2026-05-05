using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class SettingButton : MonoBehaviour
{
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject mask;

    public void OpenPanel()
    {
        if (settingPanel != null)
        {
            settingPanel.SetActive(true);
            mask.SetActive(true); 
        }
    }

    public void ClosePanel()
    {
        if (settingPanel != null)
        {
            settingPanel.SetActive(false);
            mask.SetActive(false);
        }
    }
}
