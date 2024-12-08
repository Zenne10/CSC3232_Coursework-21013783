using UnityEngine;

public class WaterWave : MonoBehaviour
{
    public float waveHeight = 0.5f;
    public float waveFrequency = 1f;
    public float waveSpeed = 1f;

    private MeshFilter meshFilter;
    private Vector3[] originalVertices;

    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        originalVertices = meshFilter.mesh.vertices;
    }

    void Update()
    {
        Vector3[] vertices = new Vector3[originalVertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 vertex = originalVertices[i];
            vertex.y += Mathf.Sin(Time.time * waveSpeed + vertex.x * waveFrequency) * waveHeight;
            vertices[i] = vertex;
        }
        Mesh mesh = meshFilter.mesh;
        mesh.vertices = vertices;
        mesh.RecalculateNormals();
    }
}
