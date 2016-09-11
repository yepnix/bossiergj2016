﻿using UnityEngine;
using System.Collections;

public class Enemy2Spawner : MonoBehaviour {
	
	public float spawnInterval = 1;
    public float spawnSpacing = 2f;
    public int maxSpawn = 10;
    public GameObject objectPrefab;

	void OnEnable(){
       StartCoroutine("SpawnCoroutine");
	}

    void OnDisable(){
        StopCoroutine("SpawnCoroutine");
    }
    
    IEnumerator SpawnCoroutine()
    {
        while (enabled){
            int activeCount = Enemy2ObjectPooling.objectPool.FindAll(EnemyObjectPooling.IsActiveObject).Count;
            if (activeCount < maxSpawn){
                GameObject enemy = Enemy2ObjectPooling.Spawn(objectPrefab); //creates and pools the gameObject.
                enemy.transform.position = gameObject.transform.position + (Vector3)Random.insideUnitCircle * spawnSpacing; //sets the spawn position to randomize inside the unit circle
            }                
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void Reset () {
        foreach (GameObject g in Enemy2ObjectPooling.objectPool) {
            g.SetActive(false);
        }
        gameObject.SetActive(true);
    }
}