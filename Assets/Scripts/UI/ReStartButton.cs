using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ReStartButton : MonoBehaviour, IPointerDownHandler
{
    public PauseButton pauseButton;

    public void Awake()
    {
        pauseButton = FindObjectOfType<PauseButton>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Time.timeScale = 1;
        UI_Canvas.Instance.PuaseButton.SetActive(true);
        pauseButton.ReStart();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
