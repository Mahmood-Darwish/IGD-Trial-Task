using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    // The door to destroy and the sounds to play.
    [SerializeField]
    GameObject Door;
    [SerializeField]
    AudioClip DoorClip;
    [SerializeField]
    AudioClip CoinClip;

    // Player properties.
    float playerSpeed = 7f;
    float jumpHeight = 5f;
    public bool canMoveRight = true;
    public bool canMoveLeft = true;
    public bool canJump = true;
    public bool canDoubleJump = false;
    bool facingRight = true;
    bool Alive = true;
    float deadTimer = 3;

    // Game properties.
    int coinValue = 10;
    int keyValue = 30;

    // Supporting varibles for movement.
    float horizontalMovement;
    bool verticalMovement;

    // Setting these varibles up so we won't have to calculate them each time.
    Rigidbody2D PlayerRigid;
    Animator animo;
    Vector3 temp;
    AudioSource doorSound;
    AudioSource coinSound;
    Vector3 checkPoint;

    private void Start()
    {
        checkPoint = transform.position; // FIrst set our check point as our spawning place.
        PlayerRigid = gameObject.GetComponent<Rigidbody2D>();
        animo = transform.GetChild(0).GetComponent<Animator>();
        PlayerRigid.freezeRotation = true; // So the player won't rotate if he hits certian objects with certian angles.
        
        // Setting up the sounds.
        doorSound = AddAudio(false, false, 0.4f);
        doorSound.clip = DoorClip;
        coinSound = AddAudio(false, false, 0.4f);
        coinSound.clip = CoinClip;
    }

    // Helper function to set up sounds.
    AudioSource AddAudio(bool loop, bool playAwake, float vol)
    {
        AudioSource newAudio = gameObject.AddComponent<AudioSource>();
        newAudio.loop = loop;
        newAudio.playOnAwake = playAwake;
        newAudio.volume = vol;
        return newAudio;
    }

    // Jump physics in Fixedupdate(). Checking for life and then checking if player can jump or double jump.
    private void FixedUpdate()
    {
        if (Alive)
        {
            if (verticalMovement && canJump)
            {
                PlayerRigid.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
            }
            if (verticalMovement && canDoubleJump)
            {
                PlayerRigid.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
                canDoubleJump = false;
            }
        }
    }

    // If alive take input and calculate movement. If dead and dead timer is up go back to checkPoint.
    void Update()
    {
        if (Alive)
        {
            temp = transform.position;
            horizontalMovement = Input.GetAxisRaw("Horizontal");
            verticalMovement = Input.GetKeyDown(KeyCode.Space);
            if (horizontalMovement > 0 && canMoveRight)
            {
                temp.x += playerSpeed * horizontalMovement * Time.deltaTime;
            }
            if (horizontalMovement < 0 && canMoveLeft)
            {
                temp.x += playerSpeed * horizontalMovement * Time.deltaTime;
            }
            transform.position = temp;
            animo.SetFloat("speed", Mathf.Abs(horizontalMovement));
            Flip(horizontalMovement);  // Dealing with going in the other direction.
        }
        else
        {
            deadTimer -= Time.deltaTime;
            if(deadTimer <= 0)
            {
                deadTimer = 3;
                transform.position = checkPoint;
                Alive = true;
                animo.SetBool("Alive", true);
            }
        }
    }

    // In case we needed to flip, flip only the character and NOT leftwallcontrol or rightwallcontrol.
    private void Flip(float mySpeed)
    {
        if((mySpeed > 0 && !facingRight) || (mySpeed < 0 && facingRight))
        {
            facingRight = !facingRight;
            Vector3 vector3 = transform.GetChild(0).transform.localScale;
            vector3.x *= -1;
            transform.GetChild(0).transform.localScale = vector3;
        }
    }

    // Since All our interaction with objects are with objects that have only trigger colliders, we use
    // OnTriggerEnter2d and NOT OnCollisionEnter2D to check our collisions.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy") // If you touch an enemy die.
        {
            Alive = false;
            animo.SetBool("Alive", false);
        }
        if (collision.gameObject.tag == "Key") // If you touch a key open door.
        {
            if (collision.gameObject.GetComponent<Key>().Collected == false)
            {
                ScoreCounting.Score += keyValue;
                doorSound.Play();
                collision.gameObject.GetComponent<Key>().Collected = true;
                checkPoint = transform.position;
                Destroy(Door);
            }
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Coin") // If you touch a coin pick it up.
        {
            if (collision.gameObject.GetComponent<Coin>().Collected == false)
            {
                ScoreCounting.Score += coinValue;
                coinSound.Play();
                collision.gameObject.GetComponent<Coin>().Collected = true;
            }
            Destroy(collision.gameObject);
        }
    }

}
