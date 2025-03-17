using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    //正常状态：1移动 2休息
    //战斗状态

    public enum EnemyState
    {
        NormalState,
        FightingState,
        MovingState,
        RestingState

    }   


    private EnemyState state = EnemyState.NormalState;
    private EnemyState childState = EnemyState.RestingState;
    private NavMeshAgent enemyAgent;

    public float restTime = 2;
    private float restTimer = 0;

    public int HP = 100;

    public int exp = 20;
    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        if (state==EnemyState.NormalState)
        {
            if (childState==EnemyState.RestingState)
            {
                restTimer += Time.deltaTime;

                if (restTimer > restTime)
                {
                    Vector3 randomPositon = FindRandomPositon();
                    enemyAgent.SetDestination(randomPositon);
                    childState = EnemyState.MovingState;
                }
            }else if(childState == EnemyState.MovingState)
            {
                if (enemyAgent.remainingDistance <= 0)
                {
                    restTimer = 0;
                    childState = EnemyState.RestingState;
                }
            }
        }

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    TakeDamage(30);
        //}
    }

 

    Vector3 FindRandomPositon()
    {
        Vector3 randomDir = new Vector3(Random.Range(-1, 1f), 0, Random.Range(-1, 1f));
        return transform.position + randomDir.normalized * Random.Range(2, 5);
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;
        if (HP<=0)
        {
            Die();
        }
    } 
    private void Die()
    {
        GetComponent<Collider>().enabled = false;
        int count = Random.Range(0, 4);
        for (int i = 0; i < count; i++)
        {
            SpawnPickableItem();
        }
        EventCenter.EnemyDied(this);
        Destroy(this.gameObject);
    }

    private void SpawnPickableItem()
    {
        ItemSO item = ItemDBManager.Instance.GetRandomItem();

        GameObject go = GameObject.Instantiate(item.prefab, transform.position, Quaternion.identity);
        go.tag = Tag.INTERACTABLE;

        Animator anim = go.GetComponent<Animator>();
        if (anim != null)
        {
            anim.enabled = false;
        }
        PickableObject po = go.AddComponent<PickableObject>();
        po.itemSO = item;

        Collider collider = go.GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = true;
            collider.isTrigger = false;
        }
        Rigidbody rgd = go.GetComponent<Rigidbody>();
        if (rgd != null)
        {
            rgd.isKinematic = false;
            rgd.useGravity = true;
        }
    }
}
