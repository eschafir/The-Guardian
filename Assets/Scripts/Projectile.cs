using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float speed;
    public float damage;

    private void OnBecameInvisible() {
        Destroy(gameObject);
    }

    public float GetDamage() {
        return damage;
    }

    public void Hit() {
        Destroy(gameObject);
    }
}
