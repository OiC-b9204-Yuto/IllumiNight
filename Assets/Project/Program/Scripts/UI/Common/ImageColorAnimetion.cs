using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class ImageColorAnimetion : MonoBehaviour
{
    Image _image;
    Vector3 _startPosition;

    Color _startColor;
    [SerializeField] Color _targetColor;
    [SerializeField] AnimationCurve _animationCurve;

    [SerializeField] float _animetionSpeed = 1;

    [SerializeField] bool _isLoop = true;

    private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

    float _timer = 0;

    void Awake()
    {
        _image = GetComponent<Image>();
        _startColor = _image.color;
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
            if (_timer > _animationCurve.keys[_animationCurve.length - 1].time * 2)
            {
                _timer -= _animationCurve.keys[_animationCurve.length - 1].time * 2;
            }
            var c = Color.Lerp(_startColor, _targetColor, _animationCurve.Evaluate(_timer));
            c.a = _image.color.a;
            _image.color = c;
            await UniTask.DelayFrame(1, cancellationToken: token);
        }
    }

    public void OnDestroy()
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource?.Dispose();
    }
}
