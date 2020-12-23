#pragma warning disable CS0649

using UnityEngine;

namespace OTG.CombatSystem
{
    [CreateAssetMenu]
    public class OTGAnimationEvent : ScriptableObject
    {
        [SerializeField] private OTGVFXIdentification m_vfxID;
        [SerializeField] private OTGHurtColliderID m_hurtColliderID;
        [SerializeField] private OTGSFXIdentification m_sfxID;

        public OTGVFXIdentification VfxID { get { return m_vfxID; } }
        public OTGSFXIdentification SfxID { get { return m_sfxID; } }
        public OTGHurtColliderID HurtColliderID { get { return m_hurtColliderID; } }
    }
}