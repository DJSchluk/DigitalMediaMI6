using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFigures
{

    public List<GameObject> chessmanPrefabs;
    private FigurFactory figur_factory;
    public List<GameObject> activeChessman;

    public Chessman[,] Chessmans { set; get; }
    public Chessman selectedChessman;

    private const float TILE_SIZE = 1.0f;
    private const float TILE_OFFSET = 0.5f;
    private Transform transform;

    public SpawnFigures(Transform _transform)
    {
        this.transform = _transform;
        figur_factory = FigurFactory.GetInstance();
    }

    public void SpawnAllChessmans()
    {
        if (figur_factory == null)
        {
            Debug.Log("Factory is null");
        }
        activeChessman = new List<GameObject>();
        Chessmans = new Chessman[8, 8];

        //weiss
        //König
        SpawnChessman(4, 0);
        //Dame
        SpawnChessman(3, 0);
        //Türme
        SpawnChessman(0, 0);
        SpawnChessman(7, 0);
        //Läufer
        SpawnChessman(2, 0);
        SpawnChessman(5, 0);
        //Springer
        SpawnChessman(1, 0);
        SpawnChessman(6, 0);
        //Bauern
        for (int i = 0; i < 8; i++)
        {
            SpawnChessman(i, 1);
        }

        //schwarz
        //König
        SpawnChessman(4, 7);
        //Dame
        SpawnChessman(3, 7);
        //Türme
        SpawnChessman(0, 7);
        SpawnChessman(7, 7);
        //Läufer
        SpawnChessman(2, 7);
        SpawnChessman(5, 7);
        //Springer
        SpawnChessman(1, 7);
        SpawnChessman(6, 7);
        //Bauern
        for (int i = 0; i < 8; i++)
        {
            SpawnChessman(i, 6);
        }

    }

    private void SpawnChessman(int x, int y)
    {
        GameObject test = figur_factory.BuildWhitePawn();
        if (test == null)
            Debug.Log("Objekt von Fabrik ist null");
        GameObject go = MonoBehaviour.Instantiate(figur_factory.BuildWhitePawn(), GetTileCenter(x, y), Quaternion.identity) as GameObject;
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
