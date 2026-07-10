using UnityEngine;
using UnityEngine.UI;

public class DrawMap : MonoBehaviour
{
    Color colorEmpty = new Color(0,0,0,0);   
    Color colorStart = Color.green;
    Color colorBoss = Color.darkRed;
    Color colorMiniBoss = Color.red;
    Color colorShop = Color.yellow;
    Color colorReward = Color.purple;
    Color colorPath = Color.white;
    Color colorCurrent = Color.black;


    public void MapDisplay(int[,] map)
    {
        int width = map.GetLength(0);
        int height = map.GetLength(1);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int index = y * width + x;

                Image img = transform.GetChild(index).GetComponent<Image>();

                if (img != null)
                {
                    int mapValue = map[x, y];
                    img.color = GetColor(mapValue);
                }
            }
        }
    }

    private Color GetColor(int value)
    {
        switch (value)
        {
            case 1:
                return colorStart;
            case 2:
                return colorBoss;
            case 3:
                return colorMiniBoss;
            case 4:
                return colorShop;
            case 5:
                return colorReward;
            case 6:
                return colorPath;
            case 10:
                return colorCurrent;
            default:
                return colorEmpty;
        }
        
    }
}
