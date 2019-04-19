using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class FigurFactory : MonoBehaviour
{
    private static FigurFactory instance;
    [SerializeField]
    private GameObject pawnWhitePrefab;
    [SerializeField]
    private GameObject knightWhitePrefab;
    [SerializeField]
    private GameObject rookWhitePrefab;
    [SerializeField]
    private GameObject bishopWhitePrefab;
    [SerializeField]
    private GameObject queenWhitePrefab;
    [SerializeField]
    private GameObject kingWhitePrefab;
    [SerializeField]
    private GameObject pawnBlackPrefab;
    [SerializeField]
    private GameObject knightBlackPrefab;
    [SerializeField]
    private GameObject rookBlackPrefab;
    [SerializeField]
    private GameObject bishopBlackPrefab;
    [SerializeField]
    private GameObject queenBlackPrefab;
    [SerializeField]
    private GameObject kingBlackPrefab;
    [SerializeField]

    private void Awake()

    {   
        if(pawnWhitePrefab == null){
            Debug.Log("Weisser Bauer Prefab is null");
        }
        else {
            Debug.Log("Geht");
        }
        if (instance == null)
            instance = this;
        else
            this.enabled = false;
    }

    public static FigurFactory GetInstance()
    {
        if (instance == null)
        {
            GameObject gameobject = new GameObject("FigurFactory");
            instance = gameobject.AddComponent<FigurFactory>();
        }

        return instance;
    }

    public GameObject BuildWhitePawn()
    {
        return pawnWhitePrefab;
    }

    public GameObject BuildWhiteKnight()
    {
        return knightWhitePrefab;
    }

    public GameObject BuildWhiteRook()
    {
        return rookWhitePrefab;
    }

    public GameObject BuildWhiteBishop()
    {
        return bishopWhitePrefab;
    }

    public GameObject BuildWhiteQueen()
    {
        return queenWhitePrefab;
    }

    public GameObject BuildWhiteKing()
    {
        return kingWhitePrefab;
    }

    public GameObject BuildBlackPawn()
    {
        return pawnBlackPrefab;
    }

    public GameObject BuildBlackKnight()
    {
        return kingBlackPrefab;
    }

    public GameObject BuildBlackRook()
    {
        return rookBlackPrefab;
    }

    public GameObject BuildBlackBishop()
    {
        return bishopBlackPrefab;
    }

    public GameObject BuildBlackQueen()
    {
        return queenBlackPrefab;
    }

    public GameObject BuildBlackKing()
    {
        return knightBlackPrefab;
    }
}