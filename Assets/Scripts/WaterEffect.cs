using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterEffect : MonoBehaviour
{
    public float DestroyTime;

    public void Start()
    {
        //Invoke("Destroy", DestroyTime);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
