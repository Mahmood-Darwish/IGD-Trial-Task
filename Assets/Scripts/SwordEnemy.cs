using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEnemy : MonoBehaviour
{

    // Setting up properties of the enemy.
    bool facingRight = false;
    float speed = -3.5f;
    float timer = 2.5f;

    void Update()
    {
        Vector3 vector;
        vector = transform.position;
        timer -= Time.deltaTime;
        if(timer <= 0) // It is time to turn around.
        {
            timer = 2.5f; // Reset timer.
            facingRight = !facingRight; // Change direction.

            // Flip his X scale and reverse his speed.
            vector = transform.localScale; 
            vector.x *= -1;
            transform.localScale = vector;
            speed *= -1;


            return;
        }
        // Change his position.
        vector.x += speed * Time.deltaTime;
        transform.position = vector;
    }
}
