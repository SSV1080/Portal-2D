using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Box : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float movedPos;
    [SerializeField] private float initialPos;
    [SerializeField] private float platformMovementRate;

    void Start()
    {
        InvokeRepeating("PlatformInteraction", 0, 4);
        initialPos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void PlatformInteraction()
    {
        StartCoroutine(MovePlatform());
    }

    public IEnumerator MovePlatform()
    {
        transform.DOMoveX(movedPos, platformMovementRate);
        yield return new WaitForSeconds (platformMovementRate);
        transform.DOMoveX(initialPos, platformMovementRate);
        yield return new WaitForSeconds (platformMovementRate);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(gameObject.transform);
    }
    public void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}
