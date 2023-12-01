using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ControllerHaptics : MonoBehaviour
{
    public static ControllerHaptics instance;

    public XRNode inputSource = XRNode.RightHand;
    private InputDevice targetDevice;
    private bool isHapticSupported;

    private float defaultDuration = 0.5f;
    private float defaultAmplitude = 0.5f;

    void Start()
    {
        instance = this;

        InputDevices.deviceConnected += OnDeviceConnected;
        TryInitializeHapticDevice();
    }

    private void TryInitializeHapticDevice()
    {
        InputDevices.GetDevicesAtXRNode(inputSource, new List<InputDevice>());
    }

    private void OnDeviceConnected(InputDevice device)
    {
        if (device.isValid)
        {
            targetDevice = device;
            HapticCapabilities capabilities;

            if (targetDevice.TryGetHapticCapabilities(out capabilities) && capabilities.supportsImpulse)
            {
                isHapticSupported = true;
            }
            else
            {
                isHapticSupported = false;
                Debug.LogWarning("Haptic feedback is not supported on this device.");
            }
        }
    }

    public void TriggerHapticFeedback(float duration, float amplitude)
    {
        if (isHapticSupported)
        {
            HapticCapabilities capabilities;
            if (targetDevice.TryGetHapticCapabilities(out capabilities))
            {
                if (capabilities.supportsImpulse)
                {
                    float scaledAmplitude = Mathf.Clamp01(amplitude);
                    targetDevice.SendHapticImpulse(0, scaledAmplitude, duration);
                }
            }
        }
    }

    public void ShootingHaptic()
    {
        TriggerHapticFeedback(0.5f, 0.13f);
    }

    public void EnvPlacementHaptic()
    {
        TriggerHapticFeedback(0.5f, 0.13f);
    }

    public void SuccessHaptic()
    {
        TriggerHapticFeedback(0.5f, 0.25f);
    }
}
