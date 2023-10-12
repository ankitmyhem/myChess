using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;
using ProjectScope;

public class Pawn : Piece
{
    private Vector3[] _attackDirections;
    private void Start()
    {
        _gameLayer = LayerMask.GetMask("Game");
        _moveDirection = IsWhite ? 1 : -1;

        _moveDirections = new Vector3[]
        {
            Vector3.up
        };
        _attackDirections = new Vector3[]
        {
            MixedVector3.UpLeft * _moveDirection, MixedVector3.UpRight * _moveDirection
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
            if (targetPosition.y - transform.position.y == _moveDirection * 2)
            {
                if (targetPosition.x == transform.position.x)
                {
                    HasMoved = true;
                    return true;
                }
            }
            if (targetPosition.y - transform.position.y == _moveDirection)
            {
                HasMoved = true;
                return true;
            }
        }
        else
        {
            if (targetPosition.y - transform.position.y == _moveDirection)
            {
                return true;
            }
        }
        return false;
    }

    public override bool IsValidMove(Vector3 targetPosition)
    {
        Vector3 raycastOrigin = new(targetPosition.x, targetPosition.y, 0f);

        if (IsValidPosition(targetPosition))
        {
            if (IsAttacking(targetPosition))
            {
                if (IsSelf(raycastOrigin, targetPosition))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                if (AnyPieceAhead(transform.position, targetPosition))
                {
                    return false;
                }
                return true;
            }
        }
        return false;
    }

    private bool IsAttacking(Vector3 targetPosition)
    {
        if (Mathf.Abs(targetPosition.x - transform.position.x) == 1)
        {
            return true;
        }
        return false;
    }

    private bool IsSelf(Vector3 origin, Vector3 target)
    {
        Collider[] colliders = Physics.OverlapSphere(target, 0.2f, _gameLayer);
        if (colliders.Length > 0)
        {
            return colliders[0].transform.GetComponent<Piece>().IsWhite == this.IsWhite;
        }
        return true;
    }

    private bool AnyPieceAhead(Vector3 origin, Vector3 target)
    {
        if (Physics.Raycast(origin, target - origin, out RaycastHit hit, Vector3.Distance(target, origin), _gameLayer))
        {
            if (target.x * _moveDirection >= hit.transform.position.x * _moveDirection)
            {
                return true;
            }
            return false;
        }
        return false;
    }
    IEnumerator ShowPath(Vector3 target)
    {
        while (true)
        {
            Debug.DrawRay(transform.position, target - transform.position, Color.green);
            yield return null;
        }
    }

    public override bool CheckWarning()
    {
        foreach (Vector3 dir in _attackDirections)
        {
            if (IsEnemyKing(transform.position, dir, Mathf.Infinity, _gameLayer))
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
            if (hit.transform.GetComponent<King>())
            {
                return true;
            }
        }
        return false;
    }

    public bool DiscoveredCheck()
    {
        return false;
    }
}
