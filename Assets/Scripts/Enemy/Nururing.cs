using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Nururing : MonoBehaviour
{

    public Rigidbody rigid;
    public float Power;

    [Header("랜덤 방향 이동")]
    public int RandomNum;
    public bool LeftMove;
    public bool RightMove;
    public float MovementSpeed;
    public Animator Anim;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        RandomNum = Random.Range(1, 9); //1~8 사이(정착지에서 개척자 랜덤 행동 난수)
        StartCoroutine(Nururing_Action());
    }


    void Update()
    {
        if (LeftMove)
        {
            transform.Translate(new Vector2(-1, 0) * MovementSpeed * Time.deltaTime);

        }
        else if (RightMove)
        {
            transform.Translate(new Vector2(1, 0) * MovementSpeed * Time.deltaTime);

        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(collision.gameObject.transform.position.x < transform.position.x)
            {
                collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(-1, 1, 0) * Power, ForceMode.Impulse);
    
            }
            else
            {
                collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(1, 1, 0) * Power, ForceMode.Impulse);
            }
            collision.gameObject.GetComponent<Player>().EnemyHit();
            Debug.Log("충돌량: " + Power);
        }
    }

    public IEnumerator Nururing_Action()
    {
        yield return new WaitForSeconds(2.5f);
        RandomNum = Random.Range(1, 4); //1~3 사이
                                        // 1: 좌로 이동, 2: 우로 이동, 3: 이동 멈춤
        if (RandomNum == 1)
        {
            LeftMove = true;
            RightMove = false;
            transform.localScale = new Vector3(-1, 1, 1);
            //Anim.SetBool("isWalk", true);
        }
        else if (RandomNum == 2)
        {
            LeftMove = false;
            RightMove = true;
            transform.localScale = new Vector3(1, 1, 1);
            //Anim.SetBool("isWalk", true);
        }
        else if (RandomNum == 3)
        {

            LeftMove = false;
            RightMove = false;
            //Anim.SetBool("isWalk", false);
        }
     
        StartCoroutine(Nururing_Action());
    }
}


