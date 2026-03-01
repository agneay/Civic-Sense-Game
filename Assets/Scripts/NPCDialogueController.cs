using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;

public class NPCDialogueController : MonoBehaviour
{
    [Header("References")]
    public NPCPersona persona;
    public DialogueManager dialogueManager;
    public PlayerDialogueInput playerInputUI;
    public GeminiNPCService geminiService;
    public PlayerMovement playerMovement;

    private List<string> memory = new List<string>();
    private const int MAX_MEMORY = 6;

    private bool isProcessing = false;
    private bool isTalking = false;   // ✅ Tracks conversation state

    // =================================
    // CHECK IF ACTIVE
    // =================================
    public bool IsConversationActive()
    {
        return isTalking;
    }

    // =================================
    // START CONVERSATION (Greeting only once)
    // =================================
    public async void StartConversation()
    {
        if (isProcessing) return;
        if (isTalking) return;        // ✅ Prevent restart
        if (persona == null) return;

        isTalking = true;
        isProcessing = true;

        playerInputUI.Activate(this);
        playerInputUI.SetInteractable(false);

        string history = string.Join("\n", memory);
        string introPrompt = PromptBuilder.Build(persona, "Start the conversation with a greeting.", history);

        string npcReply = await geminiService.GetNPCResponse(introPrompt);

        isProcessing = false;

        HandleReply("", npcReply);
    }

    // =================================
    // PLAYER TALKS
    // =================================
    public async void Talk(string playerInput)
    {
        if (!isTalking) return;                 // ✅ Must be in conversation
        if (isProcessing) return;
        if (string.IsNullOrWhiteSpace(playerInput)) return;

        isProcessing = true;
        playerInputUI.SetInteractable(false);

        string history = string.Join("\n", memory);
        string prompt = PromptBuilder.Build(persona, playerInput, history);

        string npcReply = await geminiService.GetNPCResponse(prompt);

        isProcessing = false;

        HandleReply(playerInput, npcReply);
    }

    // =================================
    // HANDLE REPLY
    // =================================
    private void HandleReply(string playerInput, string npcReply)
    {
        string tone = ExtractTone(npcReply);
        npcReply = RemoveToneTag(npcReply);

        ApplyEmotionalDamage(tone);

        if (!string.IsNullOrEmpty(playerInput))
            SaveToMemory("Player: " + playerInput);

        SaveToMemory($"{persona.npcName}: {npcReply}");

        dialogueManager.ShowDialogue(persona.npcName, npcReply);

        playerInputUI.SetInteractable(true);
    }

    // =================================
    // MEMORY
    // =================================
    private void SaveToMemory(string line)
    {
        memory.Add(line);

        if (memory.Count > MAX_MEMORY)
            memory.RemoveAt(0);
    }

    // =================================
    // TONE SYSTEM
    // =================================
    private string ExtractTone(string text)
    {
        if (text.Contains("[TONE: Aggressive]")) return "Aggressive";
        if (text.Contains("[TONE: Calm]")) return "Calm";
        return "Neutral";
    }

    private string RemoveToneTag(string text)
    {
        int index = text.IndexOf("[TONE:");
        if (index >= 0)
            return text.Substring(0, index).Trim();

        return text;
    }

    private void ApplyEmotionalDamage(string tone)
    {
        if (playerMovement == null) return;

        int damage = tone == "Aggressive" ? 10 :
                     tone == "Neutral" ? 3 : 0;

        if (damage > 0)
            playerMovement.TakeDamage(damage);
    }

    // =================================
    // FORCE END CONVERSATION
    // =================================
    public void ForceEndConversation()
    {
        if (!isTalking) return;

        isTalking = false;
        isProcessing = false;

        memory.Clear();   // Optional: reset memory per conversation

        if (playerInputUI != null)
            playerInputUI.EndConversation();

        if (dialogueManager != null)
            dialogueManager.HideDialogue();
    }
}