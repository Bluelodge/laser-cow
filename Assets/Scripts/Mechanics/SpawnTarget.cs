using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class SpawnTarget : MonoBehaviour
{
    public static SpawnTarget Instance;

    [Header("Spawns")]
    public List<GameObject> spawnPoints;

    [Header("Targets prefabs")]
    public GameObject cowPrefab;
    public GameObject ovniPrefab;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            DestroyImmediate(gameObject);
            return;
        }

        Instance = this;
    }

    // Spawn until player wins game
    public IEnumerator SpawnTargets()
    {
        while (GameController.finishedGame == false)
        {
            yield return new WaitForSeconds(1);
            Instantiate(cowPrefab, spawnPoints[0].transform.position, spawnPoints[0].transform.rotation);
            Instantiate(ovniPrefab, spawnPoints[1].transform.position, spawnPoints[1].transform.rotation);
            Instantiate(ovniPrefab, spawnPoints[2].transform.position, spawnPoints[2].transform.rotation);

            yield return new WaitForSeconds(1);
            Instantiate(ovniPrefab, spawnPoints[0].transform.position, spawnPoints[0].transform.rotation);
            Instantiate(cowPrefab, spawnPoints[1].transform.position, spawnPoints[1].transform.rotation);
            Instantiate(cowPrefab, spawnPoints[2].transform.position, spawnPoints[2].transform.rotation);
        }
    }
}
