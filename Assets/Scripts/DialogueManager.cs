using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject dialoguePanel;
    public TMP_Text nameText;
    public TMP_Text dialogueText;

    private bool isConversationActive = false;

    // ================================
    // SHOW DIALOGUE
    // ================================

    public void ShowDialogue(string speakerName, string message)
    {
        // If already talking, do NOT reset greeting
        if (isConversationActive)
        {
            dialogueText.text = message;
            return;
        }

        dialoguePanel.SetActive(true);

        nameText.text = speakerName;
        dialogueText.text = message;

        isConversationActive = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // ================================
    // HIDE DIALOGUE
    // ================================

    public void HideDialogue()
    {
        dialoguePanel.SetActive(false);

        isConversationActive = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Optional helper
    public bool IsConversationActive()
    {
        return isConversationActive;
    }
}