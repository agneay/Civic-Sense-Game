using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public NPCDialogueController dialogueController;

    private bool playerNearby = false;

    void Update()
    {
        // Press E only if player is near AND no conversation is active
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            if (!dialogueController.IsConversationActive())
            {
                dialogueController.StartConversation();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;

            // End conversation when leaving NPC range
            dialogueController.ForceEndConversation();
        }
    }
}