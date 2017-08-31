using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum WeaponType { SINGLE, BROAD, MISSILE };

public class Plane_Details : MonoBehaviour {
    Health myHealth;
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
    GameObject Projectile, MissileProjectile;
    Transform ProjectileOrigin;

    [SerializeField]
    Sprite Single, Broad, Missile;

    [Space]
    [SerializeField]
    Transform AmmoIcon, AmmoNumber, HealthBar;

    Material HealthBarMat;
    Text ammoText;
    Image ammoIconRenderer;

	Sound sounds;

    void Start()
    {
        ProjectileOrigin = transform.Find("Projectile_Origin");
        HealthBarMat = HealthBar.GetComponent<Image>().material;
        ammoText = AmmoNumber.GetComponent<Text>();
        ammoIconRenderer = AmmoIcon.GetComponent<Image>();
        ammoIconRenderer.sprite = Single;
        ammoText.text = myAmmo.ToString();
        myHealth = GetComponent<Health>();
        HealthBarMat.SetFloat("_Range", 1);
		sounds = GetComponent<Sound>();
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
                        ammoText.text = PowerUpAmmo.ToString();
                    } else
                    {
                        myCurrentWeapon = WeaponType.SINGLE;
                        ammoIconRenderer.sprite = Single;
                        ammoText.text = myAmmo.ToString();
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
                        ammoText.text = PowerUpAmmo.ToString();
                    }
                    else
                    {
                        myCurrentWeapon = WeaponType.SINGLE;
                        ammoIconRenderer.sprite = Single;
                        ammoText.text = myAmmo.ToString();
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
                        ammoText.text = myAmmo.ToString();
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
		sounds.MissileSound();
        CanShoot = false;
        PowerUpAmmo--;
        Rigidbody2D rgd2d = Instantiate(MissileProjectile, ProjectileOrigin.position, Quaternion.identity).GetComponent<Rigidbody2D>();
        rgd2d.gravityScale = 0;
        rgd2d.velocity = transform.up * 25;
        yield return new WaitForSeconds(MissileCD);
        CanShoot = true;
    }

    IEnumerator ShootBroad()
    {
		sounds.ShotgunSound();
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
		sounds.GunSound();
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
                ammoText.text = myAmmo.ToString();
                break;
            case (LootType.HEALTH):

                //Heals ship to max health if heal value does not exceed max.
                myHealth.health = (myHealth.health + ctx.Heal > myHealth.maxHealth ? myHealth.maxHealth : myHealth.health + ctx.Heal);
                HealthBarMat.SetFloat("_Range", myHealth.getHealthPercent());
                break;
            case (LootType.WEAPON):

                //Determining Pickup Weapon Type
                myCurrentWeapon = ctx.MyWeaponType;
                //Based on which type of weapon powerup it is, assign ammo value
                PowerUpAmmo = (myCurrentWeapon == WeaponType.BROAD ? 10 : 5);

                if (myCurrentWeapon == WeaponType.BROAD)
                {
                    ammoIconRenderer.sprite = Broad;
                }
                else
                {
                    ammoIconRenderer.sprite = Missile;
                }
                ammoText.text = PowerUpAmmo.ToString();
                break;
        }

        //Destroying pickup, removing from the game world.
        Destroy(ctx.gameObject);
		sounds.PickupSound();
    }

    public void SetHealthBar(float percent)
    {
        HealthBarMat.SetFloat("_Range", percent);
    }
}
