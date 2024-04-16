using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public SpriteRenderer sprite;
    public DialogueSystem dialogueSystem;
    private PlayerMovement playerMovement;

    public Witnesses witnesses;

    public Storyline storyline;

    private int stage1 = 1;

    private bool checkDogDialogue = false;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (dialogueSystem.gameObject.activeSelf)
        {
            playerMovement.FreezePlayer();
        }
        else
        {
            playerMovement.UnfreezePlayer();
        }
        if (checkDogDialogue)
        {
            if (dialogueSystem.HasDialogueEnded())
            {
                witnesses.moveWitnessesToRoom(1);
                checkDogDialogue = false;
            }
        }
    }

    public void updateStageState(string stage_id)
    {
        switch (stage_id)
        {
            case "stage1":
                stage1++;
                break;
            default:
                break;
        }
    }

    public void Interact(GameObject gameObject)
    {
        //Debug.Log("Interacting with " + gameObject.tag);
        // Interactable NPCs
        if (gameObject.CompareTag("Guard"))
        {
            dialogueSystem.gameObject.SetActive(true);
            switch (stage1)
            {
                case 1:
                    dialogueSystem.StartDialogueById(1);
                    updateStageState("stage1");
                    break;
                case 2:
                    dialogueSystem.StartDialogueById(2);
                    break;
            }

            playerMovement.FreezePlayer();
        }

        if (gameObject.CompareTag("DOG") && checkDogDialogue == false)
        {
            dialogueSystem.gameObject.SetActive(true);
            dialogueSystem.StartDialogueById(8);
            playerMovement.FreezePlayer();
            updateStageState("stage1");
            checkDogDialogue = true;
        }

        if (gameObject.CompareTag("BankDoor"))
        {
            // Exit the bank
            if (stage1 == 2)
            {
                storyline.increaseStoryline1();
                updateStageState("stage1");
            }
            if (stage1 == 4)
            {
                storyline.increaseStoryline1();
            }
        }
    }
}
