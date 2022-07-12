using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public SpawnerController sc;
    public HealthUI healthController;
    public GameObject player;
    public List<GameObject> blocks;
    public GameObject gameOverText;
    public float score;
    public bool paused;
    public GameObject scoreUI;

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
            gameOverText.SetActive(false);
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
        Instantiate(player, new Vector3(0, -3.65f, 0), Quaternion.identity);
        healthController.GetPlayerController();
        paused = false;
        score = 0;
        canRestart = false;
    }

    IEnumerator GameOver()
    {
        gameOverText.SetActive(true);
        paused = true;
        yield return new WaitForSeconds(3f);
        canRestart = true;
    }
}
