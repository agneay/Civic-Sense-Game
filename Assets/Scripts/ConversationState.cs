using UnityEngine;

public class ConversationState : MonoBehaviour
{
    public static ConversationState Instance;

    public bool IsConversationActive { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void StartConversation()
    {
        IsConversationActive = true;
    }

    public void EndConversation()
    {
        IsConversationActive = false;
    }
}