using UnityEngine;
using TMPro;

public class MeshInfoDisplay : MonoBehaviour
{
    public TextMeshProUGUI verticesText;
    public TextMeshProUGUI trisText;

    private void Update()
    {
        DisplayMeshInfo();
    }

    private void DisplayMeshInfo()
    {
        int totalVertices = 0;
        int totalTriangles = 0;

        MeshFilter[] meshFilters = FindObjectsOfType<MeshFilter>();
        foreach (MeshFilter meshFilter in meshFilters)
        {
            Mesh mesh = meshFilter.sharedMesh;
            if (mesh != null && mesh.isReadable)
            {
                totalVertices += mesh.vertexCount;
                totalTriangles += mesh.triangles.Length / 3;
            }
        }

        verticesText.text = "Vertices: " + totalVertices;
        trisText.text = "Triangles: " + totalTriangles;
    }
}