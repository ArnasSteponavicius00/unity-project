using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(AudioSource))]
public class Enemy : MonoBehaviour
{
    // public variables
    public int GemValue { 
        set { gemValue = value; }
        get { return gemValue; } }

    public int DamageValue { 
        set { damageValue = value; }
        get { return damageValue; } }

    public int maxHealth = 5;
    public int currentHealth;

    public HealthController healthBar;

    public delegate void EnemyKilled(Enemy enemy);
    public static EnemyKilled EnemyKilledEvent;

    // private variables
    [SerializeField] private AudioClip hitClip;
    [SerializeField][Range(0f, 1.0f)] private float hitVol = 0.5f;
    [SerializeField] private int gemValue = 20;
    [SerializeField] private int damageValue = 5;
    [SerializeField] private GameObject healthPotion;
    private Rigidbody2D rb;
    private AudioSource audioSource;
    private bool poisonCount = true;
    private float dropRate = 10f;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Used to take away health from an enemy and destroy obj,
    // when health is 0
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        audioSource.PlayOneShot(hitClip, hitVol);

        if(currentHealth <= 0)
        {
            PublishEnemyKilledEvent();
            Destroy(gameObject);

            // chance to drop a health potion on death
            // Reference: https://forum.unity.com/threads/random-drop.57562/
            if(Random.Range(0, 100) <= dropRate)
            {
                var drop = Instantiate(healthPotion, gameObject.transform.position, Quaternion.identity);
            }
        }
    }
    
    // Handle how much damage an arrow does to the enemy depending on what
    // type of arrow hits them.
    private void OnTriggerEnter2D(Collider2D hit)
    {
        // Get arrow components
        var arrow = hit.GetComponent<Arrow>();
        var fireArrow = hit.GetComponent<FireArrow>();
        var poisonArrow = hit.GetComponent<PoisonArrow>();

        // check if there is a hit
        if(arrow)
        {
            //Debug.Log("Enemy took damage");
            // Destroy the arrow, take away 1 health and play a sound
            Destroy(arrow.gameObject);
            TakeDamage(5);
        }

        if(fireArrow)
        {
            Destroy(fireArrow.gameObject);
            TakeDamage(10);
        }

        if(poisonArrow)
        {
            Destroy(poisonArrow.gameObject);

            StartCoroutine(PoisonCoroutine());
        }

    }

    // Use a coroutine to damage enemy periodically to simulate a poisoned effect
    private IEnumerator PoisonCoroutine()
    {
        while(poisonCount)
        {
            TakeDamage(5);
            yield return new WaitForSeconds(2f);
        }
    }

    // Let the world know an enemy has been killed!
    private void PublishEnemyKilledEvent()
    {
        // make sure somebody is listening
        if(EnemyKilledEvent != null)   
        {
            EnemyKilledEvent(this);
        }
    }
}