using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private AudioManager.AudioGroup groupName;
    [SerializeField] private float volumeMin;
    [SerializeField] private float volumeMax;
    [SerializeField] private AudioClip sound;
    [SerializeField, Range(0.0f, 1.0f)] private float testSoundVolume = 1.0f;
    private const float SoundCooldownTime = 0.1f;
    private float time = SoundCooldownTime;

    private void Awake()
    {
        time = SoundCooldownTime;
        slider.SetValueWithoutNotify(Mathf.InverseLerp(volumeMin, volumeMax, AudioManager.Instance.GetVolume(groupName)));
        slider.onValueChanged.AddListener(delegate { ValueChanged(); });
    }

    private void Update()
    {
        if (time >= 0.0f)
        {
            time -= Time.unscaledDeltaTime;
        }
    }

    public void SliderReset()
    {
        slider.SetValueWithoutNotify(Mathf.InverseLerp(volumeMin, volumeMax, AudioManager.Instance.GetVolume(groupName)));
    }

    void ValueChanged()
    {
        if (slider.value == slider.minValue)
        {
            AudioManager.Instance.SetVolume(groupName, -80.0f);
        }
        else
        {
            AudioManager.Instance.SetVolume(groupName, Mathf.Lerp(volumeMin, volumeMax, slider.value));
        }
        if (sound)
        {
            if (time <= 0.0f)
            {
                AudioManager.Instance.SE.PlayOneShot(sound, testSoundVolume);
                time += SoundCooldownTime;
            }
        }
    }
}
