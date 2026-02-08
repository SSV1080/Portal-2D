using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    public delegate void PlayerInteraction();
    public static  PlayerInteraction onLevelCompleted;

    public PlayerController playerController;

    private void Awake()
    {
        current = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.levelCompleted)
            onLevelCompleted?.Invoke();
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
