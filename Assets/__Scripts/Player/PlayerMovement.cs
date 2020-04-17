using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PolygonCollider2D))]
public class PlayerMovement : MonoBehaviour
{

    // public variables
    public float Speed { 
        set { speed = value; }
        get { return speed; } }

    // private variables
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private Camera cam;
    [SerializeField] private Rigidbody2D rb;
    private Vector2 mousePos;
    private Vector2 movement;
 
    // private methods
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if(rb == null)
        {
            Debug.Log("Cant find Rigidbody");
        }

    }

    void Update()
    {
        // move along x and y axis
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // find the position of the mouse
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        // move the player
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        // calculate the direction the player needs to face the mouse
        Vector2 direction = mousePos - rb.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // rotate player towards the mouse, but needs to be offset by 90 deg
        rb.rotation = angle - 90;
    }
}
