using UnityEngine;

namespace DefaultNamespace
{
    public class CollectCubeEffectFactory : MonoBehaviour
    {
        [SerializeField]
        private CollectCubeEffect _collectCubeEffectPrefab;

        private ObjectPool<CollectCubeEffect> _effectsPool;

        private void Awake()
        {
            _effectsPool = new ObjectPool<CollectCubeEffect>(InstantiateEffect, TurnOnCallback, TurnOffCallback, 5);
        }
        
        private CollectCubeEffect InstantiateEffect()
        {
            var effect = Instantiate(_collectCubeEffectPrefab, transform);
            effect.gameObject.SetActive(false);
            effect.BackToPool += EffectOnBackToPool;
            return effect;
        }
        private void TurnOffCallback(CollectCubeEffect collectCubeEffect)
        {
            collectCubeEffect.gameObject.SetActive(false);
        }
        private void TurnOnCallback(CollectCubeEffect collectCubeEffect)
        {
            collectCubeEffect.gameObject.SetActive(true);
        }
        
        private void EffectOnBackToPool(CollectCubeEffect effect)
        {
            _effectsPool.ReturnObject(effect);
        }
        
        public void CreateEffectAt(Vector3 position)
        {
           var effect = _effectsPool.GetObject();
           effect.transform.position = position;
           effect.PlayEffect();
        }
    }
}