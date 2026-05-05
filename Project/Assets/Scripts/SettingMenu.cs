using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider clipSlider;

    [SerializeField] private Button musicButton;
    [SerializeField] private Button clipButton;

    private bool isMusicMute = false;
    private bool isClipMute = false;

    // 实际上这里并不参与任何音量设置，只做传参使用
    private void Start()
    {
        musicSlider.onValueChanged.AddListener((val) => {
            GameEvent.OnVolumeChanged?.Invoke("Music", val);
        });

        clipSlider.onValueChanged.AddListener((val) => {
            GameEvent.OnVolumeChanged?.Invoke("Clip", val);
        });

        musicButton.onClick.AddListener(() => {
            GameEvent.OnButtonVolumeChanged?.Invoke("Music", isMusicMute);
            if (isMusicMute)
            {
                isMusicMute = false;
                musicSlider.value = 1;
            }
            else
            {

                isMusicMute = true;
                musicSlider.value = 0;
            }
        });

        clipButton.onClick.AddListener(() => {
            GameEvent.OnButtonVolumeChanged?.Invoke("Clip", isClipMute);
            if (isClipMute)
            {
                isClipMute = false;
                clipSlider.value = 1;
            }
            else
            {

                isClipMute = true;
                clipSlider.value = 0;
            }
        });

        // 初始化滑块音量
        musicSlider.value = 1;
        clipSlider.value = 1;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
