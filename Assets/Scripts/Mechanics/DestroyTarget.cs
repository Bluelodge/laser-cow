using UnityEngine;

public class DestroyTarget : MonoBehaviour
{
    // Destroy Target when reachs end
    void OnTriggerEnter(Collider target)
    {
        Destroy(target.gameObject);
        Debug.Log($"Target {target.gameObject.tag} was destroy");
    }
}
