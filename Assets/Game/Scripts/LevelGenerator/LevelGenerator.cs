using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] InitialsegmentPrefab;
    public GameObject[] SegmentPrefabs;
    public int maxSegments = 10;
    public float segmentSize = 10f;

    private GameObject currentSegment;

    void Start()
    {
        // Instanciar el inicial
        currentSegment = Instantiate(SegmentPrefabs[0], Vector3.zero, Quaternion.identity);

        // Generar el resto
        for (int i = 1; i < maxSegments; i++)
        {
            LevelSegment segScript = currentSegment.GetComponent<LevelSegment>();
            LevelSegment.Direction nextDir = segScript.GetNextDirection();

            Vector3 newPos = currentSegment.transform.position + segScript.GetOffset(nextDir, segmentSize);

            GameObject newSegment = Instantiate(SegmentPrefabs[Random.Range(0, SegmentPrefabs.Length)], newPos, Quaternion.identity);

            // Configurar dirección de entrada del nuevo segmento
            LevelSegment newSegScript = newSegment.GetComponent<LevelSegment>();
            newSegScript.incomingDirection = nextDir;

            currentSegment = newSegment;
        }
    }

}
