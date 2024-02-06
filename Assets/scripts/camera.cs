using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Camera cam;
    private bool _zooming;
    private float _zoomSpeed;
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
            cam.orthographicSize -= _zoomSpeed * Time.deltaTime;
            if (cam.orthographicSize < 5)
            {
                _zooming = false;
            }
        }
        else
        {
            if (player.transform.position.x > 0)
            {
                transform.position = new Vector3(player.transform.position.x, 0, -10);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void finalZoom(Vector2 pos)
    {
        transform.position = new Vector3(pos.x, pos.y, 0);
        _zooming = true;
    }
}
