using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Storyline : MonoBehaviour
{

    public DialogueSystem dialogueSystem;

    public CinemachineVirtualCamera virtualCamera;
    public Transform player;
    public Transform courtroom;

    public Transform witnesses;

    public PlayerMovement playerMovement;

    public Witnesses witnessesScript;

    public int storyine1 = 0;

    private bool load = false;
    private bool processing = false;

    void Start()
    {
        // Ensure virtualCamera is assigned
        if (virtualCamera == null)
        {
            //Debug.LogError("Virtual Camera not assigned to script!");
            return;
        }
    }

    void Update()
    {
        if (dialogueSystem.didLoad())
        {
            load = true;
        }

        if (load && !processing)
        {
            processing = true;
            processStoryline1();
        }

        // Wait until dialogues are processed to continue
        if (load && processing)
        {
            if (dialogueSystem.HasDialogueEnded())
            {
                processing = false;
            }
        }
    }

    public void lookAtPlayer()
    {
        virtualCamera.Follow = player;
    }

    public void lookAtCourtroom()
    {
        virtualCamera.Follow = courtroom;
    }

    public void lookAtWitnesses()
    {
        virtualCamera.Follow = witnesses;
    }

    public void increaseStoryline1()
    {
        processing = false;
        storyine1++;
    }

    public void processStoryline1() 
    {
        switch (storyine1)
        {
            case 0:
                dialogueSystem.gameObject.SetActive(true);
                lookAtCourtroom();
                if (dialogueSystem.gameObject.activeSelf) playerMovement.FreezePlayer();
                dialogueSystem.StartDialogueById(6);
                storyine1++;
                break;
            case 1:
                dialogueSystem.gameObject.SetActive(false);
                lookAtPlayer();
                if (!dialogueSystem.gameObject.activeSelf) playerMovement.UnfreezePlayer();
                storyine1++;
                break;
            case 2:
                // Do nothing
                break;

            case 3:
                lookAtCourtroom();
                witnessesScript.moveWitnessesToRoom(0);
                dialogueSystem.gameObject.SetActive(true);
                if (dialogueSystem.gameObject.activeSelf) playerMovement.FreezePlayer();
                dialogueSystem.StartDialogueById(7);
                storyine1++;
                break;

            case 4:
                dialogueSystem.gameObject.SetActive(false);
                lookAtPlayer();
                if (!dialogueSystem.gameObject.activeSelf) playerMovement.UnfreezePlayer();
                storyine1++;
                break;
            case 5:
                // Do nothing
                break;
            case 6:
                lookAtCourtroom();
                dialogueSystem.gameObject.SetActive(true);
                if (dialogueSystem.gameObject.activeSelf) playerMovement.FreezePlayer();
                dialogueSystem.StartDialogueById(9);
                storyine1++;
                break;
            case 7:
                // Do nothing
                
                break;
        }
    }
}