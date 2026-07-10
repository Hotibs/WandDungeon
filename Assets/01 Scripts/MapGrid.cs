using UnityEngine;




public class MapGrid : MonoBehaviour
{

    private int[,] map;

    private Vector2Int boss;
    private Vector2Int miniBoss;
    private Vector2Int start;
    private Vector2Int shop;
    private Vector2Int reward;
    
    DrawMap dm = new DrawMap();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        
        
    }
    private void OnEnable()
    {
        dm = FindFirstObjectByType<DrawMap>();
        map = new int[9, 9];
        start = new Vector2Int(4, 4);
        boss = new Vector2Int(Random.Range(0, 2) == 1 ? Random.Range(0, 2) : Random.Range(7, 9),Random.Range(7, 9));
        miniBoss = new Vector2Int(Random.Range(0, 2) == 1 ? Random.Range(0, 2) : Random.Range(7, 9), Random.Range(0, 2));
        shop = new Vector2Int(Random.Range(6, 8), Random.Range(3, 6));
        reward = new Vector2Int(Random.Range(1, 3), Random.Range(3, 6));

        CreateGrid();
        CreateMap();
        dm.MapDisplay(map,start);
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public int[,] GetMap()
    {
        return map;
    }
    void CreateGrid()
    {
        map[start.x,start.y] = 1;       //시작 지점 및 길
        map[boss.x, boss.y] = 2;        //보스방
        map[miniBoss.x, miniBoss.y]=3;  //미니보스방 
        map[shop.x, shop.y] = 4;        //상점방
        map[reward.x, reward.y] = 5;    //보상방
    }
    /*
    void DebugGrid()
    {
        for (int i = 0; i < 9; i++)
        {
            Debug.Log($"{map[0, i]}{map[1, i]}{map[2, i]}{map[3, i]}{map[4,i]}{map[5, i]}{map[6, i]}{map[7, i]}{map[8, i]}");
        }
    }
    */
    void CreateMap() 
    {
        Vector2Int startPos = new Vector2Int(start.x, start.y);
        Vector2Int currentPos;
        Vector2Int targetPos = new Vector2Int();
        
        for (int i = 2; i < 6; i++) 
        {

            if (i == 2) targetPos = boss;
            else if (i == 3) targetPos = miniBoss;
            else if (i == 4) targetPos = shop;
            else if (i == 5) targetPos = reward;

            currentPos = startPos;
            
            while (true)
            {
                    
                int distX = targetPos.x - currentPos.x;
                int distY = targetPos.y - currentPos.y;

                int dirX = distX == 0 ? 0 : (distX > 0 ? 1 : -1);
                int dirY = distY == 0 ? 0 : (distY > 0 ? 1 : -1);

                if (dirX != 0 && dirY != 0)
                {
                    if (Random.Range(0, 2) == 0) currentPos.x += dirX;
                    else currentPos.y += dirY;
                }
                else if (dirX != 0)
                {
                    currentPos.x += dirX;
                }
                else if (dirY != 0)
                {
                    currentPos.y += dirY;
                }
                currentPos.x = Mathf.Clamp(currentPos.x, 0, 8);
                currentPos.y = Mathf.Clamp(currentPos.y, 0, 8);

                if (currentPos == targetPos)
                {
                    break;
                }
                if (map[currentPos.x, currentPos.y] == 0)
                {
                    map[currentPos.x, currentPos.y] = 6;
                }
            }
            
        }
    }


}
