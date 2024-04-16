using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Called when a collision occurs
    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision object is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Player collided with NPC");
        }
    }
}
