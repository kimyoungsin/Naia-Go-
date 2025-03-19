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
    public static SoundManager SharedInstance = null; //Instance 변수를 싱글톤으로 선언, 다른 오브젝트에서 사용 가능

    private void Awake()
    {
        if (SharedInstance == null)//Instance가 시스템에 없을 때 
        {
            SharedInstance = this;//자신을 인스턴스로 넣어줌
            DontDestroyOnLoad(gameObject);//OnLoad(씬이 로드되었을때)자신을 파괴안하고 유지

        }
        else
        {
            if (SharedInstance != this)//인스턴스가 자신이 아니라면 이미 인스턴스가 존재
            {
                Destroy(this.gameObject);//Awake()로 생성된 자신 파괴
            }
        }
    }

    public Sound[] effectSound;
    public Sound[] BgmSound;

    public AudioSource audioSourceBFM; //BGM 재생기
    public AudioSource[] audioSourceEffects; //효과음 여러개 재생 가능하므로

    public string[] playSoundName; //재생중인 효과음 이름 배열

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
                Debug.Log("모든 사운드가 사용중.");
                return;
            }
        }
        Debug.Log(_name + "사운드가 soundmanager에 등록되지 않음.");
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
        Debug.Log(_name + "사운드가 soundmanager에 등록되지 않음.");
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
        Debug.Log("재생중인" + _name + "사운드가 없음.");
    }

}
