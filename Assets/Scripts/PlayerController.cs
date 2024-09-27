using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    public GameObject ProjectilePrefab; // Reference to the projectile prefab
    GameObject ProjectileInstance;

    public Transform ProjectileSpawnPosition;
    public GameObject Slingshot;  // Reference to the slingshot object
    public Volume PostProcessingVolume; // Reference to the Post Processing Volume

    private Vector3 initialMousePosition; // Store the initial mouse position
    private Vignette vignette; // Store the Vignette component

    [Range(0.01f, 1f)]
    public float launchThreshold = 0.3f;

    void Start()
    {
        // Attempt to get the Vignette component once on start
        if (!PostProcessingVolume.profile.TryGet<Vignette>(out vignette))
        {
            Debug.LogWarning("Vignette not found in Post Processing Volume!");
        }

        ProjectileInstance = Instantiate(ProjectilePrefab, ProjectileSpawnPosition.position, Quaternion.identity);
    }

    void Update()
    {
        TrackMousePosition();
    }

    public TMP_Text chargeText;
    public TMP_Text canLaunchText;

    float charge;
    void HandleUI(float chargeValue)
    {
        charge = Mathf.Lerp(charge, chargeValue, 0.1f);
        chargeText.text = charge.ToString("0.00");
        if(charge > launchThreshold)
        {
            canLaunchText.text = "Ready";
            canLaunchText.color = Color.green;
        }
        else
        {
            canLaunchText.text = "Not Ready";
            canLaunchText.color = Color.red;
        }
    }

    IEnumerator SpawnProjectile()
    {
        yield return new WaitForSeconds(1f);

        // Spawn a new Projectile instance
        ProjectileInstance = Instantiate(ProjectilePrefab, ProjectileSpawnPosition.position, Quaternion.identity);
    }

    Vector3 mouseDelta;
    void TrackMousePosition()
    {
        // Initalize
        if (Input.GetMouseButtonDown(0) && ProjectileInstance != null)
        {
            initialMousePosition = Input.mousePosition; // Capture the initial position
        }

        //if(ProjectileInstance != null)
        //{
        //    mouseDelta = Input.mousePosition - initialMousePosition;
        //}
        mouseDelta = Input.mousePosition - initialMousePosition;

        float deltaY = -mouseDelta.y / 1000f; // Sensitivity adjustment

        if (Input.GetMouseButton(0) && ProjectileInstance != null)
        {
            // Adjust intensity based on deltaY
            float newIntensity = Mathf.Clamp(vignette.intensity.value + (0.4f * deltaY), 0.2f, 0.4f);
            vignette.intensity.value = newIntensity; // Update vignette intensity directly

            // Limit the change in field of view between 60f and 65f
            float targetFOV = Mathf.Clamp(Camera.main.fieldOfView + (65f * deltaY), 60f, 65f);
            HandleIntensityEffect(vignette, newIntensity, targetFOV);

            HandleUI(deltaY);
        }
        else
        {
            // Reset to default values when mouse is released
            HandleIntensityEffect(vignette, 0.2f, 60f); // Default values
            HandleUI(0);
        }

        // Launch Projectile
        if (deltaY > launchThreshold && Input.GetMouseButtonUp(0) && ProjectileInstance != null)
        {
            Rigidbody rb = ProjectileInstance.GetComponent<Rigidbody>();
            rb.AddForce(Camera.main.transform.forward * 10, ForceMode.Impulse);
            rb.AddForce(Camera.main.transform.up * 5, ForceMode.Impulse);
            rb.AddForce(10 * (-mouseDelta.x / 1000) * Camera.main.transform.right, ForceMode.Impulse);

            Destroy(ProjectileInstance, 20f);
            ProjectileInstance = null;
            StartCoroutine(SpawnProjectile());

            initialMousePosition = Vector3.zero;
        }
    }

    /// <summary>
    /// Controls the intensity of the vignette and camera field of view.
    /// </summary>
    void HandleIntensityEffect(Vignette vignette, float vignetteIntensity, float fieldOfView)
    {
        vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, vignetteIntensity, 3.5f * Time.deltaTime);
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, fieldOfView, 5f * Time.deltaTime);
    }
}
