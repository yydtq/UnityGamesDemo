using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameTaksState
{
    Waiting,
    Executing,
    Completed,
    End
}
[CreateAssetMenu()]
public class GameTaskSO : ScriptableObject
{
    public GameTaksState state;

    public string[] diague;

    public ItemSO startReward;
    public ItemSO endReward;

    public int enemyCountNeed = 10;

    public int currentEnemyCount = 0;
   public void Start()
    {
        currentEnemyCount = 0;
        state = GameTaksState.Executing;
        EventCenter.OnEnemyDied += OnEnemyDied;
    }
    private void OnEnemyDied(Enemy enemy)
    {
        if (state == GameTaksState.Completed)return;      
        currentEnemyCount++;
        if (currentEnemyCount >= enemyCountNeed)
        {
            state = GameTaksState.Completed;
            MessageUI.Instance.Show("任务已完成!");
        }
    }

    public void End()
    {
        state = GameTaksState.End;
        EventCenter.OnEnemyDied -= OnEnemyDied;
    }
}
