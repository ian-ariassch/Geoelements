using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    public int speed = 3;
    Vector2 direction;
    

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"),0);
    }

    private void FixedUpdate()
    {
        Move(direction);
    }

    private void Move(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + direction * speed * Time.deltaTime);
    }
}
