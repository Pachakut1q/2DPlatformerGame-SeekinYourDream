using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // 玩家的Transform组件
    public BoxCollider2D boundary; // 限制摄像机移动的边界对象的BoxCollider2D组件
    public float smoothSpeed; // 摄像机平滑移动的速度

    private void Start()
    {
        player = GameObject.Find("Elf").transform;
        boundary = GameObject.Find("CameraEdge").GetComponent<BoxCollider2D>();
    }

    void LateUpdate()
    {
        // 摄像机跟随玩家移动
        Vector3 desiredPosition = new Vector3(player.position.x, player.position.y, transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
        // 限制摄像机在指定范围内移动
        LimitCamera();
    }

    void LimitCamera()
    {
        if (boundary == null) return;

        Vector2 boundarySize = boundary.size;
        Vector2 boundaryCenter = boundary.offset;
        Vector2 boundaryPosition = new Vector2(boundary.transform.position.x, boundary.transform.position.y); // 将Vector3转换为Vector2

        Vector2 min = boundaryPosition + new Vector2(boundaryCenter.x - boundarySize.x * 0.5f, boundaryCenter.y - boundarySize.y * 0.5f);
        Vector2 max = boundaryPosition + new Vector2(boundaryCenter.x + boundarySize.x * 0.5f, boundaryCenter.y + boundarySize.y * 0.5f);

        Vector3 cameraP = new Vector3(transform.position.x, transform.position.y,transform.position.z);


        if (cameraP.x < min.x)
        {
            cameraP.x = min.x;
        }
        else if (cameraP.x > max.x)
        {
            cameraP.x = max.x;
        }

        if (cameraP.y < min.y)
        {
            cameraP.y = min.y;
        }
        else if (cameraP.y > max.y)
        {
            cameraP.y = max.y;
        }

        transform.position = cameraP;
    }
    
}