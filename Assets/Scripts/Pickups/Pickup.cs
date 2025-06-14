using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 100f;
      protected AudioSource pickupAudioSource;
    void Start()
    {
            pickupAudioSource = GetComponent<AudioSource>();

    }
    const string PlayerTag = "Player";
    private void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PlayerTag))
        {
            pickupAudioSource.Play();
            Debug.Log("Pickup collected by player");
            OnPickup();
            
            gameObject.GetComponent<Collider>().enabled = false; // Disable the collider to prevent further interactions
            Destroy(gameObject,1f);
        }

    }
    protected abstract void OnPickup();
    
}
