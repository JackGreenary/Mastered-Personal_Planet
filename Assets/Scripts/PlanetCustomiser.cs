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

    public GameObject colourWheelParent;
    public ColorPaletteController colourWheel;
    public TextMeshProUGUI colourWheelTitle;

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

    [SerializeField]
    private int customisationInt;
    [SerializeField]
    private int colourInt;

    private Color seaColour;
    private Color beachColour;
    private Color groundColour;
    private Color mountainColour;

    public void Start()
    {
        // Setup name button listener
        nameButton.onClick.AddListener(delegate { ShowHideNameInput(); });

        // Setup slider button listeners
        planetSizeButton.onClick.AddListener(delegate { sliderController.ShowPercentSlider(0, planetToCustomise); });
        seaLevelButton.onClick.AddListener(delegate { sliderController.ShowPercentSlider(1, planetToCustomise); });
        mountainHeightButton.onClick.AddListener(delegate { sliderController.ShowPercentSlider(2, planetToCustomise); });

        // Setup colour button listeners
        seaColourButton.onClick.AddListener(delegate { ShowColourWheel(0); });
        beachColourButton.onClick.AddListener(delegate { ShowColourWheel(1); });
        groundColourButton.onClick.AddListener(delegate { ShowColourWheel(2); });
        mountainColourButton.onClick.AddListener(delegate { ShowColourWheel(3); });

        // Setup back button listeners
        backToCustFromColour.onClick.AddListener(delegate { BackToCustomisations(); });
        backToCustFromMoons.onClick.AddListener(delegate { BackToCustomisations(); });

        // Setup customisation listeners
        colourWheel.OnColorChange.AddListener(delegate { ColourWheelChanged(); });

        // Setup ring button listener
        showHideRingButton.onClick.AddListener(delegate { planetRing.AddRemoveRing(showHideRingButton); });
        // Setup moon button listener
        editMoonButton.onClick.AddListener(delegate { ShowMoonMenu(); });

        // Start with customisation screens inactive
        colourWheelParent.SetActive(false);
        moonMenuParent.SetActive(false);
        nameInput.gameObject.SetActive(false);

        moonController.HideHiddenMoons();
    }

    private void BackToCustomisations()
    {
        //percentSliderParent.SetActive(false);
        colourWheelParent.SetActive(false);
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

    public void ShowColourWheel(int colour)
    {
        colourInt = colour;
        colourWheelParent.SetActive(true);
        iTween.MoveTo(gameObject, new Vector3(transform.position.x - 600, transform.position.y), .5f);

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
