using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

public class HexTileCreate : MonoBehaviour
{
    public GameObject parentObject;
    public GameObject hextile;
    public Tile tilestate;      // 프림

    // 타일 간격
    public const float tileXOffset = 1.24f;
    public const float tileYOffset = 1.08f;

    public Tile[] cells;

    // 맵 로드(텍스트 읽기)
    TextAsset maptextload;
    String[] mapreadlines;
    char[] mapReadChar;

    // cells 인덱스
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
     * @brief 헥사 타일맵 생성, 타일 posX posY 설정
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
                Mng.I.mapTile[y, x] = child.transform.GetComponent<Tile>();      // 각각의 타일 스크립트 GameMng.I.mapTile 2차원 배열에 저장
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
