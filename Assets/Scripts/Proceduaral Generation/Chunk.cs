using UnityEngine;
using System.Collections.Generic;
public class Chunk : MonoBehaviour
{
    [Header("Chunk Prefabs")]
    [SerializeField] GameObject fence;
    [SerializeField] GameObject apple;
    [SerializeField] GameObject coin;
    [Header("Lane Settings")]
    [Tooltip("The lanes that the fences can be spawned on")]
    [SerializeField] float[] lanes = { 2.58f, 0.49f, -1.6f };
    [Tooltip("Available lanes for spawning pickups")]
    [SerializeField] List<int> availableLanes = new List<int> { 0, 1, 2 };
    [Header("Spawn Settings for Pickups")]
    [SerializeField] float appleSpawnChance = 0.5f;
    [SerializeField] float coinSpawnChance = 0.5f;
    [SerializeField] float coinSeperanceLength = 2f;

    Health health; // Reference to the Health component
    Scoreboard scoreboard;
    private void Start()
    {
        SpawnFences();
        SpawnApple();
        SpawnCoins();
    }
    public void Init(Health health,Scoreboard scoreboard)
    {
        this.health = health; // Initialize the health reference
        this.scoreboard = scoreboard;
    }
    void SpawnFences()
    {
        int fencesToSpawn = Random.Range(0, lanes.Length); // Randomly choose how many fences to spawn (1 to 3)
        for (int i = 0; i < fencesToSpawn; i++)
        {

            InstaniatePrefab(fence,transform.position.z); // Instantiate the fence prefab
        }

    }
    void SpawnApple()
    {
        if ( Random.value> appleSpawnChance) return;  // Randomly decide whether to spawn an apple based on the chance 
        InstaniatePrefab(apple,transform.position.z);
    }

    void SpawnCoins()
    {
         if (Random.value > coinSpawnChance) return; // Randomly decide whether to spawn coins based on the chance
        int coinsToSpawn = Random.Range(1, 6);
        float ChunkPosZ = transform.position.z + (coinSeperanceLength * 2f);
        Debug.Log(ChunkPosZ);
        for (int i = 0; i < coinsToSpawn; i++)
        {
            float spawnPositionZ = ChunkPosZ - (i * coinSeperanceLength);
            InstaniatePrefab(coin,spawnPositionZ);
        }
        

    }
    private void InstaniatePrefab(GameObject prefab, float positionZ)
    {
        if (availableLanes.Count == 0) return;
        int selectedLane = SelectLane(); // Select a random lane from the available lanes
        Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, positionZ);
        GameObject InstancedObject = Instantiate(prefab, spawnPosition, Quaternion.identity, transform);// Instantiate the prefab at the selected lane and position
        if (prefab.name == "Apple") InstancedObject.GetComponent<Apple>().Init(health);
        
        else if (prefab.name == "Coin") InstancedObject.GetComponent<Coin>().Init(scoreboard);
        
        

    }
    private int SelectLane()
    {
        int randomLaneIndex = Random.Range(0, availableLanes.Count);
        int selectedLane = availableLanes[randomLaneIndex];
        availableLanes.RemoveAt(randomLaneIndex); // Remove the lane to avoid duplicates
        return selectedLane;
    }
}
