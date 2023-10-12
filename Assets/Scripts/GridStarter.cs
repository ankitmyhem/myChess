using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridStarter : MonoBehaviour
{
    [SerializeField] private Grid _whiteGrid;
    [SerializeField] private Grid _blackGrid;
    private readonly int _gridSize = 8;
    private bool _spawnedWhite = false;
    private Vector3 _spawnPos = Vector3.zero;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        for (int i = 0; i<_gridSize; i++)
        {
            _spawnPos.y = i;
            for(int j = 0; j<_gridSize; j++)
            {
                _spawnPos.x = j;
                if (_spawnedWhite)
                {
                    _spawnPos.z = _blackGrid.transform.position.z;
                    Instantiate(_blackGrid, _spawnPos, _blackGrid.transform.rotation);
                }
                else
                {
                    _spawnPos.z = _whiteGrid.transform.position.z;
                    Instantiate(_whiteGrid, _spawnPos, _whiteGrid.transform.rotation);
                }
                _spawnedWhite = !_spawnedWhite;
                yield return null;
            }
            _spawnedWhite = !_spawnedWhite;
        }
    }
}
