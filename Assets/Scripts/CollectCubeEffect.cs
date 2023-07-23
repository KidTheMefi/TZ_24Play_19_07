using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCubeEffect : MonoBehaviour
{
    public event Action<CollectCubeEffect> BackToPool = delegate(CollectCubeEffect effect) { };
    
    [SerializeField, Range(1, 10)]
    private float _disableTime;
    [SerializeField]
    private float _textForceApplied;
    [SerializeField]
    private Rigidbody _textRigidbody;
    [SerializeField]
    private ParticleSystem _particleEffects;



    public void PlayEffect()
    {
        _textRigidbody.AddForce((Vector3.up + Vector3.left)*_textForceApplied, ForceMode.Impulse);
        _particleEffects.Play();
        StartCoroutine(DisableTimer());

    }

    private IEnumerator DisableTimer()
    {
        yield return new WaitForSeconds(_disableTime);
        BackToPool.Invoke(this);
    }

    private void OnDisable()
    {
        _particleEffects.Stop();
        _textRigidbody.velocity = Vector3.zero;
        _textRigidbody.transform.localPosition = Vector3.zero;
    }
}
