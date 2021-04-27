using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;

    // ī�޶� �ܿ� ���̴� ������
    private float zoomSize;
    private const float zoomScale = 10f;
    private const float zoomLerpSpeed = 10f;
    [SerializeField]
    private float minZoom = 16f;
    [SerializeField]
    private float maxZoom = 4.5f;
    private float scrollData;

    // ----
    // ī�޶� ������ ����
    [SerializeField]
    private Vector3 limitPos;
    public float fMoveSpeed = 10f;
    private const float borderThickness = 10f;      // ���콺�� ��ũ�� �ۿ� ��� ����( �β� )
    void Start()
    {
        _camera = Camera.main;
        zoomSize = _camera.orthographicSize;
    }

    void LateUpdate()
    {
        CameraMove();
        MouseScrollzoom();
    }

    /**
      * @brief ���콺 ��ũ�ѷ� ī�޶� ��
      */
    void MouseScrollzoom()
    {
        scrollData = Input.GetAxis("Mouse ScrollWheel");

        zoomSize -= scrollData * zoomScale;
        zoomSize = Mathf.Clamp(zoomSize, maxZoom, minZoom);
        _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, zoomSize, Time.deltaTime * zoomLerpSpeed);
    }

    /**
     * @brief ���콺 �巡�׷� ī�޶� ������
     */
    void CameraMove()
    {
        Vector3 pos = this.transform.position;
        if (Input.GetKey("w"))
        {
            pos.y += fMoveSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s"))
        {
            pos.y -= fMoveSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d"))
        {
            pos.x += fMoveSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            pos.x -= fMoveSpeed * Time.deltaTime;
        }
        if (Input.GetKey("l"))
        {
            Mng.I.hexTileCreate.LoadMapFile("mapinfo");
        }
        pos.z = -20;

        pos.x = Mathf.Clamp(pos.x, -limitPos.x, limitPos.x);
        pos.y = Mathf.Clamp(pos.y, -limitPos.y, limitPos.y);
        this.transform.position = pos;
    }
}
