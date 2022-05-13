using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;




public class RandomSpawnScript : MonoBehaviour
{

    [SerializeField] private int count;
    [SerializeField] private int additionalHeight = 0;
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
    }
   

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
                return;
            }
            GameObject selectedLocation = spawnLocations[locationIndex];

            Vector3 tempPosition = selectedLocation.transform.position;
            tempPosition.y += additionalHeight;

            Vector3 elementPosition = new Vector3(Random.Range(selectedLocation.transform.position.x, selectedLocation.transform.position.x - selectedLocation.transform.localScale.x), tempPosition.y, Random.Range(selectedLocation.transform.position.z, selectedLocation.transform.position.z - selectedLocation.transform.localScale.z));
            int numberOfTries = 10;
            for (int i = 0; i < numberOfTries; i++)
            {
                if (!oldSpawnLocations.Contains(selectedLocation.transform))
                {
                    Transform _sp = selectedLocation.transform;
                    oldSpawnLocations.Add(_sp);
                    Instantiate(prefabToSpawn, elementPosition, prefabToSpawn.rotation);
                    break;
                } 
            }
            locationsWithElement[locationIndex].Add(1);

        }
    }
}






