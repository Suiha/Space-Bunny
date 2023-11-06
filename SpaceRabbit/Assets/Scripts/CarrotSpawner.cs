using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotSpawner : MonoBehaviour
{
    public Transform carrotPrefab;
    public Transform spawnPoint;

    private static CarrotSpawner instance;

    void Start()
    {
        instance = this;
    }

    // destroy carrot object, then wait for respawn
    public static void RespawnCarrot(CarrotBuff c)
    {
        Destroy(c.gameObject);
        instance.StartCoroutine(instance.RespawnCarrot());
    }
    
    // wait a few seconds before respawning
    public IEnumerator RespawnCarrot()
    {
        yield return new WaitForSeconds(4);
        Instantiate(carrotPrefab, spawnPoint.position, spawnPoint.rotation, spawnPoint);
    }
}
