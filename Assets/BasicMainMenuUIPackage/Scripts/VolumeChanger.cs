using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeChanger : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] AudioMixer audioMixer;


    private void Start()
    {
        if (!PlayerPrefs.HasKey("Volume"))
        {
            PlayerPrefs.SetFloat("Volume", 0f);
        }
        Load();
    }
    public void ChangeVolume()
    {
        audioMixer.SetFloat("Volume", volumeSlider.value);
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
    }

    public void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("Volume");
        audioMixer.SetFloat("Volume", PlayerPrefs.GetFloat("Volume"));
    }

    private void OnEnable() {
        if (!PlayerPrefs.HasKey("Volume"))
        {
            PlayerPrefs.SetFloat("Volume", 0f);
        }
        Load();
    }
}
