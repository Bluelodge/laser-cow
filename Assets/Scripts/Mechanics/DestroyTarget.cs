using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTarget : MonoBehaviour
{
    // Destroy Target when reachs end
    void OnTriggerEnter(Collider target)
    {
        Destroy(target.gameObject);
        Debug.Log("Some target was destroy");
    }
}
