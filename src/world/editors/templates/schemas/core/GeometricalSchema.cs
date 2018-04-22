using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProceduralNoiseProject;

public abstract class GeometricalSchema : Schema {
    protected FractalNoise Noise { get; private set; }

    protected float x, y, z, cx, cy, cz, cux, cuy, cuz;

    //ACCESSORS
    public abstract void Init();
    public abstract float Voxel();

    public void Feed(float x, float y, float z, float cx, float cy, float cz)
    {
        this.x = x;
        this.y = y;
        this.z = z;

        this.cx = cx;
        this.cy = cy;
        this.cz = cz;

        this.cux = (x + cx) / (Chunk.RESOLUTION - 1.0f);
        this.cuy = (y + cy) / (Chunk.RESOLUTION - 1.0f);
        this.cuz = (z + cz) / (Chunk.RESOLUTION - 1.0f);
    }

    protected void SetNoise(int seed, int octaves, float pnFreq, float pnAmp, float frFreq, float frAmp){
        Noise = new FractalNoise(new PerlinNoise(seed, pnFreq, pnAmp), octaves, frFreq, frAmp);
    }

    protected float Rnd(float value, int places){
        long factor = (long)Mathf.Pow(10, places);
        value = value * factor;
        float tmp = Mathf.Round(value);
        return tmp / factor;
    }

    protected float Rnd(float value){
        return Rnd(value, 1);
    }
}
