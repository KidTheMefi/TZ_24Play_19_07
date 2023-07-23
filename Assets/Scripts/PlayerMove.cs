using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private GameObject _warpEffect;
    [SerializeField]
    private int _forwardSpeed;
    [SerializeField]
    private float _sideSpeed;
    [SerializeField]
    private float limitValue;
    
    private float _relativeTouchPositionX;
    private bool _isRunning;

    public void SetRun(bool isRunning)
    {
        _isRunning = isRunning;
        _warpEffect.SetActive(isRunning);
    }

    public void MoveTo(float relativeTouchPositionX)
    {
        _relativeTouchPositionX = relativeTouchPositionX;
    }
    
    void Update()
    {
        if (_isRunning)
        {
            float xPos = Mathf.Clamp(_relativeTouchPositionX * limitValue, -limitValue, limitValue);
            float lerpValue = Time.deltaTime * _sideSpeed;
            float xPosNew = Mathf.Lerp(transform.position.x, xPos, lerpValue);
            float zPos = transform.position.z + Time.deltaTime * _forwardSpeed;
            transform.position = new Vector3(xPosNew, transform.position.y, zPos);
        }
    }
}
