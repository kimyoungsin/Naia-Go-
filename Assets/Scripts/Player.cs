using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody Rigid;
    public Animator Ani;
    public float MovementSpeed;
    public float JumpPower;
    public ParticleSystem WaterEffect;
    public GameObject DizzyEffect;
    public bool isDizzy;


    //public bool isJump = false;
    public float SpinTime = 0.25f;
    public bool isSpin = false;
    public bool isWindSpin = false;
    public bool isInvisible = false;
    public bool isWindHit = false;

    void Start()
    {
        Rigid = GetComponent<Rigidbody>();
        Ani = GetComponent<Animator>();
        if (WaterEffect.isPlaying)
        {
            WaterEffect.Stop();


        }

    }


    void Update()
    {
        //이동
        if (Input.GetKey(KeyCode.A))
        {
            transform.localScale = new Vector2(1, 1);
            Rigid.velocity = new Vector3(-MovementSpeed, Rigid.velocity.y, Rigid.velocity.z);
        }

        if (Input.GetKey(KeyCode.D))
        {
            
            transform.localScale = new Vector2(-1, 1);
            Rigid.velocity = new Vector3(MovementSpeed, Rigid.velocity.y, Rigid.velocity.z);
        }

        /*
        //이동 종료 시
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            Rigid.velocity = new Vector3(0, Rigid.velocity.y, Rigid.velocity.z);
            
        }
        */

        /*

        //착지 체크용
        Debug.DrawRay(Rigid.position, Vector3.down, new Color(0,0.5f,0));
        if(isJump && Physics.Raycast(Rigid.position, Vector3.down, 0.5f, LayerMask.GetMask("Ground")))
        {
            isJump = false;
        }
          */

        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            if (!isDizzy)
            {
                WaterShoot();
            }
         
        }
        if(Input.GetButtonUp("Horizontal") || Input.GetButtonUp("Vertical"))
        {
            if(WaterEffect != null)
            {
                WaterEffect.GetComponent<ParticleSystem>().Stop();
            }
       
            StartCoroutine(WaterRecoverStart());
            Ani.SetBool("isWaterShoot", false);
        }
      

        //텀블러 회전(스핀)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(!isSpin && TumblerUI.Instance.TumblerGauge > (TumblerUI.Instance.TumblerGauge / 10))
            {
                StartCoroutine(Spin());
            }

        }
        //회전 보정용
        if(!isSpin && transform.rotation.z > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }


        //텀블러 수압 게이지 감소
        if (TumblerUI.Instance.WaterRecover && TumblerUI.Instance.WaterShootPower > 0)
        {
            if(TumblerUI.Instance.WaterShootPower <= 0)
            {
                TumblerUI.Instance.WaterShootPower = 0;
            }
            else
            {
                TumblerUI.Instance.WaterShootPower -= (TumblerUI.Instance.WaterRecoverValue * 0.75f) * Time.deltaTime;
            }
    
        }
        //텀블러 수분 게이지 회복
        if (TumblerUI.Instance.WaterRecover && TumblerUI.Instance.TumblerGauge < 100 && TumblerUI.Instance.TumblerGauge >= 20)
        {
            TumblerUI.Instance.TumblerGauge += TumblerUI.Instance.TumblerTumblerGaugeRecoverValue * Time.deltaTime;

        }
        else if(TumblerUI.Instance.WaterRecover && TumblerUI.Instance.TumblerGauge < 20)
        {
            TumblerUI.Instance.TumblerGauge += (TumblerUI.Instance.TumblerTumblerGaugeRecoverValue * 2) * Time.deltaTime;
        }
    }

    public void Walk()
    {
        float h = Input.GetAxisRaw("Horizontal");
        Rigid.velocity = new Vector3(h * MovementSpeed, Rigid.velocity.y, Rigid.velocity.z);
    }
    /*
    public void Jump()
    {
        Rigid.velocity = new Vector3(Rigid.velocity.x, JumpPower, Rigid.velocity.z);
        isJump = true;

    }
    */

    public IEnumerator Spin()
    {
        //Mathf.Abs() : 절대값 반환(90 = 90, -90 = 90)
        //Mathf.Sign() : 값이 양수면 1, 음수면 -1, 0이면 0을 반환

        isSpin = true;
        float rotated = 0f;
        while (rotated < Mathf.Abs(360))
        {
            float step = (Mathf.Abs(360) / SpinTime) * Time.deltaTime;
            transform.Rotate(0, 0, Mathf.Sign(360) * step, Space.Self);
            rotated += step;
            yield return null;
        }
        isSpin = false;
    }

    public IEnumerator WindSpin()
    {
        //Mathf.Abs() : 절대값 반환(90 = 90, -90 = 90)
        //Mathf.Sign() : 값이 양수면 1, 음수면 -1, 0이면 0을 반환

        isWindSpin = true;
        float rotated = 0f;
        while (rotated < Mathf.Abs(360))
        {
            float step = (Mathf.Abs(360) / SpinTime) * Time.deltaTime;
            transform.Rotate(0, Mathf.Sign(360) * step, 0, Space.Self);
            rotated += step;
            yield return null;
        }
        isWindSpin = false;
    }

    public void WaterShoot()
    {
        if(TumblerUI.Instance.TumblerGauge > 0)
        {
            if(!isDizzy)
            {
                float dir_x = Input.GetAxisRaw("Horizontal");
                float dir_y = Input.GetAxisRaw("Vertical");
                Vector2 direction = new Vector2(dir_x, dir_y).normalized;
                if (direction != Vector2.zero)
                {
                    float angle = Mathf.Atan2(-direction.y, direction.x) * Mathf.Rad2Deg;
                    Quaternion rotation = Quaternion.Euler(angle, 90f, 0);
                    WaterEffect.transform.rotation = Quaternion.Slerp(WaterEffect.transform.rotation, rotation, Time.deltaTime * 60f);
                }
                
                
                
                if (!WaterEffect.isPlaying)
                {
                    WaterEffect.Play();
                }

                Rigid.velocity = new Vector3(-dir_x * TumblerUI.Instance.WaterShootPower + 1f, -dir_y * TumblerUI.Instance.WaterShootPower + 1f, 0);
                TumblerUI.Instance.WaterShootPower += 1 * Time.deltaTime;
                TumblerUI.Instance.TumblerGauge -= 10 * Time.deltaTime;
                TumblerUI.Instance.WaterRecover = false;
                //WaterDirection(dir_x, dir_y);
                Ani.SetBool("isWaterShoot", true);

            }

        }
        else if(TumblerUI.Instance.TumblerGauge <= 1)
        {
            isDizzy = true;
            TumblerUI.Instance.TumblerDizzyImage.SetActive(true);
            DizzyEffect.SetActive(true);
            StartCoroutine(DizzyRecoverStart());
        }
   
    }

    public void JoyStickWaterShoot(float dir_x, float dir_y)
    {
        if (TumblerUI.Instance.TumblerGauge > 0)
        {
            if (!isDizzy)
            {
                Vector2 direction = new Vector2(dir_x, dir_y).normalized;
                if (direction != Vector2.zero)
                {
                    float angle = Mathf.Atan2(-direction.y, direction.x) * Mathf.Rad2Deg;
                    Quaternion rotation = Quaternion.Euler(angle, 90f, 0);
                    WaterEffect.transform.rotation = Quaternion.Slerp(WaterEffect.transform.rotation, rotation, Time.deltaTime * 60f);
                }

                Rigid.velocity = new Vector3(-dir_x * TumblerUI.Instance.WaterShootPower + 1f, -dir_y * TumblerUI.Instance.WaterShootPower + 1f, 0);
        
                if (!WaterEffect.isPlaying)
                {
                    WaterEffect.Play();
                    
         
                }

                TumblerUI.Instance.WaterShootPower += 1 * Time.deltaTime;
                TumblerUI.Instance.TumblerGauge -= 10 * Time.deltaTime;
                TumblerUI.Instance.WaterRecover = false;
                //WaterDirection(dir_x, dir_y);
                Ani.SetBool("isWaterShoot", true);

            }

        }
        else if (TumblerUI.Instance.TumblerGauge <= 1)
        {
            isDizzy = true;
            TumblerUI.Instance.TumblerDizzyImage.SetActive(true);
            Ani.SetBool("isStun", true);
            DizzyEffect.SetActive(true);
            StartCoroutine(DizzyRecoverStart());
        }

    }
    public IEnumerator DizzyRecoverStart()
    {
        yield return new WaitForSeconds(1.5f);
        TumblerUI.Instance.TumblerDizzyImage.SetActive(false);
        Ani.SetBool("isStun", false);
        DizzyEffect.SetActive(false);
        isDizzy = false;
    }

    public IEnumerator WaterRecoverStart()
    {
        yield return new WaitForSeconds(TumblerUI.Instance.WaterRecoverTime);
        TumblerUI.Instance.WaterRecover = true;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (!isSpin)
        {
            if (!isInvisible)
            {
                if (collision.gameObject.tag == "Stone")
                {
                    if (collision.gameObject.transform.position.x > transform.position.x)
                    {
                        Rigid.AddForce(new Vector3(-1, 1, 0), ForceMode.Impulse);

                    }
                    else
                    {
                        Rigid.AddForce(new Vector3(1, 1, 0), ForceMode.Impulse);
                    }
                    Rigid.velocity = new Vector3(0, 0, Rigid.velocity.z);
                    TumblerUI.Instance.TumblerDizzyImage.SetActive(true);
                    DizzyEffect.SetActive(true);
                    StartCoroutine(HitDizzyRecovery());
                    isDizzy = true;
                    isInvisible = true;
                    Ani.SetBool("isStun", true);
                    Destroy(collision.gameObject);
                }
    
            }
        }
       
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Wind")
        {

            if (!isSpin)
            {
                if (!isInvisible && !isWindHit)
                {
                    Rigid.velocity = new Vector3(0, 0, Rigid.velocity.z);
                    isWindHit = true;
                    if (TumblerUI.Instance.WaterShootPower > 0)
                    {
                        TumblerUI.Instance.WaterShootPower -= 2;
                        StartCoroutine(WindSpin());
                        StartCoroutine(WindHitRecovery());

                    }
                }
            }

        }
        if (collision.gameObject.tag == "Fire")
        {

            if (!isSpin)
            {
                if (!isInvisible)
                {
                    TumblerUI.Instance.TumblerGauge -= (TumblerUI.Instance.TumblerGauge / 10);
                    Destroy(collision.gameObject);
                }
            }

        }
        if (collision.gameObject.tag == "Item")
        {

            if (collision.GetComponent<Item>().itemdata != null)
            {
                //TumblerUI.Instance.TumblerGauge += collision.GetComponent<Item>().itemdata.ItemRecoverValue;
                Destroy(collision.gameObject);
            }

        }
        if (collision.gameObject.tag == "Soda")
        {

            if (collision.GetComponent<Item>().itemdata != null)
            {
                TumblerUI.Instance.TumblerGauge += collision.GetComponent<Item>().itemdata.ItemRecoverValue;
                Destroy(collision.gameObject);
            }

        }
        if (collision.gameObject.tag == "Strawberry")
        {

            if (collision.GetComponent<Item>().itemdata != null)
            {
                TumblerUI.Instance.WaterShootPower += collision.GetComponent<Item>().itemdata.ItemRecoverValue;
                Destroy(collision.gameObject);
            }

        }
        if (collision.gameObject.tag == "Goal")
        {
            UI_Canvas.Instance.ClaerUI_On();
            collision.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    public void EnemyHit()
    {
        if(!isSpin)
        {
            if (!isInvisible)
            {
                Rigid.velocity = new Vector3(0, 0, Rigid.velocity.z);
                TumblerUI.Instance.TumblerDizzyImage.SetActive(true);
                DizzyEffect.SetActive(true);
                StartCoroutine(HitDizzyRecovery());
                isDizzy = true;
                isInvisible = true;
                Ani.SetBool("isStun", true);
            }
        }
    }

    public IEnumerator HitDizzyRecovery()
    {
        yield return new WaitForSeconds(0.5f);
        TumblerUI.Instance.TumblerDizzyImage.SetActive(false);
        DizzyEffect.SetActive(false);
        isDizzy = false;
        isInvisible = false;
        Ani.SetBool("isStun", false);
    }
    public void WindHit(float windPower)
    {
        if (!isSpin)
        {
            if (!isInvisible && !isWindHit)
            {
                Rigid.velocity = new Vector3(0, 0, Rigid.velocity.z);
                isWindHit = true;
                Ani.SetBool("isStun", true);
                if (TumblerUI.Instance.WaterShootPower > 0)
                {
                    TumblerUI.Instance.WaterShootPower -= windPower;
                    StartCoroutine(WindHitRecovery());
                    
                }
            }
        }
    }

    public IEnumerator WindHitRecovery()
    {
        yield return new WaitForSeconds(0.5f);
        //TumblerUI.Instance.TumblerDizzyImage.SetActive(false);
        Ani.SetBool("isStun", false);
        isWindHit = false;
    }

    public void WaterDirection(float x, float y)
    {


        //GameObject water = Instantiate(WaterEffect, gameObject.transform);
        //water.transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);

        
        /*
        WaterEffect.transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
        if (x == 0 && y == -1)//�Ʒ�
        {
            WaterEffect.transform.rotation = Quaternion.Euler(90, 90, 0);
            //WaterEffect.transform.Rotate()
        }
        if (x == -1 && y == -1)//����-�Ʒ�
        {
            WaterEffect.transform.rotation = Quaternion.Euler(135, 90, 0);
        }
        if (x == -1 && y == 0)//����
        {
            WaterEffect.transform.rotation = Quaternion.Euler(180, 90, 0);
        }
        if (x == -1 && y == 1)//����-��
        {
            WaterEffect.transform.rotation = Quaternion.Euler(225, 90, 0);
        }   
        if (x == 0 && y == 1)//��
        {
            WaterEffect.transform.rotation = Quaternion.Euler(270, 90, 0);
        }
        if (x == 1 && y == 1)//������-��
        {
            WaterEffect.transform.rotation = Quaternion.Euler(315, 90, 0);
        }
        if (x == 1 && y == 0)//������
        {
            WaterEffect.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        if (x == 1 && y == -1)//������-�Ʒ�
        {
            WaterEffect.transform.rotation = Quaternion.Euler(45, 90, 0);
        }
        */
    }


}
