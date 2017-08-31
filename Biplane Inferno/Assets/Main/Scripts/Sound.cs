using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour 
{
	public AudioSource audioS;
	public AudioClip shotgunSound;
	public AudioClip gunSound;
	public AudioClip missileSound;
	public AudioClip explosionSound;
	public AudioClip enemyDeathSound;
	public AudioClip pickupSound;

	void Start () 
	{
		audioS = GetComponent<AudioSource>();
	}
	
	// Methods for using the various sounds

	public void GunSound()
	{
		audioS.PlayOneShot(gunSound);
	}

	public void MissileSound()
	{
		audioS.PlayOneShot(missileSound);
	}

	public void ShotgunSound()
	{
		audioS.PlayOneShot(shotgunSound);
	}

	public void ExplosionSound()
	{
		audioS.PlayOneShot(explosionSound);
	}

	public void EnemyDeathSound()
	{
		audioS.PlayOneShot(enemyDeathSound);
	}

	public void PickupSound()
	{
		audioS.PlayOneShot(pickupSound);
	}
}
