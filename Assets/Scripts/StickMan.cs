using System;
using UnityEngine;

public class StickMan : MonoBehaviour
{
    public event Action EndGameCollision = delegate { };

    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private GameObject _skelet;
    [SerializeField]
    private Collider _collider;
    [SerializeField]
    private Rigidbody _fullRigidbody;
    
    public void Jump()
    {
        _animator.SetTrigger(Animator.StringToHash("Jump"));
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Ground"))
        {
            _fullRigidbody.isKinematic = true;
            _collider.enabled = false;
            _animator.enabled = false;
            _skelet.SetActive(true);
            EndGameCollision.Invoke();
        }
    }
}
