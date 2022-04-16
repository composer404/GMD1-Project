using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;



public class RandomSpawnScript : MonoBehaviour
{

// [SerializeField] private GameObject prefabObject;
//   private float xPos;
//   private float zPos;
 [SerializeField] private int count;
 [SerializeField] private float delayTime = 2f;
   public Transform prefabToSpawn;
     public int objectCount = 50;
    public float spawnRadius = 5;
    public float spawnCollisionCheckRadius;


 public GameObject[] spawnLocations;
 //private List<Transform> spawnSpotList;
 //[SerializeField] private NavMeshSurface[] surfaces;

// private void Awake()
// {
//     NavMeshDatas = new NavMeshData[surfaces.Length];
//     for(int i=0;i<surfaces.Length; i++)
//     {
//         NavMeshDatas[i] = new NavMeshData();
//         NavMesh.AddNavMeshData(NavMeshDatas[i]);
//     }
// BuildNavMesh(false);
// StartCoroutine(CheckPlayerMovment());

void Awake()
{
    spawnLocations = GameObject.FindGameObjectsWithTag("SpawnPoint");
 }

 void Start()
 {
  
 StartCoroutine(PrefabDrop1());

 }

 
//  IEnumerator PrefabDrop1()
//  {
//   while (count <2)
//   {
//    xPos = Random.Range(-30, 30);
//    zPos = Random.Range(-23, 29);
//    Instantiate(prefabObject, new Vector3(xPos, 0, zPos), Quaternion.identity);
//    yield return new WaitForSeconds(delayTime);
//    count += 1;

//   }
//  }

IEnumerator PrefabDrop1()
 {
  while (count <3)
  {
       int spawn = Random.Range(0,spawnLocations.Length);
      for(int loop = 0; loop < count;loop++)
        {
            Vector3 spawnPoint = transform.position + Random.insideUnitSphere*spawnRadius;
            if(!Physics.CheckSphere(spawnPoint,spawnCollisionCheckRadius))
            {
                 Instantiate(prefabToSpawn, spawnLocations[spawn].transform.position, Quaternion.identity);
            }
            
        }

   yield return new WaitForSeconds(delayTime);
   count += 1;

  }
 }
}



 


