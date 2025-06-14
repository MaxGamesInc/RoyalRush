using UnityEngine;

public class Apple : Pickup
{
    [SerializeField] int healthToIncrease = 20; // Amount of health to restore
    Health health;
    public void Init(Health health)
    {
        this.health = health;
    }
    protected override void OnPickup()
    {

        health.IncreaseHealth(healthToIncrease); // Increase health by the specified amount
        transform.Find("Model/SM_Applesm").GetComponent<Renderer>().enabled = false; // Disable the renderer to hide the pickup
        Debug.Log("Apple Picked Up");
    }
}
