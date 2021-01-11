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
    SpriteRenderer tileSpriteRend;

    void Start()
    {
        _code = 0;
        tile = this.GetComponent<GameObject>();
        //this.tileSpriteRend.sprite = tileSprite[this._code];
        _name = "독도는";
        _desc = "우리땅";
    }

    private void Update()
    {
        if (!this._code.Equals((int)TILE.START_POINT))
        {
            if (this._code > (int)TILE.CAN_MOVE) { this.tileSpriteRend.sprite = tileSprite[this._code - 2]; }
            else if (this._code < (int)TILE.CAN_MOVE) { this.tileSpriteRend.sprite = tileSprite[this._code]; }
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
