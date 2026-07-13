using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapManager : MonoBehaviour
{

    public static MapManager instance;

    [SerializeField] Transform player;
    [SerializeField] float spawnPadding = 1f;


    [SerializeField] MapGrid mg;
    DrawMap dm;

    [SerializeField] List<GameObject> mapPrefabs;

    Vector2Int currentMapPos;
    Vector2Int startPos;

    Vector2Int nextDir;

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

        
        if( currentMapPos.y != 0 && map[currentMapPos.x, currentMapPos.y - 1] != 0)
        {
            hasUp = true;
        }
        if( currentMapPos.y != 8 && map[currentMapPos.x, currentMapPos.y + 1] != 0)
        {
            hasDown = true;
        }
        if( currentMapPos.x != 0 && map[currentMapPos.x - 1, currentMapPos.y] != 0)
        {
            hasLeft = true;
        }
        if( currentMapPos.x != 8 && map[currentMapPos.x + 1, currentMapPos.y] != 0)
        {
            hasRight = true;
        }
        wallDoor.SetDoor(hasUp,hasDown,hasLeft,hasRight);
        
    }

    public void MoveNextRoom(Vector2Int dir)
    {
        nextDir=dir;
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
            if (map[currentMapPos.x, currentMapPos.y] == 2)  //보스
            {
                currentMap = Instantiate(mapPrefabs[6]);
                spawnMap[currentMapPos.x, currentMapPos.y] = currentMap;
            }
            else if (map[currentMapPos.x, currentMapPos.y] == 3) //미니보스
            {
                currentMap = Instantiate(mapPrefabs[7]);
                spawnMap[currentMapPos.x, currentMapPos.y] = currentMap;
            }
            else if (map[currentMapPos.x, currentMapPos.y] == 4) //상점
            {
                currentMap = Instantiate(mapPrefabs[8]);
                spawnMap[currentMapPos.x, currentMapPos.y] = currentMap;
            }
            else if (map[currentMapPos.x, currentMapPos.y] == 5) //보상
            {
                currentMap = Instantiate(mapPrefabs[9]);
                spawnMap[currentMapPos.x, currentMapPos.y] = currentMap;
            }
            else
            {
                currentMap = Instantiate(mapPrefabs[Random.Range(1, mapPrefabs.Count - 4)]);
                spawnMap[currentMapPos.x, currentMapPos.y] = currentMap;
            }
                
        }
        else
        {
            spawnMap[currentMapPos.x,currentMapPos.y].SetActive(true);
        }

        PositionPlayer();
        SetDoorWall();

        MonsterSpawner ms = currentMap.GetComponentInChildren<MonsterSpawner>();
        if (ms != null && mapState[currentMapPos.x,currentMapPos.y]==1)
        {
            ms.SpawnMonster();
        }
            
        mapState[currentMapPos.x, currentMapPos.y] = 2;

    }

    void PositionPlayer()
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

        

        Vector2 targetPos;


        if (player == null) return;
        if (nextDir == Vector2.up)              // 아래방향임
        {
            targetPos = wallDoor.UpAnchor().position;
            targetPos.y -= spawnPadding;
            player.position = targetPos;
        }
        else if (nextDir == Vector2.down)       // 위방향임
        {
            targetPos = wallDoor.DownAnchor().position;
            targetPos.y += spawnPadding;
            player.position = targetPos;
        }
        else if (nextDir == Vector2.left)
        {
            targetPos = wallDoor.RightAnchor().position;
            targetPos.x -= spawnPadding;
            player.position = targetPos;
        }
        else if(nextDir == Vector2.right)
        {
            targetPos = wallDoor.LeftAnchor().position;
            targetPos.x += spawnPadding;
            player.position = targetPos;
        }
    }
}
