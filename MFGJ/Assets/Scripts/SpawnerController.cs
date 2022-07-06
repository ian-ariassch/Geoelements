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

    public bool canSpawnNew = false;

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
            BlockSpawner spawnerBlockSpawnerComp = spawner.GetComponent<BlockSpawner>();
            if (!spawnerBlockSpawnerComp.gotMoved)
            {
                float newX = Random.Range(minRange, maxRange);
                while (CheckIfTooCloseToLast(newX))
                {
                    newX = Random.Range(minRange, maxRange);
                }
                Debug.Log(newX);
                Debug.Log("LastX" + (lastX));
                MoveToPosition(spawner, newX);
                spawnerBlockSpawnerComp.gotMoved = true;
                lastX = newX;
            }
        }
        if ((blockCounter%10 == 0) && spawners.Count < maxSpawns && canSpawnNew)
        {
            GameObject tempSpawner = Instantiate(blockSpawner, transform.position, Quaternion.identity, this.transform);
            canSpawnNew = false;
            spawners.Add(tempSpawner);
            tempSpawner.GetComponent<BlockSpawner>().gravity = generalGravity;
            tempSpawner.GetComponent<BlockSpawner>().spawnInterval = spawnInterval;
            foreach (GameObject spawner in spawners) 
            {
                spawner.GetComponent<BlockSpawner>().currentTime = spawner.GetComponent<BlockSpawner>().spawnInterval;
            }
        }
    }

    bool CheckIfTooCloseToLast(float x)
    {
        return Mathf.Abs(x - lastX) < 5f;
    }

    void MoveToPosition(GameObject spawner, float newX) 
    {
        spawner.transform.position = new Vector2(newX, transform.position.y);
    }
}
