using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour, IPointerDownHandler
{
    public Sprite Pause;
    public Sprite Play;
    public Image sprite;
    public bool isPause = false;
    public GameObject PuaseMenu;
    public GameObject ClearUI;

    public void Awake()
    {
        sprite = GetComponent<Image>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(isPause)
        {
            sprite.sprite = Pause;
            isPause = false;
            PuaseMenu.SetActive(false);
            Time.timeScale = 1;

        }
        else
        {
           
            sprite.sprite = Play;
            isPause = true;
            PuaseMenu.SetActive(true);
            Time.timeScale = 0;
        }

    }

    public void ReStart()
    {
        sprite.sprite = Pause;
        isPause = false;
        PuaseMenu.SetActive(false);
        ClearUI.SetActive(false);
        Time.timeScale = 1;
    }
}
