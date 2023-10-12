using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PieceManager : MonoBehaviour
{
    [SerializeField] private White[] WhitePieces;
    [SerializeField] private Black[] BlackPieces;

    private List<White> WhiteList;
    private List<Black> BlackList;

    [SerializeField] private White[] _revivableWhitePieces;
    [SerializeField] private Black[] _revivableBlackPieces; 

    public Piece LastMovedPiece { get; private set; }
    public Vector3 LastPosition { get; private set; }

    public King BlackKing { get; private set; }
    public King WhiteKing { get; private set; }

    public static PieceManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        foreach (var piece in WhitePieces)
        {
            if (piece.GetComponent<King>())
            {
                WhiteKing = piece.GetComponent<King>();
                break;
            }
            yield return null;
        }
        foreach (var piece in BlackPieces)
        {
            if (piece.GetComponent<King>())
            {
                BlackKing = piece.GetComponent<King>();
                break;
            }
            yield return null;
        }
        WhiteList = WhitePieces.ToList();
        BlackList = BlackPieces.ToList();
    }

    public bool IsAttacked(bool whiteTurn)
    {
        if (whiteTurn)
        {
            foreach (var piece in BlackList)
            {
                if (piece.GetComponent<King>())
                {
                    continue;
                }
                if (piece != null && piece.GetComponent<Piece>().CheckWarning())
                {
                    return true;
                }
            }
            return false;
        }
        else
        {
            foreach (var piece in WhiteList)
            {
                if (piece.GetComponent<King>())
                {
                    continue;
                }
                if (piece != null && piece.GetComponent<Piece>().CheckWarning())
                {
                    return true;
                }
            }
            return false;
        }
    }

    public void RecordLastMove(Piece lastMovedPiece, Vector3 positionBeforeMoved)
    {
        LastMovedPiece = lastMovedPiece;
        LastPosition = positionBeforeMoved;
    }

    public void GetPreviousMoveInfo(out Piece lastMoved, out Vector3 lastPosition)
    {
        lastMoved = LastMovedPiece;
        lastPosition = LastPosition;
    }

    public void PieceCaptured(Piece capturedPiece, bool whiteTurn)
    {
        if (capturedPiece == null) { return; }
        if (whiteTurn)
        {
            foreach (var piece in WhiteList)
            {
                if (capturedPiece == piece.GetComponent<Piece>())
                {
                    WhiteList.Remove(piece);
                    Destroy(piece.gameObject);
                    break;
                }
            }
        }
        else
        {
            foreach (var piece in BlackList)
            {
                if (capturedPiece == piece.GetComponent<Piece>())
                {
                    BlackList.Remove(piece);
                    Destroy(piece.gameObject);
                    break;
                }
            }
        }
    }
}
