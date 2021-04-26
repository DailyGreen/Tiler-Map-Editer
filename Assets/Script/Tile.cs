using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Tile : Object
{
    [SerializeField]
    private int posX, posY;

    GameObject tile;
    [SerializeField]
    Sprite[] tileSprite;
    [SerializeField]
    Sprite[] tiledecoSprite;
    [SerializeField]
    SpriteRenderer tileSpriteRend;

    public bool startpoint;

    void Start()
    {
        _code = 0;
        startpoint = false;
        tile = this.GetComponent<GameObject>();
        //this.tileSpriteRend.sprite = tileSprite[this._code];
        _name = "독도는";
        _desc = "우리땅";
    }

    private void Update()
    {
        if (this._code >= (int)TILE.GRASS_TREE)
        {
            this.tileSpriteRend.sprite = tiledecoSprite[this._code - (int)TILE.GRASS_TREE];
        }
        else if (this._code >= (int)TILE.GRASS_START && this._code < (int)TILE.GRASS_TREE) { this.tileSpriteRend.sprite = tileSprite[this._code - (int)TILE.GRASS_START]; }
        else if (this._code < (int)TILE.CAN_MOVE) { this.tileSpriteRend.sprite = tileSprite[this._code]; }

        if (startpoint)
        {
            switch (_code)
            {
                case (int)TILE.GRASS:
                    _code = (int)TILE.GRASS_START;
                    break;
                case (int)TILE.SAND:
                    _code = (int)TILE.SAND_START;
                    break;
                case (int)TILE.DIRT:
                    _code = (int)TILE.DIRT_START;
                    break;
                case (int)TILE.STONE:
                    _code = (int)TILE.STONE_START;
                    break;
            }
        }
    }
    /**
     * @brief 타일의 posX,posY값  설정 또는 값 알아오기
     */
    public int PosX
    {
        get
        {
            return posX;
        }
        set
        {
            posX = value;
        }
    }

    public int PosY
    {
        get
        {
            return posY;
        }
        set
        {
            posY = value;
        }
    }

    /**
     * @brief 타일이 어디있는지 Vec2 알아오기
     */
    public Vector2 GetTileVec2
    {
        get
        {
            return new Vector2(this.transform.position.x, this.transform.position.y);
        }
    }
}
