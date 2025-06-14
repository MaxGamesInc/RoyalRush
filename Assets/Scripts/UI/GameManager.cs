using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] PlayerController playerController;
    [SerializeField] TMP_Text timeText;
    [SerializeField] TMP_Text finalScoreText;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] float startTime = 5f;
    [SerializeField] int finalTimeMultiplier = 10; // Multiplier for final score based on remaining time
    float timeSpent;
    bool isGameOver = false;

    public bool IsGameOver => isGameOver;

    Scoreboard scoreboard;
    private void Start()
    {
        timeSpent = 0f;
        gameOverPanel.SetActive(false);
        finalScoreText.text = "";
        scoreboard = FindFirstObjectByType<Scoreboard>();
    }
    void Update()
    {
        IncreaseTime();
    }



    public void GameOver()
    {
        isGameOver = true;
        playerController.enabled = false; // Disable player controls
        gameOverPanel.SetActive(true); 
        CalculateFinalScore();
        timeSpent = 0f;
        Time.timeScale = 0.1f; // Pause the game
        float delay = 0.5f;
        Invoke("ReloadScene", delay); // Reload the scene after a delay
    }
    public void IncreaseTime(float amount)
    {
        if (isGameOver) return;
        timeSpent += amount;
    }
    void IncreaseTime()
    {
        if (isGameOver) return;
        timeSpent += Time.deltaTime;
        timeText.text = "Time spent :" + timeSpent.ToString("F2");
    }
    void ReloadScene()
    {
        // Delay before reloading the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f; // Reset time scale
        Debug.Log("Scene reloaded");
    }
    void CalculateFinalScore()
    {
        // This method can be used to calculate the final score based on various factors
        // For now, it just returns the current score from the scoreboard
        int finalScore = scoreboard.score + (Mathf.FloorToInt(timeSpent) * finalTimeMultiplier); // Example calculation: score multiplied by remaining time
        scoreboard.score = Mathf.Max(finalScore, 0); // Ensure score is not negative
        finalScoreText.text = "Final Score: " + scoreboard.score.ToString();
    }
}
