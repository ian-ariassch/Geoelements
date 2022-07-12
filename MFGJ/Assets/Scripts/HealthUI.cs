using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public GameController gc;
    public List<Image> hearts;
    public List<Image> Outlines;
    public PlayerElementController playerElController;
    // Start is called before the first frame update
    void Start()
    {
        GetPlayerController();
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        StartCoroutine("WaveAnimation");
    }

    // Update is called once per frame
    void Update()
    {
        if(!gc.paused){
        for(int i = 0; i < hearts.Count; i++)
        {
            if(i+1 <= playerElController.healthPoints)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled= false;
            }
        }
        }
    }

    public void GetPlayerController()
    {
        playerElController = GameObject.FindGameObjectWithTag("PlayerElementController").GetComponent<PlayerElementController>();
    }

    IEnumerator WaveAnimation()
    {
        while(true){
            foreach(Image heart in hearts)
            {
                heart.gameObject.GetComponent<Animator>().Play("Jump");
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
