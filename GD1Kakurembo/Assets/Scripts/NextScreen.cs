using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class NextScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject _canvas;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(NextScreenCode);
    }

    // Update is called once per frame
    void NextScreenCode()
    {
        _canvas.GetComponent<CameraSwitcher>()._currentStep++;
        _canvas.GetComponent<UnlockingPieces>()._currentTurn++;
    }
}
