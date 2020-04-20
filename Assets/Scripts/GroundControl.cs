using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundControl : MonoBehaviour
{

    // Setting these varibles so you won't have to calculate them everytime.
    GameObject player;
    Animator animo;

    void Start()
    {
        // Get the grandparent of this game object which is the player and then get its animator. 
        player = transform.parent.transform.parent.gameObject;
        animo = player.transform.GetChild(0).GetComponent<Animator>();
    }

    // If you enter a collision with the ground, modifiy the player so he can jump and modifiy his animator.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            animo.SetBool("IsJumping", false);
            player.GetComponent<PlayerControl>().canJump = true;
            player.GetComponent<PlayerControl>().canDoubleJump = false;
        }
    }


    // If you leave a collision with the ground, modifiy the player so he can't jump (but can double jump) and modifiy his animator.
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            animo.SetBool("IsJumping", true);
            player.GetComponent<PlayerControl>().canJump = false;
            player.GetComponent<PlayerControl>().canDoubleJump = true;
        }
    }
}
