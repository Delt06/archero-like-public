using UnityEngine;

namespace Features.PopUpText.Assets
{
    [CreateAssetMenu]
    public class PopUpTextDataAsset : ScriptableObject
    {
        [SerializeField] private Color _damageTextColor = Color.white;
        [SerializeField] [Min(0f)] private float _damageTextRandomOffset = 0.25f;
        [SerializeField] [Min(0f)] private float _damageTextDuration = 0.5f;
        [SerializeField] private Color _criticalDamageTextColor = Color.red;
        [SerializeField] private Color _healingTextColor = Color.green;
        [SerializeField] [Min(0f)] private float _healingTextDuration = 0.5f;
        [SerializeField] private Color _comboHitColor = Color.red;
        [SerializeField] [Min(0f)] private float _comboHitDuration = 0.5f;
        [SerializeField] private Vector3 _comboHitOffset = Vector3.right;

        public Color DamageTextColor => _damageTextColor;

        public float DamageTextDuration => _damageTextDuration;

        public float DamageTextRandomOffset => _damageTextRandomOffset;

        public Color CriticalDamageTextColor => _criticalDamageTextColor;
        public Color HealingTextColor => _healingTextColor;

        public float HealingTextDuration => _healingTextDuration;

        public Color ComboHitColor => _comboHitColor;

        public float ComboHitDuration => _comboHitDuration;

        public Vector3 ComboHitOffset => _comboHitOffset;
    }
}