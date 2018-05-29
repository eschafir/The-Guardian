using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyPrefab;
    public float width = 10f;
    public float height = 5f;
    public float speed = 5f;

    float xmin;
    float xmax;
    bool isMovingToRight = true;

    void Start() {
        DetectPlayArea();
        DeployEnemiesUntilFull();
    }

    void Update() {
        MoveFormation();
        if (AllMembersDead()) {
            DeployEnemiesUntilFull();
        }
    }

    private void DetectPlayArea() {
        float distance = transform.position.z - Camera.main.transform.position.z;

        // Detecting the borderds of the Camera view
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));

        xmin = leftEdge.x;
        xmax = rightEdge.x;
    }

    private void MoveFormation() {

        float rightBorder = transform.position.x + (0.5f * width);
        float leftBorder = transform.position.x - (0.5f * width);

        if (isMovingToRight) {
            transform.position += Vector3.right * speed * Time.deltaTime;
            if (rightBorder > xmax) {
                isMovingToRight = false;
            }
        } else {
            transform.position += Vector3.left * speed * Time.deltaTime;
            if (leftBorder < xmin) {
                isMovingToRight = true;
            }
        }

    }

    private void DeployEnemies() {
        foreach (Transform child in transform) {
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = child;
        }
    }

    private void DeployEnemiesUntilFull() {
        Transform freePosition = NextFreePosition();
        if (freePosition) {
            GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
            enemy.transform.SetParent(freePosition);
            Invoke("DeployEnemiesUntilFull", 0.5f);
        }

    }

    Transform NextFreePosition() {
        foreach (Transform child in transform) {
            if (child.childCount == 0) {
                return child;
            }
        }
        return null;
    }

    bool AllMembersDead() {
        foreach (Transform child in transform) {
            if (child.childCount > 0) {
                return false;
            }
        }
        return true;
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }
}
