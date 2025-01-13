using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] RoadTiles;
    public GameObject[] SideTiles;
    public float zSpawn=0f;
    public float tileLength=16f;
    public int nbRandomTilesAtStart=5;
    public Transform PlayerTransform;
    private List<GameObject> activeTiles=new List<GameObject>();
    private List<GameObject> activeSideTiles=new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<nbRandomTilesAtStart;i++)
        {
            if(i==0)
            {
                //first spawn the tile without obstacle
                spawnTile(0);
            }
            else if(i==1)
            {
                //secondly make sure the second tile isn't again without obstacles
                spawnTile(Random.Range(1,RoadTiles.Length-1));
            }
            else
            {
                spawnTile(Random.Range(0,RoadTiles.Length-1));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //adding a new tile as the player moves
        if(PlayerTransform.position.z-tileLength > zSpawn-(nbRandomTilesAtStart*tileLength))
        {
            spawnTile(Random.Range(0,RoadTiles.Length-1));
            DeleteTile();
        }
        
    }
    public void spawnTile(int tileIndex)
    {
        GameObject go1=Instantiate(RoadTiles[tileIndex],transform.forward*zSpawn,transform.rotation);
        activeTiles.Add(go1);
        GameObject go2=Instantiate(SideTiles[tileIndex],transform.forward*zSpawn,transform.rotation);
        activeSideTiles.Add(go2);
        zSpawn+=tileLength;
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
        Destroy(activeSideTiles[0]);
        activeSideTiles.RemoveAt(0);
    }
}
