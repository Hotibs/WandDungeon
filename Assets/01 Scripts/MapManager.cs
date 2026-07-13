using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapManager : MonoBehaviour
{

    public static MapManager instance;

    
    [SerializeField] MapGrid mg;
    DrawMap dm;

    [SerializeField] List<GameObject> mapPrefabs;

    Vector2Int currentMapPos;
    Vector2Int startPos;

    int[,] map;
    int[,] mapState;

    GameObject[,] spawnMap;


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
        startPos = new Vector2Int(4, 4);
        currentMapPos = startPos;

        
    }
    void Start()
    {
        dm = FindFirstObjectByType<DrawMap>();
        map = mg.GetMap();
        int width = map.GetLength(0);
        int height = map.GetLength(1);
        spawnMap = new GameObject[width,height];
        mapState = new int[width,height];
        MapSetting();

        GameObject startMap = Instantiate(mapPrefabs[0]);
        spawnMap[currentMapPos.x,currentMapPos.y] = startMap;

        
        mapState[currentMapPos.x, currentMapPos.y] = 2;
        SetDoorWall();
    }
    


    void Update()
    {
        
    }

    void SetDoorWall()
    {
        GameObject currentMapObj = spawnMap[currentMapPos.x, currentMapPos.y];
        if (currentMapObj == null)
        {
            return;
        }

        WallDoorContoller wallDoor = currentMapObj.GetComponentInChildren<WallDoorContoller>();
        if (wallDoor == null)
        {
            
            return;
        }

        bool hasUp=false;
        bool hasDown = false;
        bool hasLeft = false;
        bool hasRight = false;

        if(map[currentMapPos.x,currentMapPos.y - 1 ]!= 0)
        {
            hasUp = true;
        }
        if(map[currentMapPos.x,currentMapPos.y + 1] != 0)
        {
            hasDown = true;
        }
        if(map[currentMapPos.x - 1, currentMapPos.y] != 0)
        {
            hasLeft = true;
        }
        if(map[currentMapPos.x + 1,currentMapPos.y] != 0)
        {
            hasRight = true;
        }
        wallDoor.SetDoor(hasUp,hasDown,hasLeft,hasRight);
        
    }

    public void MoveNextRoom(Vector2Int dir)
    {
        spawnMap[currentMapPos.x,currentMapPos.y].SetActive(false);
        
        currentMapPos += dir;
        dm.MapDisplay(map,currentMapPos);
        MoveMap();
    }
    void MapSetting()
    {
        int width = map.GetLength(0);
        int height = map.GetLength(1);

        for (int i = 0; i < width; i++) 
        { 
            for(int j = 0; j < height; j++)
            {
                if (map[i,j]!= 0)
                {
                    mapState[i,j] = 1;
                }
            }
        }

    }
    void MoveMap()
    {


        GameObject currentMap = spawnMap[currentMapPos.x, currentMapPos.y];
        if (currentMap == null)
        {
            spawnMap[currentMapPos.x,currentMapPos.y] = Instantiate(mapPrefabs[Random.Range(1,mapPrefabs.Count)]);
        }
        else
        {
            spawnMap[currentMapPos.x,currentMapPos.y].SetActive(true);
        }
        mapState[currentMapPos.x, currentMapPos.y] = 2;
        SetDoorWall();
    }
}
