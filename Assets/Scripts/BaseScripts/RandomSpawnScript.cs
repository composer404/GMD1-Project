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

    private Dictionary<int, ArrayList> locationsWithElement = new Dictionary<int, ArrayList>();
    private GameObject[] spawnLocations;
    private List<Vector3> oldSpawnPositions;

    void Awake()
    {
        spawnLocations = GameObject.FindGameObjectsWithTag("Ground");
        oldSpawnPositions = new List<Vector3>();
    }

    void Start()
    {
        SpawnPrototype();
    }
   

    public void SpawnPrototype()
    {
        for (int i = 0; i < count; i++)
        {

            int locationIndex = Random.Range(0, spawnLocations.Length);
            if (!locationsWithElement.ContainsKey(locationIndex)) {
                locationsWithElement.Add(locationIndex, new ArrayList());
            }
            if (locationsWithElement[locationIndex] == null) {
                locationsWithElement[locationIndex] = new ArrayList();
            }
            if (locationsWithElement[locationIndex].Count > 3) {
                return;
            }

            GameObject selectedLocation = spawnLocations[locationIndex];

            if (selectedLocation.transform.childCount > 5) {
                return;
            }

            List<Vector3> locationCorners = GetCornerPoints(selectedLocation);
            Vector3 generatedPosition = GenerateElementRandomPostion(locationCorners);

            Vector3 tempPosition = selectedLocation.transform.position;
            tempPosition.y += additionalHeight;

            Vector3 elementPosition = new Vector3(generatedPosition.x, tempPosition.y, generatedPosition.z);
            Transform spawnedTransform = Instantiate(prefabToSpawn, elementPosition, prefabToSpawn.rotation);

            locationsWithElement[locationIndex].Add(1);

        }
    }

    public Vector3 GenerateElementRandomPostion(List<Vector3> corners) {
        int randomCornerIdx = Random.Range(0, 2) == 0 ? 0 : 2; 
        List<Vector3> edges = GetEdgePoints(randomCornerIdx, corners);

        float u = Random.Range(0.0f, 1.0f); 
        float v = Random.Range(0.0f, 1.0f);

        if (v + u > 1) {
            v = 1 - v;
            u = 1 - u;
        }

        Vector3 spwanPoint = corners[randomCornerIdx] + u * edges[0] + v * edges[1];
        return spwanPoint;

    }

    private bool V3Equal(Vector3 a, Vector3 b) {
        return Vector3.SqrMagnitude(a - b) < 0.0001;
    }

    private List<Vector3> GetCornerPoints(GameObject location)
    {
        List<Vector3> vertices = new List<Vector3>(location.GetComponent<MeshFilter>().sharedMesh.vertices);
        List<Vector3> corners = new List<Vector3>();

        corners.Add(location.transform.TransformPoint(vertices[0]));
        corners.Add(location.transform.TransformPoint(vertices[10]));
        corners.Add(location.transform.TransformPoint(vertices[110]));
        corners.Add(location.transform.TransformPoint(vertices[120]));

        return corners;
    }

    private List<Vector3> GetEdgePoints(int vector, List<Vector3> corners) {
        List<Vector3> edges = new List<Vector3>();
        edges.Add(corners[3] - corners[vector]);
        edges.Add(corners[1] - corners[vector]);

        return edges;
    }
}






