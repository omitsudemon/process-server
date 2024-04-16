using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;
using UnityEngine.SceneManagement;


public class DialogueSystem : MonoBehaviour
{
    public Image dialogueImage;
    public TextMeshProUGUI dialogueText;
    private int currentDialogueIndex = 0;
    public Sprite[] portraitImages;

    public Storyline storyline;

    public AudioClip[] sounds;
    public AudioSource SFX;

    private List<Dialogue> guardDialogues_1;
    private List<Dialogue> guardDialogues_2;
    private List<Dialogue> guardDialogues_3;

    private List<Dialogue> genericDialogue_1;

    private List<Dialogue> genericDialogue_2;

    private List<Dialogue> genericDialogue_3;

    private List<Dialogue> stage1_court_dialogue;
    private List<Dialogue> stage1_court_dialogue_2;

    private List<Dialogue> stage1_court_dialogue_3;

    private List<Dialogue> dog_dialogue;

    private List<Dialogue> dialogues = null;

    private bool isVisible = false;

    private bool hasDialogueEnded = false;

    private bool load = false;

    private bool endgame = false;

    public bool didLoad()
    {
        return load;
    }

    private void Start()
    {
        guardDialogues_1 = new List<Dialogue>
        {
            new Dialogue("Hello Mr.Guard I am...", "player_portrait"),
            new Dialogue("The bank is closed, Miss.  Noone is permitted to enter.", "guard_portrait"),
            new Dialogue("Wah!... Uhhh, I just need to talk with you...", "player_portrait"),
            new Dialogue("Seems like you were the only witness in a theft case that happened recently...", "player_portrait"),
            new Dialogue("I didn't see anything.  I won't testify against my boss!", "guard_portrait"),
            new Dialogue("Th-then maybe this summons will change your mind?", "player_portrait"),
            new Dialogue("Alright, fine, I'll be there.", "guard_portrait"),
            new Dialogue("Security Guard...", "player_portrait"),
            new Dialogue("You've Been Served!", "player_portrait"),
        };

        guardDialogues_2 = new List<Dialogue>
        {
            new Dialogue("Hello!", "player_portrait"),
            new Dialogue("What's this?  I'm busy here, move along.", "guard_portrait")
        };

        genericDialogue_1 = new List<Dialogue>
        {
            new Dialogue("Hello!", "player_portrait"),
            new Dialogue("This is no time for games young lady, we're trying to conduct business here.", "guard_portrait")
        };

        stage1_court_dialogue = new List<Dialogue>
        {
            new Dialogue("Ahem! Let’s call these proceedings to order! Now where did I put my gavel?", "judge_portrait"),
            new Dialogue("No matter, I’ll just use whatever’s at hand... This drumstick would do the job", "judge_portrait"),
            new Dialogue("...", "judge_portrait", "gavel_full"),
            new Dialogue("Next case is The People vs Elden Moneybags, CEO of Profiteering Bank.", "judge_portrait"),
            new Dialogue("Your Honor, Mr. Moneybags is accused of stealing money out of clients’ accounts", "lawyer_portrait"),
            new Dialogue("We have piles of documents that prove he drained their accounts on purpose.", "prettywoman_portrait"),
            new Dialogue("And multiple witnesses that report Mr. Moneybags saying, and I quote:", "prettywoman_portrait"),
            new Dialogue("It’s all my money anyway, so why should I give you any?", "prettywoman_portrait"),
            new Dialogue("Ha ha ha ha ha ha ha ha ha ha…", "prettywoman_portrait"),
            new Dialogue("Ha ha ha ha ha… Your Honor, this goes on for three pages.", "prettywoman_portrait"),
            new Dialogue("*smirk*", "oldrichman_portrait"),
            new Dialogue("I object!", "lawyer_portrait"),
            new Dialogue("On what grounds?", "judge_portrait"),
            new Dialogue("I don’t see any witnesses here. Clearly the prosecution is making it all up.", "lawyer_portrait"),
            new Dialogue("The court was supposed to issue a summons to each of the witnesses, your honor.", "prettywoman_portrait"),
            new Dialogue("Starting with… The bank security guard.", "prettywoman_portrait"),
            new Dialogue("Oh was that my job? What should I do?", "judge_portrait"),
            new Dialogue("*smirk*", "oldrichman_portrait"),
            new Dialogue("Hmmmm...", "judge_portrait"),
            new Dialogue("Hmmmmmmmm......", "judge_portrait"),
            new Dialogue("Your Honor?!", "prettywoman_portrait"),
            new Dialogue("Might I suggest that we serve each summons now?", "prettywoman_portrait"),
            new Dialogue("Stop talking, councilor. I have a great idea. We should serve each summons now!", "judge_portrait"),
            new Dialogue("Fantastic idea, Your Honor…", "prettywoman_portrait"),
            new Dialogue("Bailiff! Summon… The Process Server!", "judge_portrait"),
            new Dialogue(". . .", "judge_portrait"),
            new Dialogue("Waah!.. Y-Your Honor! Here I Am. What do you need?", "player_portrait"),
            new Dialogue("Serve this summons to the security guard at Profiteering Bank! He’s wearing a blue hat and sunglasses.", "judge_portrait"),
            new Dialogue("Understood!", "player_portrait")
        };

        stage1_court_dialogue_2 = new List<Dialogue>
        {
            new Dialogue("Order in the court!", "judge_portrait", "gavel_full"),
            new Dialogue("Alright Mister…  Guard?  Your actual name is \"Security\", last name, \"Guard\"?", "prettywoman_portrait"),
            new Dialogue("That’s correct.", "guard_portrait", 1),
            new Dialogue("Very well, Mr. Guard.  Tell us what you saw on the day of the 19th.", "prettywoman_portrait", 0),
            new Dialogue("*winks and smiles at Security Guard*", "lawyer_portrait"),
            new Dialogue("Well, I, uh…  didn’t hear much, ma’am.", "guard_portrait", 1),
            new Dialogue("What DID you hear, Mr. Guard?", "prettywoman_portrait", 0),
            new Dialogue("I didn’t hear much of anything, actually.", "guard_portrait", 1),
            new Dialogue("Our bank dog kept barking up a storm, everything Mr. Moneybags said was drowned out.", "guard_portrait"),
            new Dialogue("You see, Your honor?  There are no witnesses.  I move to dismiss the case.", "lawyer_portrait", 0),
            new Dialogue("There must be some way...", "prettywoman_portrait"),
            new Dialogue("So unless you suggest we summon this...  \"dog\" for testimony...", "lawyer_portrait"),
            new Dialogue("Tail-er Swift.", "guard_portrait", 1),
            new Dialogue("What?", "lawyer_portrait", 0),
            new Dialogue("Her name is Tail-er Swift.", "guard_portrait", 1),
            new Dialogue("That's it!", "prettywoman_portrait", 0),
            new Dialogue("Your Honor, I wish to call Tail-er Swift to the stand!", "prettywoman_portrait"),
            new Dialogue("Your Honor!  We can't summon a dog to testify!", "lawyer_portrait"),
            new Dialogue("Don't tell me what I can and can't do in my courtroom!", "judge_portrait"),
            new Dialogue("Bailiff!  Summon The Process Server!", "judge_portrait"),
            new Dialogue("Waaaaah?  I'm needed again?  Yippee!", "player_portrait"),
            new Dialogue("Serve this summons to Tail-er Swift!  He's...  a dog.", "judge_portrait"),
            new Dialogue("I'll do my best!", "player_portrait")
        };

        dog_dialogue = new List<Dialogue>
        {
            new Dialogue("So here she is.  How are you doing Ms. Swift?  I love your music!", "player_portrait"),
            new Dialogue("Woof! Woof!", "dog_portrait"),
            new Dialogue("But... What am I supposed to do now?? T_T", "player_portrait"),
            new Dialogue("I can't hand a dog a piece of paper, she doesn't have hands...", "player_portrait"),
            new Dialogue("Oh well, I was hoping to avoiding using... THAT.", "player_portrait"),
            new Dialogue("Woof?", "dog_portrait"),
            new Dialogue("From nameless realms, O ancient ones, draw near, Through the voids of time and space appear!", "player_portrait_demon"),
            new Dialogue("GRRRrrrr!!", "dog_portrait"),
        };

        stage1_court_dialogue_3 = new List<Dialogue>
        {
            new Dialogue("Order in the...  Hey, that's my gavel, not a chew toy!", "judge_portrait", "gavel_full"),
            new Dialogue("*Growl*", "dog_portrait"),
            new Dialogue("Your Honor, I object!  How can we question a dog who can't talk?", "lawyer_portrait"),
            new Dialogue("Who says I can't talk?", "dog_portrait", 1),
            new Dialogue("Whaaaaa?  How come she didn't talk to me? T_T", "player_portrait"),
            new Dialogue("Because you never asked, my dear!", "dog_portrait"),
            new Dialogue("Objection overruled!", "judge_portrait", 0),
            new Dialogue("Now, then, Ms. Swift.", "prettywoman_portrait"),
            new Dialogue("Security Guard has testified that you were barking on the day in question", "judge_portrait", 1),
            new Dialogue("That is correct.  Rowr!", "dog_portrait"),
            new Dialogue("What were you barking about?", "prettywoman_portrait", 0),
            new Dialogue("My ex-dogfriend, of course.  He did me wrong.  I wrote a song about it...", "dog_portrait", 1),
            new Dialogue("That's quite alright, Ms. Swift, maybe later.", "prettywoman_portrait", 0),
            new Dialogue("Did you hear anything Mr. Moneybags said on the day in question?", "prettywoman_portrait"),
            new Dialogue("I sure did!  Woof!  He said he would remove all the bank records and bury them.", "dog_portrait", 1),
            new Dialogue("Bury them?", "prettywoman_portrait", 0),
            new Dialogue("By the light of the full moon.  Which is a most sensible time to bury things, I must say", "dog_portrait", 1),
            new Dialogue("There you have it, Your Honor!  We have testimony that Mr. Moneybags hid the documents!", "prettywoman_portrait", 0),
            new Dialogue("*sweat*", "oldrichman_portrait"),
            new Dialogue("Ah, but there's one problem with your defense.", "lawyer_portrait"),
            new Dialogue("?!", "prettywoman_portrait"),
            new Dialogue("?!", "oldrichman_portrait"),
            new Dialogue("?!", "judge_portrait"),
            new Dialogue("If these documents really were buried, why can't the defense produce them?", "lawyer_portrait"),
            new Dialogue("Nobody has seen this alleged \"burial\" short of the moon itself!", "lawyer_portrait"),
            new Dialogue("Very well, then.  Your Honor, I would next like to question...  The Moon!", "prettywoman_portrait"),
            new Dialogue("*slaps forhead*", "lawyer_portrait"),
            new Dialogue("Very well.  Bailiff!  Once again summon...  The Process Server!", "judge_portrait"),
            new Dialogue("I've actually been here this whole time.  This trial is actually pretty interest...", "player_portrait"),
            new Dialogue("Never mind that now!", "judge_portrait"),
            new Dialogue("Serve this summons to...  The moon!  It's big!  And round!", "player_portrait"),
            new Dialogue("The moon?  But how can I... ", "player_portrait"),
            new Dialogue("I mean, Yes, Your Honor.  I'll do it!", "player_portrait_demon"),
            new Dialogue("Ahroooooooooooo!", "dog_portrait")
        };

        load = true;
    }

