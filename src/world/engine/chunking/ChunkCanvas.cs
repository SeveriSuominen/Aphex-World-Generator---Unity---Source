using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkCanvas : MonoBehaviour {

    public readonly static Vector3 CANVAS_SIZE = new Vector3(16, 3, 16);

    public GameObject chunksParent, chunkModel;
    public ChunkProcessor processor;

    Chunk[] chunks; 

	void Start () {
        Initialize();
    }

    void Initialize() {
        Vector3[] canvas = CalculateCanvas();
        chunks = new Chunk[canvas.Length];

        for (int i = 0; i < chunks.Length; i++){
            GameObject chunk = Instantiate<GameObject>(chunkModel, chunksParent.transform);

            chunks[i] = chunk.AddComponent<Chunk>();
            chunks[i].transform.position = canvas[i] * (Chunk.RESOLUTION - 1);
            chunks[i].canvas = this;
            chunks[i].Bean();
        }
    }

    Vector3[] CalculateCanvas(){
        Vector3[] canvas = new Vector3[(int)(CANVAS_SIZE.x * CANVAS_SIZE.y * CANVAS_SIZE.z)];

        for (int x = 0, i = 0; x < CANVAS_SIZE.x; x++){
            for (int y = 0; y < CANVAS_SIZE.y; y++){
                for (int z = 0; z < CANVAS_SIZE.z; z++, i++){
                    canvas[i] = (new Vector3(
                            x - CANVAS_SIZE.x * 0.5f,
                            y - CANVAS_SIZE.y * 0.5f,
                            z - CANVAS_SIZE.z * 0.5f
                        )
                    );
                }
            }
        }
        return canvas;
    }
}
