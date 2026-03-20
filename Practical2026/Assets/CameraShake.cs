using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    enum cameraMode
    {
        Normal, Shake
    }
    cameraMode isCurrently = cameraMode.Normal;

    [Header("Shake Settings")]
    private float shakeDuration = 0.15f; // How long the shake lasts
    private float shakeMagnitude = 0.05f; // How intense the shake is
    private float dampingSpeed = 1.0f;   // How quickly the shake fades

    private float DefaultShakeDuration = 0.15f; // How long the shake lasts
    private float DefaultShakeMagnitude = 0.05f; // How intense the shake is
    private float DefaultDampingSpeed = 1.0f;   // How quickly the shake fades

    private Vector3 initialPosition;
    private float currentShakeTime;

    



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        // Store the starting position to return to it after shaking
        initialPosition = Camera.main.transform.localPosition;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (isCurrently)
        {
            case cameraMode.Normal:

                break;
                case cameraMode.Shake:
                if (currentShakeTime > 0)
                {
                    // Shake the object using a random point inside a sphere
                    Camera.main.transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
                    currentShakeTime -= Time.deltaTime * dampingSpeed;
                }
                else
                {
                    currentShakeTime = 0f;
                    Camera.main.transform.localPosition = initialPosition;
                    isCurrently = cameraMode.Normal;
                }
                break;

        }
        
    }
    internal void startShake()
    {

        shakeDuration = DefaultShakeDuration;
        shakeMagnitude = DefaultShakeMagnitude;
        dampingSpeed = DefaultDampingSpeed;
        StartS();

    }

    private void StartS()
    {
        isCurrently = cameraMode.Shake;
        currentShakeTime = shakeDuration;
        initialPosition = transform.localPosition;
    }

    internal void startShake(float duration, float magnitude, float damping)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude; 
        dampingSpeed = damping;
        StartS();
}
}
