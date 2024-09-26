using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using AYellowpaper.SerializedCollections;


public class DialogueSystem : MonoBehaviour
{
    public GameObject DialoguePanel; //Reference to dialogue panel

    [SerializedDictionary("Index","Dialogue infos")]
    public SerializedDictionary<int, DialogueInformation> DialogueInfos = new SerializedDictionary<int, DialogueInformation>();

    public Text DialogueText;
    public Text CharacterNameText;

    public string[] DialoguesList;
    public int DialogueIndex;

    public QuestSystem QuestSystem;

    private bool isDialogueOn, isDialogueDisplayed;
    private DialogueInformation info;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetDialogue(DialogueInfos[0]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetDialogue(DialogueInfos[1]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetDialogue(DialogueInfos[2]);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(isDialogueOn)
            {
                SkipDialogue();
            }
        }
    }

    //Called when user hits space when dialogue is finished
    public void SkipDialogue()
    {
        DialogueIndex += 1;

        if (DialoguesList.Length <= DialogueIndex)
        {
            OnDialogueEnd();
            return;
        }

        isDialogueDisplayed = false;
        DisplayDialogue();
    }

    //Called when dialogue ends
    private void OnDialogueEnd()
    {
        isDialogueOn = false;
        DialoguePanel.SetActive(false);

        QuestSystem.SetQuest(info.quest);
    }

    //Called from NPCs interactions etc
    public void SetDialogue(DialogueInformation information)
    {
        if (isDialogueOn) return;
        info = information;

        isDialogueOn = true;
        isDialogueDisplayed = false;

        DialoguePanel.SetActive(true);
        CharacterNameText.text = information.CharacterName;

        DialogueIndex = 0;
        DialoguesList = information.Dialogues;

        DisplayDialogue();
    }

    //Displays dialogue
    public void DisplayDialogue()
    {
        if (isDialogueDisplayed) return;
        isDialogueDisplayed = true;
        
        string Dialogue = DialoguesList[DialogueIndex];

        DialogueText.text = "";
        StartCoroutine(FancyDialogueWrite());
    }

    //Fancy way of writing dialogues
    private IEnumerator FancyDialogueWrite()
    {
        char[] charList = DialoguesList[DialogueIndex].ToCharArray();

        foreach(char c in charList)
        {
            DialogueText.text += c;
            yield return new WaitForSeconds(0.05f);
        }
    }
}

[Serializable]
public struct DialogueInformation
{
    public string CharacterName;
    public string[] Dialogues;

    public Quest quest;
}

[Serializable]
public struct Quest
{
    public string Name;
    public string Description;

    public int ExperienceReward;

    public Destination origin;
    public Destination Destination;
}

[Serializable]
public struct Destination
{
    public Vector3 Position;
}