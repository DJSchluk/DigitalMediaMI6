using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFigures : MonoBehaviour {

    public List<GameObject> chessmanPrefabs;
    public List<GameObject> activeChessman;

    public Chessman[,] Chessmans { set; get; }
    public Chessman selectedChessman;

    private const float TILE_SIZE = 1.0f;
    private const float TILE_OFFSET = 0.5f;

    void Start () {
        SpawnAllChessmans();
	}

	void Update () {
		
	}

    public void SpawnAllChessmans()
    {
        activeChessman = new List<GameObject>();
        Chessmans = new Chessman[8, 8];

        //weiss
        //König
        SpawnChessman(0, 4, 0);
        //Dame
        SpawnChessman(1, 3, 0);
        //Türme
        SpawnChessman(2, 0, 0);
        SpawnChessman(2, 7, 0);
        //Läufer
        SpawnChessman(3, 2, 0);
        SpawnChessman(3, 5, 0);
        //Springer
        SpawnChessman(4, 1, 0);
        SpawnChessman(4, 6, 0);
        //Bauern
        for (int i = 0; i < 8; i++)
        {
            SpawnChessman(5, i, 1);
        }

        //schwarz
        //König
        SpawnChessman(6, 4, 7);
        //Dame
        SpawnChessman(7, 3, 7);
        //Türme
        SpawnChessman(8, 0, 7);
        SpawnChessman(8, 7, 7);
        //Läufer
        SpawnChessman(9, 2, 7);
        SpawnChessman(9, 5, 7);
        //Springer
        SpawnChessman(10, 1, 7);
        SpawnChessman(10, 6, 7);
        //Bauern
        for (int i = 0; i < 8; i++)
        {
            SpawnChessman(11, i, 6);
        }

    }

    private void SpawnChessman(int index, int x, int y)
    {
        GameObject go = Instantiate(chessmanPrefabs[index], GetTileCenter(x, y), Quaternion.identity) as GameObject;
        go.transform.SetParent(transform);
        Chessmans[x, y] = go.GetComponent<Chessman>();
        Chessmans[x, y].setPosition(x, y);
        activeChessman.Add(go);

    }

    public Vector3 GetTileCenter(int x, int y)
    {
        Vector3 origin = Vector3.zero;
        origin.x += (TILE_SIZE * x) + TILE_OFFSET;
        origin.z += (TILE_SIZE * y) + TILE_OFFSET;
        return origin;
    }

}
