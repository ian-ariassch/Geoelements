using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public GameController gc;
    public List<GameObject> spawners;
    public GameObject blockSpawner;
    public int blockCounter = 1;
    public int maxSpawns = 2;
    public float spawnInterval;
    public float generalGravity;

    public float maxGravity = 4f, minSpawnInterval = 0.325f;
    public float gravityIncreasePerBlock = 0.0125f;
    public float spawnIntervalDecreasePerBlock = 0.0125f;

    public bool canSpawnNew = false;

    public float minRange = -8, maxRange = 8;

    float lastSpawnerPosition;
    float lastX = 0;
    
    float startingGravity, startingSpawnInterval;
    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        InitValues();
    }

    // Update is called once per frame
    void Update()
    {
        MoveSpawners();
        if ((blockCounter%10 == 0) && spawners.Count < maxSpawns && canSpawnNew)
        {
            SpawnNewSpawner();
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

    void SpawnNewSpawner()
    {
        GameObject tempSpawner = Instantiate(blockSpawner, transform.position, Quaternion.identity, this.transform);
        canSpawnNew = false;
        spawners.Add(tempSpawner);
        tempSpawner.GetComponent<BlockSpawner>().gravity = generalGravity;
        tempSpawner.GetComponent<BlockSpawner>().spawnInterval = spawnInterval;
        tempSpawner.GetComponent<BlockSpawner>().maxGravity = maxGravity;
        tempSpawner.GetComponent<BlockSpawner>().minSpawnInterval = minSpawnInterval;
        tempSpawner.GetComponent<BlockSpawner>().gravityIncreasePerBlock = gravityIncreasePerBlock;
        tempSpawner.GetComponent<BlockSpawner>().spawnIntervalDecreasePerBlock = spawnIntervalDecreasePerBlock;
        foreach (GameObject spawner in spawners) 
        {
            spawner.GetComponent<BlockSpawner>().currentTime = spawner.GetComponent<BlockSpawner>().spawnInterval;
        }
    }

    void MoveSpawners()
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
    }

    void InitValues()
    {
        GameObject tempSpawner = Instantiate(blockSpawner, transform.position, Quaternion.identity, this.transform);
        tempSpawner.GetComponent<BlockSpawner>().gravity = generalGravity;
        tempSpawner.GetComponent<BlockSpawner>().spawnInterval = spawnInterval;
        tempSpawner.GetComponent<BlockSpawner>().maxGravity = maxGravity;
        tempSpawner.GetComponent<BlockSpawner>().minSpawnInterval = minSpawnInterval;
        tempSpawner.GetComponent<BlockSpawner>().gravityIncreasePerBlock = gravityIncreasePerBlock;
        tempSpawner.GetComponent<BlockSpawner>().spawnIntervalDecreasePerBlock = spawnIntervalDecreasePerBlock;
        spawners.Add(tempSpawner);
        startingGravity = generalGravity;
        startingSpawnInterval = spawnInterval;

    }
    public void RestartValues()
    {
        blockCounter = 1;
        canSpawnNew = false;
        foreach (GameObject spawner in spawners)
        {
            Destroy(spawner);
        }
        spawners.Clear();
        generalGravity = startingGravity;
        spawnInterval = startingSpawnInterval;
        InitValues();
    }
}
