using UnityEngine;
using TMPro;

public class PlayerDialogueInput : MonoBehaviour
{
    [Header("UI References")]
    public TMP_InputField inputField;
    public GameObject sendButton;

    [Header("Player Control")]
    public PlayerMovement playerMovement;   // Drag your Player here

    private NPCDialogueController currentNPC;
    private bool isActive = false;

    void Update()
    {
        if (!isActive) return;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SendMessageToNPC();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EndConversation();
        }
    }

    // Called by NPCDialogueController when dialogue finishes
    public void Activate(NPCDialogueController npc)
    {
        currentNPC = npc;
        isActive = true;

        inputField.text = "";
        inputField.gameObject.SetActive(true);
        sendButton.SetActive(true);

        inputField.ActivateInputField();

        // 🔒 Disable movement
        if (playerMovement != null)
            playerMovement.enabled = false;

        // 🔓 Unlock cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void SendMessageToNPC()
    {
        if (string.IsNullOrWhiteSpace(inputField.text)) return;

        string msg = inputField.text.Trim();

        currentNPC.Talk(msg);

        inputField.text = "";
        inputField.ActivateInputField();
    }

    public void EndConversation()
    {
        isActive = false;

        inputField.gameObject.SetActive(false);
        sendButton.SetActive(false);

        currentNPC = null;

        // 🔓 Re-enable movement
        if (playerMovement != null)
            playerMovement.enabled = true;

        // 🔒 Lock cursor again (for FPS style games)
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}