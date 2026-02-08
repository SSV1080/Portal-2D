using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    public int id;
    private Color defaultColor;
    // Start is called before the first frame update
    void Start()
    {
        defaultColor = GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Turns the gate lever green when the player is on it
    /// </summary>
    /// <param name="collision"></param>
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player") || collision.tag.Equals("interact"))
        {
            GetComponent<Renderer>().material.color = Color.green;
            GameEvents.current.GateOpenTrigger(id);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        GetComponent<Renderer>().material.color = defaultColor;
        GameEvents.current.GateCloseTrigger(id);
    }

}
