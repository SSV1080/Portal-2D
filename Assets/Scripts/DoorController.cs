using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorController : MonoBehaviour
{
    public int id;
    public float initialPos;
    public float movedPos;
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onGateOpenTrigger += OnGateOpen;
        GameEvents.current.onGateCloseTrigger += OnGateClose;
        initialPos = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnGateOpen(int id)
    {
        if(id == this.id)
        {
            transform.DOMoveY(movedPos, 1.2f);
        }
    }

    public void OnGateClose(int id)
    {
        if (id == this.id)
            transform.DOMoveY(initialPos, 1.2f);
    }
}
