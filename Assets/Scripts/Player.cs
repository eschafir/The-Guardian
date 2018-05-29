using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    //public properties
    #region
    public float totalHealth = 300f;
    public float speed = 0.5f;
    public float padding = 0.5f;
    public GameObject smokePrefab;
    public Projectile projectile;
    public AudioClip fireSound;
    public AudioClip hitSound;
    public AudioClip dieSound;
    #endregion

    //private properties
    #region
    float xmin;
    float xmax;
    float ymin;
    float ymax;
    float currentHealth;
    Slider healthBar;
    #endregion

    private void Start() {
        healthBar = GameObject.FindObjectOfType<Slider>();
        currentHealth = totalHealth;
        healthBar.maxValue = totalHealth;
        healthBar.value = currentHealth;

        DetectPlayArea();
    }

    void Update() {
        DetectPressedKeys();
        float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
        float newY = Mathf.Clamp(transform.position.y, ymin, ymax);
        transform.position = new Vector3(newX, newY, transform.position.z);
        healthBar.value = currentHealth;

        Debug.Log(dieSound.length);
    }

    private void DetectPlayArea() {
        float distance = transform.position.z - Camera.main.transform.position.z;

        // Detecting the borderds of the Camera view
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        Vector3 topEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.40f, distance));
        Vector3 bottomEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));

        xmin = leftEdge.x + padding;
        xmax = rightEdge.x - padding;
        ymin = bottomEdge.y + padding;
        ymax = topEdge.y;

    }

    private void DetectPressedKeys() {
        if (Input.GetKey(KeyCode.LeftArrow)) {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.UpArrow)) {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            InvokeRepeating("Shoot", 0.0000001f, 0.2f);
        }
        if (Input.GetKeyUp(KeyCode.Space)) {
            CancelInvoke("Shoot");
        }
    }

    private void Shoot() {
        AudioSource.PlayClipAtPoint(fireSound, transform.position, 0.3f);
        GameObject laser = Instantiate(projectile.gameObject, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = Vector3.up * projectile.speed;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Projectile missile = other.gameObject.GetComponent<Projectile>();
        if (missile) {
            currentHealth -= missile.GetDamage();
            missile.Hit();
            if (currentHealth <= (totalHealth *0.5f)) {
                GenerateSmoke();
            }
            if (currentHealth <= 0) {
                StartCoroutine(Die());
            } else {
                AudioSource.PlayClipAtPoint(hitSound, transform.position);
            }
        }
    }

    private IEnumerator Die() {
        LevelManager levelManager = GameObject.FindObjectOfType<LevelManager>();
        healthBar.value = 0;
        //Destroy gameObject or dieAnimation
        GetComponent<SpriteRenderer>().sprite = null;
        Destroy(GetComponentInChildren<Smoke>().gameObject);
        Destroy(GetComponent<PolygonCollider2D>());
        AudioSource.PlayClipAtPoint(dieSound, transform.position);
        yield return new WaitForSeconds(dieSound.length);
        levelManager.Lose();
    }

    private void GenerateSmoke() {
        GameObject smoke = Instantiate(smokePrefab, transform.position, Quaternion.identity) as GameObject;
        smoke.transform.parent = gameObject.transform;
        smoke.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.5f, gameObject.transform.position.z);
    }
}
