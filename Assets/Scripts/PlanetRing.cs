using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlanetRing : MonoBehaviour
{
    public bool ringEnabled;

    // Manual settings
    [Range(3, 360)]
    public int segments = 3;
    public float innerRadius = 0.7f;
    public float thickness = 0.5f;
    public Material ringMat;
    public Transform centerPoint;

    // Cached references
    GameObject ring;
    Mesh ringMesh;
    MeshFilter ringMF;
    MeshRenderer ringMR;

    // Start is called before the first frame update
    private void AddRing()
    {
        // Create ring object
        ring = new GameObject(name + " Ring");
        ring.transform.parent = centerPoint;
        ring.transform.localScale = Vector3.one;
        ring.transform.position = centerPoint.transform.position;
        ring.transform.localRotation = Quaternion.identity;
        ringMF = ring.AddComponent<MeshFilter>();
        ringMesh = ringMF.mesh;
        ringMR = ring.AddComponent<MeshRenderer>();
        ringMR.material = ringMat;

        // Build ring mesh
        Vector3[] verticies = new Vector3[(segments + 1) * 2 * 2];
        int[] triangles = new int[segments * 6 * 2];
        Vector2[] uv = new Vector2[(segments + 1) * 2 * 2];
        int halfway = (segments + 1) * 2;

        for (int i = 0; i < segments + 1; i++)
        {
            float progress = (float)i / (float)segments;
            float angle = Mathf.Deg2Rad * progress * 360;
            float x = Mathf.Sin(angle);
            float z = Mathf.Cos(angle);

            verticies[i * 2] = verticies[i * 2 + halfway] = new Vector3(x, 0f, z) * (innerRadius + thickness);
            verticies[i * 2 + 1] = verticies[i * 2 + 1 + halfway] = new Vector3(x, 0f, z) * (innerRadius);
            uv[i * 2] = uv[i * 2 + halfway] = new Vector2(progress, 0f);
            uv[i * 2 + 1] = uv[i * 2 + 1 + halfway] = new Vector2(progress, 1f);

            if(i != segments)
            {
                triangles[i * 12] = i * 2;
                triangles [i * 12 + 1] = triangles [i * 12 + 4] = (i + 1) * 2;
                triangles[i * 12 + 2] = triangles[i * 12 + 3] = i * 2 + 1;
                triangles[i * 12 + 5] = (i + 1) * 2 + 1;

                triangles[i * 12 + 6] = i * 2 + halfway;
                triangles[i * 12 + 7] = triangles[i * 12 + 10] = i * 2 + 1 + halfway;
                triangles[i * 12 + 8] = triangles[i * 12 + 9] = (i + 1) * 2 + halfway;
                triangles[i * 12 + 11] = (i + 1) * 2 + 1 + halfway;
            }
        }

        ringMesh.vertices = verticies;
        ringMesh.triangles = triangles;
        ringMesh.uv = uv;
        ringMesh.RecalculateNormals();
    }

    private void RemoveRing()
    {
        ringMesh.vertices = null;
        ringMesh.triangles = null;
        ringMesh.uv = null;
        ringMesh.RecalculateNormals();
    }

    public void AddRemoveRing(Button ringButton)
    {
        string planetName = GetComponent<Planet>().name;
        if (ringEnabled)
        {
            PlayerPrefs.SetInt($"{planetName}.Ring", 0);
            // Remove ring
            RemoveRing();
            ringEnabled = false;

            // Update button
            ringButton.GetComponentInChildren<TextMeshProUGUI>().text = "Enabled Ring";
        }
        else
        {
            PlayerPrefs.SetInt($"{planetName}.Ring", 1);
            // Add ring
            AddRing();
            ringEnabled = true;

            // Update button
            ringButton.GetComponentInChildren<TextMeshProUGUI>().text = "Disable Ring";
        }
    }

    public void ShowHideRing(int ringOn)
    {
        if(ringOn == 1)
        {
            // Show
            ringEnabled = true;
            AddRing();
        }
        else
        {
            // Hide
            ringEnabled = false;
            RemoveRing();
        }
    }
}
