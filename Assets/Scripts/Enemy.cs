using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	[Header("Enemy Stats")]
	[SerializeField] float health = 100;
	[SerializeField] int scoreValue = 150;

	[Header("Shooting")]
	float shotCounter;
	[SerializeField] float minTimeBetweenShots = 0.2f;
	[SerializeField] float maxTimeBetweenShots = 3f;
	[SerializeField] float projectileSpeed = 10f;
	[SerializeField] float durationOfExplosion = 1f;
	[SerializeField] GameObject projectilePrefab;

	[Header("FX")]
	[SerializeField] GameObject explosionVFX;
	[SerializeField] AudioClip laserSFX;
	[SerializeField] [Range(0, 2)] float laserSFXVolume = 0.5f;
	[SerializeField] AudioClip deathSFX;
	[SerializeField] [Range(0, 2)] float deathSFXVolume = 1f;

	// Start is called before the first frame update
	void Start() {
		shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update() {
		CountDownAndShoot();
    }

	private void CountDownAndShoot() {
		shotCounter -= Time.deltaTime;
		if (shotCounter <= 0) {
			Fire();
			shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
		}
	}

	private void Fire() {
		GameObject laser = Instantiate(projectilePrefab, transform.position, Quaternion.identity) as GameObject;
		laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -projectileSpeed);
		AudioSource.PlayClipAtPoint(laserSFX, Camera.main.transform.position, laserSFXVolume);
	}

	private void OnTriggerEnter2D(Collider2D other) {
		DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
		if (damageDealer) {
			ProcessHit(damageDealer);
		}
	}

	private void ProcessHit(DamageDealer damageDealer) {
		health -= damageDealer.GetDamage();
		damageDealer.Hit();
		if (health <= 0) {
			DestroyEnemy();
		}
	}

	private void DestroyEnemy() {
		FindObjectOfType<GameSession>().AddToScore(scoreValue);
		AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
		GameObject explosion = Instantiate(explosionVFX, transform.position, transform.rotation);
		Destroy(explosion, durationOfExplosion);
		Destroy(gameObject);
	}
}
