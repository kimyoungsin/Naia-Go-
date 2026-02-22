using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class JoyStick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public RectTransform Lever;
    public RectTransform RectTransform;
    [SerializeField, Range(10f, 180f)]
    public float LeverMaxRange;

    public Vector2 InputVector;
    public bool isControl;
    public Player player;

    public void Start()
    {
        RectTransform = GetComponent<RectTransform>();
        player = FindObjectOfType<Player>();
    }

    public void Update()
    {
        if (isControl)
        {
            InputControlVector();
        }
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

    public void OnBeginDrag(PointerEventData eventData)
    {
        var inputDir = eventData.position - RectTransform.anchoredPosition;
        var clampedDir = inputDir.magnitude < LeverMaxRange ? inputDir : inputDir.normalized * LeverMaxRange;
        Lever.anchoredPosition = clampedDir;

        ControlStickLever(eventData);
        isControl = true;
    }
    public void OnDrag(PointerEventData eventData)
    {
        var inputDir = eventData.position - RectTransform.anchoredPosition;
        var clampedDir = inputDir.magnitude < LeverMaxRange ? inputDir : inputDir.normalized * LeverMaxRange;
        Lever.anchoredPosition = clampedDir;

        ControlStickLever(eventData);
        isControl = true;
    }

    public void ControlStickLever(PointerEventData eventData)
    {
        var inputDir = eventData.position - RectTransform.anchoredPosition;
        var clampedDir = inputDir.magnitude < LeverMaxRange ? inputDir : inputDir.normalized * LeverMaxRange;
        Lever.anchoredPosition = clampedDir;
        InputVector = clampedDir / LeverMaxRange;
    }
    
    public void InputControlVector()
    {
        player.JoyStickWaterShoot(InputVector.x, InputVector.y);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (player.WaterEffect != null)
        {
            player.WaterEffect.GetComponent<ParticleSystem>().Stop();
        }

        StartCoroutine(player.WaterRecoverStart());
        player.Ani.SetBool("isWaterShoot", false);
        
        Lever.anchoredPosition = Vector2.zero;
        isControl = false;
    }
}
