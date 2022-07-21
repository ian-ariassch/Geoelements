using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public SpawnerController sc;
    public AudioSource audioSource;
    public HealthUI healthController;
    public GameObject player;
    public List<GameObject> blocks;
    public GameObject gameOverText;
    public GameObject restartText;
    public GameObject howToPlay;
    public float score;
    public bool paused;
    public GameObject scoreUI;
    public GameObject highScoreUI;

    PlayerElementController playerElementController;

    bool canRestart, gameStarted;
    void Start()
    {
        score = 0;
        sc.gameObject.SetActive(false);
        howToPlay.SetActive(true);
        gameStarted = false;
        playerElementController = player.GetComponentInChildren<PlayerElementController>();
        playerElementController.healthPoints = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if(!paused && gameStarted){
        score += Time.deltaTime;
        }
        scoreUI.GetComponent<TMP_Text>().text = "Score: " + score.ToString("F0");
        if(Input.anyKeyDown)
        {
            if(canRestart)
            restartGame();
            else if(!gameStarted)
            {
                howToPlay.SetActive(false);
                gameStarted = true;
                sc.gameObject.SetActive(true);
                restartGame();
            }
        }
    }

    void restartGame()
    {   
        foreach(GameObject block in blocks)
        {
            Destroy(block);
        }
        sc.RestartValues();
        player.transform.position = new Vector3(0, -3.65f, 0);
        playerElementController.healthPoints = 3;
        playerElementController.paused = false;
        playerElementController.currentTime = -1;
        player.SetActive(true);
        healthController.GetPlayerController();
        paused = false;
        score = 0;
        canRestart = false;
        // scoreUI.SetActive(true);
        gameOverText.SetActive(false);
        highScoreUI.SetActive(false);
        restartText.SetActive(false);
    }

    IEnumerator GameOver()
    {
        audioSource.PlayOneShot(audioSource.clip);
        updateHighScore();
        gameOverText.SetActive(true);
        // scoreUI.SetActive(false);
        highScoreUI.GetComponent<TMP_Text>().text = "High Score\n" + PlayerPrefs.GetFloat("HighScore").ToString("F0");
        highScoreUI.SetActive(true);
        paused = true;
        yield return new WaitForSeconds(3f);
        restartText.SetActive(true);
        canRestart = true;
    }

    void updateHighScore()
    {
        if(score > PlayerPrefs.GetFloat("HighScore"))
        {
            PlayerPrefs.SetFloat("HighScore", score);
        }
    }
}
