using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class ClickAnimation : MonoBehaviour
{
    [SerializeField] Button _button;
    [SerializeField] RectTransform _animationTarget;

    [SerializeField] AnimationCurve _animationCurve;

    [SerializeField] float _animetionSpeed = 3  ;

    bool _isAnimetion;
    bool _isEnd;

    private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

    private void Start()
    {   
        _button.onClick.AddListener(ClickAnimetion);
        if (_animationTarget == null)
        {
            _animationTarget = _button.GetComponent<RectTransform>();
        }
    }

    public void ClickAnimetion()
    {
        if (_isAnimetion)
        {
            return;
        }
        _isAnimetion = true;
        ClickAnimetionTask(_cancellationTokenSource.Token).Forget(e => { });
    }

    async UniTask ClickAnimetionTask(CancellationToken token)
    {
        float _timer = 0;
        while (_timer < 1) {
            _timer += Time.deltaTime * _animetionSpeed;
            _animationTarget.localScale = new Vector3(_animationCurve.Evaluate(_timer), _animationCurve.Evaluate(_timer), 1.0f);
            await UniTask.DelayFrame(1, cancellationToken: token);
        }
        _button.transform.localScale = new Vector3(1,1,1);
        _isAnimetion = false;
    }
}
