using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private void Update()
    {
        // Cursor Lock Toggle
        HandleCursorToggle();

        // Camera Look
        HandleCameraLook();

        // Slingshot responds to player input
        HandleSlingshotTilt();
    }

    public Transform CameraPivot;
    public float sensitivity = 0.1f;
    float rotY, rotX = 0;
    void HandleCameraLook()
    {
        if (IsCursorLocked() == false) return;

        float inputY = Input.GetAxis("Mouse Y");
        float inputX = Input.GetAxis("Mouse X");

        rotY -= sensitivity * inputY;
        rotX += sensitivity * inputX;

        rotY = Mathf.Clamp(rotY, -25, 25);
        rotX = Mathf.Clamp(rotX, -15, 15);

        CameraPivot.rotation = Quaternion.Euler(rotY, rotX, CameraPivot.eulerAngles.z);
    }

    bool IsCursorLocked() => Cursor.lockState == CursorLockMode.Locked;

    void HandleCursorToggle()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Cursor.lockState = IsCursorLocked() ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }

    public Transform Slingshot;
    float slingshotRotZ = 0;
    void HandleSlingshotTilt()
    {
        float inputX = Input.GetAxis("Mouse X");
        slingshotRotZ = Mathf.Lerp(slingshotRotZ, inputX * 5.5f, 0.01f);
        Slingshot.rotation = Quaternion.Euler(Slingshot.eulerAngles.x, Slingshot.eulerAngles.y, slingshotRotZ);
    }

    GameObject pelletInstance;
    void HandleSlingshotPull()
    {
        if (pelletInstance == null) return;
    }

    IEnumerator SpawnPelletCoroutine()
    {
        // We need to wait for the pellet to leave the slingshot area before spawning a new one
        yield return new WaitForSeconds(0.1f);

        // Spawn and Assign Pellet GameObject
        SpawnPellet();
    }

    void SpawnPellet()
    {

    }

    void LaunchPellet()
    {
        // pelletInstance = 
    }
}
