using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class RuleSwapper : MonoBehaviour
{
    [SerializeField]
    private bool _bringsYouToTheNextPage = true;

    private RulesChanger _parentObjectRulesChanger;
    // Start is called before the first frame update
    void Start()
    {
        _parentObjectRulesChanger = gameObject.transform.parent.gameObject.GetComponent<RulesChanger>();
        GetComponent<Button>().onClick.AddListener(NextPage);
    }

    void NextPage()
    {
        if (_bringsYouToTheNextPage)
        {
            _parentObjectRulesChanger.GetComponent<RulesChanger>()._currentPage += 1;
        }
        else
            _parentObjectRulesChanger.GetComponent<RulesChanger>()._currentPage -= 1;

        int currentPagerInt = _parentObjectRulesChanger.GetComponent<RulesChanger>()._currentPage;
        if(currentPagerInt < 0)
            _parentObjectRulesChanger.GetComponent<RulesChanger>()._currentPage = 0;
        else if(currentPagerInt > _parentObjectRulesChanger.GetComponent<RulesChanger>()._rulesPages.Length - 1)
        {
            _parentObjectRulesChanger.GetComponent<RulesChanger>()._currentPage
                = _parentObjectRulesChanger.GetComponent<RulesChanger>()._rulesPages.Length - 1;


            _parentObjectRulesChanger.GetComponent<RulesChanger>()._currentPage = 0;

            SceneManager.LoadScene(1);
        }
    }
}
