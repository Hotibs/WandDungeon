using UnityEngine;

public class MapManager : MonoBehaviour
{

    public static MapManager instance;

    [SerializeField] WallDoorContoller wallDoor;
    [SerializeField] MapGrid mg;

    Vector2Int currentMapPos;
    Vector2Int startPos;

    int[,] map;

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
        
        map = mg.GetMap();
        SetDoorWall();
    }
    


    void Update()
    {
        
    }

    void SetDoorWall()
    {
        bool hasUp=false;
        bool hasDown = false;
        bool hasLeft = false;
        bool hasRight = false;

        if(map[currentMapPos.x,currentMapPos.y + 1 ]!= 0)
        {
            hasUp = true;
        }
        if(map[currentMapPos.x,currentMapPos.y - 1] != 0)
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

    void MoveMap()
    {

    }
}
