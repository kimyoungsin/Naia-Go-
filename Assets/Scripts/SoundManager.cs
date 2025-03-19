using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager SharedInstance = null; //Instance ������ �̱������� ����, �ٸ� ������Ʈ���� ��� ����

    private void Awake()
    {
        if (SharedInstance == null)//Instance�� �ý��ۿ� ���� �� 
        {
            SharedInstance = this;//�ڽ��� �ν��Ͻ��� �־���
            DontDestroyOnLoad(gameObject);//OnLoad(���� �ε�Ǿ�����)�ڽ��� �ı����ϰ� ����

        }
        else
        {
            if (SharedInstance != this)//�ν��Ͻ��� �ڽ��� �ƴ϶�� �̹� �ν��Ͻ��� ����
            {
                Destroy(this.gameObject);//Awake()�� ������ �ڽ� �ı�
            }
        }
    }

    public Sound[] effectSound;
    public Sound[] BgmSound;

    public AudioSource audioSourceBFM; //BGM �����
    public AudioSource[] audioSourceEffects; //ȿ���� ������ ��� �����ϹǷ�

    public string[] playSoundName; //������� ȿ���� �̸� �迭

    void Start()
    {
        playSoundName = new string[audioSourceEffects.Length];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            PlayBGM(BgmSound[0].clip.name);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayBGM(BgmSound[1].clip.name);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayBGM(BgmSound[2].clip.name);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlayBGM(BgmSound[3].clip.name);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            PlayBGM(BgmSound[4].clip.name);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            PlayBGM(BgmSound[5].clip.name);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            PlayBGM(BgmSound[6].clip.name);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            PlayBGM(BgmSound[7].clip.name);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            PlayBGM(BgmSound[8].clip.name);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            PlayBGM(BgmSound[9].clip.name);
        }

        if (Input.GetKeyDown(KeyCode.Minus))
        {
            audioSourceBFM.pitch -= 0.1f;
        }
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            audioSourceBFM.pitch += 0.1f;
        }
    }

    public void PlaySE(string _name)
    {

        for (int i = 0; i < effectSound.Length; i++)
        {
            if (_name == effectSound[i].name)
            {
                for (int j = 0; j < audioSourceEffects.Length; j++)
                {
                    if (!audioSourceEffects[j].isPlaying)
                    {
                        audioSourceEffects[j].clip = effectSound[i].clip;
                        audioSourceEffects[j].Play();
                        playSoundName[j] = effectSound[i].name;
                        return;
                    }
                }
                Debug.Log("��� ���尡 �����.");
                return;
            }
        }
        Debug.Log(_name + "���尡 soundmanager�� ��ϵ��� ����.");
    }

    public void PlayBGM(string _name)
    {
        //audioSourceBFM.Stop();
        for (int i = 0; i < BgmSound.Length; i++)
        {
            if (_name == BgmSound[i].name)
            {
                audioSourceBFM.clip = BgmSound[i].clip;
                if (audioSourceBFM.isPlaying)
                {
                    return;
                }
                else
                {
                    audioSourceBFM.Play();
                    return;
                }


            }
        }
        Debug.Log(_name + "���尡 soundmanager�� ��ϵ��� ����.");
    }

    public void StopBGM()
    {
        audioSourceBFM.Stop();
    }

    public void StopAllSE()
    {
        for (int i = 0; i < audioSourceEffects.Length; i++)
        {
            audioSourceEffects[i].Stop();
        }
    }

    public void StopSE(string _name)
    {
        for (int i = 0; i < audioSourceEffects.Length; i++)
        {
            if (playSoundName[i] == _name)
            {
                audioSourceEffects[i].Stop();
                break;
            }
        }
        Debug.Log("�������" + _name + "���尡 ����.");
    }

}
