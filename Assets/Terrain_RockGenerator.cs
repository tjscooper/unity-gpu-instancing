using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
 * Constraints
 * 
 * 1. Batches represent groups of meshes to be rendered
 * 2. Batches have a max size of 1023 items
 * 3. Instance count is less concrete
 * 4. Should be aligned on the Z axis (represent the grid)
 * 5. Deal breaker - Not sure if the meshes can become game objects (tags? / colliders?)
 * 
 * Steps
 * 
 * 1. Import a Texture2D bitmap
 * 2. Iterate over X/Y map
 * 3. Identify "Dirt" blocks and Add to batch with position
 * 4. Render as per X/Y Vector2 position
 * 
 */

public class Terrain_RockGenerator : MonoBehaviour
{
    [SerializeField] BitmapToPrefab bitmap = null;

    public Mesh terrainMesh;
    public Material terrainMaterial;

    private List<List<TerrainData>> batches = new List<List<TerrainData>>();

    void Start()
    {
        int gridSize = bitmap.map.width;
        int batchIndexNum = 0;
        List<TerrainData> currBatch = new List<TerrainData>();

        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {

                AddObj(currBatch, x, y);
                batchIndexNum++;
                if (batchIndexNum >= 1000)
                {
                    batches.Add(currBatch);
                    currBatch = BuildNewBatch();
                    batchIndexNum = 0;
                }
            }
        }
    }

    private void AddObj(List<TerrainData> currBatch, int x, int y)
    {
        Vector3 position = new Vector3(x, y, 0);
        Color pixelColor = bitmap.map.GetPixel(x, y);

        // Rock
        if (bitmap.TerrainColor.Equals(pixelColor))
        {
            currBatch.Add(new TerrainData(position, new Vector3(1, 1, 1), Quaternion.identity));
        }
        
    }

    private List<TerrainData> BuildNewBatch()
    {
        return new List<TerrainData>();
    }

    void Update()
    {
        RenderBatches();
    }

    private void RenderBatches()
    {
        foreach (var batch in batches)
        {
            Graphics.DrawMeshInstanced(terrainMesh, 0, terrainMaterial, batch.Select((a) => a.matrix).ToList());
        }
    }
}
