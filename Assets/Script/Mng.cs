using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mng : MonoBehaviour
{
    private static Mng _Instance = null;

    /**********
    * �����ɽ�Ʈ ���� ����
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
    * @brief �����ɽ�Ʈ ������ ����
    * @param isTarget �����ɽ�Ʈ Ÿ���� �����Ҷ� ���. targetTile ���� �޾ƿö� true ���ָ� ��
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
        if (Input.GetMouseButtonDown(0))
        {
            mouseRaycast(true);
            if (hit.collider != null)
            {
                showtargettile.transform.localPosition = targetTile.transform.localPosition;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            targetTile._code = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            targetTile._code = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            targetTile._code = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            targetTile._code = 3;
        }
    }
}
