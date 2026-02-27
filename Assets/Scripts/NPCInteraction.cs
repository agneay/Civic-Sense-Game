using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public NPCDialogueController dialogueController;

    private bool playerNearby;

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            dialogueController.Talk(
                "Why are you behaving like this? Do you know the rules?"
            );
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
        }
    }
}