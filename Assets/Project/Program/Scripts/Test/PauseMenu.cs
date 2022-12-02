using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] AudioSource gameBGM;

    float defaultVolume;

    private static bool pause;
    public static bool IsPause { get { return pause; } }

    private void Awake()
    {
        defaultVolume = gameBGM.volume;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            pause = !pause;
            if (pause)
            {
                Cursor.visible = true;
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
                gameBGM.volume = defaultVolume * 0.5f;
            }
            else
            {
                Cursor.visible = false;
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
                gameBGM.volume = defaultVolume;
            }
        }

    }


}
