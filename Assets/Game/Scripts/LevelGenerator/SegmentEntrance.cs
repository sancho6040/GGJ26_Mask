using UnityEngine;

public class SectionEntrance : MonoBehaviour
{
    public bool isUsed = false;

    public void UseEntrance()
    {
        isUsed = true;
        gameObject.SetActive(true);
    }
}