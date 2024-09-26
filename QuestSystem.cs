using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestSystem : MonoBehaviour
{
    public List<Quest> QuestList;

    public Transform PlayerTransform;
    public GameObject QuestPanel;

    public Text QuestNames;
    public Text ExperienceText;

    private List<string> QuestNamesList = new List<string>();
    private List<Quest> TempQuestList = new List<Quest>();

    private int Exp = 0;

    //Add quest
    public void SetQuest(Quest quest)
    {
        QuestPanel.SetActive(true);
        QuestList.Add(quest);

        QuestNamesList.Add(quest.Name+"\n");
        ExperienceText.text = Exp.ToString();
        QuestNames.text += quest.Name;
    }

    public void QuestComplete(Quest quest)
    {
        if (QuestList.Contains(quest))
        {
            QuestList.Remove(quest);

            if (QuestList.Count <= 0)
            {
                QuestPanel.SetActive(false);
            }
            else
            {
                QuestPanel.SetActive(true);
            }

            QuestNames.text = QuestNamesList.ToString();

            Exp += quest.ExperienceReward;
            ExperienceText.text = Exp.ToString();
        }
    }

    private void Update()
    {
        foreach (Quest quest in QuestList)
        {
            if(IsNear(PlayerTransform.position, quest.Destination.Position))
            {
                QuestComplete(quest);
                QuestList.Remove(quest);

                return;
            }
        }
    }

    public bool IsNear(Vector3 a, Vector3 b, int marign = 2)
    {
        float dist = Vector3.Distance(a, b);
        return dist <= marign;
    }
}
