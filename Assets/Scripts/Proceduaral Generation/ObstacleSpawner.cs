using UnityEngine;
using System.Collections;
public class ObstacleSpawner : MonoBehaviour
{
    [Header("Obstacle Spawner References")]
    [Tooltip("Array of obstacle prefabs to spawn")]
    [SerializeField] GameObject[] obstaclePrefabs;
    [SerializeField] Transform ObstacleParent;
    [Header("Obstacle Spawner Settings")]
    [Tooltip("Time interval between obstacle spawns")]
    [SerializeField] float spawnInterval = 3f;
    [Tooltip("Width of the spawn area for obstacles")]
    [SerializeField] float minSpawnInterval = 0.5f; // Minimum spawn interval to prevent too frequent spawns
    [SerializeField] float spawnWidth = 4f;
 
    void Start()
    {
        StartCoroutine(SpawnObstacles());
    }
    public void DecreaseSpawnInterval(float amount)
    {
        spawnInterval -= amount;
        if(spawnInterval <= minSpawnInterval) // Ensure the spawn interval doesn't go below a minimum threshold
        {
            spawnInterval = minSpawnInterval;
        }
        
    }
    IEnumerator SpawnObstacles()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval); // Adjust the delay as needed
            GameObject obstacle = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnWidth, spawnWidth), 5f, transform.position.z);
            Instantiate(obstacle, spawnPosition, Random.rotation,ObstacleParent);
            
        }
    }
}
