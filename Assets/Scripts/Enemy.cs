using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float health;
    public float fireRate;
    public float damage;
    public int scoreValue;
    public GameObject projectile;
    public AudioClip fireSound;
    public AudioClip dieSound;

    ScoreKeeper scoreKeeper;

    private void Start() {
        scoreKeeper = GameObject.FindObjectOfType<ScoreKeeper>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Projectile missile = other.gameObject.GetComponent<Projectile>();
        if (missile) {
            health -= missile.GetDamage();
            missile.Hit();
            if (health <= 0) {
                DestroyEnemy();
            }
        }
    }

    private void Update() {
        float probability = fireRate * Time.deltaTime;
        if (Random.value < probability) {
            Shoot();
        }
    }

    private void Shoot() {
        AudioSource.PlayClipAtPoint(fireSound, transform.position);
        GameObject laser = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = Vector3.down * projectile.GetComponent<Projectile>().speed;
    }

    void DestroyEnemy() {
        AudioSource.PlayClipAtPoint(dieSound, transform.position);
        scoreKeeper.Score(scoreValue);
        Destroy(gameObject);
    }
}
