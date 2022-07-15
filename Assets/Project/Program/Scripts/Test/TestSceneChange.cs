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
    /// シーン読み込み中フラグ
    /// </summary>
    public bool IsLoadScene { get { return isLoadScene; } }

    private AsyncOperation _nextScene;
    
    bool _isLoadingComplete = false;
    public bool IsLoadingComplete => _isLoadingComplete;

    bool _isNextScene = false;
    public bool IsNextScene => _isNextScene;

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
    /// シーンを読み込み遷移する関数
    /// </summary>
    /// <param name="name">シーン名</param>
    public void LoadSceneStart(string name, bool loadingScene = false)
    {
        if (isLoadScene)
        {
            return;
        }
        isLoadScene = true;
        EventSystem eventSystem = (EventSystem)FindObjectOfType(typeof(EventSystem));
        if (eventSystem) eventSystem.enabled = false;
        StartCoroutine(LoadScene(name, loadingScene));
    }

    public void NextSceneStart()
    {
        if (!_isNextScene && _isLoadingComplete)
        {
            _isNextScene = true;
            StartCoroutine(NextScene());
        }
    }

    /// <summary>
    /// シーンを読み込み遷移する関数
    /// </summary>
    /// <param name="index">シーン番号</param>
    public void LoadSceneStart(int index, bool loadingScene = false)
    {
        if (isLoadScene)
        {
            return;
        }
        isLoadScene = true;
        EventSystem eventSystem = (EventSystem)FindObjectOfType(typeof(EventSystem));
        if (eventSystem) eventSystem.enabled = false;
        StartCoroutine(LoadScene(index, loadingScene));
    }

    private IEnumerator LoadScene(string name, bool loadingScene)
    {
        if (loadingScene)
        {
            yield return LoadScene("LoadingScene", false);

        }
        if (!loadingScene)
        {
            yield return FadeOut();
        }
        isLoadScene = true;
        _nextScene = SceneManager.LoadSceneAsync(name);
        _nextScene.allowSceneActivation = false;
        while (_nextScene.progress < 0.9f)
        {
            yield return null;
        }
        _isLoadingComplete = true;

        if (!loadingScene)
        {
            yield return NextScene();
        }
    }

    private IEnumerator LoadScene(int index, bool loadingScene)
    {
        if (loadingScene)
        {
            yield return LoadScene("LoadingScene", false);
        }
        if (!loadingScene)
        {
            yield return FadeOut();
        }
        isLoadScene = true;
        _nextScene = SceneManager.LoadSceneAsync(name);
        _nextScene.allowSceneActivation = false;
        while (_nextScene.progress < 0.9f)
        {
            yield return null;
        }
        _isLoadingComplete = true;
        if (!loadingScene)
        {
            yield return new WaitForSeconds(0.2f);
            yield return NextScene();
        }
    }

    private IEnumerator NextScene()
    {
        yield return FadeOut();
        _nextScene.allowSceneActivation = true;
        _isLoadingComplete = false;
        yield return FadeIn();
        _isNextScene = false;
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
    /// ゲームを終了する関数
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
