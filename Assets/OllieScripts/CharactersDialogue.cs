using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersDialogue : MonoBehaviour
{
    [SerializeField] List<AudioClip> cloisterDialogue;
    [SerializeField] List<AudioClip> CutsceneOneDialogue;
    [SerializeField] List<AudioClip> CutsceneTwoDialogue;

    List<AudioClip> activeDialogueList;
    int dialogueIndex=0;

    // Start is called before the first frame update
    void Start()
    {
        //activeDialogueList = CutsceneOneDialogue;
        activeDialogueList = CutsceneTwoDialogue;
    }

    public AudioClip GetNextDialogueClip()
    {
        dialogueIndex++;
        Debug.Log(dialogueIndex);
        return activeDialogueList[dialogueIndex];
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

    
}
