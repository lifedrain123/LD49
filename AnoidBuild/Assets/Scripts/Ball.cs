using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // 游戏系统
    public GameSystem gameSystem;
    // 速度
    public float speed;
    // 加速度
    public float acc;
    public Rigidbody2D rb;
    // 卡住时间
    public float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = rb.velocity.normalized * speed;
        if(Mathf.Abs(rb.velocity.normalized.y) < 0.05)
        {
            timer += Time.deltaTime;
            if(timer > 3f)
            {
                rb.velocity = (rb.velocity.normalized + new Vector2(0,-0.2f)) * speed;
                timer = 0;
            }
        }
        else
        {
            timer = 0;
        }
    }

    void JudgeOut()
    {
        if(transform.position.x < -101 || transform.position.x > 51 || transform.position.y > 101 || transform.position.y < -101)
        {
            gameSystem.BallOut();
        }
    }

    void OnCollisionEnter2D(Collision2D col) 
    {
        speed += acc;
    }
}
