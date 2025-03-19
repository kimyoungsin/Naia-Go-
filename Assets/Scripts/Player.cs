using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody Rigid;
    public float MovementSpeed;
    public float JumpPower;
    public GameObject WaterEffect;
    public bool isDizzy;


    public bool isJump = false;
    public bool isInvisible = false;

    void Start()
    {
        Rigid = GetComponent<Rigidbody>();
    }


    void Update()
    {
        //�̵�
        if (Input.GetKey(KeyCode.A))
        {
            transform.localScale = new Vector2(-1, 1);
            Rigid.velocity = new Vector3(-MovementSpeed, Rigid.velocity.y, Rigid.velocity.z);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.localScale = new Vector2(1, 1);
            Rigid.velocity = new Vector3(MovementSpeed, Rigid.velocity.y, Rigid.velocity.z);
        }

        //�̵� ���� ��
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            Rigid.velocity = new Vector3(0, Rigid.velocity.y, Rigid.velocity.z);
            
        }

        /*
        //�̵�
        if (Input.GetButton("Horizontal"))
        {
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                transform.localScale = new Vector2(-1, 1);
            }
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                transform.localScale = new Vector2(1, 1);
            }
            Walk();
        }
    

        //�̵� ���� ��
        if (Input.GetButtonUp("Horizontal"))
        {
            Rigid.velocity = new Vector3(0, Rigid.velocity.y, Rigid.velocity.z);
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                transform.localScale = new Vector2(-1, 1);
            }
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                transform.localScale = new Vector2(1, 1);
            }
        }
        */

        //����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isJump)
            {
                Jump();
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
         
                if (Rigid.velocity.y > 0)
                {
                    Rigid.velocity = new Vector3(Rigid.velocity.x, -Rigid.velocity.y, Rigid.velocity.z);
                }
                else
                {
                    Rigid.velocity = new Vector3(Rigid.velocity.x, Rigid.velocity.y, Rigid.velocity.z);
                }
       


        }

        //���� üũ
        Debug.DrawRay(Rigid.position, Vector3.down, new Color(0,0.5f,0));
        if(isJump && Physics.Raycast(Rigid.position, Vector3.down, 0.5f, LayerMask.GetMask("Ground")))
        {
            isJump = false;
        }

        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            if (!isDizzy)
            {
                WaterShoot();
            }
         
        }
        if(Input.GetButtonUp("Horizontal") || Input.GetButtonUp("Vertical"))
        {

            StartCoroutine(WaterRecoverStart());
        }

        //�� ��� �ð� ������ �� ���� ���� ����
        if(TumblerUI.Instance.WaterRecover && TumblerUI.Instance.WaterShootPower > 0)
        {
            if(TumblerUI.Instance.WaterShootPower <= 0)
            {
                TumblerUI.Instance.WaterShootPower = 0;
            }
            else
            {
                TumblerUI.Instance.WaterShootPower -= TumblerUI.Instance.WaterRecoverValue * Time.deltaTime;
            }
    
        }
        //�� ��� �ð� ������ �� �Һ� ���� ȸ��
        if (TumblerUI.Instance.WaterRecover && TumblerUI.Instance.TumblerGauge < 100)
        {
            TumblerUI.Instance.TumblerGauge += TumblerUI.Instance.TumblerTumblerGaugeRecoverValue * Time.deltaTime;

        }
    }

    public void Walk()
    {
        float h = Input.GetAxisRaw("Horizontal");
        Rigid.velocity = new Vector3(h * MovementSpeed, Rigid.velocity.y, Rigid.velocity.z);
    }

    public void Jump()
    {
        Rigid.velocity = new Vector3(Rigid.velocity.x, JumpPower, Rigid.velocity.z);
        isJump = true;

    }

    public void WaterShoot()
    {
        if(TumblerUI.Instance.TumblerGauge > 0)
        {
            if(!isDizzy)
            {
                float dir_x = Input.GetAxisRaw("Horizontal");
                float dir_y = Input.GetAxisRaw("Vertical");
                Rigid.velocity = new Vector3(-dir_x * TumblerUI.Instance.WaterShootPower, -dir_y * TumblerUI.Instance.WaterShootPower, 0);

                TumblerUI.Instance.WaterShootPower += 1 * Time.deltaTime;
                TumblerUI.Instance.TumblerGauge -= 10 * Time.deltaTime;
                TumblerUI.Instance.WaterRecover = false;
                WaterDirection(dir_x, dir_y);

                //Vector3 rot = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
                //water.transform.rotation = Quaternion.Euler(Mathf.Atan2(rot.x, rot.y) * Mathf.Rad2Deg, Mathf.Atan2(rot.x, rot.y) * Mathf.Rad2Deg, 0);
            }

        }
        else if(TumblerUI.Instance.TumblerGauge <= 1)
        {
            isDizzy = true;
            TumblerUI.Instance.TumblerDizzyImage.SetActive(true);
            StartCoroutine(DizzyRecoverStart());
        }
   
    }
    public IEnumerator DizzyRecoverStart()
    {
        yield return new WaitForSeconds(1.5f);
        TumblerUI.Instance.TumblerDizzyImage.SetActive(false);
        isDizzy = false;
    }

    public IEnumerator WaterRecoverStart()
    {
        yield return new WaitForSeconds(TumblerUI.Instance.WaterRecoverTime);
        TumblerUI.Instance.WaterRecover = true;
    }

    public void EnemyHit()
    {
        if(!isInvisible)
        {
            Rigid.velocity = new Vector3(0, 0, Rigid.velocity.z);
            TumblerUI.Instance.TumblerDizzyImage.SetActive(true);
            StartCoroutine(HitDizzyRecovery());
            isDizzy = true;
            isInvisible = true;
        }
  
    }

    public IEnumerator HitDizzyRecovery()
    {
        yield return new WaitForSeconds(0.5f);
        TumblerUI.Instance.TumblerDizzyImage.SetActive(false);
        isDizzy = false;
        isInvisible = false;
    }

    public void WaterDirection(float x, float y)
    {
        GameObject water = Instantiate(WaterEffect, gameObject.transform);
        if(x == 0 && y == -1)//�Ʒ�
        {
            water.transform.rotation = Quaternion.Euler(90, 90, 0);
        }
        if (x == -1 && y == -1)//����-�Ʒ�
        {
            water.transform.rotation = Quaternion.Euler(135, 90, 0);
        }
        if (x == -1 && y == 0)//����
        {
            water.transform.rotation = Quaternion.Euler(180, 90, 0);
        }
        if (x == -1 && y == 1)//����-��
        {
            water.transform.rotation = Quaternion.Euler(225, 90, 0);
        }
        if (x == 0 && y == 1)//��
        {
            water.transform.rotation = Quaternion.Euler(270, 90, 0);
        }
        if (x == 1 && y == 1)//������-��
        {
            water.transform.rotation = Quaternion.Euler(315, 90, 0);
        }
        if (x == 1 && y == 0)//������
        {
            water.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        if (x == 1 && y == -1)//������-�Ʒ�
        {
            water.transform.rotation = Quaternion.Euler(45, 90, 0);
        }
    }

       
    
}
