using System.Collections.Generic;
using UnityEngine;

public class ProjectileObjectPoolManager : MonoBehaviour
{
    public static ProjectileObjectPoolManager instance;


    Dictionary<string, Queue<GameObject>> pools = new Dictionary<string, Queue<GameObject>>();
    [SerializeField] List<GameObject> objList;
    private int poolSize;

    GameObject parentPool;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
        poolSize = 100;
        foreach (GameObject obj in objList)
        {
            pools[obj.name] = new Queue<GameObject>();
            parentPool = new GameObject($"{obj.name}_Object_Pool");
            parentPool.transform.SetParent(this.transform);
            for (int i = 0; i < poolSize; i++)
            {
                GameObject go = Instantiate(obj, parentPool.transform);
                go.SetActive(false);
                pools[obj.name].Enqueue(go);
            }
        }
    }

    public GameObject GetObject(string name)
    {
        if (!pools.ContainsKey(name))
        {
            return null;
        }
        if (pools[name].Count > 0)
        {
            GameObject go = pools[name].Dequeue();
            go.SetActive(true);
            return go;
        }
        return null;
    }

    public void ReturnObject(string name, GameObject go)
    {
        if (!pools.ContainsKey(name))
        {
            Destroy(go);
            return;
        }
        go.SetActive(false);
        pools[name].Enqueue(go);
    }

    public void ResetPool()
    {
        foreach (Transform pool in transform)
        {
            string key = pool.name.Replace("_Object_Pool", "");

            foreach (Transform child in pool)
            {
                GameObject go = child.gameObject;

                if (go.activeSelf)
                {
                    ReturnObject(key, go);
                }
            }
        }
    }
}
