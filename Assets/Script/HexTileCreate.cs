using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

public class HexTileCreate : MonoBehaviour
{
    public GameObject parentObject;
    public GameObject hextile;
    public Tile tilestate;      // ����

    // Ÿ�� ����
    public const float tileXOffset = 1.24f;
    public const float tileYOffset = 1.08f;

    public Tile[] cells;

    // �� �ε�(�ؽ�Ʈ �б�)
    TextAsset maptextload;
    String[] mapreadlines;
    char[] mapReadChar;

    // cells �ε���
    int i;
    // Start is called before the first frame update
    void Start()
    {   
        CreateHexTileMap();
    }

    void OnApplicationQuit()
    {
        tilestate.PosX = 0;
        tilestate.PosY = 0;
    }

    /**
     * @brief ��� Ÿ�ϸ� ����, Ÿ�� posX posY ����
     */
    void CreateHexTileMap()
    {
        cells = new Tile[Mng.I.getMapHeight * Mng.I.getMapwidth];
        for (int y = 0; y < Mng.I.getMapHeight; y++)
        {
            for (int x = 0; x < Mng.I.getMapwidth; x++)
            {
                GameObject child = Instantiate(hextile) as GameObject;
                child.transform.parent = parentObject.transform;
                cells[i] = child.GetComponent<Tile>();
                Mng.I.mapTile[y, x] = child.transform.GetComponent<Tile>();      // ������ Ÿ�� ��ũ��Ʈ GameMng.I.mapTile 2���� �迭�� ����
                if (y % 2 == 0)
                {
                    child.transform.position = new Vector2(x * tileXOffset, y * tileYOffset);
                }
                else
                {
                    child.transform.position = new Vector2(x * tileXOffset + tileXOffset / 2, y * tileYOffset);
                }
                tilestate.PosY++;
                i++;
            }
            tilestate.PosY = 0;
            tilestate.PosX++;
        }
    }

    public void LoadMapFile(string _filename)
    {
        i = 0;
        maptextload = Resources.Load(_filename) as TextAsset;
        mapreadlines = maptextload.text.Split('\n');
        Debug.Log(maptextload);
        for (int y = 0; y < Mng.I.getMapHeight; y++)
        {
            mapReadChar = mapreadlines[y].ToCharArray();
            for (int x = 0; x < Mng.I.getMapwidth; x++)
            {
                cells[i]._code = (int)Char.GetNumericValue(mapReadChar[y]);
                if (mapReadChar[x] >= (char)TILE.GRASS_START) { cells[i]._code = (int)mapReadChar[x];
                    if(mapReadChar[x] < (char)TILE.GRASS_TREE)
                    {
                        cells[i]._code = 0;
                    }
                }
                else { cells[i]._code = (int)Char.GetNumericValue(mapReadChar[x]); }
                i++;
            }
        }
    }
}
