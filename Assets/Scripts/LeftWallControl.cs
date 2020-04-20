using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftWallControl : MonoBehaviour
{

    // Set this varible beforehand so you won't have to call him each time.
    GameObject player;

    private void Start()
    {
        player = transform.parent.gameObject;
    }

    // If you enter a collision to the left you can't go left anymore.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            player.GetComponent<PlayerControl>().canMoveLeft = false;
        }
    }

    // If you leave a collision to the left you can go left.
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            player.GetComponent<PlayerControl>().canMoveLeft = true;
        }
    }
}
