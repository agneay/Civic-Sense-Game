using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;

public class NPCDialogueController : MonoBehaviour
{
    public NPCPersona persona;
    public DialogueManager dialogueManager;
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
}