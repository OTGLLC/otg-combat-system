#pragma warning disable CS0649

using UnityEngine;

namespace OTG.CombatSM.Core
{
    public class OTGVFXController : MonoBehaviour
    {
        [SerializeField] private OTGVFXIdentification m_vfxID;
        public OTGVFXIdentification VfxID { get { return m_vfxID; } }

        [SerializeField]private ParticleSystem m_psys;
        [SerializeField] private ParticleSystem m_psysHit;
       
       public void OnPlayVFX()
        {
            m_psys.Stop();
            m_psys.Play();
        }
    }
}