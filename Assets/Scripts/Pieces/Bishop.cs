using ProjectScope;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece, IDiscoveredCheck
{
    private void Start()
    {
        _gameLayer = LayerMask.GetMask("Game");
        _moveDirections = new Vector3[]
        {
            MixedVector3.UpLeft,
            MixedVector3.UpRight,
            MixedVector3.DownLeft,
            MixedVector3.DownRight,
        };
    }
    public override Piece GetPiece()
    {
        return this;
    }

    public override bool IsValidPosition(Vector3 targetPosition)
    {
        if (!HasMoved)
        {
            HasMoved = true;
        }
        if (targetPosition.x - transform.position.x == targetPosition.y - transform.position.y)
        {
            return true;
        }
        if (targetPosition.x - transform.position.x == -(targetPosition.y - transform.position.y))
        {
            return true;
        }
        
        return false;
    }

    public override bool IsValidMove(Vector3 targetPosition)
    {
        if (IsValidPosition(targetPosition))
        {
            Vector3 dir = targetPosition - transform.position;
            Debug.Log(targetPosition - transform.position);

            if (AnyPieceAhead(transform.position, targetPosition, out RaycastHit other))
            {
                if (IsSelf(other))
                {
                    return false;
                }
            }
            return true;
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

    public override bool CheckWarning()
    {
        foreach(Vector3 dir in _moveDirections)
        {
            if(IsEnemyKing(transform.position, dir, Mathf.Infinity, _gameLayer))
            {

                return true;
            }
        }
        return false;
    }

    private bool IsEnemyKing(Vector3 origin, Vector3 direction, float maxDistance, int layerMask)
    {
        if (Physics.Raycast(origin, direction, out RaycastHit hit, maxDistance, layerMask))
        {
            if (hit.transform.GetComponent<King>() && !IsSelf(hit))
            {
                return true;
            }
        }
        return false;
    }

    private bool IsSelf(RaycastHit detected)
    {
        return detected.transform.GetComponent<Piece>().IsWhite == this.IsWhite;
    }

    private bool IsEnemyKing(RaycastHit piece)
    {
        if (!IsSelf(piece))
        {
            return piece.transform.GetComponent<King>();
        }
        return false;
    }

    public bool DiscoveredCheck(out RaycastHit pinnedPiece, out bool isDiscovered)
    {
        foreach (Vector3 dir in _moveDirections)
        {
            RaycastHit[] raycastHits = Physics.RaycastAll(transform.position, dir, 8, _gameLayer);
            if (raycastHits.Length > 1)
            {
                if (IsEnemyKing(raycastHits[1]))
                {
                    pinnedPiece = raycastHits[0];
                    isDiscovered = true;
                    return true;
                }
                else if (IsEnemyKing(raycastHits[0]))
                {
                    pinnedPiece = raycastHits[0];
                    isDiscovered = false;
                    return false;
                }
            }
        }
        isDiscovered = false;
        pinnedPiece = new RaycastHit();
        return false;
    }

}
