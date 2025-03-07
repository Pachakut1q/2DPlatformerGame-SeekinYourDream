using System.Collections;
using UnityEngine;

public class MumbleController : MonoBehaviour
{
    public GameObject prologue1;
    public GameObject prologue2;
    public GameObject prologuePoint;

    public GameObject death1;
    public GameObject death2;
    public GameObject death3;// 碰到幽灵
    public GameObject death4;// 倒计时结束

    public GameObject end;
    public GameObject endPoint;
    
    public GameObject ghost;
    public GameObject ghostPoint;
    
    public float duration = 3.0f; // 物体显示的持续时间

    void Start()
    {
        //获取对象
        prologue1 = GameObject.Find("Prologue1");
        prologue1.SetActive(false);
        prologue2 = GameObject.Find("Prologue2");
        if(prologue2 != null)
            prologue2.SetActive(false);
        prologuePoint = GameObject.Find("ProloguePoint");
        death1 = GameObject.Find("Death1");
        death1.SetActive(false);
        death2 = GameObject.Find("Death2");
        if (death2 != null)
            death2.SetActive(false);
        death3 = GameObject.Find("Death3");
        if (death3 != null)
            death3.SetActive(false);
        death4 = GameObject.Find("Death4");
        if (death4 != null)
            death4.SetActive(false);
        end = GameObject.Find("End");
        end.SetActive(false);
        endPoint = GameObject.Find("EndPoint");
        ghost = GameObject.Find("Ghost");
        if (ghost != null)
            ghost.SetActive(false);
        ghostPoint = GameObject.Find("GhostPoint");
    }
    public void ShowPrologue()
    {
        StartCoroutine(Prologue());
    }

    IEnumerator Prologue() //利用协程实现每隔一段时间自动切换自动消失的效果
    {
        Destroy(prologuePoint);
        prologue1.SetActive(true);
        yield return new WaitForSeconds(duration);
        prologue1.SetActive(false);
        if (prologue2 != null)
        {
            prologue2.SetActive(true);
            yield return new WaitForSeconds(duration);
            prologue2.SetActive(false);
        }
    }

    public void ShowDeath1()//用于展示失足的文本
    {
        StartCoroutine(Death1());
    }

    public void ShowDeath2()//用于展示碰到幽灵后的文本
    {
        StartCoroutine(Death2());
    }

    public void ShowDeath3()//用于展示倒计时结束的文本
    {
        StartCoroutine(Death3());
    }
    IEnumerator Death1()
    {
        death1.SetActive(true);
        yield return new WaitForSeconds(duration);
        death1.SetActive(false);
        if (death2 != null)
        {
            death2.SetActive(true);
            yield return new WaitForSeconds(duration);
            death2.SetActive(false);
        }
    }

    IEnumerator Death2()
    {
        death3.SetActive(true);
        yield return new WaitForSeconds(duration);
        death3.SetActive(false);
    }

    IEnumerator Death3()
    {
        death4.SetActive(true);
        yield return new WaitForSeconds(duration);
        death4.SetActive(false);
    }
    public void ShowEnd()
    {
        StartCoroutine(End());
    }

    IEnumerator End()
    {
        Destroy(endPoint);
        end.SetActive(true);
        yield return new WaitForSeconds(duration);
        end.SetActive(false);
    }

    public void ShowGhost() //第一次碰到幽灵
    {
        StartCoroutine(Ghost());
    }

    IEnumerator Ghost()
    {
        Destroy(ghostPoint);
        ghost.SetActive(true);
        yield return new WaitForSeconds(duration);
        ghost.SetActive(false);
    }
}