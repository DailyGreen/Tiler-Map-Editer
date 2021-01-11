using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TILE
{
    GRASS = 0,
    SAND,
    DIRT,
    MARS,
    STONE,
    START_POINT,
    CAN_MOVE,
    SEA_01,
    SEA_02,
    SEA_03
}

public class Mng : MonoBehaviour
{
    private static Mng _Instance = null;

    /**********
    * 레이케스트 위한 변수
    */
    public RaycastHit2D hit;
    public Tile selectedTile = null;
    public Tile targetTile = null;

    private const int mapHeight = 50;
    private const int mapWidth = 50;

    public GameObject showtargettile;

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
            else selectedTile = hit.collider.gameObject.GetComponent<Tile>();
        }
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            mouseRaycast(true);
            if (hit.collider != null)
            {
                showtargettile.transform.localPosition = targetTile.transform.localPosition;
                targetTile._code = _code;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _code = (int)TILE.GRASS;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _code = (int)TILE.SAND;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _code = (int)TILE.DIRT;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _code = (int)TILE.MARS;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            _code = (int)TILE.STONE;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            _code = (int)TILE.SEA_01;
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            _code = (int)TILE.SEA_02; ;
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            _code = (int)TILE.SEA_03;
        }
        if (hit.collider != null)
        {
            if (Input.GetKeyDown(KeyCode.Space) && nCount < 24 && !targetTile._code.Equals((int)TILE.START_POINT))
            {
                targetTile._code = (int)TILE.START_POINT;
                sp = Instantiate(startpoint, targetTile.transform);
                sp.transform.localPosition = Vector2.zero;
                nCount++;
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
                if (mapTile[i, j]._code.Equals((int)TILE.START_POINT))
                {
                    mapTile[i, j]._code = 0;
                    DestroyImmediate(mapTile[i, j].transform.GetChild(0).gameObject);
                }
            }
        }
        nCount = 0;
    }
}

