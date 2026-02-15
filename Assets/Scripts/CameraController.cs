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
    private float halfWidth;
    public bool isCameraBoundReached { get; private set; }


    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.Find("Player").transform;
        transform.position = startingOffset;

        cam = transform.GetComponent<Camera>();

        halfWidth = cam.orthographicSize * cam.aspect;
        horizontalUpperBound = transform.position.x + halfWidth;

        Debug.Log(cam.orthographicSize * cam.aspect);

        Debug.Log("lowerBound " + (transform.position.x - halfWidth) + "upper bound " + (transform.position.x + halfWidth));
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
        if (cameraWithinLevelRange && playerPosition >= horizontalUpperBound && !isCameraBoundReached)
        {
            isCameraBoundReached = true;
            UpdateCameraBounds();
            Debug.Log("Run condition");
        }
    }

    public void UpdateCameraBounds()
    {
        StartCoroutine(PanCameraRightRoutine());
    }

    public IEnumerator PanCameraRightRoutine()
    {
        yield return new WaitForSeconds(.4f);

        float targetPosition = transform.position.x + 2 * halfWidth;

        targetPosition = Mathf.Clamp(targetPosition, cameraMinBound, cameraMaxBound);

        transform.DOMoveX(targetPosition, 1);
        horizontalUpperBound += 2 * halfWidth;
        yield return new WaitForSeconds(.4f);
        isCameraBoundReached = false;
        Debug.Log(transform.position.x - cam.aspect * cam.orthographicSize);
        yield return null;
    }

    void OnDrawGizmos()
    {
        if (!cam || !cam.orthographic) return;

        float halfWidth = cam.orthographicSize * cam.aspect;

        float rightBound = cam.transform.position.x + halfWidth;
        float leftBound = cam.transform.position.x - halfWidth;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(
            new Vector3(rightBound, -1000f, 0),
            new Vector3(rightBound, 1000f, 0)
        );

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(
            new Vector3(leftBound, -1000f, 0),
            new Vector3(leftBound, 1000f, 0)
        );
    }
}
