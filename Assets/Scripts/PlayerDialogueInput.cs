using UnityEngine;
using TMPro;

public class PlayerDialogueInput : MonoBehaviour
{
    public TMP_InputField inputField;
    public GameObject sendButton;

    private NPCDialogueController currentNPC;
    private bool isConversationActive;

    void Update()
    {
        if (!isConversationActive) return;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SendPlayerMessage();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EndConversation();
        }
    }

    public void EnableInput(NPCDialogueController npc)
    {
        currentNPC = npc;
        isConversationActive = true;

        inputField.text = "";
        inputField.gameObject.SetActive(true);
        sendButton.SetActive(true);

        inputField.ActivateInputField();
    }

    public void SendPlayerMessage()
    {
        if (string.IsNullOrWhiteSpace(inputField.text)) return;

        string msg = inputField.text.Trim();

        currentNPC.Talk(msg);

        inputField.text = "";
        inputField.ActivateInputField();
    }

    public void EndConversation()
    {
        isConversationActive = false;

        if (ConversationState.Instance != null)
            ConversationState.Instance.EndConversation();

        inputField.gameObject.SetActive(false);
        sendButton.SetActive(false);

        currentNPC = null;
    }
}