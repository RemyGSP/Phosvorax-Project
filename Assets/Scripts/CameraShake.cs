using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private float intensity;
    private void Start()
    {

    }

    public void ShakeCamera(float duration)
    {
        CinemachineBasicMultiChannelPerlin noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        if (noise != null)
        {
            noise.m_AmplitudeGain = intensity;
            // You might want to set different frequencies for X, Y, and Z axes depending on your preference
            noise.m_FrequencyGain = intensity * 10f;
        }

        StartCoroutine(_ResetShake(duration));
;
    }

    private IEnumerator _ResetShake(float time)
    {
        yield return new WaitForSeconds(time);
        CinemachineBasicMultiChannelPerlin noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        if (noise != null)
        {
            noise.m_AmplitudeGain = 0f;
            noise.m_FrequencyGain = 0f;
        }
    }
}
