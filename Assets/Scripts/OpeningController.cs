using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningController : MonoBehaviour
{
    public Animator animator; void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            AudioManager.PlayBGM(4);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    public void Skip()
    {
        AudioManager.PlayClickClip();
        AudioManager.PlayBGM(4);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
