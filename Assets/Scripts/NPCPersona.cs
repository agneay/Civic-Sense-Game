using UnityEngine;

[CreateAssetMenu(menuName = "NPC/Persona")]
public class NPCPersona : ScriptableObject
{
    public string npcName;

    [TextArea(3, 10)]
    public string personality;

    [TextArea(3, 10)]
    public string background;

    [TextArea(3, 10)]
    public string speakingStyle;

    [TextArea(3, 10)]
    public string rules;
}
