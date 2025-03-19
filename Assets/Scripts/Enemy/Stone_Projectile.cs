using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone_Projectile : MonoBehaviour
{
    public Transform PlayerTarget;
    public float Power;
    void Start()
    {
        PlayerTarget = FindObjectOfType<Player>().transform;
        Vector3 dir = PlayerTarget.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        GetComponent<Rigidbody>().AddForce(transform.right * Power, ForceMode.Impulse);

        Invoke("Destroy", 1.5f);
    }


    public void OnCollisionEnter(Collision collision)
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
            Debug.Log("Ãæµ¹·®: " + Power);
            Destroy(gameObject);
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
