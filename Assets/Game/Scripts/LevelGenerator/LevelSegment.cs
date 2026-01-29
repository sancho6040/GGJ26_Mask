using UnityEngine;
using System.Collections.Generic;

public class LevelSegment : MonoBehaviour
{
    public SectionEntrance forwardEntrance;
    public SectionEntrance backEntrance;
    public SectionEntrance leftEntrance;
    public SectionEntrance rightEntrance;

    public enum Direction { Forward, Back, Left, Right }
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

        // Excluir dirección de entrada
        if (incomingDirection.HasValue)
        {
            var oppositeDirection = Opposite(incomingDirection.Value);
            possible.Remove(oppositeDirection);
            print($"{incomingDirection} -> {oppositeDirection}");
        }

        // Excluir sockets ya usados
        possible.RemoveAll(dir => GetEntrance(dir).isUsed);

        print($"{gameObject.name} - posibble values: ");
        foreach (var item in possible)
        {
            print(possible.ToString());
        }

        return possible.Count > 0 ? possible[Random.Range(0, possible.Count)] : incomingDirection.Value;
    }

    // Ajusta la posición y rotación del segmento para que su socket coincida con otro socket
    public void SetPositionToEntrance(SectionEntrance targetEntrance, SectionEntrance myEntrance)
    {
        // Calcula la diferencia de rotación
        Quaternion rotationOffset = Quaternion.Inverse(myEntrance.transform.rotation) * targetEntrance.transform.rotation;

        // Aplica la rotación al segmento completo
        transform.rotation = rotationOffset * transform.rotation;

        // Calcula el desplazamiento necesario
        Vector3 positionOffset = targetEntrance.transform.position - myEntrance.transform.position;

        // Aplica el desplazamiento al segmento completo
        transform.position += positionOffset;
    }


    public SectionEntrance GetEntrance(Direction dir)
    {
        switch (dir)
        {
            case Direction.Forward: return forwardEntrance;
            case Direction.Back: return backEntrance;
            case Direction.Left: return leftEntrance;
            case Direction.Right: return rightEntrance;
            default: return null;
        }
    }

    public Direction Opposite(Direction dir)
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
}