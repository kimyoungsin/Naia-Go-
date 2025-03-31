using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Resources;


public class UI_Canvas : MonoBehaviour
{
    public static UI_Canvas Instance = null;


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

    public void TimerStart()
    {
        isTimerStart = true;
    }

    public void TimerStop()
    {
        isTimerStart = false;
    }

}
