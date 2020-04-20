using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{

    // Get the game manager beforehand.
    GameManger manager;

    private void Start()
    {
        manager = FindObjectOfType<GameManger>(); 

    }


    // Load the the second level if the player touches this.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            manager.LoadNextLevel(2);
        }
    }
}
