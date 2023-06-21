using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public PlanetCustomiser planetCustomiser;
    public TextMeshProUGUI percentSliderTitle;
    public Slider percentSlider;
    public Planet currentlyEditing;
    public float defaultPlanetSize;
    public float defaultSeaLevel;
    public float defaultMountainHeight;

    private int customisationInt;
    private bool settingSlider;
    [SerializeField]
    private Button backToCustFromSlider;

    // Start is called before the first frame update
    void Start()
    {
        percentSlider.onValueChanged.AddListener(delegate { PercentSliderChanged(); });
        // Setup back button listeners
        backToCustFromSlider.onClick.AddListener(delegate { BackToCustomisations(); });

        gameObject.SetActive(false);
    }

    private void BackToCustomisations()
    {
        gameObject.SetActive(false);
        iTween.MoveTo(planetCustomiser.gameObject, new Vector3(planetCustomiser.transform.position.x + 600, planetCustomiser.transform.position.y), .5f);
    }

    public void ShowPercentSlider(int customisation, Planet planetToCustomise)
    {
        currentlyEditing = planetToCustomise;
        customisationInt = customisation;
        gameObject.SetActive(true);

        iTween.MoveTo(planetCustomiser.gameObject, new Vector3(planetCustomiser.transform.position.x - 600, planetCustomiser.transform.position.y), .5f);

        switch (customisation)
        {
            case 0:
                percentSliderTitle.text = "Planet Size";
                //percentSlider.value = currentlyEditing / 100;
                break;
            case 1:
                percentSliderTitle.text = "Sea Level";
                //percentSlider.value = seaLevelPercent / 100;
                break;
            case 2:
                percentSliderTitle.text = "Mountain Height";
                //percentSlider.value = mountainHeightPercent / 100;
                break;
            case 3:
                percentSliderTitle.text = "Moon Size";
                break;
            default:
                break;
        }
        settingSlider = true;
    }

    private void PercentSliderChanged()
    {
        if (settingSlider)
        {
            switch (customisationInt)
            {
                case 0:
                    PlanetSize(percentSlider.value);
                    break;
                case 1:
                    SeaLevel(percentSlider.value);
                    break;
                case 2:
                    MountainHeight(percentSlider.value);
                    break;
                case 3:
                    PlanetSize(percentSlider.value);
                    break;
                default:
                    break;
            }
        }
    }

    private void PlanetSize(float percent)
    {
        currentlyEditing.resolution = 50;
        float percentVal = defaultPlanetSize * (percent);
        currentlyEditing.shapeSettings.planetRadius = percentVal;
        currentlyEditing.OnShapeSettingsUpdated();
        currentlyEditing.resolution = 256;
    }

    private void SeaLevel(float percent)
    {
        currentlyEditing.resolution = 50;
        float minVal = defaultSeaLevel * percent;
        currentlyEditing.shapeSettings.noiseLayers[1].noiseSettings.rigidNoiseSettings.minValue = minVal;
        currentlyEditing.GeneratePlanet();
        currentlyEditing.resolution = 256;
    }

    private void MountainHeight(float percent)
    {
        currentlyEditing.resolution = 50;
        float strengthVal = defaultMountainHeight * percent;
        currentlyEditing.shapeSettings.noiseLayers[1].noiseSettings.rigidNoiseSettings.strength = strengthVal;
        currentlyEditing.GeneratePlanet();
        currentlyEditing.resolution = 256;
    }
}
