using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PaletteController : MonoBehaviour
{
    public PlanetCustomiser planetCustomiser;
    public TextMeshProUGUI colourWheelTitle;
    public ColorPaletteController colourWheel;
    public Planet currentlyEditing;

    [SerializeField]
    private int colourInt;

    private void Start()
    {
        // Setup customisation listeners
        colourWheel.OnColorChange.AddListener(delegate { ColourWheelChanged(); });
        gameObject.SetActive(false);
    }

    public void ShowColourWheel(int colour, Planet planetToCustomise)
    {
        currentlyEditing = planetToCustomise;

        colourInt = colour;
        gameObject.SetActive(true);
        iTween.MoveTo(planetCustomiser.gameObject, new Vector3(planetCustomiser.transform.position.x - 600, transform.position.y), .5f);

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
                break;
            case 1:
                planetCustomiser.beachColour = colourWheel.SelectedColor;
                break;
            case 2:
                planetCustomiser.groundColour = colourWheel.SelectedColor;
                break;
            case 3:
                planetCustomiser.mountainColour = colourWheel.SelectedColor;
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
