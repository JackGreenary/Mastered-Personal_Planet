using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public string Name;

    [Range(2, 256)]
    public int resolution = 10;
    public bool autoUpdate = true;
    public enum FaceRenderMask { All, Top, Bottom, Left, Right, Front, Back };
    public FaceRenderMask faceRenderMask;

    public PlanetCustomiser planetCustomiser;

    public ShapeSettings shapeSettings;
    public ColourSettings colourSettings;

    [HideInInspector]
    public bool shapeSettingsFoldout;
    [HideInInspector]
    public bool colourSettingsFoldout;

    public float defaultPlanetSize;
    public float defaultSeaLevel;
    public float defaultMountainHeight;

    [SerializeField]
    ShapeGenerator shapeGenerator;
    [SerializeField]
    ColourGenerator colourGenerator = new ColourGenerator();

    [SerializeField]
    MeshFilter[] meshFilters;
    [SerializeField]
    TerrainFace[] terrainFaces;

    [SerializeField]
    private bool Regen;

    void start()
    {
    }

    void Initialize()
    {
        shapeGenerator.UpdateSettings(shapeSettings);
        colourGenerator.UpdateSettings(colourSettings);

        if (meshFilters == null || meshFilters.Length == 0)
        {
            meshFilters = new MeshFilter[6];
        }
        terrainFaces = new TerrainFace[6];

        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

        for (int i = 0; i < 6; i++)
        {
            if (meshFilters[i] == null)
            {
                GameObject meshObj = new GameObject("mesh");
                meshObj.transform.parent = transform;

                meshObj.AddComponent<MeshRenderer>();
                meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
            }
            meshFilters[i].GetComponent<MeshRenderer>().sharedMaterial = colourSettings.planetMaterial;

            terrainFaces[i] = new TerrainFace(shapeGenerator, meshFilters[i].mesh, resolution, directions[i]);
            bool renderFace = faceRenderMask == FaceRenderMask.All || (int)faceRenderMask - 1 == i;
            meshFilters[i].gameObject.SetActive(renderFace);
        }

        // Retrieve settings for the planet
        if (name == "Planet")
        {
            if (PlayerPrefs.HasKey($"{ name}.PlanetSize"))
            {
                float size = PlayerPrefs.GetFloat($"{name}.PlanetSize");
                shapeSettings.planetRadius = size * defaultPlanetSize;
            }
            if (PlayerPrefs.HasKey($"{ name}.SeaLevel"))
            {
                float seaLevel = PlayerPrefs.GetFloat($"{name}.SeaLevel");
                shapeSettings.noiseLayers[1].noiseSettings.rigidNoiseSettings.minValue = seaLevel * defaultSeaLevel;
            }
            if (PlayerPrefs.HasKey($"{ name}.MountainHeight"))
            {
                float mountainHeight = PlayerPrefs.GetFloat($"{name}.MountainHeight");
                shapeSettings.noiseLayers[1].noiseSettings.rigidNoiseSettings.strength = mountainHeight * defaultMountainHeight;
            }
            //if (PlayerPrefs.HasKey($"{ name}.SeaColour.r") || PlayerPrefs.HasKey($"{ name}.SeaColour.g") || PlayerPrefs.HasKey($"{ name}.SeaColour.b"))
            //{
            //    float r = PlayerPrefs.GetFloat($"{name}.SeaColour.r");
            //    float g = PlayerPrefs.GetFloat($"{name}.SeaColour.g");
            //    float b = PlayerPrefs.GetFloat($"{name}.SeaColour.b");

            //    planetCustomiser.seaColour = new Color(r, g, b);
            //}
            //if (PlayerPrefs.HasKey($"{ name}.BeachColour.r") || PlayerPrefs.HasKey($"{ name}.BeachColour.g") || PlayerPrefs.HasKey($"{ name}.BeachColour.b"))
            //{
            //    float r = PlayerPrefs.GetFloat($"{name}.BeachColour.r");
            //    float g = PlayerPrefs.GetFloat($"{name}.BeachColour.g");
            //    float b = PlayerPrefs.GetFloat($"{name}.BeachColour.b");

            //    planetCustomiser.beachColour = new Color(r, g, b);
            //}
            //if (PlayerPrefs.HasKey($"{ name}.GroundColour.r") || PlayerPrefs.HasKey($"{ name}.GroundColour.g") || PlayerPrefs.HasKey($"{ name}.GroundColour.b"))
            //{
            //    float r = PlayerPrefs.GetFloat($"{name}.GroundColour.r");
            //    float g = PlayerPrefs.GetFloat($"{name}.GroundColour.g");
            //    float b = PlayerPrefs.GetFloat($"{name}.GroundColour.b");

            //    planetCustomiser.groundColour = new Color(r, g, b);
            //}
            //if (PlayerPrefs.HasKey($"{ name}.MountainColour.r") || PlayerPrefs.HasKey($"{ name}.MountainColour.g") || PlayerPrefs.HasKey($"{ name}.MountainColour.b"))
            //{
            //    float r = PlayerPrefs.GetFloat($"{name}.MountainColour.r");
            //    float g = PlayerPrefs.GetFloat($"{name}.MountainColour.g");
            //    float b = PlayerPrefs.GetFloat($"{name}.MountainColour.b");
            //    planetCustomiser.mountainColour = new Color(r, g, b);
            //}
            // Update colours
            //planetCustomiser.UpdateColours();

            if (PlayerPrefs.HasKey($"{ name}.Atmosphere"))
            {
                int atmosphere = PlayerPrefs.GetInt($"{name}.Atmosphere");
                planetCustomiser.ShowHideAtmosphere(atmosphere);
            }
            if (PlayerPrefs.HasKey($"{ name}.Ring"))
            {
                int ringOn = PlayerPrefs.GetInt($"{name}.Ring");
                GetComponent<PlanetRing>().ShowHideRing(ringOn);
            }
        }
    }

    private void Update()
    {
        if (Regen)
        {
            Regen = false;
            OnShapeSettingsUpdated();
        }
    }

    public void GeneratePlanet()
    {
        Initialize();
        GenerateMesh();
        GenerateColours();
    }

    public void OnShapeSettingsUpdated()
    {
        if (autoUpdate)
        {
            Initialize();
            GenerateMesh();
            GenerateColours();
        }
    }

    public void OnColourSettingsUpdated()
    {
        if (autoUpdate)
        {
            Initialize();
            GenerateColours();
        }
    }

    void GenerateMesh()
    {
        for (int i = 0; i < 6; i++)
        {
            if (meshFilters[i].gameObject.activeSelf)
            {
                terrainFaces[i].ConstructMesh();
            }
        }

        colourGenerator.UpdateElevation(shapeGenerator.elevationMinMax);
    }

    void GenerateColours()
    {
        colourGenerator.UpdateColours();
    }
}