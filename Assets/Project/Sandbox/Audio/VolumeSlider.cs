using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private AudioManager.AudioGroup _groupName;
    [SerializeField] private float _volumeMin;
    [SerializeField] private float _volumeMax;
    [SerializeField] private AudioClip _sound;
    [SerializeField, Range(0.0f, 1.0f)] private float _testSoundVolume = 1.0f;
    private const float soundCooldownTime = 0.1f;
    private float _time = soundCooldownTime;

    private void Awake()
    {
        _time = soundCooldownTime;
        _slider.SetValueWithoutNotify(Mathf.InverseLerp(_volumeMin, _volumeMax, AudioManager.Instance.GetVolume(_groupName)));
        _slider.onValueChanged.AddListener(delegate { ValueChanged(); });
    }

    private void Update()
    {
        if (_time >= 0.0f)
        {
            _time -= Time.unscaledDeltaTime;
        }
    }

    public void SliderReset()
    {
        _slider.SetValueWithoutNotify(AudioManager.Instance.GetVolume(_groupName));
    }

    void ValueChanged()
    {
        if (_slider.value == _slider.minValue)
        {
            AudioManager.Instance.SetVolume(_groupName, -80.0f);
        }
        else
        {
            AudioManager.Instance.SetVolume(_groupName, Mathf.Lerp(_volumeMin,_volumeMax,_slider.value));
        }
        if(_sound)
        {
            if (_time <= 0.0f)
            {
                AudioManager.Instance.SE.PlayOneShot(_sound, _testSoundVolume);
                _time += soundCooldownTime;
            }
        }
    }
}
