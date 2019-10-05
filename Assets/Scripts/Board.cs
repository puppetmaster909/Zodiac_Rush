using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int width, height;
    public GameObject tilePrefab;
    public GameObject[] icons;
    private Background_Tile[,] allTiles;
    public GameObject[,] allIcons;

    // Start is called before the first frame update
    void Start()
    {
        allTiles = new Background_Tile[width, height];
        allIcons = new GameObject[width, height];
        //Setup the board
        SetUp();
    }

   private void SetUp()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector2 tempPosition = new Vector2(i, j);
                GameObject backgroundTile = Instantiate(tilePrefab, tempPosition, Quaternion.identity) as GameObject;
                backgroundTile.transform.parent = this.transform;
                backgroundTile.name = "( " + i + ", " + j + " )";

                int iconToUse = Random.Range(0, icons.Length);
                GameObject icon = Instantiate(icons[iconToUse], tempPosition, Quaternion.identity);
                icon.transform.parent = this.transform;
                icon.name = "( " + i + ", " + j + " )";
                allIcons[i, j] = icon;
            }
        }
    }
}
