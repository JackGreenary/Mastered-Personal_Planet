using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MoonController : MonoBehaviour
{
    [SerializeField]
    private PlanetCustomiser planetCustomiser;
    [SerializeField]
    private Moon moonPrefabUI;
    [SerializeField]
    private Planet moonPrefabObj;
    [SerializeField]
    private Button editMoonButton;
    [SerializeField]
    private Button addMoonButton;
    [SerializeField]
    private Button backToCustFromMoons;
    [SerializeField]
    private List<Moon> moons = new List<Moon>();

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        addMoonButton.onClick.AddListener(delegate { AddMoon(); });
        UpdateRemoveButtonVisibility();
        // Setup moon button listener
        editMoonButton.onClick.AddListener(delegate { ShowMoonMenu(); });
        backToCustFromMoons.onClick.AddListener(delegate { BackToCustomisations(); });
    }

    private void BackToCustomisations()
    {
        gameObject.SetActive(false);
        iTween.MoveTo(planetCustomiser.gameObject, new Vector3(planetCustomiser.transform.position.x + 600, planetCustomiser.transform.position.y), .5f);
    }

    private void ShowMoonMenu()
    {
        gameObject.SetActive(true);
        iTween.MoveTo(planetCustomiser.gameObject, new Vector3(planetCustomiser.transform.position.x - 600, planetCustomiser.transform.position.y), .5f);
    }

    public void AddMoon()
    {
        int activeMoons = moons.Where(i => i.gameObject.activeSelf).ToList().Count;
        Moon moonToEnable = moons.Where(i => !i.gameObject.activeSelf).FirstOrDefault();
        moonToEnable.Init($"Moon {activeMoons + 1}", this, planetCustomiser);
        moonToEnable.gameObject.SetActive(true);
        moonToEnable.moonPlanet.gameObject.SetActive(true);

        // If there are three moons then disable the create moon button
        if (activeMoons + 1 >= 3)
        {
            addMoonButton.gameObject.SetActive(false);
        }
        UpdateRemoveButtonVisibility();
    }

    public void RemoveMoon(int moonInt)
    {
        int activeMoons = moons.Where(i => i.gameObject.activeSelf).ToList().Count;

        moons[moonInt].gameObject.SetActive(false);
        moons[moonInt].moonPlanet.gameObject.SetActive(false);

        // If there are less than three moons then enable create moon button
        if (activeMoons <= 3)
        {
            addMoonButton.gameObject.SetActive(true);
        }
        UpdateRemoveButtonVisibility();
    }

    public void HideHiddenMoons()
    {
        foreach (Moon moon in moons)
        {
            moon.moonPlanet.gameObject.SetActive(false);
        }
    }

    private void UpdateRemoveButtonVisibility()
    {
        int activeMoons = moons.Where(i => i.gameObject.activeSelf).ToList().Count;

        if(activeMoons > 0)
        {
            moons.ForEach(i => { i.showRemoveButton(false); });
            moons[activeMoons - 1].showRemoveButton(true);
        }
    }

    private void GoBack()
    {

    }
}
