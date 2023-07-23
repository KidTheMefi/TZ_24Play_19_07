using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private PlayerCubeHolder _playerCubeHolder;
        [SerializeField]
        private StickMan _stickMan;
        [SerializeField]
        private PlayerMove _playerMove;
        
        public PlayerCubeHolder PlayerCubeHolder => _playerCubeHolder;
        

        private void Start()
        {
            _playerCubeHolder.NewCubeAdd += OnNewPlayerCubeAdd;
            _stickMan.EndGameCollision += StickManOnEndGameCollision;
        }
        private void StickManOnEndGameCollision()
        {
            _playerMove.SetRun(false);
        }
        
        private void OnNewPlayerCubeAdd()
        {
            _stickMan.Jump();
            _stickMan.transform.position += Vector3.up;
        }

        private void OnDestroy()
        {
            _playerCubeHolder.NewCubeAdd -= OnNewPlayerCubeAdd;
            _stickMan.EndGameCollision -= StickManOnEndGameCollision;
        }
    }
}