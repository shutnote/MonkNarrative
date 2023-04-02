using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersDialogue : MonoBehaviour
{
    [SerializeField] List<AudioClip> CutsceneOneDialogue;
    [SerializeField] List<AudioClip> cloisterDialogue;
    [SerializeField] List<AudioClip> CutsceneTwoDialogue;

    [SerializeField] int DialougeToUse;

    List<AudioClip> activeDialogueList;
    int dialogueIndex=0;

    // Start is called before the first frame update
    void Start()
    {
        SwitchDialogueList(DialougeToUse);
        //activeDialogueList = CutsceneOneDialogue;
        //activeDialogueList = CutsceneTwoDialogue;
    }

    public AudioClip GetNextDialogueClip()
    {
        dialogueIndex++;
        Debug.Log(dialogueIndex);
        int clipToCall = dialogueIndex - 1;
        return activeDialogueList[clipToCall];
    }

    public void SwitchDialogueList(int index)
    {
        switch (index)
        {
            case 0:
                activeDialogueList = CutsceneOneDialogue;
                break;
            case 1:
                activeDialogueList = cloisterDialogue;
                break;
            case 2:
                activeDialogueList = CutsceneTwoDialogue;
                break;
        }
        dialogueIndex = 0;
    }

    public void SwitchActiveDialogueToCutsceneOne()
    {
        activeDialogueList = CutsceneOneDialogue;
    }

    public void SwitchActiveDialogueToCLoister()
    {
        activeDialogueList = cloisterDialogue;
    }

    public void SwitchActiveDialogueToCutsceneTwo()
    {
        activeDialogueList = CutsceneTwoDialogue;
    }
    
}
