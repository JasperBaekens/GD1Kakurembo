using UnityEngine;
using UnityEngine.UI;

public class DiceRoll : MonoBehaviour
{

    [SerializeField]
    private Camera _mainCamera;
    [SerializeField]
    private RawImage _imageObject;

    [SerializeField]
    private Texture[] _diceImages;

    private int _currentNumber;
    public int _numberRolled;

    [SerializeField]
    [Min(1)]
    private int _minDiceChanges;
    [SerializeField]
    [Min(2)]
    private int _maxDiceChanges;

    private int _randomisedMaxDiceChanges;
    private int _currentDiceChanges = 0;

    [SerializeField]
    [Min(0.1f)]
    private float _maxRollTime;
    private float _rollTimer;

    private bool _isRolled;
    private bool _isRolling = false;

    // Start is called before the first frame update
    void Start()
    {
        _randomisedMaxDiceChanges = Random.Range(_minDiceChanges, _maxDiceChanges + 1);

        _imageObject.texture = _diceImages[0];
    }

    void Reset()
    {
        _rollTimer = _maxRollTime;
    }

    // Update is called once per frame
    void Update()
    {
        DiceRollUpdater();

        if (Input.GetButtonDown("Fire1"))
        {

            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
                print("I'm looking at " + hit.transform.name);
            else
                print("I'm looking at nothing!");
        }
    }

    private void DiceRollUpdater()
    {
        if (Input.GetButtonDown("Jump"))
        {
            _isRolled = false;
            _isRolling = true;
        }
        if (_isRolled == false && _isRolling == true)
        {

            if (_currentDiceChanges < _randomisedMaxDiceChanges)
            {
                if (_rollTimer >= 0)
                {
                    _rollTimer -= Time.deltaTime;
                }
                else
                {
                    _currentDiceChanges++;
                    _rollTimer = _maxRollTime;
                    _currentNumber = Random.Range(1, _diceImages.Length+1);

                    _imageObject.texture = _diceImages[_currentNumber-1];

                }
            }
            else
            {
                _isRolled = true;
                _isRolling = false;
                _numberRolled = _currentNumber;
            }
        }
    }
}
