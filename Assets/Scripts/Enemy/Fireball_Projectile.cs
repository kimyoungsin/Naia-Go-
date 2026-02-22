using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball_Projectile : MonoBehaviour
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

    public void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
