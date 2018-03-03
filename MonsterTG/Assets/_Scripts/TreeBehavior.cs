using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBehavior : MonoBehaviour {

    public List<FruitBehavior> Fruits = new List<FruitBehavior>();
    public GameObject fruitPrefab;

    public List<Transform> spawnPoints = new List<Transform>();
    public int currentSpawn = 0;

    public float spawnTime = 5f;

    private void Start()
    {
        SpawnFruit();
        SpawnFruit();
        SpawnFruit();
    }

    public void SpawnFruit()
    {
        Vector3 pos = spawnPoints[currentSpawn].localPosition;
        currentSpawn++;
        currentSpawn %= spawnPoints.Count;

        GameObject fruit = Instantiate(fruitPrefab, transform);
        fruit.transform.localPosition = pos;
        FruitBehavior behavior = fruit.GetComponent<FruitBehavior>();
        behavior.tree = this;

        Fruits.Add(behavior);
    }
}
