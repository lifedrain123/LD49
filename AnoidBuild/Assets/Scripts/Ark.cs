using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ark : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        move();
    }

    public void move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        //Vector2 direction = Input.GetAxisRaw("Horizontal") * Vector2.right + Input.GetAxisRaw("Vertical") * Vector2.up;
        //rb.AddForce(Vector2.right * direction * speed);
        Vector2 direction = Input.GetAxisRaw("Horizontal") * Vector2.right;
        rb.velocity = direction * speed;
    }

    void OnCollisionEnter2D(Collision2D col) 
    {
        if (col.gameObject.CompareTag("Ball")) 
        {
            float x = (col.transform.position.x - transform.position.x) / GetComponent<Collider2D>().bounds.size.x ;
            Vector2 dir = new Vector2(x, 1).normalized;
            col.gameObject.GetComponent<Rigidbody2D>().velocity = dir * speed;
        }
    }
}
