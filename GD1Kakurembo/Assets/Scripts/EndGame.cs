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
    [SerializeField]
    private List<GameObject> _disableAtEndOfGame;


    // Update is called once per frame
    void Update()
    {
        if (_infoManager.GetComponent<MovementPointManager>().Player1Won == true || _infoManager.GetComponent<MovementPointManager>().Player2Won == true)
        {
            foreach (GameObject obj in _disableAtEndOfGame)
            {
                obj.SetActive(false);
            }
            if (_infoManager.GetComponent<MovementPointManager>().Player1Won == true)
            {
                _player1WinScreen.SetActive(true);
            }
            else if (_infoManager.GetComponent<MovementPointManager>().Player2Won == true)
            {
                _player2WinScreen.SetActive(true);
            }
        }
    }
}
