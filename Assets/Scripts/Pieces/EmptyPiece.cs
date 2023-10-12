using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyPiece : Piece
{
    public override bool CheckWarning()
    {
        throw new System.NotImplementedException();
    }

    public override Piece GetPiece()
    {
        throw new System.NotImplementedException();
    }

    public override bool IsValidMove(Vector3 targetPosition)
    {
        throw new System.NotImplementedException();
    }

    public override bool IsValidPosition(Vector3 targetPosition)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
