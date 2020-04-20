using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowEnemy : MonoBehaviour
{
    // The enemy's weapon
    [SerializeField]
    GameObject projectile;

    // A flag to handle scaling the enemy to the right or left
    [SerializeField]
    int FacingLeft;

    // Timer support for when to throw weapon
    float minTime = 3;
    float maxTime = 5;
    float timer;

    // Power Support for how strong to throw weapon
    float minPower = 2.5f;
    float maxPower = 5;

    private void Start()
    {
        // Set the timer
        timer = Random.Range(minTime, maxTime);
    }

    private void Update()
    {

        timer -= Time.deltaTime;
        if(timer <= 0) // If timer is up
        {
            timer = Random.Range(minTime, maxTime);      // Reset the timer
            GameObject temp = Instantiate(projectile, transform);     // Create the projectile
            
            // Set the position of the porjectile to be a bit higher than yours and to the left or right depending on your stand
            Vector3 vector = transform.position;     
            vector.y++;
            vector.x += FacingLeft;
            temp.transform.position = vector;

            // Just for safty ignore collision with the porjectile
            Physics2D.IgnoreCollision(temp.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());

            // Give it a force to be thrown
            temp.GetComponent<Rigidbody2D>().AddForce(new Vector2(FacingLeft * Random.Range(minPower, maxPower), 2), ForceMode2D.Impulse);
        }
    }

}
