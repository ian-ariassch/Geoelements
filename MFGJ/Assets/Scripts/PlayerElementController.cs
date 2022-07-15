using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct AudioNames
{
    public string name;
    public AudioClip clip;
}

public class PlayerElementController : MonoBehaviour
{

    
    public GameController gc;
    public Camera mainCamera;
    public AudioNames[] audioNames;
    public Dictionary<string, AudioClip> audioDictionary = new Dictionary<string, AudioClip>();
    public float elementSwitchInterval = 5f;
    public GameObject airBall, waterBall;
    public int healthPoints, maxHealthPoints;
    public float invulnerabilityTime = 1f;
    public float airCooldown = 5f;
    public float waterCooldown = 5f;
    public float shakePower = 10f;
    float airCooldownTimer;
    float waterCooldownTimer;

    float invulnerabilityTimer = 0;

    int earthShield = 2;
    float currentTime;
    string[] elements = new string[3] { "Air", "Water", "Earth" };
    string currentElement;

    AudioSource audioSource;

    Animator anim;
    SpriteRenderer playerSprite;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        currentTime = 0;
        foreach(AudioNames audioName in audioNames)
        {
            audioDictionary.Add(audioName.name, audioName.clip);
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;
        airCooldownTimer -= Time.deltaTime;
        waterCooldownTimer -= Time.deltaTime;
        invulnerabilityTimer -= Time.deltaTime;
        if(currentTime < 0) 
        {
            currentElement = PickRandomElement();
            switch (currentElement)
            {
                case "Water":
                    anim.Play("waterAnimation");
                    waterCooldownTimer = 0;
                    break;
                case "Air":
                    anim.Play("windAnimation");
                    airCooldownTimer = 0;
                    break;
                case "Earth":
                    anim.Play("earthFull");
                    earthShield = 2;
                    break;
            }

            currentTime = elementSwitchInterval;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }

        if(invulnerabilityTimer < 0)
        {
            Color tempColor = playerSprite.color;
            tempColor.a = 1;
            playerSprite.color = tempColor;
        }

        if(healthPoints <= 0)
        {
            Death();
        }
    }

    string PickRandomElement() 
    {
        int randomIndex = Random.Range(0, 3);
        return elements[randomIndex];
    }

    public void TakeDamage()
    {
        if(invulnerabilityTimer < 0)
        {
            StartCoroutine("ScreenShake");
            if(currentElement == "Earth")
            {
                if(earthShield == 1)
                {
                    audioSource.PlayOneShot(audioDictionary["earthSecondHit"]);
                    anim.Play("earthIdle");
                    earthShield--;
                }
                else if(earthShield == 2)
                {
                    audioSource.PlayOneShot(audioDictionary["earthFirstHit"]);
                    anim.Play("earthBroken");
                    earthShield--;
                }
                else
                {
                    audioSource.PlayOneShot(audioDictionary["ballImpact"]);
                    healthPoints--;
                }
            }
            else
            {
                audioSource.PlayOneShot(audioDictionary["ballImpact"]);
                healthPoints--;
            }
            Color tempColor = playerSprite.color;
            tempColor.a = 0.5f;
            playerSprite.color = tempColor; 
            invulnerabilityTimer = invulnerabilityTime;
        }
    }

    public void Heal()
    {
        if(healthPoints < maxHealthPoints)
        healthPoints++;
    }

    void Death()
    {
        gameObject.transform.root.gameObject.SetActive(false);
        gc.StartCoroutine("GameOver");
    }

    IEnumerator ScreenShake()
    {
        Vector3 originalPosition = mainCamera.transform.position;
        Vector3 temp;
        for(int i = 0; i < 30; i++){
            temp = originalPosition;
            temp.x += Random.Range(-0.1f, 0.1f) * shakePower;
            temp.y += Random.Range(-0.1f, 0.1f) * shakePower;
            mainCamera.transform.position = temp;
            yield return new WaitForSeconds(0.01f);
            mainCamera.transform.position = originalPosition;
        }
    }

    public void Shoot()
    {
            switch (currentElement) 
            {
                case "Air":
                    if(airCooldownTimer < 0)
                    {
                        audioSource.PlayOneShot(audioDictionary["shootAir"]);
                        GameObject spawnedAirBall = Instantiate(airBall, transform.position, Quaternion.identity);
                        Rigidbody2D airballRb = spawnedAirBall.GetComponent<Rigidbody2D>();
                        airballRb.mass = 100000;
                        airballRb.gravityScale = 0;
                        airballRb.AddForce(transform.up * airballRb.mass * 1000);
                        anim.Play("windIdle");
                        airCooldownTimer = airCooldown;
                    }
                    break;
                case "Water":
                    if(waterCooldownTimer < 0)
                    {
                        audioSource.PlayOneShot(audioDictionary["shootWater"]);
                        GameObject spawnedWaterBall = Instantiate(waterBall, transform.position, Quaternion.identity);
                        Rigidbody2D waterBallRb = spawnedWaterBall.GetComponent<Rigidbody2D>();
                        waterBallRb.AddForce(transform.up * waterBallRb.mass * 500);
                        anim.Play("waterIdle");
                        waterCooldownTimer = waterCooldown;
                    }
                    break;
            }
    }
}
