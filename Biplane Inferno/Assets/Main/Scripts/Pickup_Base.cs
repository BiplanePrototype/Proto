using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LootType { HEALTH, WEAPON, AMMO }
public class Pickup_Base : MonoBehaviour {
    [SerializeField]
    LootType myType = LootType.WEAPON;

    [SerializeField]
    WeaponType myWeaponType = WeaponType.BROAD;

    [SerializeField]
    int ammo = 0, heal = 0;

	// Use this for initialization
	void Start () {
        int typeVal = Random.Range(0, 100);
        myType = ( typeVal >= 90 ? LootType.HEALTH : typeVal >= 30 ? LootType.AMMO : LootType.WEAPON );
        if (myType == LootType.WEAPON)
        {
            myWeaponType = (Random.Range(0, 100) <= 75 ? WeaponType.BROAD : WeaponType.MISSILE);
        } else if (myType == LootType.AMMO)
        {
            ammo = Random.Range(0, 30);
        } else
        {
            heal = Random.Range(0, 10);
        }
	}

    public LootType MyType
    {
        get
        {
            return myType;
        }
    }

    public WeaponType MyWeaponType
    {
        get
        {
            return myWeaponType;
        }
    }

    public int Ammo
    {
        get
        {
            return ammo;
        }
    }

    public int Heal
    {
        get
        {
            return heal;
        }
    }
}
