using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float sensX;
    public float sensY;

    // Default sensitivity values to maintain proportions when adjusting
    private float defaultSensX;
    private float defaultSensY;

    public Transform orientation;

    float xRot = -15;
    float yRot = 180;
    public Texture2D cursorArrow;

    // Zoom variables
    private Camera playerCamera;
    public float zoomSpeed = 10f; // Now controls lerp speed for smooth transition
    public float minFOV = 60f; // Default FOV as minimum zoom
    public float maxFOV = 20f; // Maximum zoom level (lower FOV = more zoom)
    private float currentFOV;
    private float targetFOV; // Target FOV to smoothly transition to
    private bool isZoomed = false; // Track zoom state

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        Cursor.SetCursor(cursorArrow, Vector3.zero, CursorMode.ForceSoftware);

        // Store the default sensitivity values
        defaultSensX = sensX;
        defaultSensY = sensY;

        // Load saved sensitivity if available, otherwise use default
        if (PlayerPrefs.HasKey("Sensitivity"))
        {
            float savedSensitivity = PlayerPrefs.GetFloat("Sensitivity");
            AdjustSpeed(savedSensitivity);
        }

        // Get camera and initialize FOV
        playerCamera = GetComponent<Camera>();
        if (playerCamera != null)
        {
            minFOV = playerCamera.fieldOfView;
            currentFOV = minFOV;
            targetFOV = minFOV;
        }
        else
        {
            Debug.LogError("Camera component not found on the same GameObject as Movement script!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // mouse inputs
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRot += mouseX;
        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -90f, 90f);

        //rotation (cam and orientation)
        transform.rotation = Quaternion.Euler(xRot, yRot, 0);
        orientation.rotation = Quaternion.Euler(0, yRot, 0);

        // Handle zoom with scroll wheel
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0f)
        {
            // Toggle zoom state based on scroll direction
            if (scrollInput > 0 && !isZoomed)
            {
                // Zoom in
                targetFOV = maxFOV;
                isZoomed = true;
            }
            else if (scrollInput < 0 && isZoomed)
            {
                // Zoom out
                targetFOV = minFOV;
                isZoomed = false;
            }
        }

        // Smooth transition to target FOV
        if (currentFOV != targetFOV)
        {
            currentFOV = Mathf.Lerp(currentFOV, targetFOV, Time.deltaTime * zoomSpeed);

            // Snap to target if very close to avoid floating point issues
            if (Mathf.Abs(currentFOV - targetFOV) < 0.1f)
            {
                currentFOV = targetFOV;
            }

            // Apply the new FOV
            playerCamera.fieldOfView = currentFOV;

            // Adjust sensitivity based on zoom level
            UpdateSensitivity();
        }
    }

    private void UpdateSensitivity()
    {
        // Adjust sensitivity based on zoom level for much slower movement when zoomed
        float zoomRatio = (currentFOV - maxFOV) / (minFOV - maxFOV);
        // Reduced lower bound for slower camera movement when zoomed
        float sensitivityMultiplier = Mathf.Lerp(0.2f, 1f, Mathf.Pow(zoomRatio, 1.5f));

        // Apply temporary sensitivity adjustment while zoomed
        sensX = defaultSensX * sensitivityMultiplier;
        sensY = defaultSensY * sensitivityMultiplier;
    }

    public void AdjustSpeed(float sensitivityMultiplier)
    {
        // Apply the multiplier to both sensitivities while maintaining their proportion
        sensX = defaultSensX * sensitivityMultiplier;
        sensY = defaultSensY * sensitivityMultiplier;

        // Save the current sensitivity setting to PlayerPrefs
        PlayerPrefs.SetFloat("Sensitivity", sensitivityMultiplier);
    }
}
