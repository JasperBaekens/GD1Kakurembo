using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockingPieces : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _unlockablePiecesP1;
    [SerializeField]
    private GameObject[] _unlockablePiecesP2;

    public int _currentTurn = 1;

    // Update is called once per frame
    void Update()
    {

        if (_currentTurn / 2 % 2 == 0)
        {
            Debug.Log("P1");
            if (_unlockablePiecesP1 != null)
                for (int i = 0; i < _unlockablePiecesP1.Length - 1; i++)
                {
                    if (!_unlockablePiecesP1[i].gameObject.active)
                    {

                        _unlockablePiecesP1[i].gameObject?.SetActive(true);
                        break;
                    }

                }

        }
        else
        {
            Debug.Log("P2");
            if (_unlockablePiecesP2 != null)
                for (int i = 0; i < _unlockablePiecesP2.Length - 1; i++)
                {
                    if (!_unlockablePiecesP2[i].gameObject.active)
                    {

                        _unlockablePiecesP2[i].gameObject?.SetActive(true);
                        break;
                    }

                }
        }

    }
}
