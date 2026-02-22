using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Resources;
using UnityEngine.SceneManagement;

public class SceneLoadButton : MonoBehaviour, IPointerDownHandler
{
    public string SceneName;
    public void OnPointerDown(PointerEventData eventData)
    {
        if(SceneName != null)
        {
            SceneManager.LoadScene(SceneName);
        }

    }

}
