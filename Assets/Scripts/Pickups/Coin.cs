using UnityEngine;

public class Coin : Pickup
{
    [SerializeField] int scoreValue = 100;
    Scoreboard scoreboard;
    public void Init(Scoreboard scoreboard)
    {
        this.scoreboard = scoreboard;
    }
    
    protected override void OnPickup()
    {
      
        scoreboard.IncreaseScore(scoreValue);
        transform.Find("Model/SM_Powerup_Coin_Gold").GetComponent<Renderer>().enabled = false; // Disable the renderer to hide the pickup
        Debug.Log("Coin Picked Up");
    }
}
