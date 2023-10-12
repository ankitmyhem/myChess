using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    private RaycastHit _detectedObject;
    private LayerMask _gameLayer;
    private Piece _pieceOnGrid;
    // Start is called before the first frame update
    void Start()
    {
        _gameLayer = LayerMask.GetMask("Game");
        if (Physics.Raycast(transform.position, Vector3.back, out _detectedObject, Mathf.Infinity, _gameLayer))
        {
            if (_detectedObject.transform.GetComponent<Piece>())
            {
                _pieceOnGrid = _detectedObject.transform.GetComponent<Piece>();
            }
        }
    }

    public Piece GetPieceOverGrid()
    {
        return _pieceOnGrid;
    }

}
