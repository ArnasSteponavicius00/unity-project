using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyShooterBehaviour : MonoBehaviour
{ 
    // private variables
    [SerializeField] private float gooSpeed = 10.0f;
    [SerializeField] private float firingRate = 1f;
    [SerializeField] private float moveSpeed = 1.0f;
    [SerializeField] private Transform player = null;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject gooPrefab;

    private Rigidbody2D rb;
    private Vector2 movement;
    private GameObject gooParent;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        gooParent = GameObject.Find("Goo Parent");

        if (!gooParent)
        {
            gooParent = new GameObject("Goo Parent");
        }

        StartCoroutine(FireCoroutine());
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

    // Coroutine returns an IEnumerator type
    private IEnumerator FireCoroutine()
    {
         while(true)
        {
            // Create a bullet on the transform Fire Point so bullet fires from the
            // gun barrel and from the same rotation as it
            GameObject goo = Instantiate(gooPrefab, firePoint.position, firePoint.rotation, gooParent.transform);
            Rigidbody2D rbb = goo.GetComponent<Rigidbody2D>();
            // Fire bullet in the direction the player is facing
            rbb.velocity = firePoint.right * gooSpeed;

            yield return new WaitForSeconds(firingRate);
        }
    }
}
