using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingCompleteButton : MonoBehaviour
{
    [SerializeField] Image _nowLoadingImage;
    [SerializeField] Image _lodingCompleteImage;

    // Update is called once per frame
    void Update()
    {
        if (TestSceneChange.Instance.IsLoadingComplete)
        {
            if (_nowLoadingImage.gameObject.activeSelf)
            {
                _nowLoadingImage.gameObject.SetActive(false);
                _lodingCompleteImage.gameObject.SetActive(true);

            }

            if (Input.GetMouseButtonUp(0))
            {
                TestSceneChange.Instance.NextSceneStart();
            }
        }
    }
}
