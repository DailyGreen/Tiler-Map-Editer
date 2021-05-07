using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;

    // 카메라 줌에 쓰이는 변수들
    private float zoomSize;
    private const float zoomScale = 10f;
    private const float zoomLerpSpeed = 10f;
    [SerializeField]
    private float minZoom = 8f;
    [SerializeField]
    private float maxZoom = 4.5f;
    private float scrollData;

    // ----
    // 카메라 움직임 쓰임
    [SerializeField]
    private Vector3 limitPos;
    [SerializeField]
    private Vector3 minpos;
    public float fMoveSpeed = 10f;
    private const float borderThickness = 10f;      // 마우스가 스크린 밖에 닿는 범위( 두께 )
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
      * @brief 마우스 스크롤로 카메라 줌
      */
    void MouseScrollzoom()
    {
        scrollData = Input.GetAxis("Mouse ScrollWheel");

        zoomSize -= scrollData * zoomScale;
        zoomSize = Mathf.Clamp(zoomSize, maxZoom, minZoom);
        _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, zoomSize, Time.deltaTime * zoomLerpSpeed);
    }

    /**
     * @brief 카메라 세로 사이즈 알아오기
     */
    public float getCameraHeight()
    {
        return Camera.main.orthographicSize * 2.0f;
    }

    /**
     * @brief 카메라 가로 사이즈 알아오기
     */
    public float getCameraWidth()
    {
        return getCameraHeight() * Screen.width / Screen.height;
    }

    /**
     * @brief 마우스 드래그로 카메라 움직임
     */
    void CameraMove()
    {
        Vector3 pos = this.transform.position;

        pos.x = Mathf.Clamp(pos.x, minpos.x + (getCameraWidth() * 0.4f), limitPos.x - (getCameraWidth() * 0.4f));
        pos.y = Mathf.Clamp(pos.y, minpos.y + (getCameraHeight() * 0.4f), limitPos.y - (getCameraHeight() * 0.4f));

        if ((Input.GetKey("w") || Input.mousePosition.y >= Screen.height - borderThickness))
        {
            pos.y += fMoveSpeed * Time.deltaTime;
        }
        if ((Input.GetKey("s") || Input.mousePosition.y <= borderThickness))
        {
            pos.y -= fMoveSpeed * Time.deltaTime;
        }
        if ((Input.GetKey("d") || Input.mousePosition.x >= Screen.width - borderThickness))
        {
            pos.x += fMoveSpeed * Time.deltaTime;
        }
        if ((Input.GetKey("a") || Input.mousePosition.x <= borderThickness))
        {
            pos.x -= fMoveSpeed * Time.deltaTime;
        }
        pos.z = -20;

        this.transform.position = pos;
    }
}
