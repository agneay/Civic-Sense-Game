using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;

public class NPCDialogueController : MonoBehaviour
{
    public NPCPersona persona;
    public DialogueManager dialogueManager;
    public PlayerDialogueInput playerInputUI;
    public GeminiNPCService geminiService;

    private List<string> memory = new List<string>();
    private const int MAX_MEMORY = 6;

    public void Talk(string playerInput)
    {
        _ = HandleConversation(playerInput);
    }

    private async Task HandleConversation(string playerInput)
    {
        string history = string.Join("\n", memory);
        string prompt = PromptBuilder.Build(persona, playerInput, history);

        string npcReply = await geminiService.GetNPCResponse(prompt);
        string tone = ExtractTone(npcReply);
        npcReply = RemoveToneTag(npcReply);
        ApplyEmotionalDamage(tone);

        SaveToMemory("Player: " + playerInput);
        SaveToMemory($"{persona.npcName}: {npcReply}");

        Dialogue aiDialogue = new Dialogue
        {
            name = persona.npcName,
            sentences = new string[] { npcReply }
        };

        dialogueManager.StartDialogue(aiDialogue);
    }

    private void SaveToMemory(string line)
    {
        memory.Add(line);
        if (memory.Count > MAX_MEMORY)
            memory.RemoveAt(0);
    }
    public void OnDialogueFinished()
    {
        playerInputUI.Activate(this);
    }
    private string ExtractTone(string text)
    {
        if (text.Contains("[TONE: Aggressive]"))
            return "Aggressive";
        if (text.Contains("[TONE: Neutral]"))
            return "Neutral";
        if (text.Contains("[TONE: Calm]"))
            return "Calm";

        return "Neutral";
    }

    private string RemoveToneTag(string text)
    {
        int index = text.IndexOf("[TONE:");
        if (index >= 0)
            return text.Substring(0, index).Trim();

        return text;
    }
    public PlayerMovement playerMovement; // assign in inspector

    private void ApplyEmotionalDamage(string tone)
    {
        if (playerMovement == null) return;

        int damage = 0;

        switch (tone)
        {
            case "Aggressive":
                damage = 10;
                break;

            case "Neutral":
                damage = 3;
                break;

            case "Calm":
                damage = 0;
                break;
        }

        if (damage > 0)
            playerMovement.TakeDamage(damage);
    }
}