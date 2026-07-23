using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float maxHP;
    [SerializeField] float nowHP;

    Rigidbody2D rb;

    Vector2 dir;

    float moveSpeed;

    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        PlayerUiManger.instance.SetHPBar(nowHP, maxHP);
    }

    private void OnEnable()
    {
        moveSpeed = 5f;
    }

    void Update()
    {
        dir = Vector2.zero;

        if (Keyboard.current.wKey.isPressed)
        {
            dir += Vector2.up;
        }
        if (Keyboard.current.aKey.isPressed)
        {
            dir += Vector2.left;
        }
        if (Keyboard.current.sKey.isPressed)
        {
            dir += Vector2.down;
        }
        if (Keyboard.current.dKey.isPressed)
        {
            dir += Vector2.right;
        }

        dir = dir.normalized;
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = dir * moveSpeed;
    }

    bool SetFlip()
    {
        return dir.x < 0;
    }
    public void TakeDamage(float dmg)
    {
        nowHP -= dmg;
        PlayerUiManger.instance.SetHPBar(nowHP, maxHP);
        if (nowHP <= 0)
        {
            nowHP = 0;
            Die();
        }
    }
    void Die()
    {
        GameManager.instance.GameOver();
    }

}
