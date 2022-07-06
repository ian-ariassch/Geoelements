using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public List<GameObject> spawners;
    public GameObject blockSpawner;
    public int blockCounter = 1;
    public int maxSpawns = 2;
    public float spawnInterval;
    public float generalGravity;

    public float minRange = -8, maxRange = 8;

    float lastSpawnerPosition;
    float lastX = 0;
    // Start is called before the first frame update
    void Start()
    {
        GameObject tempSpawner = Instantiate(blockSpawner, transform.position, Quaternion.identity, this.transform);
        tempSpawner.GetComponent<BlockSpawner>().gravity = generalGravity;
        tempSpawner.GetComponent<BlockSpawner>().spawnInterval = spawnInterval;
        spawners.Add(tempSpawner);
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject spawner in spawners)
        {
            float newX = Random.Range(minRange, maxRange);
            while (checkIfTooCloseToLast(newX))
            {
                newX = Random.Range(minRange, maxRange);
            }
            moveToPosition(spawner, newX);
            lastX = newX;
        }
        if ((blockCounter == 10 || blockCounter == 20) && spawners.Count < maxSpawns)
        {
            GameObject tempSpawner = Instantiate(blockSpawner, transform.position, Quaternion.identity, this.transform);
            spawners.Add(tempSpawner);
            tempSpawner.GetComponent<BlockSpawner>().gravity = generalGravity;
            tempSpawner.GetComponent<BlockSpawner>().spawnInterval = spawnInterval;
            foreach (GameObject spawner in spawners) 
            {
                spawner.GetComponent<BlockSpawner>().currentTime = spawner.GetComponent<BlockSpawner>().spawnInterval;
            }
        }
    }

    bool checkIfTooCloseToLast(float x)
    {
        return Mathf.Abs(x - lastX) < 5f;
    }

    void moveToPosition(GameObject spawner, float newX) 
    {
        spawner.transform.position = new Vector2(newX, transform.position.y);
    }
}
