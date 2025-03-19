using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance = null;


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

    public Transform Player;

    public void Start()
    {
        Player = FindObjectOfType<Player>().GetComponent<Transform>();
    }

    public void Update()
    {
        transform.position = new Vector3(Player.position.x, Player.position.y + 4.15f, -10);
    }



}
