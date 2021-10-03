using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Block : MonoBehaviour
{
    // 游戏系统
    public GameSystem gameSystem;
    // 是否固定
    public bool fix;
    public Rigidbody2D rb;
    // 重力
    public float gravity;
    // 高度统计计时器
    public float timer;
    // 保存高度
    public float savedHeight;
    // Start is called before the first frame update
    void Start()
    {
        fix = true;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(fix)
        {
            rb.velocity = new Vector2(0,0);
        }
        else
        {
            if(Mathf.Abs(transform.position.y - gameSystem.downBorderY - savedHeight) < 0.001f)
            {
                timer += Time.deltaTime;
                if(timer >= 0.5f)
                {
                    if(savedHeight > gameSystem.currentHeight)
                    {
                        gameSystem.currentHeight = savedHeight;
                    }
                }
            }
            else
            {
                timer = 0;
                savedHeight = transform.position.y - gameSystem.downBorderY;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col) 
    {
        if (col.gameObject.CompareTag("Ball")) 
        {
            if(fix)
            {
                fix = false;
                rb.gravityScale = gravity;
                gameSystem.blockNumber --;
            }
        }
    }
}
