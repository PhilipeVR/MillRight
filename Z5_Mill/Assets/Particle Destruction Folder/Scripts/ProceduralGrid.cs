using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ProceduralGrid : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    //grid settings
    public float cellSize = 1;
    public Vector3 gridOffset;
    public int gridSize;

    // Start is called before the first frame update
    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
       
    }

    private void Start()
    {
        MakeCubeGrid();
        UpdateMesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MakeCubeGrid()
    {
        //set array sizes
        vertices = new Vector3[(gridSize + 1) * (gridSize + 1)];
        triangles = new int[gridSize * gridSize * 6];


        //set vertex offset
        int v = 0;
        int t = 0;
        float vertexOffset = cellSize * 0.5f;

        //populate vertices and triangles arrays
        //create vertex grid
        for (int x = 0; x <= gridSize; x++)
        {
            for (int y = 0; y <= gridSize; y++)
            {
                vertices[v] = new Vector3((x * cellSize) - vertexOffset, (x+y) * 0.2f, (y * cellSize) - vertexOffset);
                v++;
            }
        }

        //reset vertex tracker
        v = 0;
        // setting cells triangles
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                triangles[t] = v;
                triangles[t + 1] = triangles[t + 4] = v + 1;
                triangles[t + 2] = triangles[t + 3] = v + (gridSize + 1);
                triangles[t + 5] = v + (gridSize + 1) + 1;

                v++;
                t += 6;
            }
            v++;
        }
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