    public void StartDialogueById(int dialogueId)
    {
        hasDialogueEnded = false;
        isVisible = true;
        //Debug.Log("Starting dialogue with ID: " + dialogueId);
        switch (dialogueId)
        {
            case 1:
                dialogues = guardDialogues_1;
                break;
            case 2:
                dialogues = guardDialogues_2;
                break;
            case 3:
                dialogues = genericDialogue_1;
                break;
            case 4:
                dialogues = genericDialogue_2;
                break;
            case 5:
                dialogues = genericDialogue_3;
                break;
            case 6:
                dialogues = stage1_court_dialogue;
                break;
            case 7:
                dialogues = stage1_court_dialogue_2;
                break;
            case 8:
                dialogues = dog_dialogue;
                break;
            case 9:
                dialogues = stage1_court_dialogue_3;
                endgame = true;
                break;
            default:
                Debug.LogError("Dialogue ID not found!");
                dialogues = null;
                break;
        }

        if (dialogues != null)
        {
            currentDialogueIndex = 0;
            DisplayDialogue();
        }
    }

    private void Update()
    {
        if (!isVisible)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }

        // Check for mouse click or spacebar press to advance dialogue
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            PlaySoundByName("click");

            if (dialogues != null)
            {
                currentDialogueIndex++;
                if (currentDialogueIndex < dialogues.Count)
                {
                    DisplayDialogue();
                }
                else
                {
                    hasDialogueEnded = true;
                    isVisible = false;
                    if (endgame)
                    {
                        SceneManager.LoadScene("GameEndScene");
                    }

                }
            }
        }
    }

    public bool HasDialogueEnded()
    {
        return hasDialogueEnded;
    }

    private void PlaySoundByName(string soundName)
    {
        foreach (AudioClip sound in sounds)
        {
            if (sound.name == soundName)
            {
                SFX.PlayOneShot(sound);
            }
        }
    }

    private Sprite GetPortraitImage(string imageName)
    {
        foreach (Sprite portraitImage in portraitImages)
        {
            if (portraitImage.name == imageName)
            {
                return portraitImage;
            }
        }
        return null; // Return null if image not found
    }

    private void DisplayDialogue()
    {
        Dialogue currentDialogue = dialogues[currentDialogueIndex];
        if (currentDialogue.hasSound())
        {
            PlaySoundByName(currentDialogue.soundName);
        }
        if (currentDialogue.hasView())
        {
            switch(currentDialogue.viewIndex){
                case 0:
                    storyline.lookAtCourtroom();
                    break;
                case 1:
                    storyline.lookAtWitnesses();
                    break;
                case 2:
                    storyline.lookAtPlayer();
                    break;
            }
        }
        dialogueText.text = currentDialogue.text;
        dialogueImage.sprite = GetPortraitImage(currentDialogue.imageName);
    }
}

public class Dialogue
{
    public string text;
    public string imageName;

    public string soundName = null;

    public int viewIndex = -1;

    public bool hasSound()
    {
        return soundName != null;
    }
    public bool hasView()
    {
        return viewIndex > -1;
    }

    public Dialogue(string text, string imageName)
    {
        this.text = text;
        this.imageName = imageName;
    }

    public Dialogue(string text, string imageName, string soundName)
    {
        this.text = text;
        this.imageName = imageName;
        this.soundName = soundName;
    }

    public Dialogue(string text, string imageName, int viewIndex)
    {
        this.text = text;
        this.imageName = imageName;
        this.viewIndex = viewIndex;
    }
}