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
    private Button addMoonButton;

    [SerializeField]
    private List<Moon> moons = new List<Moon>();

    // Start is called before the first frame update
    void Start()
    {
        addMoonButton.onClick.AddListener(delegate { AddMoon(); });
        UpdateRemoveButtonVisibility();
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
