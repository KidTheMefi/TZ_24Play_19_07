using System;

using UnityEngine;

public class PlayerCube : MonoBehaviour
{
    public event Action<PlayerCube> GrabCube = delegate(PlayerCube cube) { };
    public event Action<PlayerCube> CubeWallCollide = delegate(PlayerCube cube) { };

    private float _yDifferenceToInteract = 0.95f;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall") && Mathf.Abs(collision.transform.position.y - transform.position.y) < _yDifferenceToInteract)
        {
            transform.SetParent(null);
            transform.SetParent(collision.transform.parent);
            CubeWallCollide.Invoke(this);
            return;
        }
        
        
        if (collision.gameObject.CompareTag("PickupCube") && Mathf.Abs(collision.transform.position.y - transform.position.y) < _yDifferenceToInteract)
        {
            if (collision.gameObject.TryGetComponent<PlayerCube>(out var cube))
            {
                GrabCube.Invoke(cube);
            }
        }
    }
}
