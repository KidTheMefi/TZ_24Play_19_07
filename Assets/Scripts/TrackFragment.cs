using System;
using System.Collections;
using UnityEngine;

public class TrackFragment : MonoBehaviour
{
    public event Action PlayerEndTrackFragment = delegate{ };
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerEndTrackFragment.Invoke();
        }
    }
    
    public IEnumerator MoveToPosition(Vector3 position)
    {
        float lerp = 0;
        while (lerp <1)
        {
            var newPos = Vector3.Lerp(transform.position, position, lerp);
            transform.position = newPos;
            lerp += Time.deltaTime;
            yield return null;
        }
    }
}
