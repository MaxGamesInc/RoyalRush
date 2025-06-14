using UnityEngine;
using Unity.Cinemachine;
public class Rock : MonoBehaviour
{
    [SerializeField] float shakeModifier = 2f;
    [SerializeField] float cooldownTime = 1f; // Cooldown time in seconds
    AudioSource smashAudioSource;
    CinemachineImpulseSource impulseSource;
    ParticleSystem collisionparticleSystem;

    float cooldownTimer = 1f;
    private void Awake()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
        collisionparticleSystem = GetComponentInChildren<ParticleSystem>();
        smashAudioSource = GetComponent<AudioSource>();
    }
    private void Update() {
        cooldownTimer += Time.deltaTime;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (cooldownTimer < cooldownTime) return;
        FireImpulse();
        CollisionFX(other);
        cooldownTimer = 0f; // Reset cooldown timer after handling collision
        
    }
    void FireImpulse()
    {
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        float maxShake = 1f;
        float shakeIntensity = (maxShake / distance) * shakeModifier;
        shakeIntensity = Mathf.Min(shakeIntensity, maxShake); // Clamp to a maximum of 1
        impulseSource.GenerateImpulse(shakeIntensity);
        Debug.Log("Impulse generated on collision with chunk");
    }
    void CollisionFX(Collision other)
    {
        ContactPoint contactPoint = other.contacts[0];
        collisionparticleSystem.transform.position = contactPoint.point;
        collisionparticleSystem.transform.rotation = Quaternion.LookRotation(contactPoint.normal);
        collisionparticleSystem.Play();
       
        smashAudioSource.Play();
    }
}
