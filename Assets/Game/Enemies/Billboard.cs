using UnityEngine;

public class Billboard : MonoBehaviour
{
    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void LateUpdate()
    {
        Vector3 dir = transform.position - cam.transform.position;
        dir.y = 0;
        transform.forward = dir;
    }
}
