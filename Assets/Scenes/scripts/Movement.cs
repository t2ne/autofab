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
