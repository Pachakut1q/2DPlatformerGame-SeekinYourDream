using UnityEngine;

public class Death : MonoBehaviour
{
    private int VoidLayer;
    private int EnemyLayer;
    // Start is called before the first frame update
    void Start()
    {
        VoidLayer = LayerMask.NameToLayer("Void");
        EnemyLayer = LayerMask.NameToLayer("Enemies");
    }

    private void OnTriggerEnter2D(Collider2D other) //当另一个对象进入附加到该对象的触发碰撞体时发送（仅限 2D 物理）
    {
        if (other.gameObject.layer == VoidLayer) //当落入虚空和碰到地刺时都会触发这个条件
            GameManager.PlayerDeath1();
        else if (other.gameObject.layer == EnemyLayer)
            GameManager.PlayerDeath2();
    }
}
