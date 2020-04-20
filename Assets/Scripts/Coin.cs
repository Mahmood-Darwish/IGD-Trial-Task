using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // This marks the coin if it is picked or not. It helps avoid some bugs when onTriggerEnter2D is called twice by mistake
    // this way you won't get double the score for one coin.
    public bool Collected = false;
}
