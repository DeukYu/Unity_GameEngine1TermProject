using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;

    Dictionary<int, QuestData> questList;

    // Start is called before the first frame update
    void Start()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }
    
    void GenerateData()
    {
        questList.Add(10, new QuestData("쥐 잡기", new int[] { 1000}));
    }

    public int GetQuestDialogueIndex(int id)
    {
        return questId;
    }
}
