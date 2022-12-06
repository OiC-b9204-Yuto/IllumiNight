using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UniRx;
using UnityEngine.Events;
using IllumiNight.Interface;

public class FadeAnimation : MonoBehaviour , IAnimation
{
    CanvasGroup _canvasGroup;

    [SerializeField] float _animetionSpeed = 5;

    private CancellationTokenSource _cancellationTokenSource;

    [SerializeField] UnityEvent _fadeInEndEvent = new UnityEvent();
    [SerializeField] UnityEvent _fadeOutEndEvent = new UnityEvent();

    int _animationSkipAlpha;

    void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void FadeIn()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        FadeInAnimationTask(_cancellationTokenSource.Token).Forget(e => { });
    }

    public void FadeOut()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        FadeOutAnimationTask(_cancellationTokenSource.Token).Forget(e => { });
    }

    async UniTask FadeInAnimationTask(CancellationToken token)
    {
        _animationSkipAlpha = 1;
        _canvasGroup.blocksRaycasts = true;
        while (_canvasGroup.alpha < 1)
        {
            _canvasGroup.alpha += _animetionSpeed * Time.deltaTime;
            await UniTask.DelayFrame(1, cancellationToken: token);
        }
        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = true;
        _fadeInEndEvent?.Invoke();
    }

    async UniTask FadeOutAnimationTask(CancellationToken token)
    {
        _animationSkipAlpha = 0;
        _canvasGroup.interactable = false;
        while (_canvasGroup.alpha > 0)
        {
            _canvasGroup.alpha -= _animetionSpeed * Time.deltaTime;
            await UniTask.DelayFrame(1, cancellationToken: token);
        }
        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;
        _fadeOutEndEvent?.Invoke();
    }

    public async UniTask AnimationStart(CancellationToken token)
    {
        if (_canvasGroup)
        {
            if (_canvasGroup.alpha == 1)
            {
                _cancellationTokenSource = new CancellationTokenSource();
                await FadeOutAnimationTask(_cancellationTokenSource.Token);
            }
            else if (_canvasGroup.alpha == 0)
            {
                _cancellationTokenSource = new CancellationTokenSource();
                await FadeInAnimationTask(_cancellationTokenSource.Token);
            }
        }
    }

    public void AnimationSkip()
    {
        _cancellationTokenSource.Cancel();
        if (_animationSkipAlpha == 1)
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _fadeInEndEvent?.Invoke();
        }
        else
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.blocksRaycasts = false;
            _fadeOutEndEvent?.Invoke();
        }
    }

    void OnDestroy()
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource?.Dispose();
    }
}
