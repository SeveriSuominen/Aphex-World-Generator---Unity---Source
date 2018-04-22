using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

using MarchingCubesProject;
using ProceduralNoiseProject;

public class ChunkGenerator {
    public static void Generate(Thread thread, ChunkProcessor.Process process){
        process.flag = ChunkProcessor.Flag.Processing;

        Marching mcubes = new MarchingCubes();

        mcubes.Surface = 0;
        
        float[] voxels = new float[(int)(Chunk.RESOLUTION * Chunk.RESOLUTION * Chunk.RESOLUTION)];
        
        GeometricalSchemaTemplate temp = new GeometricalSchemaTemplate();
        temp.Init();

        for (int x = 0; x < Chunk.RESOLUTION; x++){
            for (int y = 0; y < Chunk.RESOLUTION; y++){
                for (int z = 0; z < Chunk.RESOLUTION; z++){
                    temp.Feed(x, y, z, process.chunk.data.position.x, process.chunk.data.position.y, process.chunk.data.position.z);
                    voxels[x + y * Chunk.RESOLUTION + z * Chunk.RESOLUTION * Chunk.RESOLUTION] = temp.Voxel();
                }
            }
        }
        List<Vector3> verts = new List<Vector3>();
        List<int> indices = new List<int>();

        mcubes.Generate(voxels, Chunk.RESOLUTION, Chunk.RESOLUTION, Chunk.RESOLUTION, verts, indices);

        process.chunk.data.vertices = verts.ToArray();
        process.chunk.data.triangles = indices.ToArray();

        process.flag = ChunkProcessor.Flag.Ready;
        process.success = true;
        thread.Abort();
    }
}
