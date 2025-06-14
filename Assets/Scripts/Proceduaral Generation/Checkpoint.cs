using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] float timeToExtend = 5f;
    [SerializeField] float spawnIntervalDecrease = 0.25f; // Amount to decrease spawn interval
    GameManager gameManager;
    ObstacleSpawner obstacleSpawner;
    AudioSource checkpointAudioSource;
    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        obstacleSpawner = FindFirstObjectByType<ObstacleSpawner>();
        checkpointAudioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.IncreaseTime(timeToExtend); // Increase time by 5 seconds when player reaches the checkpoint
            obstacleSpawner.DecreaseSpawnInterval(spawnIntervalDecrease); // Decrease spawn interval by the specified amount
            checkpointAudioSource.Play(); // Play the checkpoint audio
        }
        
    }
 }
