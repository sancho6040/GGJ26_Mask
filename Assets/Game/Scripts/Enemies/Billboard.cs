using UnityEngine;

public class Billboard : MonoBehaviour
{
    Camera cam;

    void Start()
    {
        SetCamera();
    }

    private void SetCamera()
    {
        cam = Camera.main;
    }

    void LateUpdate()
    {
        if (cam == null)
        {
            SetCamera();
            print($"camera nor found at: {transform.parent.name}");
            return;
        }
        Vector3 dir = transform.position - cam.transform.position;
        dir.y = 0;
        transform.forward = dir;
    }
}
