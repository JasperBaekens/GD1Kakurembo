using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public AudioClip TurnSwitcherNoise;


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
    private GameObject _inBetweenScreen;
    [SerializeField]
    private GameObject _endTurnButton;
    public int _currentStep = 0;

    [SerializeField]
    private GameObject[] _toggleActiveListOnStartOfNextPlayerTurn;

    private bool IsInbetweenNoisePlayed = false;

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
        if (_currentStep > 3)
        {
            _currentStep = 0;
        }

        if (_currentStep == 0)
        {
            IsInbetweenNoisePlayed = false;
            _player1.SetActive(true);
            _inBetweenScreen.SetActive(false);
            _endTurnButton.SetActive(true);
            _currentActiveCamera = _playerCamera2;
            _player2.SetActive(false);
        }
        else if (_currentStep == 2)
        {
            IsInbetweenNoisePlayed = false;
            _player1.SetActive(false);
            _inBetweenScreen.SetActive(false);
            _endTurnButton.SetActive(true);
            _currentActiveCamera = _playerCamera1;
            _player2.SetActive(true);
        }
        else
        {
            if (!IsInbetweenNoisePlayed)
            {
                SoundFXManager.Instance.PlaySoundFXClip(TurnSwitcherNoise, transform, 1f);
                IsInbetweenNoisePlayed = true;
            }

            _inBetweenScreen.SetActive(true);
            _endTurnButton.SetActive(false);
            _player1.SetActive(false);
            _player2.SetActive(false);
        }

        foreach (GameObject obj in _toggleActiveListOnStartOfNextPlayerTurn)
            obj.SetActive(false);


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
