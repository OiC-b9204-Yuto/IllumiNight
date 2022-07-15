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

    [SerializeField] BoolReactiveProperty _isGameClear = new BoolReactiveProperty();

    public IReadOnlyReactiveProperty<bool> IsGameClear => _isGameClear;

    [SerializeField] ScoreData _scoreData;

    PlayerState _playerState;

    void Start()
    {
        _playerState = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerState>();
        NextWaveCountdown.SkipLatestValueOnSubscribe().Subscribe(_ => Debug.Log(_));
        NextWave();
        IsGameClear.SkipLatestValueOnSubscribe().Subscribe(async _ =>
        {
            //Œ‚”j”•Û‘¶(Œ»ÝŒÅ’è7‚Ì‚½‚ß’è”‚Å)
            _scoreData.DefeatedEnemy = 7;
            _scoreData.ReceivedDamage = _playerState.MaxHealth - _playerState.Health;
            await UniTask.Delay(2000);
            TestSceneChange.Instance.LoadSceneStart("ResultScene");
        });
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
                _isGameClear.Value = true;
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
