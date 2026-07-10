using UnityEngine;

public class WallDoorContoller : MonoBehaviour
{
    [SerializeField] GameObject wall;
    [SerializeField] GameObject door;

    [SerializeField] Transform upAnchor;
    [SerializeField] Transform downAnchor;
    [SerializeField] Transform leftAnchor;
    [SerializeField] Transform rightAnchor;
    

    public void SetDoor(bool hasUp, bool hasDown, bool hasLeft, bool hasRight)
    {
        if (hasUp)
        {
            Instantiate(door, upAnchor.transform);
        }
        else{
            Instantiate(wall, upAnchor.transform);
        }
        if (hasDown)
        {
            Instantiate(door, downAnchor.transform);
        }
        else
        {
            Instantiate(wall, downAnchor.transform);
        }
        if (hasLeft)
        {
            Instantiate(door, leftAnchor.transform);
        }
        else{
            Instantiate(wall, leftAnchor.transform);
        }
        if (hasRight)
        {
            Instantiate(door, rightAnchor.transform);
        }
        else
        {
            Instantiate(wall, rightAnchor.transform);
        }
    }
}
