using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameController gc;
    public float spawnInterval = 5f;
    public float gravity = 1f;
    public GameObject spawnableObject;
    public int blockCounter = 0;
    public float currentTime;
    public bool gotMoved;

    public float maxGravity = 4f, minSpawnInterval = 0.325f;
    public float gravityIncreasePerBlock = 0.0125f;
    public float spawnIntervalDecreasePerBlock = 0.0125f;

    private SpawnerController spawnerController;

    void Start()
    {
        currentTime = spawnInterval;
        spawnerController = GetComponentInParent<SpawnerController>();
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;
        if (currentTime < 0 && gotMoved)
        {
            GameObject block = Instantiate(spawnableObject, transform.position, Quaternion.identity);
            gc.blocks.Add(block);
            block.GetComponent<Rigidbody2D>().gravityScale = gravity;

            spawnerController.blockCounter++;
            spawnerController.canSpawnNew = true;

            if (gravity < maxGravity)
            {
                gravity += gravityIncreasePerBlock;
            }

            if (spawnInterval > minSpawnInterval) 
            {
                spawnInterval -= spawnIntervalDecreasePerBlock;
            }
            currentTime = spawnInterval;
            gotMoved = false;
            
        }
    }
}
