using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour

{
    public Transform target;
    public Vector3 offset;
    public float smoothSpeed = 0.125f;

    [System.Serializable]
    public class CameraBounds
    {
        public Vector2 minBounds;
        public Vector2 maxBounds;
    }

    public CameraBounds bounds16x9;
    public CameraBounds bounds4x3;
    public CameraBounds bounds2048x1536;

    private CameraBounds currentBounds;

    void Start()
    {
        SetBounds();
    }

    void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 desiredPosition = target.position + offset;
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, currentBounds.minBounds.x, currentBounds.maxBounds.x);
        desiredPosition.z = Mathf.Clamp(desiredPosition.z, currentBounds.minBounds.y, currentBounds.maxBounds.y);

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

    void SetBounds()
    {
        float aspect = Camera.main.aspect;

        if (aspect >= 1.7f && aspect <= 1.8f) // 16:9 aspect ratio
        {
            currentBounds = bounds16x9;
        }
        else if (aspect >= 1.3f && aspect <= 1.4f) // 4:3 aspect ratio
        {
            currentBounds = bounds4x3;
        }
        else if (Screen.width == 2048 && Screen.height == 1536) // 2048x1536 resolution
        {
            currentBounds = bounds2048x1536;
        }
        else
        {
            Debug.LogWarning("Aspect ratio or resolution not supported, defaulting to 16:9 bounds.");
            currentBounds = bounds16x9;
        }
    }
}