using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMove : MonoBehaviour
{
    Camera camera;

    [SerializeField] Transform target;

    Vector3 velocity;
    Vector3 targetPos;
    


     void Start()
    {
        velocity = Vector3.zero;
        camera = GetComponent<Camera>();
    }

  
    void Update()
    {
        targetPos = new Vector3(target.position.x, target.position.y, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, targetPos+MousePos(), ref velocity, 0.2f);
    }

    Vector3 MousePos()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector3 worldPosition = camera.ScreenToWorldPoint(mousePos);
        Vector2 dir = worldPosition - transform.position;
        
        mousePos = new Vector3(dir.x, dir.y);

        return mousePos*0.2f;
    }
}
