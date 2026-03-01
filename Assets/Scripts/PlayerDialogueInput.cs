using UnityEngine;
using TMPro;

public class PlayerDialogueInput : MonoBehaviour
{
    [Header("UI References")]
    public TMP_InputField inputField;
    public GameObject sendButton;

    [Header("Player Control")]
    public PlayerMovement playerMovement;

    private NPCDialogueController currentNPC;
    private bool isActive = false;

    void Update()
    {
        if (!isActive) return;

        if (Input.GetKeyDown(KeyCode.Return))
            SendMessageToNPC();

        if (Input.GetKeyDown(KeyCode.Escape))
            EndConversation();
    }

    public void Activate(NPCDialogueController npc)
    {
        if (isActive) return;

        currentNPC = npc;
        isActive = true;

        inputField.text = "";
        inputField.gameObject.SetActive(true);
        sendButton.SetActive(true);
        inputField.ActivateInputField();

        // 🔒 LOCK PLAYER CONTROL
        if (playerMovement != null)
            playerMovement.canControl = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void SendMessageToNPC()
    {
        if (currentNPC == null) return;
        if (string.IsNullOrWhiteSpace(inputField.text)) return;

        string msg = inputField.text.Trim();
        currentNPC.Talk(msg);

        inputField.text = "";
        inputField.ActivateInputField();
    }

    public void EndConversation()
    {
        if (!isActive) return;

        isActive = false;

        inputField.gameObject.SetActive(false);
        sendButton.SetActive(false);

        currentNPC = null;

        // 🔓 UNLOCK PLAYER CONTROL
        if (playerMovement != null)
            playerMovement.canControl = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void SetInteractable(bool state)
    {
        inputField.interactable = state;
        sendButton.SetActive(state);
    }
}