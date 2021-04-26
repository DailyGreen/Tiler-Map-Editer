using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TILE
{
    GRASS = 0,
    SAND,
    DIRT,
    STONE,
    CAN_MOVE,
    GRASS_START = 'A',
    SAND_START = 'B',
    DIRT_START = 'C',
    STONE_START = 'D',
    GRASS_TREE = 'E',
    GRASS_STONE = 'F',
    SAND_TREE = 'G',
    SAND_STONE = 'H',
    DIRT_TREE = 'I',
    DIRT_STONE = 'J',
    STONE_DECO1 = 'K',
    STONE_DECO2 = 'L',
    SEA_01 = 'M',
    SEA_02 = 'N',
    SEA_03 = 'O'
}

public class Mng : MonoBehaviour
{
    private static Mng _Instance = null;

    /**********
    * 레이케스트 위한 변수
    */
    public RaycastHit2D hit;
    //public Tile selectedTile = null;
    public Tile targetTile = null;

    private const int mapHeight = 50;
    private const int mapWidth = 50;

    public GameObject showtargettile;

    [SerializeField]
    public UnityEngine.UI.Text count;

    private bool multykeycheck;

    public int getMapwidth
    {
        get
        {
            return mapWidth;
        }
    }
    public int getMapHeight
    {
        get
        {
            return mapHeight;
        }
    }
    public Tile[,] mapTile = new Tile[mapHeight, mapWidth];

    public string _filename;
    public GameObject startpoint;

    public int nCount = 0;     // 스타팅 포인트 갯수
    int _code;
    GameObject sp;
    // Start is called before the first frame update
    public static Mng I
    {
        get
        {
            if (_Instance.Equals(null))
            {
                Debug.Log("instance is null");
            }
            return _Instance;
        }
    }

    void Awake()
    {
        _Instance = this;
        DontDestroyOnLoad(transform.gameObject);
    }

    /**
    * @brief 레이케스트 레이저 생성
    * @param isTarget 레이케스트 타겟을 변경할때 사용. targetTile 값을 받아올때 true 해주면 됨
    */
    public void mouseRaycast(bool isTarget = false)
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Ray2D ray = new Ray2D(pos, Vector2.zero);

        hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit.collider != null)
        {
            if (isTarget) targetTile = hit.collider.gameObject.GetComponent<Tile>();
        }
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            mouseRaycast(true);
            if (targetTile != null)
            {
                showtargettile.transform.localPosition = targetTile.transform.localPosition;
                targetTile._code = _code;
            }
        }
        if (Input.GetMouseButton(1))
        {
            mouseRaycast(true);
            showtargettile.transform.localPosition = targetTile.transform.localPosition;
            switch (_code)
            {
                case (int)TILE.GRASS:
                    targetTile._code = (int)TILE.GRASS_TREE;
                    break;
                case (int)TILE.SAND:
                    targetTile._code = (int)TILE.SAND_TREE;
                    break;
                case (int)TILE.DIRT:
                    targetTile._code = (int)TILE.DIRT_TREE;
                    break;
                case (int)TILE.STONE:
                    targetTile._code = (int)TILE.STONE_DECO1;
                    break;
            }
        }
        if (Input.GetMouseButton(2))
        {
            mouseRaycast(true);
            showtargettile.transform.localPosition = targetTile.transform.localPosition;
            switch (_code)
            {
                case (int)TILE.GRASS:
                    targetTile._code = (int)TILE.GRASS_STONE;
                    break;
                case (int)TILE.SAND:
                    targetTile._code = (int)TILE.SAND_STONE;
                    break;
                case (int)TILE.DIRT:
                    targetTile._code = (int)TILE.DIRT_STONE;
                    break;
                case (int)TILE.STONE:
                    targetTile._code = (int)TILE.STONE_DECO2;
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _code = (int)TILE.GRASS;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _code = (int)TILE.SAND;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _code = (int)TILE.DIRT;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _code = (int)TILE.STONE;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            _code = (int)TILE.SEA_01;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            _code = (int)TILE.SEA_02; ;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            _code = (int)TILE.SEA_03;
        }
        else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            multykeycheck = false;
        }
        if (targetTile != null)
        {
            if (Input.GetKeyDown(KeyCode.Space) && nCount < 24 && !targetTile.startpoint && targetTile._code < (int)TILE.SEA_01)
            {
                switch (targetTile._code)
                {
                    case (int)TILE.GRASS:
                        targetTile._code = (int)TILE.GRASS_START;
                        targetTile.startpoint = true;
                        break;
                    case (int)TILE.SAND:
                        targetTile._code = (int)TILE.SAND_START;
                        targetTile.startpoint = true;
                        break;
                    case (int)TILE.DIRT:
                        targetTile._code = (int)TILE.DIRT_START;
                        targetTile.startpoint = true;
                        break;
                    case (int)TILE.STONE:
                        targetTile._code = (int)TILE.STONE_START;
                        targetTile.startpoint = true;
                        break;
                }
                sp = Instantiate(startpoint, targetTile.transform);
                sp.transform.localPosition = Vector2.zero;
                nCount++;
                count.text = "시작지점 갯수: " + nCount;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartingPointReset();
        }
    }

    void StartingPointReset()
    {
        for (int i = 0; i < mapHeight; i++)
        {
            for (int j = 0; j < mapWidth; j++)
            {
                if (mapTile[i, j].startpoint)
                {
                    mapTile[i, j]._code = 0;
                    DestroyImmediate(mapTile[i, j].transform.GetChild(0).gameObject);
                    mapTile[i, j].startpoint = false;
                }
            }
        }
        nCount = 0;
        count.text = "시작지점 갯수: " + nCount;
    }
}

