using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField]
    private GameObject _infoManager;
    [SerializeField]
    private GameObject _player1WinScreen;
    [SerializeField]
    private GameObject _player2WinScreen;


    // Update is called once per frame
    void Update()
    {

        if (_infoManager.GetComponent<MovementPointManager>().Player1Won == true)
        {
            _player1WinScreen.SetActive(true);
        }
        else if(_infoManager.GetComponent<MovementPointManager>().Player2Won == true)
        {
            _player2WinScreen.SetActive(true);
        }

    }
}
