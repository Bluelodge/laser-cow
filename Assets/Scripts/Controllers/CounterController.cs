using System.Collections;
using UnityEngine;

public class CounterController : MonoBehaviour
{
    public static CounterController Instance;

    private int finalTime = 0;
    private bool running = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            DestroyImmediate(gameObject);
            return;
        }

        Instance = this;
    }

    public void StartTimer()
    {
        running = true;
        StartCoroutine(UpdateTimer());
    }

    IEnumerator UpdateTimer()
    {
        while (running == true)
        {
            finalTime++;
            yield return new WaitForSeconds(1);
        }
    }

    public int EndTimer()
    {
        running = false;

        return finalTime;
    }
}
