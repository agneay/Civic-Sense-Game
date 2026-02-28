using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public NPCDialogueController dialogueController;

    private bool playerNearby = false;

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            dialogueController.StartConversation();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerNearby = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;

            // 🔥 FORCE END CONVERSATION
            dialogueController.ForceEndConversation();
        }
    }
}