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

    private float horizontalUpperBound;

    public bool isCameraBoundReached { get; private set; }


    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.Find("Player").transform;
        transform.position = startingOffset;

        cam = transform.GetComponent<Camera>();

        horizontalUpperBound = transform.position.x + cam.orthographicSize * cam.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        CameraPosition();
    }

    private void CameraPosition()
    {
        float playerPosition = playerPos.transform.position.x;
        //transform.position = new Vector3(playerPosition.x, transform.position.y, transform.position.z) + offset;
        //float horizontalUpperBound =
        if (playerPosition >= horizontalUpperBound && !isCameraBoundReached)
        {
            isCameraBoundReached = true;
            UpdateCameraBounds();
            Debug.Log("Run condition");
        }
    }

    public void UpdateCameraBounds()
    {
        StartCoroutine(UpdateCameraBoundsRoutine());
    }

    public IEnumerator UpdateCameraBoundsRoutine()
    {
        yield return new WaitForSeconds(.4f);
        transform.DOMoveX(2 * horizontalUpperBound, 1);
        horizontalUpperBound += cam.orthographicSize * cam.aspect;
        yield return new WaitForSeconds(.4f);
        isCameraBoundReached = false;
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
