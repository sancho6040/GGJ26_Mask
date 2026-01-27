using System.Collections.Generic;
using UnityEngine;

public class LevelSegment : MonoBehaviour
{
    // Direcciones posibles
    public enum Direction { Forward, Back, Left, Right }

    // Dirección desde la que vino el segmento anterior
    public Direction? incomingDirection = null;

    // Devuelve una dirección aleatoria válida
    public Direction GetNextDirection()
    {
        List<Direction> possible = new List<Direction>
        {
            Direction.Forward,
            Direction.Back,
            Direction.Left,
            Direction.Right
        };

        // Excluir la dirección de entrada
        if (incomingDirection.HasValue)
        {
            possible.Remove(Opposite(incomingDirection.Value));
        }

        // Seleccionar aleatoria
        return possible[Random.Range(0, possible.Count)];
    }

    // Función auxiliar para obtener la dirección opuesta
    private Direction Opposite(Direction dir)
    {
        switch (dir)
        {
            case Direction.Forward: return Direction.Back;
            case Direction.Back: return Direction.Forward;
            case Direction.Left: return Direction.Right;
            case Direction.Right: return Direction.Left;
            default: return Direction.Forward;
        }
    }

    // Devuelve el offset en Vector3 para colocar el siguiente segmento
    public Vector3 GetOffset(Direction dir, float size = 10f)
    {
        switch (dir)
        {
            case Direction.Forward: return Vector3.forward * size;
            case Direction.Back: return Vector3.back * size;
            case Direction.Left: return Vector3.left * size;
            case Direction.Right: return Vector3.right * size;
            default: return Vector3.zero;
        }
    }

}
