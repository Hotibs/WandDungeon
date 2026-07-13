using UnityEngine;

public class WallDoorContoller : MonoBehaviour
{
    [SerializeField] GameObject wall;
    [SerializeField] GameObject door;

    [SerializeField] Transform upAnchor;
    [SerializeField] Transform downAnchor;
    [SerializeField] Transform leftAnchor;
    [SerializeField] Transform rightAnchor;
    
    public Transform UpAnchor()
    {
        return upAnchor;
    }
    public Transform DownAnchor()
    {
        return downAnchor;
    }
    public Transform LeftAnchor()
    {
        return leftAnchor;
    }
    public Transform RightAnchor()
    {
        return rightAnchor;
    }


    public void SetDoor(bool hasUp, bool hasDown, bool hasLeft, bool hasRight)
    {
        ClearAnchor(upAnchor);
        ClearAnchor(downAnchor);
        ClearAnchor(leftAnchor);
        ClearAnchor(rightAnchor);


        if (hasUp)
        {
            GameObject doorObj = Instantiate(door, upAnchor.transform);
            doorObj.GetComponent<Door>().doorDir = Vector2Int.down;
        }
        else{
            Instantiate(wall, upAnchor.transform);
        }
        if (hasDown)
        {
            GameObject doorObj = Instantiate(door, downAnchor.transform);
            doorObj.GetComponent<Door>().doorDir = Vector2Int.up;
        }
        else
        {
            Instantiate(wall, downAnchor.transform);
        }
        if (hasLeft)
        {
            GameObject doorObj = Instantiate(door, leftAnchor.transform);
            doorObj.GetComponent<Door>().doorDir = Vector2Int.left;
        }
        else{
            Instantiate(wall, leftAnchor.transform);
        }
        if (hasRight)
        {
            GameObject doorObj = Instantiate(door, rightAnchor.transform);
            doorObj.GetComponent<Door>().doorDir = Vector2Int.right;
        }
        else
        {
            Instantiate(wall, rightAnchor.transform);
        }
    }
    private void ClearAnchor(Transform anchor)
    {
        if (anchor == null) return;
        foreach (Transform child in anchor)
        {
            Destroy(child.gameObject);
        }
    }
}
