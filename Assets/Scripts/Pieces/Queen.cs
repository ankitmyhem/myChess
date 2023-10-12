using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using ProjectScope;

public class Queen : Piece, IDiscoveredCheck
{
    private void Start()
    {
        // Make sure in start that right LayerMask is provided
        _gameLayer = LayerMask.GetMask("Game");
        _moveDirections = new Vector3[]
        {
            Vector3.up,
            Vector3.down,
            Vector3.left,
            Vector3.right,
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
        if(targetPosition.x - transform.position.x == targetPosition.y - transform.position.y)
        {
            return true;
        }
        if(targetPosition.x - transform.position.x == -(targetPosition.y - transform.position.y))
        {
            return true;
        }
        if(targetPosition.x == transform.position.x || targetPosition.y == transform.position.y)
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
            StartCoroutine(nameof(ShowPath), targetPosition);

            if (!IsBlocked(transform.position, targetPosition, out RaycastHit detected))
            {
                if (detected.transform == null)
                {
                    return true;
                }
                else
                {
                    if (IsSelf(detected))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private bool IsSelf(RaycastHit detected)
    {
        return detected.transform.GetComponent<Piece>().IsWhite == this.IsWhite;
    }

    private bool IsBlocked(Vector3 origin, Vector3 target, out RaycastHit detected)
    {
        //Will return true if any object is between or on the targetPosition
        if (Physics.Raycast(origin, target - origin, out RaycastHit hit, Vector3.Distance(target, origin), _gameLayer))
        {   
            float magOfPiecePos = Vector3.Magnitude(hit.transform.position - transform.position);  //magnitude of vector from this.transform to detected piece
            float magOfTargetPos = Vector3.Magnitude(target - transform.position);                 //magintude of vector from this.transform to targetPos
            
            if (magOfPiecePos < magOfTargetPos)
            {
                //Return true if any piece is found between transform.position and targetposition
                detected = hit;
                return true;
            }
        }
        //Return true if no collider was hit or collider found at targetPosition.
        detected = hit;
        return false;
    }

    IEnumerator ShowPath(Vector3 target)
    {
        while (true)
        {
            Debug.DrawRay(transform.position, target-transform.position, Color.green);
            yield return null;
        }
    }

    public override bool CheckWarning()
    {
        foreach (Vector3 dir in _moveDirections)
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
            if (hit.transform.GetComponent<King>() && !IsSelf(hit))
            {
                return true;
            }
        }
        return false;
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
        RaycastHit newObject = new RaycastHit();
        isDiscovered = false;
        foreach (Vector3 dir in _moveDirections)
        {
            RaycastHit[] raycastHits = Physics.RaycastAll(transform.position, dir, 8, _gameLayer);
            if (raycastHits.Length > 1)
            {
                if (IsEnemyKing(raycastHits[1]))
                {
                    Debug.Log("Discovered Check");
                    pinnedPiece = raycastHits[0];
                    isDiscovered = true;
                    return true;
                }
                else if (IsEnemyKing(raycastHits[0]))
                {
                    isDiscovered = false;
                    pinnedPiece = raycastHits[0];
                    Debug.Log("Check");
                    return true;
                }
            }
        }
        pinnedPiece = newObject;
        return false;
    }

}
