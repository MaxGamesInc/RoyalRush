using UnityEngine;
using TMPro;
public class Health : MonoBehaviour
{
    [SerializeField] TMP_Text healthText;
    [SerializeField] int maxHealth = 150;
    [SerializeField] int currentHealth = 100;
    [SerializeField] GameManager gameManager;
    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        healthText.text = "Health: 100, Taking hits will reduce your health, and if it reaches 0, you lose the game.";
        UpdateHealthText();
    }
    public void TakeDamage(int amount)
    {
        if (gameManager.IsGameOver) return;
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            gameManager.GameOver();
        }
        UpdateHealthText();
    }
    public void IncreaseHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateHealthText();
    }
    public void UpdateHealthText()
    {
        healthText.text = "Health: " + currentHealth.ToString();
    }

}
