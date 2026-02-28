using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject dialoguePanel;
    public TMP_Text nameText;
    public TMP_Text dialogueText;

    // ================================
    // SHOW DIALOGUE
    // ================================

    public void ShowDialogue(string speakerName, string message)
    {
        dialoguePanel.SetActive(true);

        nameText.text = speakerName;
        dialogueText.text = message;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // ================================
    // HIDE DIALOGUE
    // ================================

    public void HideDialogue()
    {
        dialoguePanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}