using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

//using b = Bounds;


public class RandomSpawnScript : MonoBehaviour
{
 //[SerializeField] private GameObject spawnPrefab;
 [SerializeField] private GameObject enemyObject;
 [SerializeField] private float xPos;
 [SerializeField] private float zPos;
 [SerializeField] private int enemyCount;

 [SerializeField] private GameObject poisonFlower;
 [SerializeField] private int flowerCount;

 [SerializeField] private GameObject collectableObject;
 [SerializeField] private int collectableCount;
 
 
// [SerializeField] private GameObject enemy1Prefab;

 // public List<GameObject> objectsToSpawn = new List<GameObject>();
 // public bool isRendomized;

 // [SerializeField] private Vector3 center;
 // [SerializeField] private Vector3 size;
 
 // Update is called once per frame
 // Quaternion.identity means no rotation

 void Start()
 {

  // B.randomSpawnposition= new Vector3(Random.Range(-10,10),0,Random.Range(-10,10));
  // Vector3 randomSpawnposition1 = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
  // Vector3 randomSpawnposition2 = new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));
  // // Instantiate(enemy1Prefab, randomSpawnposition1, Quaternion.identity);
  // Instantiate(poisonFlowerPrefab, transform.position, Quaternion.identity);
  // Instantiate(poisonFlowerPrefab, transform.position, Quaternion.identity);
  // Instantiate(poisonFlowerPrefab, transform.position, Quaternion.identity);
  // Instantiate(poisonFlowerPrefab, transform.position, Quaternion.identity);
  //Instantiate(enemy1Prefab, randomSpawnposition2, Quaternion.identity); 
  //Instantiate(enemy1Prefab, randomSpawnposition1, Quaternion.identity);
  
 // SpawnObject();
 StartCoroutine(EnemyDrop());
 StartCoroutine(FlowerDrop());
 StartCoroutine(CollectableDrop());
 }

 
 // Drops enemies after a certain amount of time randomly
 IEnumerator EnemyDrop()
 {
  while (enemyCount < 10)
  {
   xPos = Random.Range(-30, 30);
   zPos = Random.Range(-23, 29);
   Instantiate(enemyObject, new Vector3(xPos, 0, zPos), Quaternion.identity);
   yield return new WaitForSeconds(2f);
   enemyCount += 1;

  }
 }

 IEnumerator FlowerDrop()
 {
  while (flowerCount < 20)
  {
   xPos = Random.Range(-20, 20);
   zPos = Random.Range(-20, 20);
   Instantiate(poisonFlower, new Vector3(xPos, 0, zPos), Quaternion.identity);
   yield return new WaitForSeconds(0.1f);
   flowerCount += 1;

  }
 }

 IEnumerator CollectableDrop()
  {
   while (collectableCount < 5)
   {
    xPos = Random.Range(-20, 20);
    zPos = Random.Range(-20, 20);
    Instantiate(collectableObject, new Vector3(xPos, 0, zPos), Quaternion.identity);
    yield return new WaitForSeconds(0.0f);
    collectableCount += 1;

   }
  }
 

 // public void SpawnObject()
 // {
 //     // int index = isRendomized ? Random.Range(0, objectsToSpawn.Count) : 0;
 //     // if (objectsToSpawn.Count > 0)
 //     // {
 //     //     Instantiate(objectsToSpawn[index], transform.position, Quaternion.identity);
 //     //    
 //     // }
 //     Vector3 pos = center + new Vector3(Random.Range(size.x / 2, size.x / 2), Random.Range(size.y / 2, size.y / 2),
 //         Random.Range(size.z / 2, size.z / 2));
 //     Instantiate(spawnPrefab, pos, Quaternion.identity);
 //     
 //     Vector3 pos1 = center + new Vector3(Random.Range(size.x / 2, size.x / 2), Random.Range(size.y / 2, size.y / 2),
 //         Random.Range(size.z / 2, size.z / 2));
 //     Instantiate(spawnPrefab, pos1, Quaternion.identity);
 //     
 //     Vector3 pos2 = center + new Vector3(Random.Range(size.x / 2, size.x / 2), Random.Range(size.y / 2, size.y / 2),
 //         Random.Range(size.z / 2, size.z / 2));
 //     Instantiate(spawnPrefab, pos2, Quaternion.identity);
 // }
}

// void Update()
    // {
    //     // Vector3 randomSpawnposition = new Vector3(Random.Range(-10,10),5,Random.Range(-10,10));
    //     // Instantiate(enemy1Prefab, randomSpawnposition, Quaternion.identity);
    //     // if (Input.GetKeyDown(KeyCode.Space))
    //     // {
    //     //     //Vector3 randomSpawnposition = new Vector3(Random.Range(-10,10),5,Random.Range(-10,10));
    //     //     Instantiate(poisonFlowerPrefab, randomSpawnposition, Quaternion.identity);
    //     //    
    //     // }
    //
    //     // if (Input.GetKeyDown(KeyCode.UpArrow))
    //     // {
    //     //     Instantiate(enemy1Prefab, randomSpawnposition, Quaternion.identity);
    //     //    // Instantiate(enemy1Prefab, B.randomSpawnposition, Quaternion.identity);
    //     // }
    // }

//     private void FixedUpdate()
//     {
//         Vector3 randomSpawnposition = new Vector3(Random.Range(-10,10),5,Random.Range(-10,10));
//         Instantiate(enemy1Prefab, randomSpawnposition, Quaternion.identity);
//     }
// }
//Vector2 newSpawn = new Vector2(Random.Range(_BoundsMin.x, _BoundsMax.x), Random.Range(_BoundsMin.y, _BoundsMax.y));