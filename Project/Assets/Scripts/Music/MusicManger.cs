using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Slider = UnityEngine.UI.Slider;

public class MusicManger : Singleton<MusicManger>
{
    [SerializeField] private AudioMixer audioMixer;

    [Header("记录当前音量(0-1)")]
    [SerializeField] private float musicVolume = 0.75f;
    [SerializeField] private float clipVolume = 0.75f;

    protected override void Init()
    {
        base.Init();
        DontDestroyOnLoad(this.gameObject);
    }

    #region 事件订阅 & 音量修改
    void OnEnable()
    {
        // 订阅事件
        Debug.Log("订阅事件成功！");
        GameEvent.OnVolumeChanged += HandleVolumeChange;
        GameEvent.OnButtonVolumeChanged += ButtonVolumeChange;
    }

    void OnDisable()
    {
        // 取消订阅，防止内存泄漏
        GameEvent.OnVolumeChanged -= HandleVolumeChange;
        GameEvent.OnButtonVolumeChanged -= ButtonVolumeChange;
    }

    private void ButtonVolumeChange(string type, bool isMute)
    {
        if (type == "Music")
        {
            if (isMute)
            {
                musicVolume = 1;
                SetMixerVolume(type, 1);
            }
            else
            {
                musicVolume = 0;
                SetMixerVolume(type, 0);
            }
        }
        else if (type == "Clip")
        {
            if (isMute)
            {
                clipVolume = 1;
                SetMixerVolume(type, 1);
            }
            else
            {
                clipVolume = 0;
                SetMixerVolume(type, 0);
            }
        }
    }

    private void HandleVolumeChange(string type, float value)
    {
        if (type == "Music")
        {
            musicVolume = value;
            SetMixerVolume(type,value);
        }
        else if (type == "Clip")
        {
            clipVolume = value;
            SetMixerVolume(type ,value);    
        }
    }

    private void SetMixerVolume(string parameterName, float sliderValue)
    {
        // 1. 防止 Log10(0) 报错，限制最小值为 0.0001
        float volume = Mathf.Log10(Mathf.Max(0.0001f, sliderValue)) * 20;

        // 2. 修改 Mixer 的参数
        audioMixer.SetFloat(parameterName, volume);
    }
    #endregion

    public void Play()
    {

    }

    public void Pause()
    {

    }

}
