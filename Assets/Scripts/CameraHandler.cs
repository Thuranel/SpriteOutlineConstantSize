using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private float _maxZoom = 5;
    [SerializeField] private float _minZoom = 12;

    [SerializeField] private float _dragSpeed = 0.1f;
    private Vector3 _dragPos;

    private void Update()
    {
        DragCamera();

        HandleZoom();
    }

    private void HandleZoom()
    {
        float mouseScroll = Input.mouseScrollDelta.y;
        if (mouseScroll == 0)
        {
            return;
        }

        Camera.main.orthographicSize = Math.Clamp(Camera.main.orthographicSize - mouseScroll, _maxZoom, _minZoom);
    }

    private void DragCamera()
    {
        if (!Input.GetMouseButton(1))
        {
            _dragPos = Input.mousePosition;
            return;
        }

        if (_dragPos == Input.mousePosition)
        {
            return;
        }

        Camera cam = Camera.main;
        Vector3 dragPosWorld = cam.ScreenToWorldPoint(new Vector3(_dragPos.x, _dragPos.y, cam.nearClipPlane));
        Vector3 mousePositionWorld = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane));
        Vector3 mouseMovementWorld = dragPosWorld - mousePositionWorld;

        cam.transform.position = new Vector3(cam.transform.position.x + mouseMovementWorld.x, cam.transform.position.y + mouseMovementWorld.y, cam.transform.position.z);

        _dragPos = Input.mousePosition;
    }
}
