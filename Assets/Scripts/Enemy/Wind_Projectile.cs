using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind_Projectile : MonoBehaviour
{
    public float WindPower;
    public Rigidbody Rigid;
    void Start()
    {
        Rigid = GetComponent<Rigidbody>();
    }

    /*
    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
            collision.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, Rigid.velocity.z);
 
            collision.gameObject.GetComponent<Player>().WindHit(WindPower);
            Debug.Log("바람 세기: " + WindPower);
        }
    }
    */

    /*
    public void OnTriggerEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.transform.position.x < transform.position.x)
            {
                collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(-1, 1, 0), ForceMode.Impulse);

            }
            else
            {
                collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(1, 1, 0), ForceMode.Impulse);
            }
            collision.gameObject.GetComponent<Player>().EnemyHit();
            Debug.Log("바람 세기: " + WindPower);
            Destroy(gameObject);
        }
    }

    */
    public void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
