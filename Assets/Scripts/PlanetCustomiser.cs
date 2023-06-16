using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlanetCustomiser : MonoBehaviour
{
    public Planet planetToCustomise;

    public new string name;

    public float defaultSeaLevel;
    public float seaLevelPercent;

    public float defaultMountainHeight;
    public float mountainHeightPercent;

    public float defaultPlanetSize;
    public float planetSizePercent;

    public GameObject percentSliderParent;
    public TextMeshProUGUI percentSliderTitle;
    public Slider percentSlider;

    public GameObject colourWheelParent;
    public ColorPaletteController colourWheel;
    public TextMeshProUGUI colourWheelTitle;

    [SerializeField]
    private TextMeshProUGUI nameTxt;
    [SerializeField]
    private TMP_InputField nameInput;
    [SerializeField]
    private Button nameButton;
    [SerializeField]
    private Sprite nameButtonEditImg;
    [SerializeField]
    private Sprite nameButtonTickImg;
    [SerializeField]
    private Button planetSizeButton;
    [SerializeField]
    private Button seaLevelButton;
    [SerializeField]
    private Button mountainHeightButton;

    [SerializeField]
    private Button seaColourButton;
    [SerializeField]
    private Button beachColourButton;
    [SerializeField]
    private Button groundColourButton;
    [SerializeField]
    private Button mountainColourButton;

    [SerializeField]
    private Button backToCustFromSlider;
    [SerializeField]
    private Button backToCustFromColour;

    [SerializeField]
    private int customisationInt;
    [SerializeField]
    private int colourInt;

    private Color seaColour;
    private Color beachColour;
    private Color groundColour;
    private Color mountainColour;

    private bool settingSlider;

    public void Start()
    {
        // Setup name button listener
        nameButton.onClick.AddListener(delegate { ShowHideNameInput(); });

        // Setup slider button listeners
        planetSizeButton.onClick.AddListener(delegate { ShowPercentSlider(0); });
        seaLevelButton.onClick.AddListener(delegate { ShowPercentSlider(1); });
        mountainHeightButton.onClick.AddListener(delegate { ShowPercentSlider(2); });

        // Setup colour button listeners
        seaColourButton.onClick.AddListener(delegate { ShowColourWheel(0); });
        beachColourButton.onClick.AddListener(delegate { ShowColourWheel(1); });
        groundColourButton.onClick.AddListener(delegate { ShowColourWheel(2); });
        mountainColourButton.onClick.AddListener(delegate { ShowColourWheel(3); });

        // Setup back button listeners
        backToCustFromSlider.onClick.AddListener(delegate { BackToCustomisations(); });
        backToCustFromColour.onClick.AddListener(delegate { BackToCustomisations(); });

        // Setup customisation listeners
        percentSlider.onValueChanged.AddListener(delegate { PercentSliderChanged(); });
        colourWheel.OnColorChange.AddListener(delegate { ColourWheelChanged(); });

        // Start with customisation screens inactive
        percentSliderParent.SetActive(false);
        colourWheelParent.SetActive(false);

        settingSlider = false;
        nameInput.gameObject.SetActive(false);
    }

    private void BackToCustomisations()
    {
        settingSlider = false;
        percentSliderParent.SetActive(false);
        colourWheelParent.SetActive(false);
        iTween.MoveTo(gameObject, new Vector3(transform.position.x + 500, transform.position.y), .5f);
    }

    private void ShowHideNameInput()
    {
        if (nameInput.IsActive())
        {
            // Hide input and update name
            name = nameInput.text;
            nameTxt.text = name;
            nameInput.gameObject.SetActive(false);
            nameTxt.gameObject.SetActive(true);
            nameButton.image.sprite = nameButtonEditImg;
        }
        else
        {
            // Show input update input
            name = nameTxt.text;
            nameInput.text = name;
            nameInput.gameObject.SetActive(true);
            nameTxt.gameObject.SetActive(false);
            nameButton.image.sprite = nameButtonTickImg;
        }
    }

    private void ShowColourWheel(int colour)
    {
        colourInt = colour;
        colourWheelParent.SetActive(true);
        iTween.MoveTo(gameObject, new Vector3(transform.position.x - 500, transform.position.y), .5f);

        switch (colour)
        {
            case 0:
                colourWheelTitle.text = "Sea Colour";
                //colourWheel
                break;
            case 1:
                colourWheelTitle.text = "Beach Colour";
                //percentSlider.value = seaLevelPercent;
                break;
            case 2:
                colourWheelTitle.text = "Ground Colour";
                //percentSlider.value = mountainHeightPercent;
                break;
            case 3:
                colourWheelTitle.text = "Mountain Colour";
                //percentSlider.value = mountainHeightPercent;
                break;
            default:
                break;
        }
    }

    private void ColourWheelChanged()
    {
        switch (colourInt)
        {
            case 0:
                seaColour = colourWheel.SelectedColor;
                break;
            case 1:
                beachColour = colourWheel.SelectedColor;
                break;
            case 2:
                groundColour = colourWheel.SelectedColor;
                break;
            case 3:
                mountainColour = colourWheel.SelectedColor;
                break;
            default:
                break;
        }

        // Set planet to low res
        planetToCustomise.resolution = 50;
        UpdateColours();
        // Set planet to high res
        planetToCustomise.resolution = 256;
    }

    private void ShowPercentSlider(int customisation)
    {
        customisationInt = customisation;
        percentSliderParent.SetActive(true);

        iTween.MoveTo(gameObject, new Vector3(transform.position.x - 500, transform.position.y), .5f);

        switch (customisation)
        {
            case 0:
                percentSliderTitle.text = "Planet Size";
                percentSlider.value = planetSizePercent/100;
                break;
            case 1:
                percentSliderTitle.text = "Sea Level";
                percentSlider.value = seaLevelPercent / 100;
                break;
            case 2:
                percentSliderTitle.text = "Mountain Height";
                percentSlider.value = mountainHeightPercent / 100;
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
                    planetSizePercent = (percentSlider.value * 100);
                    PlanetSize();
                    break;
                case 1:
                    seaLevelPercent = (percentSlider.value * 100);
                    SeaLevel();
                    break;
                case 2:
                    mountainHeightPercent = (percentSlider.value * 100);
                    MountainHeight();
                    break;
                default:
                    break;
            }
        }
    }

    public void PlanetSize()
    {
        planetToCustomise.resolution = 50;
        float percentVal = defaultPlanetSize * (planetSizePercent / 100);
        planetToCustomise.shapeSettings.planetRadius = percentVal;
        planetToCustomise.OnShapeSettingsUpdated();
        planetToCustomise.resolution = 256;
    }

    public void SeaLevel()
    {
        planetToCustomise.resolution = 50;
        float minVal = defaultSeaLevel * (seaLevelPercent / 100);
        planetToCustomise.shapeSettings.noiseLayers[1].noiseSettings.rigidNoiseSettings.minValue = minVal;
        planetToCustomise.GeneratePlanet();
        planetToCustomise.resolution = 256;
    }

    public void MountainHeight()
    {
        planetToCustomise.resolution = 50;
        float strengthVal = defaultMountainHeight * (mountainHeightPercent / 100);
        planetToCustomise.shapeSettings.noiseLayers[1].noiseSettings.rigidNoiseSettings.strength = strengthVal;
        planetToCustomise.GeneratePlanet();
        planetToCustomise.resolution = 256;
    }

    public void UpdateColours()
    {
        GradientColorKey[] colours = new GradientColorKey[4];
        colours[0].color = seaColour;
        colours[0].time = 0;

        colours[1].color = beachColour;
        colours[1].time = 0.038f;

        colours[2].color = groundColour;
        colours[2].time = 0.321f;

        colours[3].color = mountainColour;
        colours[3].time = 0.676f;

        planetToCustomise.colourSettings.gradient.colorKeys = colours;
        planetToCustomise.GeneratePlanet();
    }
}
