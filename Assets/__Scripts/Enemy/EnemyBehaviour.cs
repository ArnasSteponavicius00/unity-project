using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBehaviour : MonoBehaviour
{ 

    // private variables
    [SerializeField] private float moveSpeed = 1.0f;
    [SerializeField] private Transform player = null;

    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Finding "Player" tag allows the prefab enemies to look for the tag
        // and follow the player, without this line enemies will just go towards the
        // players spawn location
        player = GameObject.FindWithTag("Player").transform;
    }

    // Reference on enemy following player:
    // https://www.youtube.com/watch?v=4Wh22ynlLyk
    void FixedUpdate()
    {
        Vector2 direction = player.position - transform.position;
        //Debug.Log("Direction: " + direction);

        // Calculate angle between player and enemy so enemy faces player.
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle - 90;

        //Built in function to set direction value between -1 and 1
        direction.Normalize();
        movement = direction;

        // Move enemy to player position
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }
}
