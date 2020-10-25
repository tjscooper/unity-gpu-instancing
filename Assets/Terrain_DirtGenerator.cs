using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Terrain_DirtGenerator : MonoBehaviour
{
    [SerializeField] BitmapToPrefab bitmap = null;

    public Mesh terrainMesh;
    public Material terrainMaterial;

    private List<List<TerrainData>> batches = new List<List<TerrainData>>();

    int objectsPerBlock = 1;
    int batchIndexNum = 0;
    List<TerrainData> currBatch = new List<TerrainData>();
    MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();

    void Start()
    {

        int gridSize = bitmap.map.width;
        MeshCollider meshc = gameObject.AddComponent(typeof(MeshCollider)) as MeshCollider;
        meshc.sharedMesh = null;
        meshc.sharedMesh = terrainMesh;
        materialPropertyBlock.Clear();

        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                AddObj(currBatch, x, y);
                batchIndexNum += objectsPerBlock;
                if (batchIndexNum >= 1000)
                {
                    batches.Add(currBatch);
                    currBatch = BuildNewBatch();
                    batchIndexNum = 0;
                }
            }
        } 
    }

    private void Update()
    {
        RenderBatches();
    }

    private void AddObj(List<TerrainData> currBatch, int x, int y)
    {
        Color pixelColor = bitmap.map.GetPixel(x, y);
        
        // Dirt
        if (bitmap.TerrainColor.Equals(pixelColor))
        {
            Vector3 position = new Vector3(x, y, 0);
            GameObject objToSpawn = new GameObject("Dirt");
            objToSpawn.tag = "Dirt";
            objToSpawn.transform.position = position;
            objToSpawn.AddComponent<BoxCollider>();
            objToSpawn.transform.parent = transform.parent;
            currBatch.Add(new TerrainData(position, new Vector3(1, 1, 5), Quaternion.identity));
        }

    }

    private List<TerrainData> BuildNewBatch()
    {
        return new List<TerrainData>();
    }

    private void RenderBatches()
    {
        foreach (var batch in batches)
        {
            Graphics.DrawMeshInstanced(terrainMesh, 0, terrainMaterial, batch.Select((a) => a.matrix).ToList(), materialPropertyBlock);
        }
    }
}
