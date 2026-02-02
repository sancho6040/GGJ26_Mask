using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public LevelSegment initialSegment;
    public LevelSegment[] segmentPrefabs;
    public int maxSegments = 10;

    private LevelSegment currentSegment;
    private HashSet<Vector3> occupiedPositions = new HashSet<Vector3>();
    public float segmentSize = 10f; // tamaño de cada casilla

    public GameObject SpawnPointInstance;

    void Start()
    {
        currentSegment = Instantiate(initialSegment, Vector3.zero, Quaternion.identity);
        occupiedPositions.Add(Vector3.zero);

        if (currentSegment.SpawnPoint != null)
        {
            SpawnPointInstance = currentSegment.SpawnPoint;
        }

        for (int i = 1; i < maxSegments; i++)
        {
            LevelSegment SelectedSegment = currentSegment;
            LevelSegment.Direction nextDir = SelectedSegment.GetNextDirection();
            SectionEntrance currentEntrance = SelectedSegment.GetEntrance(nextDir);

            // Instanciar nuevo segmento
            LevelSegment newSegment = Instantiate(
                segmentPrefabs[Random.Range(0, segmentPrefabs.Length)],
                Vector3.zero,
                Quaternion.identity
            );

            var oppositeDirection = newSegment.Opposite(nextDir);
            SectionEntrance newEntrance = newSegment.GetEntrance(oppositeDirection);

            // Posicionar
            newSegment.SetPositionToEntrance(currentEntrance, newEntrance);

            // Verificar si ya está ocupado
            Vector3 gridPos = RoundToGrid(newSegment.transform.position);
            if (occupiedPositions.Contains(gridPos))
            {
                Destroy(newSegment.gameObject); // cancelar si ya hay algo
                continue;
            }

            occupiedPositions.Add(gridPos);

            // Marcar sockets
            currentEntrance.UseEntrance();
            newEntrance.UseEntrance();

            newSegment.incomingDirection = oppositeDirection;
            currentSegment = newSegment;
        }
    }

    Vector3 RoundToGrid(Vector3 pos)
    {
        return new Vector3(
            Mathf.Round(pos.x / segmentSize) * segmentSize,
            Mathf.Round(pos.y / segmentSize) * segmentSize,
            Mathf.Round(pos.z / segmentSize) * segmentSize
        );
    }
}