using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

/// <summary>
/// An optional script for a different play style; follows the player in case the level's length is beyond the x coordinate of the camera's size
/// </summary>
public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerPos;
    [SerializeField] private Vector3 startingOffset;
    [SerializeField] float initalXPos;
    [SerializeField] private float translationOffset;
    private float horizontalUpperBound;
    private Camera cam;
    private readonly float aspectRatio = 16 / 9;
    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.Find("Player").transform;
        transform.position = startingOffset;

        cam = transform.GetComponent<Camera>();

        horizontalUpperBound = transform.position.x + cam.orthographicSize * cam.aspect;
        Debug.Log("Horizontal Uppser bound: " + horizontalUpperBound);
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
        if (playerPosition >= horizontalUpperBound)
        {
            Debug.Log("Playerreached end of camera range");
            //transform.position = new Vector3(transform.position.x + translationOffset, transform.position.y, transform.position.z);
        }
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
