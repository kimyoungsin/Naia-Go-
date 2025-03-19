using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class GrandSpirit : MonoBehaviour
{
    public Rigidbody rigid;
    public float Power;
    public GameObject StonePrefab;
    public float AttackRange;
    public float AttackSpeed;
    public float AttackPower;
    public bool isAttack;
    public Transform PlayerTarget;

    [Header("���� ���� �̵�")]
    public int RandomNum;
    public bool LeftMove;
    public bool RightMove;
    public float MovementSpeed;
    public Animator Anim;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        RandomNum = Random.Range(1, 9); //1~8 ����(���������� ��ô�� ���� �ൿ ����)
        PlayerTarget = FindObjectOfType<Player>().transform;
        StartCoroutine(GrandSpirit_Action());
        StartCoroutine(GrandSpirit_Attack());
    }

    
    void Update()
    {
        if(!isAttack)
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
    


    }

    public void OnDrawGizmos() //���� ��Ÿ� üũ��
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, AttackRange);

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
            Debug.Log("�浹��: " + Power);
        }
    }

    public IEnumerator GrandSpirit_Action()
    {
        yield return new WaitForSeconds(2.5f);

        RandomNum = Random.Range(1, 4); //1~3 ����
                                        // 1: �·� �̵�, 2: ��� �̵�, 3: �̵� ����
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

        StartCoroutine(GrandSpirit_Action());
   
    }

    public IEnumerator GrandSpirit_Attack()
    {
      
        if (Vector3.Distance(PlayerTarget.position, transform.position) <= AttackRange)
        {


            Debug.Log("�÷��̾� ����");
            GameObject stone = Instantiate(StonePrefab, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
            stone.GetComponent<Stone_Projectile>().Power = AttackPower;
            isAttack = true;
        }
        else
        {
            Debug.Log("�÷��̾� ���� ���� ���");
            isAttack = false;
        }
        yield return new WaitForSeconds(AttackSpeed);
        StartCoroutine(GrandSpirit_Attack());
    }


}
