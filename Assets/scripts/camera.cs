using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private Camera cam;
    private bool _zooming;
    private float _zoomSpeed;
    private float _zoomStartTime;
    private float _zoomDur;
    private Vector3 _zoomStartPos;
    private Vector3 _zoomFinalPos;
    public static CameraScript Instance; // singleton instance

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // singletons can only have one
        }
    }

    void Start()
    {
        _zooming = false;
        _zoomSpeed = 1;
        cam = GetComponent<Camera>();
        if (cam == null) Debug.LogError("cameraScript failed to link instance of camera");
    }
    // Update is called once per frame
    void Update()
    {
        if (_zooming)
        {
            float t = (Time.time - _zoomStartTime) / _zoomDur;
            transform.position = new Vector3(_zoomStartPos.x + (_zoomFinalPos.x - _zoomStartPos.x * t),
                _zoomStartPos.y + (_zoomFinalPos.y - _zoomStartPos.y * t), 0);
            cam.orthographicSize -= _zoomSpeed * Time.deltaTime;
            if (cam.orthographicSize < 5)
            {
                _zooming = false;
            }
        }
        else
        {
            Vector3 playerTransformPosition = Player.Instance.gameObject.transform.position;
            if (playerTransformPosition.x > 0)
            {
                transform.position = new Vector3(playerTransformPosition.x, transform.position.y, -10);
            }
            if (playerTransformPosition.y > 4)
            {
                transform.position = new Vector3(transform.position.x, playerTransformPosition.y - 4, -10);
            }
            else if (playerTransformPosition.y < -7)
            {
                transform.position = new Vector3(transform.position.x, playerTransformPosition.y + 7, -10);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void finalZoom(Vector2 pos)
    {
        _zoomStartTime = Time.time;
        _zoomFinalPos = new Vector3(pos.x, pos.y, -10);
        _zoomStartPos = transform.position;
        _zooming = true;
    }
}
