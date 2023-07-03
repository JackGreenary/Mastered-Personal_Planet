using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Moon : MonoBehaviour
{
    public Planet moonPlanet;
    private MoonController _moonController;
    private PlanetCustomiser _planetCustomiser;
    private SliderController _sliderController;
    private PaletteController _paletteController;

    [SerializeField]
    private int _moonId;
    [SerializeField]
    private TextMeshProUGUI _title;
    [SerializeField]
    private float _size;
    [SerializeField]
    private Color32 _color;
    [SerializeField]
    private Button removeMoonButton;
    [SerializeField]
    private Button sizeButton;
    [SerializeField]
    private Button colourButton;

    public void Init(string title, MoonController moonController, PlanetCustomiser planetCustomiser)
    {
        _title.text = title;
        _moonController = moonController;
        _planetCustomiser = planetCustomiser;
        _sliderController = planetCustomiser.sliderController;
        _paletteController = planetCustomiser.paletteController;

        removeMoonButton.onClick.AddListener(delegate { _moonController.RemoveMoon(_moonId); });
        sizeButton.onClick.AddListener(delegate { ShowPercentSlider(); });
        colourButton.onClick.AddListener(delegate { _paletteController.ShowColourWheel(5, moonPlanet); });
    }

    public void ShowPercentSlider()
    {
        _sliderController.ShowPercentSlider(3, moonPlanet);
    }

    public void showRemoveButton(bool show)
    {
        if (show)
        {
            removeMoonButton.gameObject.SetActive(true);
        }
        else
        {
            removeMoonButton.gameObject.SetActive(false);
        }
    }
}
