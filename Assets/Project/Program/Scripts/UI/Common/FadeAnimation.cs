using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UniRx;
using UnityEngine.Events;

public class FadeAnimation : MonoBehaviour
{
    CanvasGroup _canvasGroup;

    [SerializeField] float _animetionSpeed = 5;

    private CancellationTokenSource _cancellationTokenSource;

    [SerializeField] UnityEvent _fadeInEndEvent = new UnityEvent();
    [SerializeField] UnityEvent _fadeOutEndEvent = new UnityEvent();

    void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void FadeIn()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        FadeInAnimetionTask(_cancellationTokenSource.Token).Forget(e => { });
    }

    public void FadeOut()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        FadeOutAnimetionTask(_cancellationTokenSource.Token).Forget(e => { });
    }

    async UniTask FadeInAnimetionTask(CancellationToken token)
    {
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

    async UniTask FadeOutAnimetionTask(CancellationToken token)
    {
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
}
