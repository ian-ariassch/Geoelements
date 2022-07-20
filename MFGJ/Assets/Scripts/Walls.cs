using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject leftWall, rightWall;
    Vector3 stageDimensions;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,0));
        leftWall.transform.position = new Vector3(-stageDimensions.x, -4.5f, 0);
        rightWall.transform.position = new Vector3(stageDimensions.x, -4.5f, 0);
    }
}
