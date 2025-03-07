using UnityEngine;

// 第二关幽灵AI，比较笨的方法，需要手动获取幽灵移动的起点终点坐标
public class yEnemyAI : MonoBehaviour
{
    public float minY = 1f; // 最下边的y坐标
    public float maxY = 8f;  // 最上边的y坐标
    public float speed = 1f; // 物体移动的速度

    private Vector3 targetPosition; // 目标位置
    private bool movingUp = true; // 是否向上移动
    void Start()
    {
        // 初始化目标位置为最下边的位置
        targetPosition = new Vector3(transform.position.x, minY, transform.position.z);
    }
    void Update()
    {
        // 移动物体
        MoveObject();
    }
    void MoveObject()
    {
        // 根据移动方向计算目标位置
        if (movingUp)
        {
            targetPosition = new Vector3(transform.position.x, maxY, transform.position.z);
        }
        else
        {
            targetPosition = new Vector3(transform.position.x, minY, transform.position.z);
        }

        // 使用Lerp函数平滑移动物体
        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);

        // 检查是否到达目标位置
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            // 到达目标位置后反转移动方向
            movingUp = !movingUp;
        }
    }
}