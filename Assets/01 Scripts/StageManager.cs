using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    [SerializeField] float summonDelay;

    WaitForSeconds wait;

    [SerializeField] List<Rect> spawnArea;
    [SerializeField] Color color = new Color(1, 0, 0, 0.5f);

    List<GameObject> monsterList = new List<GameObject>();

    

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        summonDelay = 3f;
        wait = new WaitForSeconds(summonDelay);

        StartCoroutine(SummonMonster());
    }


    IEnumerator SummonMonster()
    {
        while (true)
        {
            yield return wait;
            SummonEnemy();
        }
    }
    private void SummonEnemy()
    {
        Rect spawnRect = spawnArea[Random.Range(0, spawnArea.Count)];

        Vector2 randPos = new Vector2(Random.Range(spawnRect.xMin, spawnRect.xMax), Random.Range(spawnRect.yMin, spawnRect.yMax));

        GameObject enemy = ObjectPoolManager.instance.GetObject("Monster");
        enemy.transform.position = randPos;



        monsterList.Add(enemy);
    }

    public void RemoveEnemy(GameObject monster)
    {
        monsterList.Remove(monster);
    }
    /*
    public void ClearMonsterList()
    {
        foreach (GameObject monster in monsterList)
        {
            monster.GetComponent<MonsterController>().ReturnPool();
        }
        monsterList.Clear();
    }
    */
    private void OnDrawGizmosSelected()
    {
        if (spawnArea == null)
        {
            return;
        }
        Gizmos.color = color;
        color.a = 0.5f;
        foreach (var area in spawnArea)
        {
            Vector3 center = new Vector3(area.x + area.width / 2, area.y + area.height / 2);
            Vector3 size = new Vector3(area.width, area.height);

            Gizmos.DrawCube(center, size);
        }
    }
}
