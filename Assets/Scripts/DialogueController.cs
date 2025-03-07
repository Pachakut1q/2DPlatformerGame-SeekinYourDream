using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    public Image image;// 对话框
    public int level;// 三关
    private Sprite newImage;// 替换图片
    private string imagePath = "Dialogue/";// 图片路径（Resources文件夹下的相对路径）
    
    private string[] suffix = new[] { "chat1", "chat2", "chat3" };//后缀名
    private int i = 1;
    private bool flag = false; //对话框是否播放完了
    // Start is called before the first frame update
    void Start()
    {
        image = GameObject.Find("Chat").GetComponent<Image>();
        switch (level)
        {
            case 1:
                imagePath += "Level1/";
                break;
            case 2:
                imagePath += "Level2/";
                break;
            case 3:
                imagePath += "Level3/";
                break;
        }
        GameManager.PauseGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && flag == false)// TODO:待优化（仅能适用于三张图片的情况）
        {
            if (i == 3)
            {
                flag = true;
                Destroy(image);
                GameManager.ResumeGame();
                return;
            }
            newImage = Resources.Load<Sprite>(imagePath+suffix[i]);
            image.sprite = newImage;
            i++;
        }
    }
}
