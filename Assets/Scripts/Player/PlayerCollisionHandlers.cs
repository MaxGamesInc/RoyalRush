using UnityEngine;

public class PlayerCollisionHandlers : MonoBehaviour
{

   [Header("Animations")]
   [Tooltip("Animator for the player")]
   [SerializeField] Animator anim;
   [Header("Collision Settings")]
   [Tooltip("Cooldown time before the player can hit again")]
   [SerializeField] float cooldowntime = 2f;
   [Tooltip("Speed to adjust the level speed by")]
   [SerializeField] float adjustSpeed = -2f;
   [SerializeField] int hitDamage = 10; // Damage taken on hit
   const string hitString = "Hit";
   float cooldownTimer = 0f;
   LevelGenerator levelgen;
   AudioSource stumbleAudioSource;
   Health health;
   void Start()
   {
      levelgen = FindFirstObjectByType<LevelGenerator>();
      stumbleAudioSource = GetComponent<AudioSource>();
      health = FindFirstObjectByType<Health>();
   }
   void Update()
   {
      cooldownTimer += Time.deltaTime;
      if (cooldownTimer > cooldowntime)
      {
         levelgen.ChangeChunkSpeed(levelgen.maxMoveSpeed);
      }
   }
   private void OnCollisionEnter(Collision other)
   {
      if (Time.deltaTime > cooldowntime) return;
      levelgen.ChangeChunkSpeed(adjustSpeed);
      cooldownTimer = 0.0f;
      anim.SetTrigger(hitString);
      stumbleAudioSource.Play(); // Play stumble audio
      
      health.TakeDamage(hitDamage); // Take damage based on hitDamage variable

   }
}
