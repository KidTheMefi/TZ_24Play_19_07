using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace
{
    public class PointerInput : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField]
        private TextMeshProUGUI _tapToPlayText;
        [SerializeField]
        private PlayerMove _playerMove;
        
        private float _halfScreen;

        private void Start()
        {
            _halfScreen = Screen.width / 2f;
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            _playerMove.SetRun(true);
            _tapToPlayText.enabled = false;
        }
        public void OnDrag(PointerEventData eventData)
        {
            float relativeTouchPositionX = (eventData.position.x - _halfScreen) / _halfScreen;
            _playerMove.MoveTo(relativeTouchPositionX);
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            
        }
    }
}