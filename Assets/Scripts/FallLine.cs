using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallLine : MonoBehaviour
{
    public Transform RespawnPos;
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Transform player = collision.gameObject.GetComponent<Transform>();
            player.transform.position = RespawnPos.position;
        }
    }
}
