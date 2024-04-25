using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour
{
    public bool IsEnabled;
    public bool IsStabalized;

    [SerializeField] float _amplitude = 1f;
    [SerializeField] float _heightAmplitude = 0.1f;
    [SerializeField] float _sideAmplitude = 0.01f;
    [SerializeField] float _frequency = 10f;
    [SerializeField] float _bobLerpSpeed = 20f;
    [SerializeField] float _returnLerpSpeed = 3f;
    [SerializeField] float _footstepSpeed = 3f;
    [SerializeField] float _stabilizationDistance = 15f;

    Rigidbody _body;
    PlayerController _playerController;
    bool _isMoving => _body.GetRelativePointVelocity(Vector3.zero).magnitude > 0.01f;
    Vector3 _focusTarget => transform.parent.position + transform.parent.forward * _stabilizationDistance;
    Vector3 _lastBodyPos;

    float _theta = 0f;

    private void Awake()
    {
        _body = GetComponentInParent<Rigidbody>();
        _playerController = GetComponentInParent<PlayerController>();
    }

    private void Update()
    {
        if (!IsEnabled) return;

        HandleLerp();
        Stabalize();
    }

    void Stabalize()
    {
        if (!IsStabalized) return;
        transform.LookAt(_focusTarget);
    }

    void HandleLerp()
    {
        float footstepAmount = Mathf.Clamp01(_body.velocity.magnitude / _footstepSpeed);
        float lerpSpeed = _isMoving ? _bobLerpSpeed : _returnLerpSpeed;
        LocalLerp(FootstepMotion() * footstepAmount, _bobLerpSpeed);
    }

    void LocalLerp(Vector3 target, float lerpSpeed)
    {
        Vector3 localPos = transform.localPosition;
        Vector3 lerpLocalPos = Vector3.Lerp(localPos, target, lerpSpeed * Time.deltaTime);
        transform.localPosition = lerpLocalPos;
    }

    Vector3 FootstepMotion()
    {
        Vector3 pos = Vector3.zero;

        float moveSpeedMult = Mathf.Clamp(_body.velocity.magnitude / (_playerController.Speed-1f), 0f, 2f);
   
        _theta += Time.deltaTime * moveSpeedMult * _frequency;
        _theta %= 4f * Mathf.PI;
        
        pos.y += Mathf.Sin(_theta) * _heightAmplitude * _amplitude;
        pos.x += Mathf.Cos(_theta / 2) * _sideAmplitude * _amplitude;
        return pos;
    }
}
