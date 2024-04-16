using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool playerNearby = false;

    public SpriteRenderer sprite;

    void Start()
    {
        
    }

    void Update()
    {
        if (playerNearby)
        {
            sprite.gameObject.SetActive(true);
        }
        else
        {
            sprite.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerCollider"))
        {
            playerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerCollider"))
        {
            playerNearby = false;
        }
    }
}
