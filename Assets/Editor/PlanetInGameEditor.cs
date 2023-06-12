using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlanetCustomiser))]
public class PlanetInGameEditor : Editor {

    PlanetCustomiser planetCustomiser;
    Editor shapeEditor;
    Editor colourEditor;

	public override void OnInspectorGUI()
    {
        GUILayout.Label("Inspector Customisation Controls");
        //base.OnInspectorGUI();

        using (var check = new EditorGUI.ChangeCheckScope())
        {
            base.OnInspectorGUI();
            if (check.changed)
            {
                //planetCustomiser.PlanetSize();
                //planetCustomiser.MountainHeight();
                //planetCustomiser.SeaLevel();
                //planetCustomiser.planetToCustomise.GeneratePlanet();
            }
        }

        //if (GUILayout.Button("Generate Planet"))
        //{
        //    planet.GeneratePlanet();
        //}

        //DrawSettingsEditor(planet.shapeSettings, planet.OnShapeSettingsUpdated, ref planet.shapeSettingsFoldout, ref shapeEditor);
        //DrawSettingsEditor(planet.colourSettings, planet.OnColourSettingsUpdated, ref planet.colourSettingsFoldout, ref colourEditor);
    }

    void DrawSettingsEditor(Object settings, System.Action onSettingsUpdated, ref bool foldout, ref Editor editor)
    {
        if (settings != null)
        {
            foldout = EditorGUILayout.InspectorTitlebar(foldout, settings);
            using (var check = new EditorGUI.ChangeCheckScope())
            {
                if (foldout)
                {
                    CreateCachedEditor(settings, null, ref editor);
                    editor.OnInspectorGUI();

                    if (check.changed)
                    {
                        if (onSettingsUpdated != null)
                        {
                            onSettingsUpdated();
                        }
                    }
                }
            }
        }
    }

    private void OnEnable()
    {
        planetCustomiser = FindAnyObjectByType<PlanetCustomiser>();
    }
}
