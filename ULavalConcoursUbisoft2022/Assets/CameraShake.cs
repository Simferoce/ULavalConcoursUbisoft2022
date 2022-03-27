using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera = null;


    private CinemachineBasicMultiChannelPerlin _perlinNoise;
    private float _originAmplitude = 0.0f;
    private float _timeEndShake = 0.0f;

    private void Start()
    {
        _perlinNoise = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _originAmplitude = _perlinNoise.m_AmplitudeGain;
        _perlinNoise.m_AmplitudeGain = 0f;
    }

    public void ShakeCamera(float time)
    {
        _perlinNoise.m_AmplitudeGain = _originAmplitude;
        _timeEndShake = Time.time + time;
    }

    private void Update()
    {
        if (_timeEndShake < Time.time)
        {
            _perlinNoise.m_AmplitudeGain = 0f;
        }
    }
}
