using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using TMPro;

public class OptionTest : MonoBehaviour
{
    private Resolution _currentResolution;
    private FullScreenMode fullScreenMode;
    private int currentResolutionIndex = -1;

    [SerializeField] List<VolumeSlider> _volumeSliderList = new List<VolumeSlider>();
    [SerializeField] TMP_Dropdown _resolutionDropdown;
    [SerializeField] Toggle _fullScreenToggle;

    private void Start()
    {
        //初期値保存
        _currentResolution = Screen.currentResolution;
        fullScreenMode = Screen.fullScreenMode;

        _fullScreenToggle.SetIsOnWithoutNotify(Screen.fullScreenMode != FullScreenMode.Windowed);

        //トグルにフルスクリーン切り替えのコールバック追加
        _fullScreenToggle.onValueChanged.AddListener((_) => Screen.fullScreenMode = _ ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed);
        //ドロップダウンにコールバック追加
        _resolutionDropdown.onValueChanged.AddListener((_) => {
            int index = Screen.resolutions.Length - 1 - _;
            Screen.SetResolution(Screen.resolutions[index].width, Screen.resolutions[index].height, Screen.fullScreenMode, Screen.resolutions[index].refreshRate);
        });

        //解像度のリスト
        List<string> resList = new List<string>();
        for (int i = Screen.resolutions.Length - 1; i > -1 ; i--)
        {
            if (_currentResolution.width == Screen.resolutions[i].width &&
                _currentResolution.height == Screen.resolutions[i].height &&
                _currentResolution.refreshRate == Screen.resolutions[i].refreshRate)
            {
                currentResolutionIndex = Screen.resolutions.Length - 1 - i;
            }
            resList.Add(string.Format("{0,4} x {1,4} {2,3}Hz", Screen.resolutions[i].width, Screen.resolutions[i].height, Screen.resolutions[i].refreshRate));
        }
        //リスト追加
        _resolutionDropdown.AddOptions(resList);
        if (currentResolutionIndex > -1)
        {
            _resolutionDropdown.SetValueWithoutNotify(currentResolutionIndex);
        }
    }

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

    public void WindowSettingReset()
    {
        Screen.SetResolution(_currentResolution.width, _currentResolution.height, fullScreenMode, _currentResolution.refreshRate);
        for (int i = Screen.resolutions.Length - 1; i > -1; i--)
        {
            if (_currentResolution.width == Screen.resolutions[i].width &&
                _currentResolution.height == Screen.resolutions[i].height &&
                _currentResolution.refreshRate == Screen.resolutions[i].refreshRate)
            {
                currentResolutionIndex = Screen.resolutions.Length - 1 - i;
            }
        }
    }

    public void WindowSettingSave()
    {
        _currentResolution = Screen.currentResolution;
        fullScreenMode = Screen.fullScreenMode;
    }
}
