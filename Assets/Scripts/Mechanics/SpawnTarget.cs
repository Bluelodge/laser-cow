using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class SpawnTarget : MonoBehaviour
{
    [Header("Spawns")]
    public List<GameObject> spawnPoints;

    [Header("Targets prefabs")]
    public GameObject cowPrefab;
    public GameObject ovniPrefab;
    
    private void Start()
    {
        // Spawn after countdown
        StartCoroutine(SpawnTargets(3));
    }

    // Spawn unitl player wins game
    IEnumerator SpawnTargets(int time = 0)
    {
        yield return new WaitForSeconds(time);

        while (CanvasManager.winner == false)
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
