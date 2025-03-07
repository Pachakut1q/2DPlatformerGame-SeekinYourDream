using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    private static bool playerInEndZone = false; //判断是否到达梦泡区域
    public GameObject elf;
    public GameObject respawnPoint; //重生点
    public int levelIndex; //当前关数
    public int unlockedLevelIndex; //已解锁关卡
    public MumbleController mumbleController; //碎碎念
    void Awake()
    {
        instance = this; //实例化
    }

    void Start()
    {
        //获取GameObject
        elf = GameObject.Find("Elf");
        respawnPoint = GameObject.Find("RespawnPoint");
        mumbleController = GameObject.Find("Elf").GetComponent<MumbleController>();
        unlockedLevelIndex = PlayerPrefs.GetInt("UnlockedLevelIndex");
    }

    void RespawnElf()//重生
    {
        elf.transform.position = respawnPoint.transform.position;
    }
    //TODO:死亡的处理逻辑待优化
    public static void PlayerDeath1()// 落入虚空（以及碰到地刺）
    {
        instance.Invoke("HandleDeath1",0);
    }
    void HandleDeath1()
    {
        RespawnElf();
        mumbleController.ShowDeath1();
    }
    public static void PlayerDeath2()// 碰上幽灵
    {
        instance.Invoke("HandleDeath2",0);
    }
    void HandleDeath2()
    {
        RespawnElf();
        mumbleController.ShowDeath2();
    }
    public static void PlayerDeath3()// 倒计时结束
    {
        instance.Invoke("HandleDeath3",0);
    }
    void HandleDeath3()
    {
        RespawnElf();
        mumbleController.ShowDeath3();
    }
    public static void SetPlayerInEndZone(bool value)
    {
        playerInEndZone = value;
    }
    public static bool IsPlayerInEndZone()
    {
        return playerInEndZone;
    }
    public static void LevelComplete()
    {
        instance.HandleLevelComplete();
    }
    void HandleLevelComplete()
    {
        if (levelIndex >= unlockedLevelIndex)
        {
            unlockedLevelIndex = levelIndex + 1;
            PlayerPrefs.SetInt("UnlockedLevelIndex", unlockedLevelIndex); //PlayerPrefs用于存储玩家相关数据
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public static void ResumeGame() //恢复游戏
    {
        Time.timeScale = 1.0f;
    }
    public static void PauseGame() //暂停游戏
    {
        Time.timeScale = 0.0f;
    }
}
