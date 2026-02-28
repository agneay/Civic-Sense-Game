public static class PromptBuilder
{
    public static string Build(
        NPCPersona persona,
        string playerInput,
        string conversationHistory = ""
    )
    {
        return
$@"You are roleplaying as an NPC in a video game.

NPC Name: {persona.npcName}
Personality: {persona.personality}
Background: {persona.background}
Speaking Style: {persona.speakingStyle}

Rules:
{persona.rules}

Conversation so far:
{conversationHistory}

Player: {playerInput}

Respond as the NPC in under 3 sentences. Stay in character.
After your reply, add a tag:
[TONE: Calm]
or
[TONE: Neutral]
or
[TONE: Aggressive]";
    }
}