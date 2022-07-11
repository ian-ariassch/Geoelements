using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public SpawnerController sc;
    public HealthUI healthController;
    public GameObject player;
    public List<GameObject> blocks;
    public GameObject gameOverText;
    public bool paused;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown && paused)
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
    }

    IEnumerator GameOver()
    {
        gameOverText.SetActive(true);
        yield return new WaitForSeconds(3f);
        paused = true;
    }
}
