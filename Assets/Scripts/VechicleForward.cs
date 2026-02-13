using UnityEngine;

public class VehicleForward : MonoBehaviour
{
    [Header("Vehicle Prefabs")]
    public GameObject[] vehiclePrefabs;

    [Header("Spawn Settings")]
    public Transform spawnPoint;
    public float spawnDelay = 3f;

    [Header("Movement Settings")]
    public float vehicleSpeed = 10f;
    public float vehicleLifeTime = 8f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnVehicle), 0f, spawnDelay);
    }

    void SpawnVehicle()
    {
        if (vehiclePrefabs.Length == 0) return;

        int index = Random.Range(0, vehiclePrefabs.Length);
        GameObject vehicle = Instantiate(
            vehiclePrefabs[index],
            spawnPoint.position,
            spawnPoint.rotation
        );

        StartCoroutine(MoveAndDestroy(vehicle));
    }

    System.Collections.IEnumerator MoveAndDestroy(GameObject vehicle)
    {
        float timer = 0f;

        while (timer < vehicleLifeTime)
        {
            if (vehicle == null) yield break;

            vehicle.transform.Translate(
                Vector3.forward * vehicleSpeed * Time.deltaTime
            );

            timer += Time.deltaTime;
            yield return null;
        }

        Destroy(vehicle);
    }
}
