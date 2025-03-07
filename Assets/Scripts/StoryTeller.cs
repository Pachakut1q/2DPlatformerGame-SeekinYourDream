using UnityEngine;
using UnityEngine.UI;

// 每关通关后的小故事
public class StoryTeller : MonoBehaviour
{
    public int level;
    public Image image;
    private Sprite newImage;
    private string imagePath = "Stories/";
    private string[] suffix = new string[] { "Page1", "Page2", "Page3" };
    private int i = 1;

    public Button backtoLevelSelection;
    // Start is called before the first frame update
    void Start()
    {
        image = GameObject.Find("Image").GetComponent<Image>();
        backtoLevelSelection = GameObject.Find("Button").GetComponent<Button>();
        if(backtoLevelSelection != null)
            backtoLevelSelection.gameObject.SetActive(false);
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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && i < 3)// TODO:待优化
        {
            newImage = Resources.Load<Sprite>(imagePath+suffix[i]);
            image.sprite = newImage;
            i++;
            if (i == 3)
            {
                backtoLevelSelection.gameObject.SetActive(true);
            }
        }
    }
}
