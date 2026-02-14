using UnityEngine;

public class policeSay : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public Dialogue dialogueData;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dialogueManager.StartDialogue(dialogueData);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dialogueManager.EndDialogue(); // optional but clean
        }
    }
}
