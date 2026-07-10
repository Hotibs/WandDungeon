using UnityEngine;

public class Door : MonoBehaviour
{

    Collider2D cd;

    public Vector2Int doorDir;

    private void Awake()
    {
        cd = GetComponent<Collider2D>();
    }
    void Start()
    {
        OpenDoor();
    }

    
    void OpenDoor()
    {
        cd.isTrigger=true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            MapManager.instance.MoveNextRoom(doorDir);
        }
    }
}
