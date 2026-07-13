using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    

    [SerializeField] List<Transform> MonsterPos;
    [SerializeField] List<GameObject> MonsterPrefabs;


    


    public void SpawnMonster()
    {
        
        if (MonsterPos.Count == 1) 
        {
            GameObject enemy = ObjectPoolManager.instance.GetObject(MonsterPrefabs[0].name);
            enemy.transform.position = MonsterPos[0].position;
            
        }
        else
        {
            for (int i = 0; i < MonsterPos.Count; i++)
            {
                GameObject enemy = ObjectPoolManager.instance.GetObject(MonsterPrefabs[Random.Range(0, MonsterPrefabs.Count)].name);
                enemy.transform.position = MonsterPos[i].position;
            }
        }

            
    }
    


}
