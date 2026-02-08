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
    [SerializeField] private Vector3 offset;
    [SerializeField] float initalXPos;
    [SerializeField] private float translationOffset;
    private float horizontalUpperBound;
    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.Find("Player").transform;
        transform.position += offset;

        horizontalUpperBound = transform.position.x + MathF.Abs(transform.GetComponent<Camera>().orthographicSize / 2);
        Debug.Log("Horizontal Uppser bound: " + horizontalUpperBound);
    }

    // Update is called once per frame
    void Update()
    {
        CameraPosition();
    }

    private void CameraPosition()
    {
        Vector2 playerPosition = playerPos.position;
        //transform.position = new Vector3(playerPosition.x, transform.position.y, transform.position.z) + offset;
        //float horizontalUpperBound =
        if (playerPosition.x >= transform.position.x)
        {
            //transform.position = new Vector3(transform.position.x + translationOffset, transform.position.y, transform.position.z);
        }
    }
}
