using UnityEngine;

public class Map : MonoBehaviour
{
    void HideMap()
    {
        gameObject.SetActive(false);
    }
    void ShowMap()
    {
        gameObject.SetActive(true);
    }

}
