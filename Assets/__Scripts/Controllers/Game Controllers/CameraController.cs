using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 offset;
    public Transform player;

    void Start () 
    {
        // Calculate and store the offset value by getting the distance between the players 
        // position and camera's position.
        offset = transform.position - player.transform.position;
        player = GameObject.FindWithTag("Player").transform;
    }

    void LateUpdate () 
    {
        // Set the position of the camera's transform to be the same as the players,
        // but offset by the calculated offset distance.
        transform.position = player.transform.position + offset;
    }
}
