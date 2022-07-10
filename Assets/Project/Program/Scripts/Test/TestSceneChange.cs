using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestSceneChange : SingletonMonoBehaviour<TestSceneChange>
{ 
    [SerializeField] private Image fadeImage;
    private float fadeSpeed = 0.5f;
    private bool isLoadScene = true;
    /// <summary>
    /// �V�[���ǂݍ��ݒ��t���O
    /// </summary>
    public bool IsLoadScene { get { return isLoadScene; } }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this.gameObject);
        Color color = fadeImage.color;
        color.a = 1;
        fadeImage.color = color;
        isLoadScene = true;
    }

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    private void Update()
    {
        if (!IsLoadScene)
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                LoadSceneStart("TitleScene");
            }
            if (Input.GetKeyDown(KeyCode.F2))
            {
                LoadSceneStart("GameScene");
            }
            if (Input.GetKeyDown(KeyCode.F3))
            {
                LoadSceneStart("ResultScene");
            }
            if (Input.GetKeyDown(KeyCode.F4))
            {
                GameQuitStart();
            }
        }
    }

    /// <summary>
    /// �V�[����ǂݍ��ݑJ�ڂ���֐�
    /// </summary>
    /// <param name="name">�V�[����</param>
    public void LoadSceneStart(string name)
    {
        if (isLoadScene)
        {
            return;
        }
        isLoadScene = true;
        EventSystem eventSystem = (EventSystem)FindObjectOfType(typeof(EventSystem));
        if (eventSystem) eventSystem.enabled = false;
        StartCoroutine(LoadScene(name));
    }

    /// <summary>
    /// �V�[����ǂݍ��ݑJ�ڂ���֐�
    /// </summary>
    /// <param name="index">�V�[���ԍ�</param>
    public void LoadSceneStart(int index)
    {
        if (isLoadScene)
        {
            return;
        }
        isLoadScene = true;
        EventSystem eventSystem = (EventSystem)FindObjectOfType(typeof(EventSystem));
        if (eventSystem) eventSystem.enabled = false;
        StartCoroutine(LoadScene(index));
    }

    private IEnumerator LoadScene(string name)
    {
        var scene = SceneManager.LoadSceneAsync(name);
        scene.allowSceneActivation = false;
        yield return FadeOut();
        scene.allowSceneActivation = true;
        yield return FadeIn();
    }

    private IEnumerator LoadScene(int index)
    {
        var scene = SceneManager.LoadSceneAsync(name);
        scene.allowSceneActivation = false;
        yield return FadeOut();
        scene.allowSceneActivation = true;
        yield return FadeIn();
    }

    private IEnumerator FadeIn()
    {
        Color color = fadeImage.color;
        while (color.a >= 0)
        {
            color.a -= fadeSpeed * Time.unscaledDeltaTime;
            fadeImage.color = color;
            yield return null;
        }
        fadeImage.raycastTarget = false;
        isLoadScene = false;
    }

    private IEnumerator FadeOut()
    {
        Color color = fadeImage.color;
        while (color.a <= 1)
        {
            color.a += fadeSpeed * Time.unscaledDeltaTime;
            fadeImage.color = color;
            yield return null;
        }
        fadeImage.raycastTarget = true;
    }

    /// <summary>
    /// �Q�[�����I������֐�
    /// </summary>
    public void GameQuitStart()
    {
        if (isLoadScene)
        {
            return;
        }
        isLoadScene = true;
        StartCoroutine(GameQuit());
    }

    private IEnumerator GameQuit()
    {
        yield return FadeOut();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    }
}
