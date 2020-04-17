using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // private variables
    [SerializeField] private AudioClip hitClip;
    [SerializeField][Range(0f, 1.0f)] private float hitVol = 0.5f;
    private AudioSource audioSource;

    // public variables
    public int maxHealth = 100;
    public int currentHealth;
    public HealthController healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        audioSource = GetComponent<AudioSource>();
    }

    // Used for dealing damage to player, and destroying obj
    // if health reaches 0
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);

        if(currentHealth <= 0)
        {
            Destroy(gameObject);
            FindObjectOfType<GameController>().GameOver();
        }
    }

    // Used to heal player when he interacts with a consumable
    public void Heal(int health)
    {
        currentHealth += health;
        healthBar.SetHealth(currentHealth);

        // Set max health 10 if healing will bring it over the max health limit
        if(currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    // Check whether player gets hit by an enemy
    private void OnTriggerEnter2D(Collider2D hit)
    {
        var enemy = hit.GetComponent<Enemy>();
        var health = hit.GetComponent<Health>();
        var goo = hit.GetComponent<Goo>();

        if(enemy)
        {
            TakeDamage(enemy.DamageValue);
            audioSource.PlayOneShot(hitClip, hitVol);
            Debug.Log("Taken damage");
        }

        if(goo)
        {
            TakeDamage(5);
            audioSource.PlayOneShot(hitClip, hitVol);
        }
 
        if(health)
        {
            Heal(10);
            Destroy(health.gameObject);
            Debug.Log("Healed");
        }
    }
}
