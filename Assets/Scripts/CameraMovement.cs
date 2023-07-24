using System.Collections;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    [SerializeField]
    private Transform _cameraTransform;
    [SerializeField]
    private float _shakeDuration;
    [SerializeField]
    private AnimationCurve _curve;

    private float cameraFollowDistance;
    private bool isShaking;

    private void Start()
    {
        _player.PlayerCubeHolder.CubeLose += PlayerCubeHolderOnCubeLose;
        cameraFollowDistance = transform.position.z - _player.transform.position.z;
    }
    
    private void PlayerCubeHolderOnCubeLose()
    {
        if (!isShaking)
        {
            StartCoroutine(CameraShake());
        }
    }

    IEnumerator CameraShake()
    {
        isShaking = true;
        float shakeTime = 0f;

        while (shakeTime < _shakeDuration)
        {
            shakeTime += Time.deltaTime;
            float strength = _curve.Evaluate(shakeTime / _shakeDuration);
            _cameraTransform.localPosition = Random.insideUnitSphere * strength;
            yield return null;
        }
        _cameraTransform.localPosition = Vector3.zero;
        isShaking = false;
    }
    
    private void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, _player.transform.position.z + cameraFollowDistance);
    }
    
}
