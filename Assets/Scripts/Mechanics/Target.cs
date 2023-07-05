using System.Collections;
using System.Collections.Generic;
using UnityEditor.Android;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    [Header("Materials")]
    public Material greenGlow;
    public Material redGlow;
    
    float units = -5f;
    int cowPoints = 10;
    int ovniPoints = 15;

    public static int cows = 0;
    public static int ovnis = 0;

    private void Update()
    {
        if (CountdownTimer.startGame == true)
        {
            // Move at constant velocity to left
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(units, 0, 0);
        }
    }

    // Manage points
    public void playerPoints(string action)
    {
        if (action == "plus")
        {
            CanvasManager.points += cowPoints;
            cows++;
        }

        if (action == "subtract")
        {
            CanvasManager.points -= ovniPoints;
            ovnis++;
        }
    }

    // Hit targets
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
