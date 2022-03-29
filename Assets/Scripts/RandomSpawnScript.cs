using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;



public class RandomSpawnScript : MonoBehaviour
{
 [SerializeField] private GameObject prefabObject;
 [SerializeField] private float xPos;
 [SerializeField] private float zPos;
 [SerializeField] private int count;
 [SerializeField] private float delayTime = 2f;

 
 

 void Start()
 {
 StartCoroutine(PrefabDrop());
 }

 
 IEnumerator PrefabDrop()
 {
  while (count <10)
  {
   xPos = Random.Range(-30, 30);
   zPos = Random.Range(-23, 29);
   Instantiate(prefabObject, new Vector3(xPos, 0, zPos), Quaternion.identity);
   yield return new WaitForSeconds(delayTime);
   count += 1;

  }
 }
}

 


