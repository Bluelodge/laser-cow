using UnityEngine;

public class Target : MonoBehaviour
{
    [Header("Materials")]
    public Material greenGlow;
    public Material redGlow;
    
    float units = -5f;

    private void Update()
    {
        // Constant movement
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(units, 0, 0);
    }

    // Hit targets and show effect
    public void HitCow(GameObject targetHitted)
    {
        targetHitted.GetComponent<MeshRenderer>().material = greenGlow;

        print("Target Cow was hitted");
    }
    
    public void HitOvni(GameObject targetHitted)
    {
        Renderer targetMesh = targetHitted.GetComponent<MeshRenderer>();
        var currentMaterials = targetMesh.materials;
        currentMaterials[2] = redGlow;
        targetMesh.materials = currentMaterials;

        print("Target Ovni was hitted");
    }
}
