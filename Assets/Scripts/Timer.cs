using TMPro;
using UnityEngine;

//第三关计时器
public class Timer : MonoBehaviour
{
    public float countdownTime = 60f;
    private float countdownTimeNow = 0f;
    public TextMeshProUGUI countdownText;
    // Start is called before the first frame update
    void Start()
    {
        countdownText = GameObject.Find("CountDown").GetComponent<TextMeshProUGUI>();
        countdownText.text = countdownTime.ToString("F1");
        countdownTimeNow = countdownTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (countdownTimeNow > 0)
        {
            countdownTimeNow -= Time.deltaTime;
            countdownText.text = countdownTimeNow.ToString("F1"); //显示文本
        }
        else
        {
            GameManager.PlayerDeath3();
            countdownTimeNow = countdownTime;//重置计时器
        }
    }
}
