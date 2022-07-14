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
    public float score;
    public bool paused;
    public GameObject scoreUI;
    public GameObject highScoreUI;

    bool canRestart;
    void Start()
    {
        score = 0;
        canRestart = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!paused){
        score += Time.deltaTime;
        }
        scoreUI.GetComponent<TMP_Text>().text = "Score: " + score.ToString("F0");
        if(Input.anyKeyDown && canRestart)
        {
            restartGame();
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
        player.GetComponentInChildren<PlayerElementController>().healthPoints = 3;
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
