using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlanetCustomiser : MonoBehaviour
{
    public Planet planetToCustomise;
    public PlanetRing planetRing;
    public MoonController moonController;

    public new string name;

    public SliderController sliderController;
    public PaletteController paletteController;

    public GameObject atmosphere;

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
    private Button showHideRingButton;
    [SerializeField]
    private Button showHideAtmosphereButton;

    public Color seaColour;
    public Color beachColour;
    public Color groundColour;
    public Color mountainColour;

    public void Start()
    {
        // Setup name button listener
        nameButton.onClick.AddListener(delegate { ShowHideNameInput(); });

        // Setup slider button listeners
        planetSizeButton.onClick.AddListener(delegate { sliderController.ShowPercentSlider(0, planetToCustomise); });
        seaLevelButton.onClick.AddListener(delegate { sliderController.ShowPercentSlider(1, planetToCustomise); });
        mountainHeightButton.onClick.AddListener(delegate { sliderController.ShowPercentSlider(2, planetToCustomise); });

        // Setup colour button listeners
        seaColourButton.onClick.AddListener(delegate { paletteController.ShowColourWheel(0, planetToCustomise); });
        beachColourButton.onClick.AddListener(delegate { paletteController.ShowColourWheel(1, planetToCustomise); });
        groundColourButton.onClick.AddListener(delegate { paletteController.ShowColourWheel(2, planetToCustomise); });
        mountainColourButton.onClick.AddListener(delegate { paletteController.ShowColourWheel(3, planetToCustomise); });

        // Setup ring button listener
        showHideRingButton.onClick.AddListener(delegate { planetRing.AddRemoveRing(showHideRingButton); });
        //Setup atmosphere button listener
        showHideAtmosphereButton.onClick.AddListener(delegate { ShowHideAtmosphere(); });

        // Start with customisation screens inactive
        nameInput.gameObject.SetActive(false);
        atmosphere.SetActive(false);
        moonController.HideHiddenMoons();
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

    public void ShowHideAtmosphere()
    {
        string planetName = planetToCustomise.name;
        if (atmosphere.activeSelf)
        {
            PlayerPrefs.SetInt($"{planetName}.Atmosphere", 0);
            atmosphere.SetActive(false);
            showHideAtmosphereButton.GetComponentInChildren<TextMeshProUGUI>().text = "Enabled Atmosphere";
        }
        else
        {
            PlayerPrefs.SetInt($"{planetName}.Atmosphere", 1);
            atmosphere.SetActive(true);
            showHideAtmosphereButton.GetComponentInChildren<TextMeshProUGUI>().text = "Disable Atmosphere";
        }
    }

    public void ShowHideAtmosphere(int atmosphereOn)
    {
        if(atmosphereOn == 1)
        {
            atmosphere.SetActive(true);
            showHideAtmosphereButton.GetComponentInChildren<TextMeshProUGUI>().text = "Disable Atmosphere";
        }
        else
        {
            atmosphere.SetActive(false);
            showHideAtmosphereButton.GetComponentInChildren<TextMeshProUGUI>().text = "Enabled Atmosphere";
        }
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
