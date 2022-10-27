using IllumiNight.Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitlePresenter : MonoBehaviour
{
    [SerializeField] GameObject _clickToScreenView;
    [SerializeField] GameObject _titleMenuView;
    [SerializeField] GameObject _optionView;
    [SerializeField] Button _startButton;
    [SerializeField] Button _optionButton;

    async void Start()
    {
        _clickToScreenView.GetComponent<Button>().onClick.AddListener(
            async () => 
            {
                await _clickToScreenView.GetComponent<IAnimation>().AnimationStart();
                await _titleMenuView.GetComponent<IAnimation>().AnimationStart();
            });
        _startButton.onClick.AddListener(
            async () => 
            {
                await _titleMenuView.GetComponent<IAnimation>().AnimationStart();
                TestSceneChange.Instance.LoadSceneStart("GameScene", true);
            });
        _optionButton.onClick.AddListener(
            async () => 
            {
                await _titleMenuView.GetComponent<IAnimation>().AnimationStart();
                _optionView.GetComponent<OptionTest>().AudioReset();
                await _optionView.GetComponent<IAnimation>().AnimationStart();
            });
    }
}
