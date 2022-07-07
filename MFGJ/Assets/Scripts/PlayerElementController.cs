using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerElementController : MonoBehaviour
{
    public float elementSwitchInterval = 5f;
    public GameObject airBall, waterBall;
    public int healthPoints, maxHealthPoints;

    int earthShield = 2;
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
            currentElement = PickRandomElement();
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
                    earthShield = 2;
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
                    airballRb.AddForce(transform.up * airballRb.mass * 1000);
                    break;
                case "Water":
                    GameObject spawnedWaterBall = Instantiate(waterBall, transform.position, Quaternion.identity);
                    Rigidbody2D waterBallRb = spawnedWaterBall.GetComponent<Rigidbody2D>();
                    waterBallRb.AddForce(transform.up * waterBallRb.mass * 1000);
                    break;
            }
        }

        if(healthPoints <= 0)
        {
            Destroy(gameObject);
        }
    }

    string PickRandomElement() 
    {
        int randomIndex = Random.Range(0, 3);
        return elements[randomIndex];
    }

    public void TakeDamage()
    {
        if(currentElement == "Earth")
        {
            if(earthShield == 1)
            {
                playerSprite.color = Color.yellow;
                earthShield--;
            }
            else if(earthShield == 2)
            {
                playerSprite.color = Color.red;
                earthShield--;
            }
            else
            {
                healthPoints--;
            }
        }
        else
        {
            healthPoints--;
        }
    }

    public void Heal()
    {
        if(healthPoints < maxHealthPoints)
        healthPoints++;
    }
}
