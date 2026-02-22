using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SpinButton : MonoBehaviour, IPointerDownHandler
{

    public bool isControl;
    public Player player;

    public void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    public void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player = FindObjectOfType<Player>();
    }

    public void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!player.isSpin && TumblerUI.Instance.TumblerGauge > (TumblerUI.Instance.TumblerGauge / 10))
        {
            StartCoroutine(player.Spin()); 
        }
    
    }


}
