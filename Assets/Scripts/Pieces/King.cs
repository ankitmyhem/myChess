using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Piece
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

        if (!HasMoved)
        {
            HasMoved = true;
        }
        if(Vector3.Distance(transform.position, targetPosition)<=Mathf.Sqrt(2))
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
            if (Physics.Raycast(transform.position, dir, out RaycastHit hit, Vector3.Distance(targetPosition, transform.position), _gameLayer))
            {

                if (hit.transform.position == targetPosition && hit.transform.GetComponent<Piece>())
                {
                    bool isSelf = this.IsWhite == hit.transform.GetComponent<Piece>().IsWhite;
                    if (isSelf)
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
                    return false;
                }
            }
            return true;
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
        return false;
    }

    public bool DiscoveredCheck()
    {
        return false;
    }
}
