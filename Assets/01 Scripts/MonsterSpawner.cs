using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    

    [SerializeField] List<Transform> MonsterPos;
    [SerializeField] List<GameObject> MonsterPrefabs;

    private int monsterCnt;
    private int currentCnt;

    private Door[] doors;

    private void Awake()
    {
        
        
        
    }

    private void Start()
    {
        doors = transform.parent.GetComponentsInChildren<Door>();
        
        monsterCnt = MonsterPos.Count;
        currentCnt = monsterCnt;
        
        if (currentCnt == 0)
        {
            OpenDoors();
        }
    }



    public void SpawnMonster()
    {
        
        if (MonsterPos.Count == 1) 
        {
            GameObject enemy = ObjectPoolManager.instance.GetObject(MonsterPrefabs[0].name);
            enemy.transform.position = MonsterPos[0].position;
            MonsterController monster = enemy.GetComponent<MonsterController>();
            monster.OnDead += MonsterDead;

            
        }
        else
        {
            for (int i = 0; i < MonsterPos.Count; i++)
            {
                GameObject enemy = ObjectPoolManager.instance.GetObject(MonsterPrefabs[Random.Range(0, MonsterPrefabs.Count)].name);
                enemy.transform.position = MonsterPos[i].position;
                MonsterController monster = enemy.GetComponent<MonsterController>();
                monster.OnDead += MonsterDead;
            }
        }
    }

    private void MonsterDead()
    {
        currentCnt--;
        if(currentCnt == 0)
        {
            OpenDoors();
        }
    }

    private void OpenDoors()
    {
        foreach (Door door in doors)
        {
            door.OpenDoor();
        }
    }
}
