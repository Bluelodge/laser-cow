using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Lasergun : MonoBehaviour
{
    [Header("Gun")]
    public Camera fpsCam;
    public LineRenderer laserShoot;
    public GameObject startLaserPoint;

    [Header("Targets")]
    public Target cowTarget;
    public Target ovniTarget;

    [Header("Effects")]
    public ParticleSystem vfxChargingParticle;
    public ParticleSystem vfxLaserMarkParticle;

    float moveSensi = 5.8f;
    float maxUpDown = 15f;
    float minUpDown = -10f;
    float maxRightLeft = 30f;
    float minRightLeft = -30f;

    Quaternion fpsCamRotation;
    Vector2 screenCenterPoint;
    Vector3 initialParticleSize = new Vector3(1, 1, 1);
    Vector3 shootingParticleSize = new Vector3(2.5f, 2.5f, 2.5f);

    void Start()
    {
        // Get initial rotation
        fpsCamRotation = fpsCam.transform.localRotation;
    }

    void Update()
    {
        // Move on running game
        if (GameController.pausedGame == false && GameController.finishedGame == false)
        {
            // Get movement
            fpsCamRotation.x += Input.GetAxisRaw("Mouse X") * moveSensi * Time.deltaTime;
            fpsCamRotation.y += Input.GetAxisRaw("Mouse Y") * moveSensi * Time.deltaTime;

            // Limit rotation
            fpsCamRotation.y = Mathf.Clamp(fpsCamRotation.y, minUpDown, maxUpDown);
            fpsCamRotation.x = Mathf.Clamp(fpsCamRotation.x, minRightLeft, maxRightLeft);

            // Rotate
            fpsCam.transform.localRotation = Quaternion.Euler(-fpsCamRotation.y, fpsCamRotation.x, 0);

            // Get center of screen
            screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);

            // Shoot on left click and unpaused game
            if (Input.GetButtonDown("Fire1") && GameController.pausedGame == false)
            {
                // Shoot charging effect
                vfxChargingParticle.transform.localScale = shootingParticleSize;

                ShootLaser();
            }
            else
            {
                // Deactivate laser
                laserShoot.enabled = false;

                // Hide shoot effect
                vfxChargingParticle.transform.localScale = initialParticleSize;
            }
        }
    }

    // Shoot laser
    void ShootLaser()
    {
        // Hit goal
        RaycastHit hit;

        // Ray with camera position and facing direction
        Ray ray = fpsCam.ScreenPointToRay(screenCenterPoint);

        // Ray, put info inside raycast
        if (Physics.Raycast(ray, out hit) && GameController.pausedGame == false)
        {
            // Activate laser
            laserShoot.enabled = true;

            // Pass starting and target positions to laser
            laserShoot.SetPosition(0, startLaserPoint.transform.position);
            laserShoot.SetPosition(1, hit.point);

            // Points and Damage
            if (hit.transform.tag == "Cow")
            {
                cowTarget.HitCow(hit.collider.gameObject);

                GameController.cows++;
                ScoreController.Instance.AddScore();
            }
            else if (hit.transform.tag == "Ovni")
            {
                GameController.ovnis++;
                ScoreController.Instance.SubtractScore();
                ovniTarget.HitOvni(hit.collider.gameObject);
            }
            else
            {
                // Show effect on unknow target
                StartCoroutine(ShootUnknown(hit));

                Debug.Log("Hitted something else");
            }
        }
    }

    IEnumerator ShootUnknown(RaycastHit hitted)
    {
        var laserMark = Instantiate(vfxLaserMarkParticle, hitted.point, hitted.transform.rotation);
        yield return new WaitForSeconds(1);
        Destroy(laserMark);
    }
}
