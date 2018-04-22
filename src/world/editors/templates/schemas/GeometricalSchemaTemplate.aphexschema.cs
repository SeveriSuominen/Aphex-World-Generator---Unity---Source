using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices; 

public class GeometricalSchemaTemplate : GeometricalSchema {

   // public static readonly GeometricalSchemaTemplate Instance = new GeometricalSchemaTemplate();
   // private GeometricalSchemaTemplate(){}

    public override void Init(){
        SetNoise(0, 4, 1, 2.0f, 0.8f, 1f);
    }

    public override float Voxel(){
        Vector3 alg3D = new Vector3(
             cux, cuy, cuz
        ) * 0.8f;

        float den = Mathf.Lerp(-1, 0, (y + cy - - 0) / (25 - 0));

        if (cuy < 0){
            return Rnd(Noise.Sample3D(alg3D.x, alg3D.y, alg3D.z), 1);
        }

        return Rnd(1 - ((den + Noise.Sample3D(alg3D.x, alg3D.y, alg3D.z)) * -1f), 1);
    }
}
