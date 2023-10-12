using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Knight : Piece
{
    private void Start()
    {
        _gameLayer = LayerMask.GetMask("Game");
    }
    public override Piece GetPiece()
    {
        return this;
    }

    public override bool IsValidPosition(Vector3 targetPosition)
    {
        if (Vector3.Distance(transform.position, targetPosition) > 3) return false;

        if (!HasMoved)
        {
            HasMoved = true;
        }
        if (Mathf.Abs(targetPosition.x - transform.position.x) * 2 == Mathf.Abs(targetPosition.y - transform.position.y))
        {
            return true;
        }
        if (Mathf.Abs(targetPosition.y - transform.position.y) * 2 == Mathf.Abs(targetPosition.x - transform.position.x))
        {
            return true;
        }
        return false;
    }

    public override bool IsValidMove(Vector3 targetPosition)
    {
        return IsValidPosition(targetPosition);
    }

    public override bool CheckWarning()
    {
        Vector3[] possiblePositions = PossibleValidPositions(transform.position);

        foreach (Vector3 pos in possiblePositions)
        {
            if (IsEnemyKing(pos, _gameLayer))
            {

                return true;
            }
        }
        return false;
    }

    private Vector3[] PossibleValidPositions(Vector3 origin)
    {
        return new Vector3[]
        {
            new Vector3(origin.x + 2, origin.y + 1, origin.z),
            new Vector3(origin.x + 2, origin.y - 1, origin.z),
            new Vector3(origin.x - 2, origin.y + 1, origin.z),
            new Vector3(origin.x - 2, origin.y - 1, origin.z),
            new Vector3(origin.x + 1, origin.y + 2, origin.z),
            new Vector3(origin.x - 1, origin.y + 2, origin.z),
            new Vector3(origin.x + 1, origin.y - 2, origin.z),
            new Vector3(origin.x - 1, origin.y - 2, origin.z)
        };
    }

    private bool IsEnemyKing(Vector3 origin, int layerMask)
    {
        Collider[] colliders = Physics.OverlapSphere(origin, 0.2f, layerMask);
        if (colliders.Length > 0)
        {
            if (colliders[0].GetComponent<King>() && !IsSelf(colliders[0]))
            {
                return true;
            }
        }
        return false;
    }

    private bool AnyPieceAhead(Vector3 origin, Vector3 target, out RaycastHit detected)
    {
        if (Physics.Raycast(origin, target - origin, out RaycastHit hit, Vector3.Distance(target, origin), _gameLayer))
        {
            if (hit.transform.position == target)
            {
                detected = hit;
                return true;
            }
        }
        detected = hit;
        return false;
    }

    private bool IsSelf(Collider detected)
    {
        return detected.transform.GetComponent<Piece>().IsWhite == this.IsWhite;
    }

    public bool DiscoveredCheck()
    {
        return false;
    }
}
