using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PaletteController : MonoBehaviour
{
    public PlanetCustomiser planetCustomiser;
    public TextMeshProUGUI colourWheelTitle;
    public ColorPaletteController colourWheel;
    public Planet currentlyEditing;

    [SerializeField]
    private Button backToCustFromColour;

    [SerializeField]
    private int colourInt;

    private void Start()
    {
        // Setup customisation listeners
        colourWheel.OnColorChange.AddListener(delegate { ColourWheelChanged(); });
        gameObject.SetActive(false);
        // Setup back button listeners
        backToCustFromColour.onClick.AddListener(delegate { BackToCustomisations(); });
    }

    private void BackToCustomisations()
    {
        gameObject.SetActive(false);
        iTween.MoveTo(planetCustomiser.gameObject, new Vector3(planetCustomiser.transform.position.x + 600, planetCustomiser.transform.position.y), .5f);
    }

    public void ShowColourWheel(int colour, Planet planetToCustomise)
    {
        currentlyEditing = planetToCustomise;

        colourInt = colour;
        gameObject.SetActive(true);
        iTween.MoveTo(planetCustomiser.gameObject, new Vector3(planetCustomiser.transform.position.x - 600, planetCustomiser.transform.position.y), .5f);

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

    public void ColourWheelChanged()
    {
        switch (colourInt)
        {
            case 0:
                planetCustomiser.seaColour = colourWheel.SelectedColor;
                PlayerPrefs.SetFloat($"{currentlyEditing.name}.SeaColour.r", colourWheel.SelectedColor.r);
                PlayerPrefs.SetFloat($"{currentlyEditing.name}.SeaColour.g", colourWheel.SelectedColor.g);
                PlayerPrefs.SetFloat($"{currentlyEditing.name}.SeaColour.b", colourWheel.SelectedColor.b);
                break;
            case 1:
                planetCustomiser.beachColour = colourWheel.SelectedColor;
                PlayerPrefs.SetFloat($"{currentlyEditing.name}.BeachColour.r", colourWheel.SelectedColor.r);
                PlayerPrefs.SetFloat($"{currentlyEditing.name}.BeachColour.g", colourWheel.SelectedColor.g);
                PlayerPrefs.SetFloat($"{currentlyEditing.name}.BeachColour.b", colourWheel.SelectedColor.b);
                break;
            case 2:
                planetCustomiser.groundColour = colourWheel.SelectedColor;
                PlayerPrefs.SetFloat($"{currentlyEditing.name}.GroundColour.r", colourWheel.SelectedColor.r);
                PlayerPrefs.SetFloat($"{currentlyEditing.name}.GroundColour.g", colourWheel.SelectedColor.g);
                PlayerPrefs.SetFloat($"{currentlyEditing.name}.GroundColour.b", colourWheel.SelectedColor.b);
                break;
            case 3:
                planetCustomiser.mountainColour = colourWheel.SelectedColor;
                PlayerPrefs.SetFloat($"{currentlyEditing.name}.MountainColour.r", colourWheel.SelectedColor.r);
                PlayerPrefs.SetFloat($"{currentlyEditing.name}.MountainColour.g", colourWheel.SelectedColor.g);
                PlayerPrefs.SetFloat($"{currentlyEditing.name}.MountainColour.b", colourWheel.SelectedColor.b);
                break;
            default:
                break;
        }

        // Set planet to low res
        currentlyEditing.resolution = 50;
        planetCustomiser.UpdateColours();
        // Set planet to high res
        currentlyEditing.resolution = 256;
    }
}
