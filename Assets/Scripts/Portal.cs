using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public static Portal instance;
    [SerializeField] private string portalType;

    private void Awake()
    {
        portalType = gameObject.name;
    }

    private void Start()
    {
       
    }

    private void Update()
    {

    }
}
