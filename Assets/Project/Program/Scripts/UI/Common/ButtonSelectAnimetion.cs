using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;

public class ButtonSelectAnimetion : MonoBehaviour
{
    [SerializeField] Image _selectedIcon;
    [SerializeField] Image _underline;

    [SerializeField] Button button;

    [SerializeField] float _animetionSpeed = 10;

    private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

    public void SelectAnimetion()
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();
        SelectAnimetionTask(_cancellationTokenSource.Token).Forget(e => { });
    }

    public void DeselectAnimetion()
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();
        DeselectAnimetionTask(_cancellationTokenSource.Token).Forget(e => { });
    }

    async UniTask SelectAnimetionTask(CancellationToken token)
    {
        while (_selectedIcon.color.a < 1.0f && _underline.fillAmount < 1.0f)
        {
            _selectedIcon.color = new Color(_selectedIcon.color.r, _selectedIcon.color.g, _selectedIcon.color.b, _selectedIcon.color.a + _animetionSpeed * Time.deltaTime);
            _underline.fillAmount = _underline.fillAmount + _animetionSpeed * Time.deltaTime;
            await UniTask.DelayFrame(1, cancellationToken: token);
        }
        _selectedIcon.color = new Color(_selectedIcon.color.r, _selectedIcon.color.g, _selectedIcon.color.b, 1);
        _underline.fillAmount = 1;
    }
    async UniTask DeselectAnimetionTask(CancellationToken token)
    {
        while (_selectedIcon.color.a > 0 && _underline.fillAmount > 0)
        {
            _selectedIcon.color = new Color(_selectedIcon.color.r, _selectedIcon.color.g, _selectedIcon.color.b, _selectedIcon.color.a - _animetionSpeed * Time.deltaTime);
            _underline.fillAmount = _underline.fillAmount - _animetionSpeed * Time.deltaTime;
            await UniTask.DelayFrame(1, cancellationToken: token);
        }
        _selectedIcon.color = new Color(_selectedIcon.color.r, _selectedIcon.color.g, _selectedIcon.color.b, 0);
        _underline.fillAmount = 0;
    }

    private void OnDestroy()
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource?.Dispose();
    }
}
