using UnityEngine;

public class imageOrientation : MonoBehaviour
{
    public Transform cameraTranform;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cameraTranform = Camera.main.transform;

        if (cameraTranform != null)
        {
            transform.rotation = Quaternion.Inverse(cameraTranform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
