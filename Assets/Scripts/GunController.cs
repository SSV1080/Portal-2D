using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    private Transform thisObjPos;
    [SerializeField] private Transform bulletSpawnPoint;

    [SerializeField] private GameObject entryBulletPrefab;
    [SerializeField] private GameObject exitBulletPrefab;

    public float bulletProjectileAngle; ///Need this for rotating the portal, and send this to the bullet script

    private GameObject entryPortalPrefab; 
    private GameObject exitPortalPrefab;

    private Vector3 hitPoint;
    public static bool canShootOrange;
    public static bool canShootBlue;

    public bool orangeDebug;
    public bool blueDebug;

    [SerializeField] private LineRenderer bulletTrail;

    private void Start()
    {
        GameEvents.onLevelCompleted += DisableScript; //Disables the gun controller script once the level has completed
        mainCam = Camera.main;
        canShootOrange = true;
        canShootBlue = true;
        thisObjPos = this.transform;
    }

    /// <summary>
    /// Shoots an orange portal with left click and blue portal with right click
    /// </summary>
    private void Update()
    {
        LookAtMouse();

        if (Input.GetButtonDown("Fire1") && canShootOrange)
        {
            entryPortalPrefab = GameObject.Find("entryPortal(Clone)");
            if(entryPortalPrefab != null)
            {
                Destroy(entryPortalPrefab);
            }
            CastRay(entryBulletPrefab);
            //StartCoroutine(TrailLine());
        }
        if (Input.GetButtonDown("Fire2") && canShootBlue)
        {
            exitPortalPrefab = GameObject.Find("exitPortal(Clone)");
            if (exitPortalPrefab != null)
            {
                Destroy(exitPortalPrefab);
            }
            CastRay(exitBulletPrefab);
            //StartCoroutine(TrailLine());
        }
        ///Debug bools to find check the bools' status in the editor
    /*  orangeDebug = canShootOrange;
         blueDebug = canShootBlue;*/
    }

    public void DisableScript()
    {
        gameObject.GetComponent<GunController>().enabled = false;
    }

    public void OnDisable()
    {
        GameEvents.onLevelCompleted -= DisableScript;
    }

    /// <summary>
    /// Spawns the bullet prefab at the gun point
    /// </summary>
    /// <param name="bullet"></param>
    /// <param name="hitPoint"></param>
    /// <param name="hitRotation"></param>
    void ShootBullet(GameObject bullet, Vector3 hitPoint, Quaternion hitRotation)
    {
        // Instantiate the object at the hit point
        if(entryBulletPrefab != null)
        {
            bullet = Instantiate(bullet, hitPoint, hitRotation);
            Vector2 direction = (hitPoint- transform.position).normalized;
            bullet.GetComponent<Rigidbody2D>().AddForce(direction * 2000);
        }       
    }

    /// <summary>
    /// Uses raycasting to cast a ray from the gun in the direction of the mouse pointer to the first obstacle it comes across
    /// </summary>
    /// <param name="bullet1"></param>
    public void CastRay(GameObject bullet1)
    {
        // Cast a ray from the mouse position
        Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(clickPosition, /*Vector2.zero*/Input.mousePosition);
        Debug.DrawLine(this.transform.position, hit.point);
        // Check if the ray hits an object
        hitPoint = hit.point;
        if (hit)
        {
            ShootBullet(bullet1,bulletSpawnPoint.position, Quaternion.identity);
        }
    }
    /// <summary>
    /// Creates a bullet trail for an instant to tell the player where the portal is going, and to tell the player a portal is being launched.
    /// </summary>
    /// <returns></returns>
    [System.Obsolete]
   /* public IEnumerator TrailLine()
    {
        bulletTrail.enabled = true;
        if (bulletTrail.material == null)
        {
            bulletTrail.material = new Material(Shader.Find("Sprites/Default"));
        }
        yield return new WaitForSeconds(0f);
        bulletTrail.enabled = false;
    }*/

    /// <summary>
    /// Rotates the gun based the cursor/mouse's position
    /// </summary>
    public void LookAtMouse()
    {
        Vector2 lookDirection = mainCam.ScreenToWorldPoint(Input.mousePosition) - thisObjPos.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        FlipGunSprite(angle);
        bulletProjectileAngle = angle;
        transform.parent.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //thisObjPos.rotation = rotation;
    }

    /// <summary>
    /// Flips the gun's sprite beyond a certain point to ensure the gun is not completely inverted
    /// </summary>
    /// <param name="gunAngle"></param>
    public void FlipGunSprite(float gunAngle)
    {
        if (bulletProjectileAngle > 90 || (bulletProjectileAngle > -180 && bulletProjectileAngle < -90))
            gameObject.GetComponent<SpriteRenderer>().flipY = true;
        else
            gameObject.GetComponent<SpriteRenderer>().flipY = false;
    }
}
