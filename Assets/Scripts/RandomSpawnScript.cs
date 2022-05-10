using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;




public class RandomSpawnScript : MonoBehaviour
{

    [SerializeField] private int count;
    [SerializeField] private float delayTime = 2f;
    [SerializeField] private Transform prefabToSpawn;
    [SerializeField] private Dictionary<int, ArrayList> locationsWithElement = new Dictionary<int, ArrayList>();
    [SerializeField] private GameObject[] spawnLocations;
    [SerializeField] private List<Transform> oldSpawnLocations;
    

    void Awake()
    {
        spawnLocations = GameObject.FindGameObjectsWithTag("Ground");
    }

    void Start()
    {
        SpawnPrototype();
        //StartCoroutine(PrefabDrop1());

    }
    // Vector3 position = new Vector3(
    //   Random.Range( LowestXValue, HighestXValue),
    //   0,      // or height of ground
    //   Random.Range( LowestZValue, HighestZValue)
    // );

    //  IEnumerator PrefabDrop1()
    //  {
    //   while (count <20)
    //   {
    //    xPos = Random.Range(-30, 30);
    //    zPos = Random.Range(-23, 29);
    //    Instantiate(prefabObject, new Vector3(xPos, 0, zPos), Quaternion.identity);
    //    yield return new WaitForSeconds(delayTime);
    //    count += 1;

    //   }
    //  }

    public void SpawnPrototype()
    {
        for (int loop = 0; loop < count; loop++)
        {

            int locationIndex = Random.Range(0, spawnLocations.Length);
            Debug.Log(locationIndex + "LocationIndex");
            if (!locationsWithElement.ContainsKey(locationIndex))
            {
                locationsWithElement.Add(locationIndex, new ArrayList());
            }
            if (locationsWithElement[locationIndex] == null)
            {
                locationsWithElement[locationIndex] = new ArrayList();
            }
            if (locationsWithElement[locationIndex].Count > 10)
            {
                Debug.Log("return");
                return;
            }
            GameObject selectedLocation = spawnLocations[locationIndex];
            Vector3 elementPosition = new Vector3(Random.Range(selectedLocation.transform.position.x, selectedLocation.transform.position.x + selectedLocation.transform.localScale.x), selectedLocation.transform.position.y, Random.Range(selectedLocation.transform.position.z, selectedLocation.transform.position.z + selectedLocation.transform.localScale.z));
            Debug.Log(elementPosition + "Element position");
            int numberOfTries = 10;
            for (int i = 0; i < numberOfTries; i++)
            {
                if (!oldSpawnLocations.Contains(selectedLocation.transform))
                {
                    Transform _sp = selectedLocation.transform;
                    oldSpawnLocations.Add(_sp);
                    Instantiate(prefabToSpawn, elementPosition, Quaternion.identity);
                    Debug.Log("GameObject Successfully Spawned");
                    break;
                }
                else
                {
                    Debug.Log("Spawning Position Already Used " + "Try #" + i);
                }
            }
            //Instantiate(prefabToSpawn, elementPosition, Quaternion.identity);
            locationsWithElement[locationIndex].Add(1);

        }
    }

    // IEnumerator PrefabDrop1()
    //  {
    //   while (count <10)
    //   {

    //        int spawn = Random.Range(0,spawnLocations.Length);
    //        for(int loop = 0; loop < count;loop++)
    //          {
    //              Vector3 spawnPoint = transform.position + Random.insideUnitSphere*spawnRadius;
    //              if(!Physics.CheckSphere(spawnPoint,spawnCollisionCheckRadius))
    //              {
    //                  Instantiate(prefabToSpawn, spawnLocations[spawn].transform.position, Quaternion.identity);
    //              }

    //          }

    //    yield return new WaitForSeconds(delayTime);
    //    count += 1;

    //   }
    //  }
}






