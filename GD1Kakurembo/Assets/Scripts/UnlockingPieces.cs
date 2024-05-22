using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockingPieces : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _unlockablePiecesP1;
    [SerializeField]
    private GameObject[] _unlockablePiecesP2;

    public int _currentTurn = 0;

    // Update is called once per frame
    void Update()
    {
        if(_currentTurn == 3)
            _unlockablePiecesP1[0].gameObject?.SetActive(true);
        else if(_currentTurn == 5)
            _unlockablePiecesP2[0].gameObject?.SetActive(true);
        else if (_currentTurn == 7)
            _unlockablePiecesP1[1].gameObject?.SetActive(true);
        else if (_currentTurn == 9)
            _unlockablePiecesP2[1].gameObject?.SetActive(true);
    }
}
