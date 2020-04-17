using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PoisonArrow : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //References: https://docs.unity3d.com/ScriptReference/RigidbodyConstraints.FreezePosition.html
    void OnCollisionEnter2D(Collision2D col)
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        Destroy(gameObject, 1.0f);
    }
}