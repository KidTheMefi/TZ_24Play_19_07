using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class Track : MonoBehaviour
    {
        [SerializeField]
        private float trackFragmentsDistance;
        [SerializeField]
        private Vector3 fragmentStartPositionOffset;
        
        [SerializeField]
        private List<TrackFragment> _trackFragmentPrefabs;

        [SerializeField]
        private List<TrackFragment> _inGameTrackFragments;
        
        private void Start()
        {
            foreach (var trackFragment in _inGameTrackFragments)
            {
                trackFragment.PlayerEndTrackFragment += OnPlayerEndTrackFragment;
            }
        }
        
        private void OnPlayerEndTrackFragment()
        {
           
            AddNewFragment();
            RemoveOldFragment();

        }

        private void AddNewFragment()
        {
            Vector3 newPosition = _inGameTrackFragments[^1].transform.position + Vector3.forward*trackFragmentsDistance;
            Vector3 startPosition = newPosition + Vector3.forward*trackFragmentsDistance + fragmentStartPositionOffset;
            var newTrackFragment = Instantiate(_trackFragmentPrefabs[Random.Range(0, _trackFragmentPrefabs.Count)], startPosition, Quaternion.identity );
            newTrackFragment.transform.SetParent(transform);
            _inGameTrackFragments.Add(newTrackFragment);
            StartCoroutine(newTrackFragment.MoveToPosition(newPosition));
            newTrackFragment.PlayerEndTrackFragment += OnPlayerEndTrackFragment;
        }

        private void RemoveOldFragment()
        {
            var fragment = _inGameTrackFragments[0];
            _inGameTrackFragments.Remove(fragment);
            fragment.PlayerEndTrackFragment -= OnPlayerEndTrackFragment;
            Destroy(fragment.gameObject);
        }
        
        private void OnDestroy()
        {
            foreach (var fragment in _trackFragmentPrefabs)
            {
                fragment.PlayerEndTrackFragment -= OnPlayerEndTrackFragment;
            }
        }
    }
}