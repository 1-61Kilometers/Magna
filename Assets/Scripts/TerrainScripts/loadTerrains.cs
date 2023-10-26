using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadterrain : MonoBehaviour
{
    // Start is called before the first frame update
    [RuntimeInitializeOnLoadMethod]
    void Start()
    {
        Debug.Log("After Scene is loaded and game is running");
        LoadTerrain("");
    }

    void LoadTerrain(string aFileName)
 {
    TerrainData aTerrain = new TerrainData();
    
     int h = 256;
     int w = 512;
     float[,] data = new float[h, w];
     using (var file = System.IO.File.OpenRead(aFileName))
     using (var reader = new System.IO.BinaryReader(file))
     {
         for (int y = 0; y < h; y++)
         {
             for (int x = 0; x < w; x++)
             {
                 float v = (float)reader.ReadUInt16() / 0xFFFF;
                 data[y, x] = v;
             }
         }
     }
     aTerrain.SetHeights(0, 0, data);
 }
}
