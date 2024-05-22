using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RulesChanger : MonoBehaviour
{
    [SerializeField]
    public Texture[] _rulesPages;

    public int _currentPage = 0;

    private RawImage _background;

    [SerializeField]
    private GameObject _previousButton;
    [SerializeField]
    private GameObject _nextButton;
    [SerializeField]
    private GameObject _mainMenuButton;

    // Start is called before the first frame update
    void Start()
    {
        _background = gameObject.GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        _background.texture = _rulesPages[_currentPage];
        if(_currentPage == 0)
        {
            _previousButton.SetActive(false);
        }
        if(_currentPage >= 1)
        {
            _previousButton?.SetActive(true);
        }
        if (_currentPage < _rulesPages.Length - 1)
        {
            _nextButton?.SetActive(true);
            _mainMenuButton.SetActive(false);
        }
        if (_currentPage == _rulesPages.Length-1)
        {
            _nextButton.SetActive(false);
            _mainMenuButton?.SetActive(true);
        }
    }
}
