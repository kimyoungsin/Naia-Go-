using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData itemdata;

    void Update()
    {
        transform.Rotate(0.0f, 90.0f * Time.deltaTime, 0.0F);

    }

}
