using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum Move { BlackMove, WhiteMove}
    private Move move;
    private Camera m_Camera;
    [SerializeField] private LayerMask _gameLayer;
    [SerializeField] private LayerMask _gridLayer;

    [SerializeField] private King _whiteKing;
    [SerializeField] private King _blackKing;

    private bool _whiteTurn = true;
    private bool _moved = false;

    private Piece _selectedPiece;
    private bool _pieceSelected = false;
    private bool _gridSelected = false;

    public static Action PieceSelected;
    public static Action GridSelected;
    public static Action PieceMoved;
    public static Action FinalizeMove;
    public static Action CancelMove;

    public static GameManager Instance { get; private set; }
    // Start is called before the first frame update
    private void Start()
    {
        move = Move.WhiteMove;
        Instance = this;
        m_Camera = Camera.main;
        StartCoroutine(nameof(Movement));
    }


    //private void LateUpdate()
    //{
    //    AttackSinceLastMove(_whiteTurn);
    //}

    IEnumerator Movement()
    {
        while (true)
        {
            if (_moved)
            {
                if (AttackSinceLastMove(_whiteTurn))
                {
                    CancelLastMove();
                }
                else PieceManager.Instance.PieceCaptured(CapturePiece(_selectedPiece.transform.position), _whiteTurn);
            }
            if (Input.GetMouseButtonDown(0))
            {
                if (_pieceSelected)
                {
                    if (!SelectAnotherPieceToMove())
                    {
                        ExecuteMove(SelectedPositionToMove());
                        yield return new WaitForSeconds(0.1f);
                    }
                }
                else
                {
                    SelectedPieceToMove(_whiteTurn);
                }
            }
            yield return null;
        }
    }

    private void SelectedPieceToMove(bool whiteTurn)
    {
        if (Physics.Raycast(m_Camera.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, out RaycastHit hit, Mathf.Infinity, _gameLayer))
        {
            if (hit.transform.GetComponent<Piece>() == null)
            {
                return;
            }

            if (whiteTurn)
            {
                if (hit.transform.GetComponent<Piece>().IsWhite)
                {
                    _selectedPiece = hit.transform.GetComponent<Piece>();
                    _pieceSelected = true;
                    _gridSelected = false;
                }
            }
            else
            {
                if (!hit.transform.GetComponent<Piece>().IsWhite)
                {
                    _selectedPiece = hit.transform.GetComponent<Piece>();
                    _pieceSelected = true;
                    _gridSelected = false;
                }
            }
        }
    }

    private bool SelectAnotherPieceToMove()
    {
        if (Physics.Raycast(m_Camera.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, out RaycastHit hit, Mathf.Infinity, _gameLayer))
        {
            if (hit.transform.GetComponent<Piece>() == null)
            {
                return false;
            }
            if (_whiteTurn)
            {
                if (hit.transform.GetComponent<Piece>().IsWhite)
                {
                    _selectedPiece = hit.transform.GetComponent<Piece>();
                    return true;
                }
            }
            else
            {
                if (!hit.transform.GetComponent<Piece>().IsWhite)
                {
                    _selectedPiece = hit.transform.GetComponent<Piece>();
                    return true;
                }
            }
        }
        return false;
    }

    private RaycastHit SelectedPositionToMove()
    {
        if (Physics.Raycast(m_Camera.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, out RaycastHit grid, Mathf.Infinity, _gridLayer))
        {
            if (grid.transform.GetComponent<Grid>())
            {
                _gridSelected = true;
                _pieceSelected = false;
            }
        }
        return grid;
    }

    private void ExecuteMove(RaycastHit grid)
    {
        Vector3 checkPos = GridToPiecePosition(grid);

        Collider[] hitted = Physics.OverlapSphere(checkPos, 0.2f, _gameLayer);

        bool isSelf = IsSelf(hitted);
        

        if (_selectedPiece.IsValidMove(checkPos) && !isSelf)
        {
            PieceManager.Instance.RecordLastMove(_selectedPiece, _selectedPiece.transform.position);
            _selectedPiece.transform.position = checkPos;
            
            if (_selectedPiece.GetComponent<IDiscoveredCheck>() != null)
            {
                //if (_selectedPiece.GetComponent<IDiscoveredCheck>().DiscoveredCheck(out RaycastHit pinnedPiece, out bool isDiscovered))
                //    Debug.Log("Check");
            }

            //if (_selectedPiece.CheckWarning())
            //{
            //    Debug.Log("Check");
            //}
            _moved = true;
        }
    }

    private bool AttackSinceLastMove(bool whiteTurn)
    {
        if (_moved)
        {
            if (PieceManager.Instance.IsAttacked(whiteTurn))
            {
                _moved = false;
                return true;
            }
            else
            {
                FinishMove();
            }
            Debug.Log($"{Time.deltaTime} This");
        }
        return false;
    }

    private void FinishMove()
    {
        _whiteTurn = !_whiteTurn;
        _moved = false;
    }
    

    private Vector3 GridToPiecePosition(RaycastHit grid)
    {
        return new Vector3(grid.transform.position.x, grid.transform.position.y, 0);
    }

    private bool IsSelf(Collider[] colliders)
    {
        if(colliders.Length > 0)
        {
            return colliders[0].transform.GetComponent<Piece>().IsWhite == _selectedPiece.IsWhite;
        }
        return false;
    }

    public void CancelLastMove()
    {
        PieceManager.Instance.GetPreviousMoveInfo(out Piece lastMoved, out Vector3 lastPos);
        lastMoved.transform.position = lastPos;
    }
    //
    // Summary:
    //     Destroys object found on the given position
    // Parameters:
    //     A position to check for objects with colliders
    //
    private Piece CapturePiece(Vector3 checkPos)
    {
        Collider[] hitted = Physics.OverlapSphere(checkPos, 0.2f, _gameLayer);
        bool hit = hitted.Length > 0;

        if (hit) {
            for (int i = 0; i < hitted.Length; i++)
            {
                if (hitted[i].transform != null && hitted[i].transform != _selectedPiece.transform)
                {
                    return hitted[i].GetComponent<Piece>();
                }
            }
        }
        return null;
    }


}
