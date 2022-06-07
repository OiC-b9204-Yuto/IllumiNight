using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    public const string dataFileName = "volume.save";
    public const float defaultVolume = 0.0f;

    public enum AudioGroup
    {
        Master,
        BGM,
        SE,
    }

    public readonly AudioGroup[] saveAudioGroups =
    {
        AudioGroup.BGM,
        AudioGroup.SE
    };

    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private AudioSource _audioBGM;
    public AudioSource BGM { get { return _audioBGM; } }
    [SerializeField] private AudioSource _audioSE;
    public AudioSource SE { get { return _audioSE; } }

    private void Start()
    {
        Load();
    }

    /// <summary>
    /// 現在の音量のデータをファイルに保存する関数
    /// </summary>
    public void Save()
    {
        VolumeDataListWrapper wrapper = new VolumeDataListWrapper();
        foreach (var audioGroup in saveAudioGroups)
        {
            VolumeData volumeData = new VolumeData(audioGroup, GetVolume(audioGroup));
            wrapper.volumeDataList.Add(volumeData);
        }
        string data = JsonUtility.ToJson(wrapper);
        FileManager.Save(dataFileName, data);
    }

    public string ResetSave()
    {
        VolumeDataListWrapper wrapper = new VolumeDataListWrapper();
        foreach (var audioGroup in saveAudioGroups)
        {
            VolumeData volumeData = new VolumeData(audioGroup, defaultVolume);
            wrapper.volumeDataList.Add(volumeData);
        }
        string data = JsonUtility.ToJson(wrapper);
        FileManager.Save(dataFileName, data);
        return data;
    }

    /// <summary>
    /// 音量のデータをファイルから読み取り適用する関数
    /// </summary>
    public void Load()
    {
        bool result;
        string data = FileManager.Load(dataFileName,out result);
        if (!result)
        {
            data = ResetSave();
        }
        VolumeDataListWrapper wrapper = JsonUtility.FromJson<VolumeDataListWrapper>(data);
        foreach (var volumeData in wrapper.volumeDataList)
        {
            SetVolume(volumeData.audioGroup, volumeData.volume);
        }
    }

    public float GetVolume(AudioGroup name)
    {
        float ret = 0;
        _audioMixer.GetFloat(name.ToString(), out ret);
        return ret;
    }

    public void SetVolume(AudioGroup name, float value)
    {
        _audioMixer.SetFloat(name.ToString(), value);
    }

    [Serializable]
    public class VolumeData
    {
        public AudioGroup audioGroup;
        public float volume;
        public VolumeData(AudioGroup audioGroup, float volume)
        {
            this.audioGroup = audioGroup;
            this.volume = volume;
        }
    }

    [Serializable]
    public class SliderValueData
    {
        
    }

    [Serializable]
    public class VolumeDataListWrapper
    {
        public List<VolumeData> volumeDataList;
        public VolumeDataListWrapper()
        {
            volumeDataList = new List<VolumeData>();
        }
    }
}