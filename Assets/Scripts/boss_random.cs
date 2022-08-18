using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_random : MonoBehaviour
{

    public GameObject[] prefabs;
    private CapsuleCollider2D area;

    public int count = 1;

    private List<GameObject> gameObjects = new List<GameObject>();

    private void Start()
    {
        area = GetComponent<CapsuleCollider2D>();

        Spawn();

        area.enabled = false;
    }

  





    private void Spawn()
    {
        int selection = Random.Range(0, prefabs.Length);
        GameObject selectedPrefab = prefabs[selection];
        Vector3 spawnPos = transform.position;
        GameObject instance = Instantiate(selectedPrefab, spawnPos, Quaternion.identity);
        gameObjects.Add(instance);
    }
}
