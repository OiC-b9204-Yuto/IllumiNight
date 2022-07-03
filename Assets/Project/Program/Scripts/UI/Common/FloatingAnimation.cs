using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FloatingAnimation : MonoBehaviour
{
    RectTransform _rectTransform;
    Vector3 _startPosition;

    [SerializeField] AnimationCurve _animationCurve;

    [SerializeField] float _animetionSpeed = 1;

    [SerializeField] bool _isLoop = true;

    private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

    float _timer = 0;

    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _startPosition = _rectTransform.localPosition;
    }

    void Start()
    {
        AnimetionStart();
    }

    public void AnimetionStart()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        AnimetionTask(_cancellationTokenSource.Token).Forget(e => { });
    }

    public void AnimetionStop()
    {
        _cancellationTokenSource.Cancel();
    }

    async UniTask AnimetionTask(CancellationToken token)
    {
        while (_isLoop)
        {
            _timer += Time.deltaTime * _animetionSpeed;
            if(_timer > _animationCurve.keys[_animationCurve.length - 1].time * 2)
            {
                _timer -= _animationCurve.keys[_animationCurve.length - 1].time * 2;
            }
            _rectTransform.localPosition = _startPosition + new Vector3(0, _animationCurve.Evaluate(_timer), 0);
            await UniTask.DelayFrame(1, cancellationToken: token);
        }
    }

    public void OnDestroy()
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource?.Dispose();
    }
}
