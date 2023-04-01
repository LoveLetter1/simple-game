using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CamManager : UnitySingleton<CamManager>
{
    public CinemachineVirtualCamera vcam;
    public float resetTime = 1;
    public float resetCounterTime;
    public bool isShaking;

    private void Update()
    {
        if (isShaking)
        {
            resetCounterTime -= Time.deltaTime;
            if(resetCounterTime <= 0f)
            {
                StopShake();
            }
        }
    }

    public void Shake(float amplitude,float frequency,float duration)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = amplitude;
        cinemachineBasicMultiChannelPerlin.m_FrequencyGain = frequency;
        resetCounterTime = duration;
        isShaking = true;
    }
    public void StopShake()
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
        cinemachineBasicMultiChannelPerlin.m_FrequencyGain = 0f;
        resetCounterTime = resetTime;
        isShaking = false;
    }
}
