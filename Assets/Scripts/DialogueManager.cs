using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject dialogueSystem;   // Parent object
    public TMP_Text nameText;
    public TMP_Text dialogueText;

    private Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();

        // Ensure clean startup state
        dialogueSystem.SetActive(false);
        DisableCursor();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialogueSystem.SetActive(true);
        EnableCursor();

        Debug.Log("Starting conversation with " + dialogue.name);

        nameText.text = dialogue.name;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        dialogueText.text = sentences.Dequeue();
    }

    public void EndDialogue()
    {
        Debug.Log("Conversation ended");

        dialogueSystem.SetActive(false);
        DisableCursor();
        FindAnyObjectByType<NPCDialogueController>()?.OnDialogueFinished();
    }

    // ============================
    // Cursor Control
    // ============================
    void EnableCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void DisableCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
