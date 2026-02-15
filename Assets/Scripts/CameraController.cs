using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// An optional script for a different play style; follows the player in case the level's length is beyond the x coordinate of the camera's size
/// </summary>
public class CameraController : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private Transform playerPos;
    [SerializeField] private Vector3 startingOffset;

    [SerializeField] private float cameraMinBound;
    [SerializeField] private float cameraMaxBound;

    bool cameraWithinLevelRange;

    private float horizontalUpperBound;
    private float horizontalLowerBound;
    private float halfWidth;
    public bool isCameraBoundReached { get; private set; }


    // Start is called before the first frame update
    void Start()
    {
        transform.position = startingOffset;

        cam = transform.GetComponent<Camera>();

        halfWidth = cam.orthographicSize * cam.aspect;
        horizontalUpperBound = transform.position.x + halfWidth;
        horizontalLowerBound = transform.position.x - halfWidth;
    }

    // Update is called once per frame
    void Update()
    {
        CameraPosition();
    }

    private void CameraPosition()
    {
        float playerPosition = playerPos.transform.position.x;

        cameraWithinLevelRange = transform.position.x >= cameraMinBound && transform.position.x <= cameraMaxBound;
        if (cameraWithinLevelRange /*&& playerPosition >= horizontalUpperBound*/ && !isCameraBoundReached)
        {
            UpdateCameraBounds(playerPosition);

        }
    }

    public void UpdateCameraBounds(float playerPosition)
    {
        int direction = playerPosition >= horizontalUpperBound ? 1 :
                        playerPosition < horizontalLowerBound ? -1 : 0;

        if (direction == 0) return;

        isCameraBoundReached = true;
        StartCoroutine(PanCameraRoutine(direction));
    }

    public IEnumerator PanCameraRoutine(int direction) // 1 = right, -1 = left
    {
        yield return new WaitForSeconds(.4f);

        float shift = 2f * halfWidth * direction;

        float targetX = Mathf.Clamp(
            transform.position.x + shift,
            cameraMinBound,
            cameraMaxBound
        );

        transform.DOMoveX(targetX, 1f);

        horizontalLowerBound += shift;
        horizontalUpperBound += shift;

        yield return new WaitForSeconds(.4f);

        isCameraBoundReached = false;
    }
}
