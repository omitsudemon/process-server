using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Witnesses : MonoBehaviour
{
    public Sprite[] sprites;

    public Transform pos1;
    public Transform pos2;
    public Transform pos3;
    public Transform pos4;
    public Transform pos5;

    public GameObject[] witnesses;

    public GameObject guardBlock;

    public GameObject getWitnessByName(string name)
    {
        foreach (GameObject witness in witnesses)
        {
            if (witness.name == name)
            {
                return witness;
            }
        }
        return null;
    }

    public async void moveWitnessesToRoom(int stage)
    {
        switch (stage)
        {
            // Move guard to witness stand pos1
            case 0:
                getWitnessByName("Guard").transform.position = pos1.position;
                guardBlock.SetActive(false);
                break;
            // Move dog too witness stand pos2
            case 1:
                // Get the function .getSummoned(); from Dog
                getWitnessByName("Dog").GetComponent<NPCMovement>().getSummoned();
                break;
        }
    }
}
