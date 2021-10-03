using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownBorder : MonoBehaviour
{
    // 游戏系统
    public GameSystem gameSystem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col) 
    {
        if (col.gameObject.CompareTag("Ball")) 
        {
            gameSystem.BallOut();
        }
    }
}
