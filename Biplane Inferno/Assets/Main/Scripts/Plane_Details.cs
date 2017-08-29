using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType { SINGLE, BROAD, MISSILE };

public class Plane_Details : MonoBehaviour {
    int Health = 100, MaxHealth = 100;
    int Score = 0;
    float DistanceTraveled = 0;
    bool CanShoot = true;

    [SerializeField]
    int PowerUpAmmo = 0, myAmmo = 30;

    [SerializeField]
    float MissileCD = 3, NormalCD = .05f, BroadCD = 1f;

    [SerializeField]
    WeaponType myCurrentWeapon = WeaponType.SINGLE;

    [SerializeField]
    GameObject Projectile;
    Transform ProjectileOrigin;

    void Start()
    {
        ProjectileOrigin = transform.Find("Projectile_Origin");
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && CanShoot)
        {
            switch (myCurrentWeapon) {
                case (WeaponType.MISSILE):
                    if (PowerUpAmmo > 0)
                    {
                        StartCoroutine(ShootMissile());
                    } else
                    {
                        myCurrentWeapon = WeaponType.SINGLE;
                        if (myAmmo > 0)
                        {
                            StartCoroutine(ShootNormal());
                        }
                    }
                    break;
                case (WeaponType.BROAD):
                    if (PowerUpAmmo > 0)
                    {
                        StartCoroutine(ShootBroad());
                    }
                    else
                    {
                        myCurrentWeapon = WeaponType.SINGLE;
                        if (myAmmo > 0)
                        {
                            StartCoroutine(ShootNormal());
                        }
                    }
                    break;
                default:
                    if (myAmmo > 0)
                    {
                        StartCoroutine(ShootNormal());
                    } else
                    {
                        //Play a click sound, flash, whatever
                    }
                    break;
            }
        }
    }

    IEnumerator ShootMissile()
    {
        CanShoot = false;
        PowerUpAmmo--;
        yield return new WaitForSeconds(MissileCD);
        CanShoot = true;
    }

    IEnumerator ShootBroad()
    {
        CanShoot = false;
        PowerUpAmmo--;
        for (int i = 0; i < 7; i++)
        {
            GameObject temp = Instantiate(Projectile, ProjectileOrigin.position, Quaternion.Euler(new Vector3(0, 0, -15 + (5 * i))));
            temp.transform.position += temp.transform.up*.25f;
            temp.GetComponent<Rigidbody2D>().gravityScale = 0;
            temp.GetComponent<Rigidbody2D>().velocity = temp.transform.up * 10;
        }
        yield return new WaitForSeconds(BroadCD);
        CanShoot = true;
    }

    IEnumerator ShootNormal()
    {
        CanShoot = false;
        myAmmo--;
        Rigidbody2D rgd2d = Instantiate(Projectile, ProjectileOrigin.position, Quaternion.identity).GetComponent<Rigidbody2D>();
        rgd2d.gravityScale = 0;
        rgd2d.velocity = transform.up * 10;
        yield return new WaitForSeconds(NormalCD);
        CanShoot = true;
    }

    public void GetPickupData(Pickup_Base ctx)
    {
        //Changing behaviour based on which type of pickup it is.
        switch (ctx.MyType)
        {
            case (LootType.AMMO):
                
                //Adds ammo to primary gun
                myAmmo += ctx.Ammo;
                break;
            case (LootType.HEALTH):

                //Heals ship to max health if heal value does not exceed max.
                Health = (Health + ctx.Heal > MaxHealth ? MaxHealth : Health + ctx.Heal);
                break;
            case (LootType.WEAPON):

                //Determining Pickup Weapon Type
                myCurrentWeapon = ctx.MyWeaponType;
               
                //Based on which type of weapon powerup it is, assign ammo value
                PowerUpAmmo = (myCurrentWeapon == WeaponType.BROAD ? 10 : 5);
                break;
        }

        //Destroying pickup, removing from the game world.
        Destroy(ctx.gameObject);
    }
}
