using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TaskNPCObject : InteractableObject
{
    public string npcName;
    public GameTaskSO gameTaskSO;

    public string[] contentInTaskExecuting;
    public string[] contentInTaskCompleted;
    public string[] contentInTaskEnd;

    private void Start()
    {
        gameTaskSO.state = GameTaksState.Waiting;
    }
    protected override void Interact()
    {
        switch (gameTaskSO.state)
        {
            case GameTaksState.Waiting:
                DialogueUI.Instance.Show(npcName, gameTaskSO.diague, OnDialogueEnd);
                break;
            case GameTaksState.Executing:
                DialogueUI.Instance.Show(npcName, contentInTaskExecuting);
                break;
            case GameTaksState.Completed:
                DialogueUI.Instance.Show(npcName, contentInTaskCompleted, OnDialogueEnd);
                break;
            case GameTaksState.End:
                DialogueUI.Instance.Show(npcName, contentInTaskEnd);
                break;
            default:
                break;
        }

        

    }
    public void OnDialogueEnd()
    {
        switch (gameTaskSO.state)
        {
            case GameTaksState.Waiting:
                gameTaskSO.Start();
                InventoryManager.Instance.AddItem(gameTaskSO.startReward);
                MessageUI.Instance.Show("你接受了一个任务!");
                break;
            case GameTaksState.Executing:
                break;
            case GameTaksState.Completed:
                gameTaskSO.End();
                InventoryManager.Instance.AddItem(gameTaskSO.endReward);
                MessageUI.Instance.Show("任务已提交!");
                break;
            case GameTaksState.End:
                break;
            default:
                break;
        }
    }
}
