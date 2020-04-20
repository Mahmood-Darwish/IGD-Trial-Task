using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader2 : MonoBehaviour
{

    // Get the game manager beforehand.
    GameManger manager2;

    private void Start()
    {
        manager2 = FindObjectOfType<GameManger>();
    }

    // Load the main menu if the player touches this.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            manager2.LoadNextLevel(0);
        }
    }
}