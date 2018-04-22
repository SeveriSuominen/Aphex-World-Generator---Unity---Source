using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour {
    public readonly static int RESOLUTION = 32;

    public ChunkCanvas canvas;

    public GameObject grassMesh;

    MeshRenderer meshRenderer;
    MeshCollider meshCollider;
    MeshFilter meshFilter;

    public Data data;

    void Start () {
        meshRenderer = GetComponent<MeshRenderer>();
        meshCollider = GetComponent<MeshCollider>();
        meshFilter = GetComponent<MeshFilter>();

        grassMesh = transform.GetChild(0).gameObject;
    }
	
    public void Bean() {
        data = new Data();
        data.position = transform.position;
        canvas.processor.Request(this);
    }

    public void Create(ChunkProcessor.Process process) {
        Destroy(meshFilter.mesh);

        Mesh mesh = new Mesh();
        mesh.vertices = process.chunk.data.vertices;
        mesh.triangles = process.chunk.data.triangles;

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        grassMesh.GetComponent<MeshFilter>().mesh = meshCollider.sharedMesh = meshFilter.mesh = mesh;
    }

    public struct Data {
        public Vector3 position;

        public Vector3[] vertices;
        public Vector3[] normals;
        public int[] triangles;
    }
}
