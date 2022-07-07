using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBehavior : MonoBehaviour
{
    BoxCollider2D blockCollider;
    bool healing = false;
    // Start is called before the first frame update
    void Start()
    {
        blockCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Water")
        {
            Destroy(other.gameObject);
            healing = true;
            GetComponent<SpriteRenderer>().color = Color.cyan;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(healing)
            {
                other.gameObject.GetComponent<PlayerElementController>().Heal();
                Destroy(gameObject);
            }
            else
            {
                other.gameObject.GetComponent<PlayerElementController>().TakeDamage();
            }
        }
    }
}
