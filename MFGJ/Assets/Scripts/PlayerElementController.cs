using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerElementController : MonoBehaviour
{
    public float elementSwitchInterval = 5f;
    public GameObject airBall;


    float currentTime;
    string[] elements = new string[3] { "Water", "Air", "Earth" };
    string currentElement;

    SpriteRenderer playerSprite;

    // Start is called before the first frame update
    void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>();
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;
        if(currentTime < 0) 
        {
            currentElement = pickRandomElement();
            switch (currentElement)
            {
                case "Water":
                    playerSprite.color = Color.cyan;
                    break;
                case "Air":
                    playerSprite.color = Color.gray;
                    break;
                case "Earth":
                    playerSprite.color = Color.green;
                    break;
            }

            currentTime = elementSwitchInterval;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            switch (currentElement) 
            {
                case "Air":
                    GameObject spawnedAirBall = Instantiate(airBall, transform.position, Quaternion.identity);
                    Rigidbody2D airballRb = spawnedAirBall.GetComponent<Rigidbody2D>();
                    airballRb.mass = 100000;
                    airballRb.gravityScale = 0;
                    airballRb.AddForce(transform.up * 100000000);
                    break;

            }
        }
    }

    string pickRandomElement() 
    {
        int randomIndex = Random.Range(0, 3);
        return elements[randomIndex];
    }
}
