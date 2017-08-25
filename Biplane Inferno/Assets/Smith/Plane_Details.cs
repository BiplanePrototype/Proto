using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum WeaponType { SINGLE, BROAD, MISSILE };

public class Plane_Details : MonoBehaviour {
    int Health = 100, MaxHealth = 100;
    int Score = 0;
    float DistanceTraveled = 0;

    [SerializeField]
    int AmmoRemaining = 0;

    [SerializeField]
    bool CanShoot = true;

    [SerializeField]
    float MissileCD = 3, NormalCD = .5f, BroadCD = 1f;

    [SerializeField]
    WeaponType myCurrentWeapon = WeaponType.SINGLE;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && CanShoot)
        {
            switch (myCurrentWeapon) {
                case (WeaponType.MISSILE):
                    if (AmmoRemaining > 0)
                    {
                        StartCoroutine(ShootMissile());
                    } else
                    {
                        myCurrentWeapon = WeaponType.SINGLE;
                        StartCoroutine(ShootNormal());
                    }
                    break;
                case (WeaponType.BROAD):
                    if (AmmoRemaining > 0)
                    {
                        StartCoroutine(ShootBroad());
                    }
                    else
                    {
                        myCurrentWeapon = WeaponType.SINGLE;
                        StartCoroutine(ShootNormal());
                    }
                    break;
                default:
                    StartCoroutine(ShootNormal());
                    break;
            }
        }
    }

    IEnumerator ShootMissile()
    {
        CanShoot = false;
        AmmoRemaining--;
        yield return new WaitForSeconds(MissileCD);
        CanShoot = true;
    }

    IEnumerator ShootBroad()
    {
        CanShoot = false;
        AmmoRemaining--;
        yield return new WaitForSeconds(BroadCD);
        CanShoot = true;
    }

    IEnumerator ShootNormal()
    {
        CanShoot = false;
        yield return new WaitForSeconds(NormalCD);
        CanShoot = true;
    }
}
