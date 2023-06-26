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

    public GameObject moonMenuParent;

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
    private Button editMoonButton;

    [SerializeField]
    private Button backToCustFromColour;
    [SerializeField]
    private Button backToCustFromMoons;

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

        // Setup back button listeners
        backToCustFromColour.onClick.AddListener(delegate { BackToCustomisations(); });
        backToCustFromMoons.onClick.AddListener(delegate { BackToCustomisations(); });

        // Setup ring button listener
        showHideRingButton.onClick.AddListener(delegate { planetRing.AddRemoveRing(showHideRingButton); });
        // Setup moon button listener
        editMoonButton.onClick.AddListener(delegate { ShowMoonMenu(); });

        // Start with customisation screens inactive
        moonMenuParent.SetActive(false);
        nameInput.gameObject.SetActive(false);

        moonController.HideHiddenMoons();
    }

    private void BackToCustomisations()
    {
        //percentSliderParent.SetActive(false);
        //colourWheelParent.SetActive(false);
        moonMenuParent.SetActive(false);
        iTween.MoveTo(gameObject, new Vector3(transform.position.x + 600, transform.position.y), .5f);
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

    private void ShowMoonMenu()
    {
        moonMenuParent.SetActive(true);
        iTween.MoveTo(gameObject, new Vector3(transform.position.x - 600, transform.position.y), .5f);
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
