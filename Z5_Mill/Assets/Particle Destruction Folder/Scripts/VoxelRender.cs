using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class VoxelRender : MonoBehaviour
{
    Mesh mesh;
    List<Vector3> vertices;
    List<int> triangles;

    public float scale = 1f;
    public int posX, posY, posZ;
    float adjustedScale;

    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        adjustedScale = scale * 0.5f;
    }

    void GenerateVoxelMesh(VoxelData data)
    {
        vertices = new List<Vector3>();
        triangles = new List<int>();

        for (int z = 0; z < data.Depth; z++)
        {
            for (int x = 0; x < data.Width; x++)
            {
                if (data.GetCell(x, z) == 0)
                {
                    continue;
                }
                MakeCubeGrid(adjustedScale, new Vector3((float)x * scale, 0, (float)z * scale),x,z,data);
            }
        }
    }

    void Start()
    {
        GenerateVoxelMesh(new VoxelData());
        UpdateMesh();
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
    }


    void MakeCubeGrid(float cubeScale, Vector3 cubPos, int x, int z, VoxelData data)
    {
        for (int i = 0; i < 6; i++)
        {
            Direction dir = (Direction) i;
            if (data.GetNeighbor(x, z,dir)==0)
            {
                MakeGridFace(dir, cubeScale, cubPos);
            }
        }
    }

    void MakeGridFace(Direction dir, float faceScale, Vector3 facePos)
    {
        vertices.AddRange(CubeMeshData.faceVertices(dir, faceScale, facePos));

        int vCount = vertices.Count;

        triangles.Add(vCount - 4);
        triangles.Add(vCount - 4 + 1);
        triangles.Add(vCount - 4 + 2);
        triangles.Add(vCount - 4);
        triangles.Add(vCount - 4 + 2);
        triangles.Add(vCount - 4 + 3);
    }

}
