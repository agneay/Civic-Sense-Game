using UnityEngine;

public class QuestCatalyst : MonoBehaviour
{
    [SerializeField] private string quest;
    [SerializeField] private GameObject notification;
    private bool questAdded = false;

    public void CreateQuest()
    {
        if (quest != null && !questAdded)
        {
            questAdded = !questAdded;
            MainManager.mainManager.questNames.Add(quest);
        }

        if (notification != null && !questAdded)
        {
            notification.SetActive(true);
        }
    }
    public void CompleteQuest()
    {
        if (quest != null && MainManager.mainManager.questNames.Contains(quest))
        {
            MainManager.mainManager.questNames.Remove(quest);
        }
    }
}
