using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public GameObject scriptedObject; // Reference to the object that needs to follow the player
    public float speed = 5f; // Speed of the movement

    void Update()
    {
        // Check if player is to the left or right of the scripted object
        if (player.position.x < scriptedObject.transform.position.x)
        {
            // If player is to the left, move scripted object left
            scriptedObject.transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        else
        {
            // If player is to the right, move scripted object right
            scriptedObject.transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }
}
