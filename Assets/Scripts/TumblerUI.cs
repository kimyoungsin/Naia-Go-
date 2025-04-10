using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Resources;

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

    [Header("�Һ� ����, UI��")]
    public float TumblerGauge = 100;
    public float TumblerTumblerGaugeRecoverValue = 5;
    public Image TumblerGaugeImage;
    public GameObject TumblerDizzyImage;

    

    [Header("�Һ� ����(���ٱ� ���� �� �ӵ�)")]
    public Image TumblerPowerGaugeImage;
    public float WaterShootPower = 1;
    public int WaterShootPowerMax = 10;
    public float WaterRecoverTime = 1.25f;
    public float WaterRecoverValue = 2;
    public bool WaterRecover = false;
    public Player player;


    void Start()
    {
        player = FindObjectOfType<Player>();
    }


    void Update()
    {
        //���� ������ ǥ��
        if (TumblerGauge <= 0)
        {
            TumblerGaugeImage.fillAmount = Mathf.Lerp(TumblerGaugeImage.fillAmount, 0, Time.deltaTime * 10f);
        }
        else if (TumblerGaugeImage.fillAmount != (float)TumblerGauge / 100)
        {
            TumblerGaugeImage.fillAmount = Mathf.Lerp(TumblerGaugeImage.fillAmount, (float)TumblerGauge / 100, Time.deltaTime * 8f);
        }
        //���� ������ ǥ��
        if (WaterShootPower <= WaterShootPowerMax)
        {
            TumblerPowerGaugeImage.fillAmount = Mathf.Lerp(TumblerPowerGaugeImage.fillAmount, 0, Time.deltaTime * 10f);
        }
        else if (TumblerPowerGaugeImage.fillAmount != (float)WaterShootPower / 100)
        {
            TumblerPowerGaugeImage.fillAmount = Mathf.Lerp(TumblerPowerGaugeImage.fillAmount, (float)WaterShootPower / 100, Time.deltaTime * 8f);
        }
    }

  
}
