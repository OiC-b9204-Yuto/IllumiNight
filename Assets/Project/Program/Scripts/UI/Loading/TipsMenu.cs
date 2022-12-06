using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;

public class TipsMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text _titleText;
    [SerializeField] private TMP_Text _descriptionText;

    [SerializeField] private List<TipsData> _tipsList;

    [SerializeField] private FadeAnimation _fadeAnimation;

    private int _tipsIndex = 0;

    private CancellationTokenSource _cancellationTokenSource;

    private bool _skippableFlag = false;

    void Awake()
    {
        _tipsIndex = UnityEngine.Random.Range(0, _tipsList.Count);
        _titleText.text = _tipsList[_tipsIndex].Title;
        _descriptionText.text = _tipsList[_tipsIndex].Description;
    }

    void Start()
    {
        UpdateLoop(this.GetCancellationTokenOnDestroy()).Forget();
    }

    async UniTaskVoid UpdateLoop(CancellationToken token)
    {
        while (true)
        {
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = new CancellationTokenSource();
            _skippableFlag = true;
            try
            {
                await WaitIsNextTips(_cancellationTokenSource.Token);
            }
            catch (OperationCanceledException e)
            {
                // キャンセル時に呼ばれる例外  
                Debug.Log("Taskキャンセル時の処理");
            }
            _skippableFlag = false;
            await _fadeAnimation.AnimationStart(token);
            _tipsIndex++;
            if (_tipsIndex >= _tipsList.Count)
            {
                _tipsIndex -= _tipsList.Count;
            }
            _titleText.text = _tipsList[_tipsIndex].Title;
            _descriptionText.text = _tipsList[_tipsIndex].Description;
            await _fadeAnimation.AnimationStart(token);
        }
    }

    private async UniTask WaitIsNextTips(CancellationToken token)
    {
        await UniTask.Delay(10000, true, PlayerLoopTiming.Update ,token);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _skippableFlag)
        {
            Debug.Log("Space");
            _cancellationTokenSource.Cancel();
        }
    }

    void OnDestroy()
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource?.Dispose();
    }
}