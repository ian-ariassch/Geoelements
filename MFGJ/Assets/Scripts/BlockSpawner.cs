using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public float spawnInterval = 5f;
    public float gravity = 1f;
    public GameObject spawnableObject;
    public int blockCounter = 0;

    private SpawnerController spawnerController;
    public float currentTime;
    void Start()
    {
        currentTime = spawnInterval;
        spawnerController = GetComponentInParent<SpawnerController>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;
        if (currentTime < 0)
        {
            GameObject block = Instantiate(spawnableObject, transform.position, Quaternion.identity);
            block.GetComponent<Rigidbody2D>().gravityScale = gravity;

            spawnerController.blockCounter++;

            if (gravity < 3f)
            {
                gravity += 0.1f;
            }

            if (spawnInterval > 0.5f) 
            {
                spawnInterval -= 0.1f;
            }
            currentTime = spawnInterval;
            
        }
    }
}
