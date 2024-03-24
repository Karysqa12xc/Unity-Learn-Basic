using System.Runtime.InteropServices;
using System.Net.Http.Headers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Unity.VisualScripting;
using System;
namespace Chapter.LogicAndGameplay
{
    public class QuestStatus
    {
        public Quest questData;

        public Dictionary<int, Quest.Status> objectiveStatuses;

        public QuestStatus(Quest questData)
        {
            this.questData = questData;
            objectiveStatuses = new Dictionary<int, Quest.Status>();

            for (int i = 0; i < questData.objectives.Count; i += 1)
            {
                var objectiveData = questData.objectives[i];
                objectiveStatuses[i] = objectiveData.initialStatus;
            }
        }

        public Quest.Status questStatus
        {
            get
            {
                for (int i = 0; i < questData.objectives.Count; i += 1)
                {
                    var objectiveData = questData.objectives[i];
                    if (objectiveData.optional)
                    {
                        continue;
                    }
                    var objectiveStatus = objectiveStatuses[i];

                    if (objectiveStatus == Quest.Status.Failed)
                    {
                        return Quest.Status.Failed;
                    }

                    else if (objectiveStatus != Quest.Status.Complete)
                    {
                        return Quest.Status.NotYetComplete;
                    }
                }
                return Quest.Status.Complete;
            }
        }
        public override string ToString()
        {
            var stringBuilder = new System.Text.StringBuilder();

            for (int i = 0; i < questData.objectives.Count; i += 1)
            {
                var objectiveData = questData.objectives[i];
                var objectiveStatus = objectiveStatuses[i];

                if (objectiveData.visible == false && objectiveStatus == Quest.Status.NotYetComplete)
                {
                    continue;
                }
                if (objectiveData.optional)
                {
                    stringBuilder.AppendFormat("{0} (Optional) - {1}\n",
                    objectiveData.name,
                    objectiveStatus.ToString());
                }
                else
                {
                    stringBuilder.AppendFormat("{0} - {1}\n",
                    objectiveData.name,
                    objectiveStatus.ToString());
                }
            }
            stringBuilder.AppendLine();
            stringBuilder.AppendFormat("Status: {0}", this.questStatus.ToString());
            return stringBuilder.ToString();
        }
    }

    public class QuestManager : MonoBehaviour
    {
        [SerializeField] Quest startingQuest = null;

        [SerializeField] UnityEngine.UI.Text objectiveSummary = null;

        QuestStatus activeQuest;

        void Start()
        {
            if (startingQuest != null)
            {
                StartQuest(startingQuest);
            }
        }
        
        public void StartQuest(Quest quest)
        {
            activeQuest = new QuestStatus(quest);
            UpdateObjectiveSummaryText();
            Debug.LogFormat("Started quest {0}", activeQuest.questData.name);
        }

        void UpdateObjectiveSummaryText()
        {
            string label;
            if(activeQuest == null){
                label = "No active quest.";
            }else{
                label = activeQuest.ToString();
            }
            objectiveSummary.text = label;
        }
        public void UpdateObjectiveStatus(Quest quest, int objectiveNumber, Quest.Status status)
        {
            if(activeQuest == null){
                Debug.LogError("Tried to sey an objective status, but no quest" +  "is active");
                return;
            }
            if(activeQuest.questData != quest){
                Debug.LogWarningFormat("Tried to set an objective status" + "for quest {0}, but this is not the active quest." +
                            "Ignoring.", quest.questName);
                return;
            }
            activeQuest.objectiveStatuses[objectiveNumber] = status;

            UpdateObjectiveSummaryText();
        }
    }
}

