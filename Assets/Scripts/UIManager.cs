using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject escapeMenu; //按esc呼出此菜单，在标题页面呼出退出游戏界面，在关卡中呼出的是暂停界面
    public GameObject startButton;
    public GameObject levelSelectionButtons;
    public GameObject cover; //转场效果
    
    public Button[] selectButtons;
    public Image[] selectButtonsImage;
    public int unlockedLevelIndex;
    
    private Animator animator;
    private void Start()
    {
        escapeMenu = GameObject.Find("EscapeMenu");
        if(escapeMenu != null) 
            escapeMenu.SetActive(false);
        
        startButton = GameObject.Find("StartButton");
        cover = GameObject.Find("Cover");
        animator = cover.GetComponent<Animator>();
        
        //实现关卡上锁解锁功能
        levelSelectionButtons = GameObject.Find("LevelSelectionButtons");
        if (levelSelectionButtons != null)
        {
            selectButtons = new Button[levelSelectionButtons.transform.childCount];
            selectButtonsImage = new Image[levelSelectionButtons.transform.childCount];
            unlockedLevelIndex = PlayerPrefs.GetInt("UnlockedLevelIndex");
            for (int i = 0; i < levelSelectionButtons.transform.childCount; i++)
            {
                selectButtons[i] = levelSelectionButtons.transform.GetChild(i).GetComponent<Button>();
                selectButtonsImage[i] = levelSelectionButtons.transform.GetChild(i).GetComponent<Image>();
            }
            for(int i = 1; i < selectButtons.Length; i++)// 只让后两关暂时锁定
                selectButtons[i].interactable = false;
            if (unlockedLevelIndex == 4)// 当第三关完成时该参数会变为4，为了防止数组溢出在此进行修正
            {
                for(int i = 1; i < unlockedLevelIndex - 1; i++) 
                    selectButtons[i].interactable = true;
            }
            else
            {
                for(int i = 1; i < unlockedLevelIndex; i++)
                    selectButtons[i].interactable = true;
            }
            
            // 关卡完成后的图片替换
            // TODO:可优化
            if(unlockedLevelIndex == 2)// 第一关完成
                selectButtonsImage[0].sprite = Resources.Load<Sprite>("LevelFinished/Level1Finished");
            if (unlockedLevelIndex == 3)// 第二关完成
            {
                selectButtonsImage[0].sprite = Resources.Load<Sprite>("LevelFinished/Level1Finished");
                selectButtonsImage[1].sprite = Resources.Load<Sprite>("LevelFinished/Level2Finished");
            }
            if (unlockedLevelIndex == 4)// 第三关完成
            {
                selectButtonsImage[0].sprite = Resources.Load<Sprite>("LevelFinished/Level1Finished");
                selectButtonsImage[1].sprite = Resources.Load<Sprite>("LevelFinished/Level2Finished");
                selectButtonsImage[2].sprite = Resources.Load<Sprite>("LevelFinished/Level3Finished");
            } 
        }
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.PauseGame();
            if(startButton != null)
                startButton.SetActive(false);
            escapeMenu.SetActive(true);
        }
    }
    
    public void ResetIndex()//重置关卡进度
    {
        PlayerPrefs.DeleteAll();
    }
    IEnumerator Transition(int i)
    {
        animator.SetBool("fadeIn",true);
        yield return new WaitForSeconds(0.99f);//等待转场动画播放完成
        switch (i)
        {
            case -1:
                SceneManager.LoadScene(1);//Main
                break;
            case 0:
                SceneManager.LoadScene(2);//Selection
                break;
            case 1:
                SceneManager.LoadScene(3);//Level1
                break;
            case 2:
                SceneManager.LoadScene(5);//Level2
                break;
            case 3:
                SceneManager.LoadScene(7);//Level3
                break;
        }
    }
    public void PlayGame() //标题界面到关卡选择
    {
        StartCoroutine(Transition(0));
        AudioManager.PlayBGM(5);
    }

    public void Cancel()
    {
        AudioManager.PlayClickClip();
        escapeMenu.SetActive(false);
        startButton.SetActive(true);
        GameManager.ResumeGame();
    }
    
    public void QuitGame()
    {
        AudioManager.PlayClickClip();
        Application.Quit();//退出游戏
    }

    public void BacktoMainMenu() //关卡选择回标题界面
    {
        AudioManager.PlayClickClip();
        StartCoroutine(Transition(-1));
        AudioManager.PlayBGM(4);
    }

    public void Resume()
    {
        AudioManager.PlayClickClip();
        escapeMenu.SetActive(false);
        GameManager.ResumeGame();
    }
    public void BacktoLevelSelection()
    {
        AudioManager.PlayClickClip();
        StartCoroutine(Transition(0));
        AudioManager.PlayBGM(5);
    }
    
    public void InGameBacktoLevelSelection() //关卡中回到关卡选择
    {
        AudioManager.PlayClickClip();
        GameManager.ResumeGame();
        SceneManager.LoadScene(2);
        AudioManager.PlayBGM(4);
    }

    public void ReloadLevel() //重置关卡
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Level1()
    {
        AudioManager.PlayClickClip();
        StartCoroutine(Transition(1));//进入第一关
        AudioManager.PlayBGM(1);
    }
    
    public void Level2()
    {
        AudioManager.PlayClickClip();
        StartCoroutine(Transition(2));//进入第二关
        AudioManager.PlayBGM(2);
    }

    public void Level3()
    {
        AudioManager.PlayClickClip();
        StartCoroutine(Transition(3));//进入第三关
        AudioManager.PlayBGM(3);
    }
}
