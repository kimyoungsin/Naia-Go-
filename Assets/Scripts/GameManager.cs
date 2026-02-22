using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;


    private void Awake()
    {
        Application.targetFrameRate = 60; //최초 실행 시 60프레임 고정

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (Instance != this)
            {
                Destroy(this.gameObject);
            }

        }

    }

    public Object TitleScene;
    public UI_Canvas UI_Canvas_Object;
    public CameraManager cameraManager;

    public void MoveToTitle()
    {
        UI_Canvas_Object = FindObjectOfType<UI_Canvas>();
        cameraManager = FindObjectOfType<CameraManager>();
        if (UI_Canvas_Object != null)
        {
            Destroy(UI_Canvas_Object.gameObject);
        }
        if(cameraManager != null)
        {
            Destroy(cameraManager.gameObject);
        }
  

        if (TitleScene != null)
        {
            SceneManager.LoadScene(TitleScene.name);
        }
    }
}
