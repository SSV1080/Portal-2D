using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An optional script for a different play style; follows the player in case the level's length is beyond the x coordinate of the camera's size
/// </summary>
public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerPos;
    [SerializeField] private Vector3 offset;
    [SerializeField] float initalXPos;
    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        CameraPosition();
    }

    private void CameraPosition()
    {
        Vector2 playerPosition = playerPos.position;
        transform.position = new Vector3(playerPosition.x, transform.position.y, transform.position.z) + offset;
    }
}
