using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Resources;
using UnityEngine.SceneManagement;

public class UI_Canvas : MonoBehaviour
{
    public static UI_Canvas Instance = null;
    public GameObject ClaerUI;
    public GameObject PuaseButton;

    private void Awake()
    {
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

    [Header("타이머 관련")]
    public float Timer_Time = 0;
    public bool isTimerStart= false;
    public TMP_Text TimerText;

    public void Start()
    {
        TimerStart();
    }

    public void Update()
    {
        if(isTimerStart)
        {
            Timer_Time += Time.deltaTime;
            TimerText.text = "" + Timer_Time.ToString("F1");
         
        }
    }
    public void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        TimerStart();
    }

    public void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void TimerStart()
    {
        Timer_Time = 0;
        isTimerStart = true;
    }

    public void TimerStop()
    {
        isTimerStart = false;
    }

    public void ClaerUI_On()
    {

        isTimerStart = false;
        ClaerUI.SetActive(true);
        PuaseButton.SetActive(false);
        Time.timeScale = 0;
    }


}
