using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class HeroSelectUI : MonoBehaviour
{
    public GameObject HeroUIPrefab;
    public GameObject AvailableHeroesContent;
    public GameObject SelectedHeroesContent;

    public GameObject PlayButton;

    public int RequiredSelectedHeroesAmount;

    private Transform _AvailableHeroesTransfrom;
    private Transform _SelectedHeroesTransfrom;

    private UnityAction GameStartCallBackAction;
    private TMP_Text _PlayText;
    private bool _PlayActive = false;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] HeroesPrefabs  = Resources.LoadAll<GameObject>("Prefabs/Heroes");
        if(AvailableHeroesContent == null || HeroUIPrefab == null || SelectedHeroesContent == null)
        {
            return;
        }
        _AvailableHeroesTransfrom = AvailableHeroesContent.transform;
        _SelectedHeroesTransfrom = SelectedHeroesContent.transform;
        if (PlayButton != null)
        {
            _PlayText = PlayButton.transform.GetChild(0).GetComponent<TMP_Text>();
        }
        // Filling available heroes
        foreach (GameObject hero in HeroesPrefabs)
        {
            GameObject heroUi = Instantiate(HeroUIPrefab);
            heroUi.name = hero.name;
            heroUi.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = hero.GetComponent<SpriteRenderer>().sprite;
            heroUi.GetComponent<Button>().onClick.AddListener(() => HeroUIOnClick(heroUi));
            heroUi.transform.SetParent(_AvailableHeroesTransfrom);
        }
    }

    void Update()
    {
        if (PlayButton)
        {
            if (_SelectedHeroesTransfrom.childCount == RequiredSelectedHeroesAmount && !_PlayActive)
            {
                _PlayText.fontStyle = FontStyles.Bold;
                PlayButton.GetComponent<Button>().onClick.AddListener(GameStartCallBackAction = () => {
                    StartGameStatic.selectedHeroesNames = new List<string>();
                    foreach(Transform hero in _SelectedHeroesTransfrom)
                    {
                        StartGameStatic.selectedHeroesNames.Add(hero.gameObject.name);
                    }
                    SceneManager.LoadScene("SampleScene");
                });
                _PlayActive = true;
            }
            else if(_SelectedHeroesTransfrom.childCount != RequiredSelectedHeroesAmount && _PlayActive)
            {
                _PlayText.fontStyle = FontStyles.Normal;
                PlayButton.GetComponent<Button>().onClick.RemoveListener(GameStartCallBackAction);
                _PlayActive = false;
            }
        }
    }

    void HeroUIOnClick(GameObject heroUi)
    {
        heroUi.transform.SetParent(
            heroUi.transform.parent == _AvailableHeroesTransfrom
            ? _SelectedHeroesTransfrom
            : _AvailableHeroesTransfrom);
    }
}
