using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectScope;

public abstract class Piece : MonoBehaviour
{
    protected Vector3 _currentPos;
    protected Vector3 _prevPos;

    protected static Vector3 _upLeftDiagonal;
    protected static Vector3 _downLeftDiagonal;
    protected static Vector3 _upRightDiagonal;
    protected static Vector3 _downRightDiagonal;

    protected static Vector3[] _diagonalsOnXYPlane;
    protected static Vector3[] _verticalDirections;
    protected static Vector3[] _horizontalDirections;

    protected Vector3[] _moveDirections;

    protected LayerMask _gameLayer;
    protected int _moveDirection;

    public bool HasMoved = false;
    public abstract bool IsValidPosition(Vector3 targetPosition);
    public abstract Piece GetPiece();
    public abstract bool IsValidMove(Vector3 targetPosition);
    public abstract bool CheckWarning();

    public bool IsWhite;

    private void Start()
    {
        AssignDirections();
    }

    private void AssignDirections()
    {
        _upLeftDiagonal = Vector3.up + Vector3.left;
        _upRightDiagonal = Vector3.up + Vector3.right;
        _downLeftDiagonal = Vector3.down + Vector3.left;
        _downRightDiagonal = Vector3.down + Vector3.right;

        _diagonalsOnXYPlane = new Vector3[]
        {
            _upLeftDiagonal,
            _upRightDiagonal,
            _downLeftDiagonal,
            _downRightDiagonal,
        };

        _verticalDirections = new Vector3[]
        {
            Vector3.up,
            Vector3.down
        };

        _horizontalDirections = new Vector3[]
        {
            Vector3.left,
            Vector3.right
        };
    }
}
