using System.Timers;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;// 刚体
    private BoxCollider2D collider;// 检测碰撞
    private Animator animator;// 动画
    private SpriteRenderer spriteRenderer;// 用于切换角色方向
    public MumbleController mumbleController;

    public float speed;// 移动速度
    float xVelocity;// 接受左右移动值
    public float jumpSpeed;
    public bool isGrounded;
    public LayerMask ground;
    public LayerMask prologueArea;// 开场白触发点
    public LayerMask endArea;// 梦泡触发点
    public LayerMask ghostArea;// 遇见幽灵触发点
    
    public float idleTimeBeforeSleep = 3f; // Idle状态下持续的时间，单位为秒
    private float idleTimer = 0f; // 计时器

    public float fallMultiplier = 2.5f;// 加快下落速率

    // Start is called before the first frame update
    void Start()
    {
        mumbleController = GameObject.Find("Elf").GetComponent<MumbleController>();
        rb2d = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && isGrounded) //跳跃
        {
            isGrounded = false;
            rb2d.AddForce(new Vector2(0f, jumpSpeed), ForceMode2D.Impulse);
            AudioManager.PlayJumpClip();
        }
        PhysicsCheck();
        Movement();
        UpdateIdleTimer();
        ChangeDirection();
        PrologueShow();
        EndShow();
        GhostShow();
    }

    void PhysicsCheck()
    {
        isGrounded = Physics2D.IsTouchingLayers(collider, ground);
        animator.SetBool("isGrounded", isGrounded);
        if (rb2d.velocity.y < 0) // 如果在下落
            rb2d.velocity += Vector2.up * (Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime);//加快下落速率
    }

    void Movement()
    {
        xVelocity = Input.GetAxis("Horizontal"); // 右返回1，左返回-1
        rb2d.velocity = new Vector2(xVelocity * speed, rb2d.velocity.y);
        animator.SetFloat("moveSpeed", Mathf.Abs(rb2d.velocity.x));
    }

    void UpdateIdleTimer()
    {
        // 如果角色不在移动，增加计时器
        if (rb2d.velocity.x == 0 && isGrounded)
        {
            idleTimer += Time.deltaTime;
        }
        else
        {
            idleTimer = 0f; // 重置计时器
            SetSleeping(false); // 并且设置参数为false
        }

        // 检查是否达到指定的Idle时间
        if (idleTimer >= idleTimeBeforeSleep)
        {
            SetSleeping(true);
        }
    }

    void SetSleeping(bool value)
    {
        animator.SetBool("isSleeping", value);
    }
    void ChangeDirection()
    {
        if (rb2d.velocity.x < 0) //向左移动
        {
            spriteRenderer.flipX = true;
        }
        else if (rb2d.velocity.x > 0) //向右移动（当==0时不改变当前朝向）
        {
            spriteRenderer.flipX = false;
        }
    }
    void PrologueShow()
    {
        if(mumbleController == null)
            Debug.LogError("MumbleController is null");
        if (Physics2D.IsTouchingLayers(collider, prologueArea))// 如果到达开场白触发点
            mumbleController.ShowPrologue();
    }

    void EndShow()
    {
        if (Physics2D.IsTouchingLayers(collider, endArea))
            mumbleController.ShowEnd();
    }

    void GhostShow()
    {
        if(Physics2D.IsTouchingLayers(collider, ghostArea))
            mumbleController.ShowGhost();
    }
}
