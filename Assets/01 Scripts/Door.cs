using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{

    Collider2D cd;
    SpriteRenderer sr;

    [SerializeField] Sprite img;

    public Vector2Int doorDir;

    private void Awake()
    {
        cd = GetComponent<Collider2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }
    void Start()
    {

    }

    
    public void OpenDoor()
    {
        cd.isTrigger=true;
        sr.sprite = img;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            MapManager.instance.MoveNextRoom(doorDir);
        }
    }
}
