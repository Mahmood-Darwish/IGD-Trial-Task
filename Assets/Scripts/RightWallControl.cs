using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightWallControl : MonoBehaviour
{

    // Same as LeftWallControl, check LeftWallControl.cs for more detail.
    GameObject player;

    private void Start()
    {
        player = transform.parent.gameObject;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            player.GetComponent<PlayerControl>().canMoveRight = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            player.GetComponent<PlayerControl>().canMoveRight = true;
        }
    }
}
