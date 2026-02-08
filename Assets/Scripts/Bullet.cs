using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private LayerMask collisionLayer;
    public string portalType;
    public GameObject portalPrefab;
    private float bulletAngle;

    // Start is called before the first frame update
    void Start()
    {
        GunController.canShootOrange = false;
        GunController.canShootBlue = false;
        bulletAngle = GameObject.Find("PortalGun").GetComponent<GunController>().bulletProjectileAngle;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Sets these bool values to true, so multiple portals are not shot at once
    /// </summary>
    public void OnDestroy()
    {
        GunController.canShootBlue = true;
        GunController.canShootOrange = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("wall") || collision.gameObject.tag.Equals("ceiling"))
        {
            GeneratePortal(collision);
            //Destroy(gameObject);
        }
        Destroy(gameObject);
    }

    /// <summary>
    ///Generates a portal at the location it is shot. Commented code used for an older, less efficient mechanism.   
    /// </summary>
    /// <param name="objectCollidedWith"></param>
    public void GeneratePortal(Collision2D objectCollidedWith)
    {
        /*Quaternion newAngle = new Quaternion();
        if((bulletAngle<80 && bulletAngle>-80) && objectCollidedWith.gameObject.tag.Equals("wall"))
        {
           newAngle = Quaternion.Euler(0, 0, 0);
        }
        else if((bulletAngle < 180 && bulletAngle>0) && objectCollidedWith.gameObject.tag.Equals("ceiling"))
        {
            newAngle = Quaternion.Euler(0, 0, 90);
        }
        else if (((bulletAngle >= 90 && bulletAngle <= 180) || (bulletAngle > -180 && bulletAngle < -150)) && objectCollidedWith.gameObject.tag.Equals("wall"))
        {
            newAngle = Quaternion.Euler(0, 0, 180);
        }
        else if (*//*bulletAngle >= -180 && *//*bulletAngle <0 && objectCollidedWith.gameObject.tag.Equals("ceiling"))
        {
           newAngle = Quaternion.Euler(0, 0, -90);
        }
        Vector3 newPos = new Vector3(gameObject.transform.position.x - 19, gameObject.transform.position.y, gameObject.transform.position.z);*/
        GameObject newPortal = Instantiate(portalPrefab, gameObject.transform.position, /*newAngle*/gameObject.transform.rotation);
        newPortal.transform.SetParent(objectCollidedWith.transform);
    }
}
