using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public NPCDialogueController dialogueController;
    public KeyCode interactKey = KeyCode.E;

    private bool playerNearby;

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(interactKey))
        {
            dialogueController.Talk("Hello");
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
            playerNearby = false;
    }
}