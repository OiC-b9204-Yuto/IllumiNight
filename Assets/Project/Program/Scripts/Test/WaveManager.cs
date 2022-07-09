using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

public class WaveManager : MonoBehaviour
{
    [SerializeField] List<WaveData> _waveDataList = new List<WaveData>();

    UnityEvent _waveChangedEvent;
    public UnityEvent WaveChengedEvent => _waveChangedEvent;

    List<BaseEnemy> _currentEnemyList = new List<BaseEnemy>();
    List<IObservable<bool>> _currentEnemyObservableList = new List<IObservable<bool>>();

    int _currentWaveIndex = -1;
    public int CurrentWaveIndex => _currentWaveIndex;
    public int CurrentWave => _currentWaveIndex + 1;

    public int MaxWave => _waveDataList.Count;

    [SerializeField] IntReactiveProperty _nextWaveCountdown = new IntReactiveProperty();
    public IReadOnlyReactiveProperty<int> NextWaveCountdown => _nextWaveCountdown;

    bool isGameClear = false;

    void Start()
    {
        NextWaveCountdown.SkipLatestValueOnSubscribe().Subscribe(_ => Debug.Log(_));
        NextWave();
    }

    async void NextWave()
    {
        _nextWaveCountdown.Value = 3;
        await UniTask.Delay(1000);
        _nextWaveCountdown.Value = 2;
        await UniTask.Delay(1000);
        _nextWaveCountdown.Value = 1;
        await UniTask.Delay(1000);
        _nextWaveCountdown.Value = 0;
        _currentWaveIndex++;
        EnemySpawn();
    }

    void EnemySpawn()
    {
        _currentEnemyList.Clear();
        _currentEnemyObservableList.Clear();
        _waveDataList[_currentWaveIndex].SpwanList.ForEach(_ => {
            var obj = Instantiate(_.Enemy, _.Position, Quaternion.identity);
            _currentEnemyList.Add(obj);
            _currentEnemyObservableList.Add(obj.IsDead.SkipLatestValueOnSubscribe());
        });
        _currentEnemyObservableList.Zip<bool>().Subscribe(_ => { 
            if (CurrentWave < MaxWave) {
                NextWave();
            } else {
                isGameClear = true;
            } 
        });
    }
}

[Serializable]
public struct WaveData
{
    public float Timelimit;
    public List<SpwanData> SpwanList;
}

[Serializable]
public struct SpwanData
{
    public BaseEnemy Enemy;
    public Vector3 Position;
}
