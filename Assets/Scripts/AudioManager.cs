using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static AudioManager instance;
    
    public AudioClip jumpClip; //elf跳跃音效
    public AudioClip clickClip; //点击
    public AudioClip openingBGM; //开场
    public AudioClip titleBGM; //标题
    public AudioClip levelSelectBGM; //关卡选择
    public AudioClip level1BGM;
    public AudioClip level2BGM;
    public AudioClip level3BGM;
    
    AudioSource jumpSource;
    AudioSource clickSource;
    AudioSource BGMSource;
    void Awake()
    {
        if (instance == null) //实例化并且“不要摧毁”该脚本挂载的对象（因为整个游戏一直会用到）
        {
            instance = this;
            DontDestroyOnLoad(this);
            jumpSource = gameObject.AddComponent<AudioSource>();
            jumpSource.volume = 0.05f; //音量
            clickSource = gameObject.AddComponent<AudioSource>();
            clickSource.volume = 0.1f;
            BGMSource = gameObject.AddComponent<AudioSource>();
            BGMSource.volume = 0.5f;
            BGMSource.loop = true; //循环播放
        }
        else if (instance != this)
        {
            //Debug.Log("Found existing AudioManager, destroying duplicate.");
            Destroy(gameObject); //防止在切换场景时生成多个AudioManager
        }
    }

    private void Start()
    {
        //播放开场bgm
        instance.BGMSource.clip = openingBGM;  
        instance.BGMSource.Play();
    }

    public static void PlayJumpClip()
    {
        instance.jumpSource.clip = instance.jumpClip;
        instance.jumpSource.Play();
    }

    public static void PlayClickClip()
    {
        instance.clickSource.clip = instance.clickClip;
        instance.clickSource.Play();
    }

    public static void PlayBGM(int i)
    {
        switch (i)
        {
            case 4:
                instance.BGMSource.clip = instance.titleBGM;
                break;
            case 5:
                instance.BGMSource.clip = instance.levelSelectBGM;
                break;
            case 1:
                instance.BGMSource.clip = instance.level1BGM;
                break;
            case 2:
                instance.BGMSource.clip = instance.level2BGM;
                break;
            case 3:
                instance.BGMSource.clip = instance.level3BGM;
                break;
        }
        instance.BGMSource.Play();
    }
}
