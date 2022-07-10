using Cysharp.Threading.Tasks;
using IllumiNight;
using IllumiNight.Interface;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class DefeatedEnemyView : MonoBehaviour, IAnimation
{
    [SerializeField] TMP_Text _text;

    [SerializeField] AnimationCurve _animationCurve;

    [SerializeField] float _animetionSpeed = 3;

    int _score;

    public bool IsAnimationEnd { get; protected set; }

    private CancellationTokenSource _cancellationTokenSource;

    void Awake()
    {
        IsAnimationEnd = false;
        _text.alpha = 0;
    }

    public void SetValue(int num)
    {
        _score = num;
        _text.text = _score.ToString();
    }

    public async UniTask AnimationStart()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        await AnimetionTask(_cancellationTokenSource.Token);
    }

    public void AnimationSkip()
    {
        _cancellationTokenSource.Cancel();
        _text.transform.localScale = new Vector3(1, 1, 1);
    }

    async UniTask AnimetionTask(CancellationToken token)
    {
        float _timer = 0;
        while (_timer < 1)
        {
            _timer += Time.deltaTime * _animetionSpeed;
            _text.alpha = Mathf.Clamp01(_timer * 2);
            _text.transform.localScale = new Vector3(_animationCurve.Evaluate(_timer), _animationCurve.Evaluate(_timer), 1.0f);
            await UniTask.DelayFrame(1, cancellationToken: token);
        }
        _text.transform.localScale = new Vector3(1, 1, 1);
        IsAnimationEnd = true;
    }

    void OnDestroy()
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource?.Dispose();
    }
}

