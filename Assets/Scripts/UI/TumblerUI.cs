using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Resources;
using UnityEngine.SceneManagement;

public class TumblerUI : MonoBehaviour
{

    public static TumblerUI Instance = null;


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

    [Header("텀블러 수분, UI들")]
    public float TumblerGauge = 100;
    public float TumblerTumblerGaugeRecoverValue = 5;
    public Image TumblerGaugeImage;
    public GameObject TumblerDizzyImage;

    

    [Header("텀블러 수압(물줄기 강도 및 속도)")]
    public Image TumblerPowerGaugeImage;
    public float WaterShootPower = 0;
    public int WaterShootPowerMax = 10;
    public float WaterRecoverTime = 0.25f;
    public float WaterRecoverValue = 2.5f;
    public bool WaterRecover = false;
    public Player player;


    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    public void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ReStart();
    }

    public void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    void Update()
    {
        
        //수분 게이지 표시
        if (TumblerGauge <= 0)//텀블러 게이지가 0일 경우
        {
            TumblerGaugeImage.fillAmount = Mathf.Lerp(TumblerGaugeImage.fillAmount, 0, Time.deltaTime * 10f);
        }
        else if (TumblerGaugeImage.fillAmount != (float)TumblerGauge / 100)
        {
            TumblerGaugeImage.fillAmount = Mathf.Lerp(TumblerGaugeImage.fillAmount, (float)TumblerGauge / 100, Time.deltaTime * 8f);
        }
        //수압 게이지 표시
        if (WaterShootPower >= WaterShootPowerMax) //풀스택일 경우
        {
            TumblerPowerGaugeImage.fillAmount = Mathf.Lerp(TumblerPowerGaugeImage.fillAmount, WaterShootPowerMax, Time.deltaTime * 10f);
        }
        else if (TumblerPowerGaugeImage.fillAmount != (float)WaterShootPower / 10)
        {
            TumblerPowerGaugeImage.fillAmount = Mathf.Lerp(TumblerPowerGaugeImage.fillAmount, (float)WaterShootPower / 10, Time.deltaTime * 8f);
        }
    }

    public void ReStart()
    {
        player = FindObjectOfType<Player>();
        WaterShootPower = 0;
        TumblerGauge = 100;
    }

  
}
