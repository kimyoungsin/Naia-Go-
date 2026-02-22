using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class WindSpirit : MonoBehaviour
{
    public Rigidbody rigid;
    public float Power;
    public GameObject WindPrefab;
    public float AttackSpeed;
    public float AttackPower;
    //public bool isAttack;

    [Header("랜덤 방향 이동")]
    public int RandomNum;
    public bool LeftMove;
    public bool RightMove;
    public bool UpMove;
    public bool DownMove;
    public float MovementSpeed;
    public Animator Anim;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        RandomNum = Random.Range(1, 9); //1~8 사이(정착지에서 개척자 랜덤 행동 난수)
        StartCoroutine(WindSpirit_Action());
        //StartCoroutine(WindSpirit_Attack());
    }




    void Update()
    {
        if (LeftMove)
        {
            if (LeftMove && UpMove)
            {
                transform.Translate(new Vector2(-1, -0.5f) * MovementSpeed * Time.deltaTime);
            }
            else if (LeftMove && DownMove)
            {
                transform.Translate(new Vector2(-1, 0.5f) * MovementSpeed * Time.deltaTime);
            }
            else
            {
                transform.Translate(new Vector2(-1, 0) * MovementSpeed * Time.deltaTime);
            }

        }
        else if (RightMove)
        {
            if (RightMove && UpMove)
            {
                transform.Translate(new Vector2(1, -0.5f) * MovementSpeed * Time.deltaTime);
            }
            else if (RightMove && DownMove)
            {
                transform.Translate(new Vector2(1, 0.5f) * MovementSpeed * Time.deltaTime);
            }
            else
            {
                transform.Translate(new Vector2(1, 0) * MovementSpeed * Time.deltaTime);
            }

        }



    }

    public void OnDrawGizmos() //공격 사거리 체크용
    {
        Gizmos.color = Color.green;

    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.transform.position.x < transform.position.x)
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

    public IEnumerator WindSpirit_Action()
    {
        yield return new WaitForSeconds(2f);

        RandomNum = Random.Range(1, 8); //1~7 사이
                                        // 1: 좌로 이동, 2: 우로 이동, 3: 좌상 이동, 4: 우상 이동, 5: 좌하 이동, 6: 우하 이동, 7: 이동 멈춤
                                        
        if (RandomNum == 1)
        {
            LeftMove = true;
            RightMove = false;
            UpMove = false;
            DownMove = false;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (RandomNum == 2)
        {
            LeftMove = false;
            RightMove = true;
            UpMove = false;
            DownMove = false;
            transform.localScale = new Vector3(1, 1, 1);
     
        }
        else if (RandomNum == 3)
        {

            LeftMove = true;
            RightMove = false;
            UpMove = true;
            DownMove = false;
            transform.localScale = new Vector3(-1, 1, 1);
     
        }
        else if (RandomNum == 4)
        {
            LeftMove = false;
            RightMove = true;
            UpMove = true;
            DownMove = false;
            transform.localScale = new Vector3(1, 1, 1);
     
        }
        else if (RandomNum == 5)
        {
            LeftMove = true;
            RightMove = false;
            UpMove = false;
            DownMove = true;
            transform.localScale = new Vector3(-1, 1, 1);
         
        }
        else if (RandomNum == 6)
        {
            LeftMove = false;
            RightMove = true;
            UpMove = false;
            DownMove = true;
            transform.localScale = new Vector3(1, 1, 1);
        
        }
        else if (RandomNum == 3)
        {
            LeftMove = false;
            RightMove = false;
        
        }
        else if (RandomNum == 7)
        {
            LeftMove = false;
            RightMove = false;
            UpMove = false;
            DownMove = false;
       
        }
        else if (RandomNum == 8)
        {
            LeftMove = false;
            RightMove = false;
            UpMove = false;
            DownMove = false;
          
        }

        StartCoroutine(WindSpirit_Action());

    }

    public IEnumerator WindSpirit_Attack()
    {

        Debug.Log("바람일으키기!");
        //좌
        GameObject windLeft = Instantiate(WindPrefab, new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z), Quaternion.identity);
        windLeft.GetComponent<Wind_Projectile>().WindPower = AttackPower;
        windLeft.GetComponent<Wind_Projectile>().Rigid.AddForce(-transform.right * AttackPower, ForceMode.Impulse);
        //우
        GameObject windRight = Instantiate(WindPrefab, new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z), Quaternion.identity);
        windRight.GetComponent<Wind_Projectile>().WindPower = AttackPower;
        windRight.GetComponent<Wind_Projectile>().Rigid.AddForce(transform.right * AttackPower, ForceMode.Impulse);
        //상
        GameObject windUp = Instantiate(WindPrefab, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
        windUp.GetComponent<Wind_Projectile>().WindPower = AttackPower;
        windUp.GetComponent<Wind_Projectile>().Rigid.AddForce(transform.up * AttackPower, ForceMode.Impulse);
        //하
        GameObject windDown = Instantiate(WindPrefab, new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z), Quaternion.identity);
        windDown.GetComponent<Wind_Projectile>().WindPower = AttackPower;
        windDown.GetComponent<Wind_Projectile>().Rigid.AddForce(-transform.up * AttackPower, ForceMode.Impulse);
        //isAttack = true;
        yield return new WaitForSeconds(AttackSpeed);
        StartCoroutine(WindSpirit_Attack());
    }

    public void OnBecameVisible()
    {
        StartCoroutine(WindSpirit_Action());
        StartCoroutine(WindSpirit_Attack());
    }

    public void OnBecameInvisible()
    {
        StopAllCoroutines();
    }

}
