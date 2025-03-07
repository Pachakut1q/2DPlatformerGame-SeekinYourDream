using UnityEngine;

// 第二关幽灵AI，比较笨的方法，需要手动获取幽灵移动的起点终点坐标
public class xEnemyAI : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    
    public float minX = 1f; // 最左边的x坐标
    public float maxX = 8f;  // 最右边的x坐标
    public float speed = 1f; // 物体移动的速度

    private Vector3 targetPosition; // 目标位置
    private bool movingRight = true; // 是否向右移动
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // 初始化目标位置为最左边的位置
        targetPosition = new Vector3(minX, transform.position.y, transform.position.z);
    }
    void Update()
    {
        // 移动物体
        MoveObject();
    }
    void MoveObject()
    {
        // 根据移动方向计算目标位置
        if (movingRight)
        {
            spriteRenderer.flipX = true;
            targetPosition = new Vector3(maxX, transform.position.y, transform.position.z);
        }
        else
        {
            spriteRenderer.flipX = false;
            targetPosition = new Vector3(minX, transform.position.y, transform.position.z);
        }

        // 使用Lerp函数平滑移动物体
        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);

        // 检查是否到达目标位置
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            // 到达目标位置后反转移动方向
            movingRight = !movingRight;
        }
    }
}