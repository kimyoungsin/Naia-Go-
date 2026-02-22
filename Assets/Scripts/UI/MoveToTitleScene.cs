using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Resources;
using UnityEngine.SceneManagement;

public class MoveToTitleScene : MonoBehaviour, IPointerDownHandler
{

    public void OnPointerDown(PointerEventData eventData)
    {
        Time.timeScale = 1;
        GameManager.Instance.MoveToTitle();

    }
}
