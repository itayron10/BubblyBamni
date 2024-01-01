using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraPlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] string sensitivitySettingName;
    [SerializeField] SettingsValuesSO settings;
    [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
    [SerializeField] GameObject cinemachineCameraTarget;
    [SerializeField] Transform playerTransform;
    [SerializeField] CinemachineVirtualCamera cinemachineCam;
    private float _cinemachineTargetPitch;
    private float _rotationVelocity;
    private Inputs playerInputActions;
    private float cameraTargetFov;
    public void ChangeFovToRunningFov() => cameraTargetFov = cameraRunningFov;
    public void ChangeFovToDefaultFov() => cameraTargetFov = cameraDefaultFov;

    [Header("Settings")]
    [SerializeField] float cameraDefaultFov = 60f;
    [SerializeField] float cameraRunningFov = 70f;
    [SerializeField] float fovLerpSpeed;
    [SerializeField] float sensitivity;
    [SerializeField] float maxCameraXRotation; // used to clamp the camera x rotation


    private void Start()
    {
        FindPrivateObjects();
        SetCursorStateLocked();
    }

    private void Update()
    {
        cinemachineCam.m_Lens.FieldOfView = Mathf.Lerp(cinemachineCam.m_Lens.FieldOfView,
            cameraTargetFov, fovLerpSpeed * Time.deltaTime);
    }

    private void LateUpdate() => HandleRotation();

    private void HandleRotation()
    {
        Vector2 mouseDelta = playerInputActions.Camera.MouseDelta.ReadValue<Vector2>();

        _cinemachineTargetPitch += -mouseDelta.y * sensitivity * Time.deltaTime;
        _rotationVelocity = mouseDelta.x * sensitivity * Time.deltaTime;

        // clamp our pitch rotation
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, -maxCameraXRotation, maxCameraXRotation);

        // Update Cinemachine camera target pitch
        cinemachineCameraTarget.transform.localRotation = Quaternion.Euler(_cinemachineTargetPitch, 0.0f, 0.0f);

        // rotate the player left and right
        playerTransform.Rotate(Vector3.up * _rotationVelocity);
    }

    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }

    private void FindPrivateObjects()
    {
        playerInputActions = InputManager.Instance.inputActions;
        playerInputActions.Camera.Enable();
        cameraTargetFov = cameraDefaultFov;

        for (int i = 0; i < settings.floatSettings.Length; i++)
        {
            if (settings.floatSettings[i].valueName == sensitivitySettingName)
            {
                settings.floatSettings[i].onSettingsChanges += UpdateSensitivity;
                UpdateSensitivity(settings.floatSettings[i].value);
            }
        }
    }

    private void UpdateSensitivity(float SensetivityValue)
    {
        sensitivity = SensetivityValue;
    }

    public static void SetCursorStateLocked()
    {
        // set cursor state
        Cursor.lockState = CursorLockMode.Locked;
    }
}
