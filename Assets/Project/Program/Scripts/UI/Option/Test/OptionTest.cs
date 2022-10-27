using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionTest : MonoBehaviour
{
    [SerializeField] List<VolumeSlider> _volumeSliderList = new List<VolumeSlider>();
    public void AudioReset()
    {
        AudioManager.Instance.Load();
        foreach (var item in _volumeSliderList)
        {
            item.SliderReset();
        }
    }

    public void AudioSave()
    {
        AudioManager.Instance.Save();
    }
}
