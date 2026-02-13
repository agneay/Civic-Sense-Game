using UnityEngine;
using System.Collections;

public class VehicleSpawner : MonoBehaviour
{
    [Header("Vehicle Prefabs")]
    public GameObject[] vehiclePrefabs;

    [Header("Spawn Points (Lanes)")]
    public Transform[] spawnPoints;

    [Header("Speed Per Lane (Same Index)")]
    public float[] spawnSpeeds;

    [Header("Lifetime Per Lane (Same Index)")]
    public float[] vehicleLifeTimes;

    [Header("Timing")]
    public float spawnDelay = 3f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnVehicles), 0f, spawnDelay);
    }

    void SpawnVehicles()
    {
        // Safety checks
        if (vehiclePrefabs.Length == 0 ||
            spawnPoints.Length == 0 ||
            spawnPoints.Length != spawnSpeeds.Length ||
            spawnPoints.Length != vehicleLifeTimes.Length)
        {
            Debug.LogWarning("Array size mismatch! Check spawn points, speeds, and lifetimes.");
            return;
        }

        // Pick ONE random vehicle prefab
        int randomVehicleIndex = Random.Range(0, vehiclePrefabs.Length);
        GameObject selectedVehicle = vehiclePrefabs[randomVehicleIndex];

        // Spawn in all lanes
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            GameObject vehicle = Instantiate(
                selectedVehicle,
                spawnPoints[i].position,
                spawnPoints[i].rotation
            );

            StartCoroutine(
                MoveAndDestroy(
                    vehicle,
                    spawnSpeeds[i],
                    vehicleLifeTimes[i]
                )
            );
        }
    }

    IEnumerator MoveAndDestroy(GameObject vehicle, float speed, float lifeTime)
    {
        float timer = 0f;

        while (timer < lifeTime)
        {
            if (vehicle == null) yield break;

            vehicle.transform.Translate(
                Vector3.forward * speed * Time.deltaTime
            );

            timer += Time.deltaTime;
            yield return null;
        }

        Destroy(vehicle);
    }
}
