using UnityEngine;
using TMPro;
public class Scoreboard : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;

    [SerializeField]  public int score = 0;

    [SerializeField] GameManager gameManager;
    void Start()
    {
        scoreText.text = "Collect coins to Earn Score  ";
    }
    public void IncreaseScore(int amount)
    {
        if (gameManager.IsGameOver) return;
        score += amount;
        scoreText.text = "Coins collected: " + score.ToString();
    }
}
