using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    public int speed = 3;
    Vector2 direction;
    

    void Start()
    {
        // rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        foreach(Touch touch in Input.touches)
        {
            if(!IsPointerOverUIObject())
            {
                getTouchDirection(touch);
            }
        }
        if(Input.touchCount < 1)
        {
            direction = new Vector2(0, 0);
        }
    }

    private void FixedUpdate()
    {
        Move(direction);
    }

    private void Move(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + direction * speed * Time.deltaTime);
    }

    private void getTouchDirection(Touch touch)
    {
        if(touch.position.x < Screen.width/2)
        {
            direction = new Vector2(-1, 0);
        }
        else
        {
            direction = new Vector2(1, 0);
        }
    }

    private bool IsPointerOverUIObject() {
         PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
         eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
     List<RaycastResult> results = new List<RaycastResult>();
     EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
     return results.Count > 0;
 }
}
