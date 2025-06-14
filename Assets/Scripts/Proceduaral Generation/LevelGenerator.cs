using UnityEngine;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{
    [Header("Level Generation References")]
    [SerializeField] GameObject noObstacleChunkPrefab; // Prefab for the first chunk with no obstacles
    [SerializeField] GameObject[] chunkPrefabs;
    [Tooltip("The checkpoint prefab to spawn at regular intervals")]
    [SerializeField] GameObject[] checkpointPrefabs;
    [SerializeField] Transform chunkParent;
    [SerializeField] CameraController cameraController;
    [SerializeField] Scoreboard scoreboard;
    [SerializeField] Health health;
    [Header("Level Generation Settings")]
    [Tooltip("The amount of chunks to spawn at the start of the game")]
    [SerializeField] int startingChunksAmount = 12;
    [SerializeField] int checkpointInterval = 8; // Interval at which checkpoints are spawned
    [Tooltip("The length of each chunk, used for calculating spawn positions")]
    [SerializeField] float chunkLength; // Default chunk length
    
    [SerializeField] float moveChunkSpeed = 8f;
    [SerializeField] float minMoveSpeed = 2f;
    [SerializeField] public float maxMoveSpeed = 10f;
    [Header("Chunk List")]
    [SerializeField]List<GameObject> chunks = new List<GameObject>();

    int chunksSpawned = 0;
      void Start()
    {
        cameraController = FindFirstObjectByType<CameraController>();
        SpawnStartingChunks();
    }

    // Update is called once per frame
    void Update()
    {
        MoveChunks();

    }
    void SpawnStartingChunks()
    {
        for (int i = 0; i < startingChunksAmount; i++)
        {
            SpawnChunk();
        }


    }
    public void ChangeChunkSpeed(float speedAmount)
    {
        moveChunkSpeed += speedAmount;
        moveChunkSpeed = Mathf.Clamp(moveChunkSpeed, minMoveSpeed, maxMoveSpeed);
        Debug.Log(moveChunkSpeed);
        
        cameraController.ChangeFOV(speedAmount);
    }
    private void SpawnChunk()
    {
        float spawnPositionZ = CalculateSpawnPositionZ();
        Vector3 spawnPosition = new Vector3(0f, 0f, spawnPositionZ);
        GameObject newChunk = Instantiate(ChunkToSpawn(), spawnPosition, Quaternion.identity, chunkParent);
        newChunk.GetComponent<Chunk>().Init(health, scoreboard); // Initialize the chunk with a reference to this LevelGenerator
        chunks.Add(newChunk);
        chunksSpawned++;
      
    }
    private GameObject ChunkToSpawn()
    {
        if (chunksSpawned == 0)
        {
            return noObstacleChunkPrefab; // Return an empty chunk prefab if no chunks have been spawned yet
        }
        else if (chunksSpawned % checkpointInterval == 0 && chunksSpawned != 0)
            {
                return checkpointPrefabs[Random.Range(0, checkpointPrefabs.Length)];
            }

            else
            {
                return chunkPrefabs[Random.Range(0, chunkPrefabs.Length)];
            }
    }
    float CalculateSpawnPositionZ()
    {
        if (chunks.Count == 0)
        {
           
            return transform.position.z; // Start at the origin if no chunks exist
        }
        else
        {
            {
                chunkLength = chunkPrefabs[0].transform.Find("Model/Chunk").GetComponent<Renderer>().bounds.size.z;
                return chunks[chunks.Count - 1].transform.position.z + chunkLength;
            }
           
        }
    }
    void MoveChunks()
    {
        for (int i = 0; i < chunks.Count; i++) // Example: move 10 chunks
        {
            GameObject chunk = chunks[i];
            chunk.transform.Translate(Vector3.back * moveChunkSpeed * Time.deltaTime);
            if (chunk.transform.position.z <= Camera.main.transform.position.z - chunkLength)
            {
                chunks.Remove(chunk);
                Destroy(chunk);
                SpawnChunk(); // Spawn a new chunk when one is destroyed

            }
        }
    }
}
