using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    public delegate void PlayerInteraction();
    public static PlayerInteraction onLevelCompleted;

    public event Action onCameraShiftStarted;
    public event Action onCameraShiftFinished;

    public PlayerController playerController;
    public CameraController cameraController;

    private void Awake()
    {
        current = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        cameraController = GameObject.Find("Main Camera").GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.levelCompleted)
            onLevelCompleted?.Invoke();

        if (cameraController.isCameraBoundReached)
        {
            onCameraShiftStarted?.Invoke();
        }
        if (!cameraController.isCameraBoundReached)
        {
            onCameraShiftFinished?.Invoke();
        }
    }

    public event Action<int> onGateOpenTrigger;
    public event Action<int> onGateCloseTrigger;
    public void GateOpenTrigger(int gateId)
    {
        onGateOpenTrigger?.Invoke(gateId);
    }
    public void GateCloseTrigger(int gateId)
    {
        onGateCloseTrigger?.Invoke(gateId);
    }
}
