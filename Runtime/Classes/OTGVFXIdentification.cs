
using UnityEngine;

namespace OTG.CombatSystem
{
    [CreateAssetMenu]
    public class OTGVFXIdentification:ScriptableObject
    {
        [SerializeField] private OTGAnimationEventTypeEnum m_eventType = OTGAnimationEventTypeEnum.VFX_ID;
        public OTGAnimationEventTypeEnum EventType { get { return m_eventType; } }
    }
}