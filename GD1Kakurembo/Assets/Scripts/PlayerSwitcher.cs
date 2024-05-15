using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{

    [SerializeField]
    private Camera _mainCamera;
    [SerializeField]
    private Camera _playerCamera1;
    [SerializeField]
    private Camera _playerCamera2;

    private Camera _currentActiveCamera;

    [SerializeField]
    private GameObject _player1;
    [SerializeField]
    private GameObject _player2;

    [SerializeField]
    private GameObject[] _toggleActiveListOnStartOfNextPlayerTurn;

    // Start is called before the first frame update
    void Start()
    {
        //copy properties of current- to maincamera
        _currentActiveCamera = _playerCamera1;
        UpdateCamera();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerView();
    }

    private void UpdatePlayerView()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (_player1.activeSelf)
            {
                _player1.SetActive(false);
                _player2.SetActive(true);
                _currentActiveCamera = _playerCamera1;
            }
            else
            {
                _player1.SetActive(true);
                _player2.SetActive(false);
                _currentActiveCamera = _playerCamera2;
            }
            foreach (GameObject obj in _toggleActiveListOnStartOfNextPlayerTurn)
                obj.SetActive(false);
        }

        if (_player1.activeSelf == true)
            _currentActiveCamera = _playerCamera1;
        else
            _currentActiveCamera = _playerCamera2;
        UpdateCamera();
    }
    private void UpdateCamera()
    {
        _mainCamera.transform.position = _currentActiveCamera.transform.position;
        _mainCamera.transform.rotation = _currentActiveCamera.transform.rotation;
        _mainCamera.fieldOfView = _currentActiveCamera.fieldOfView;
    }
}
