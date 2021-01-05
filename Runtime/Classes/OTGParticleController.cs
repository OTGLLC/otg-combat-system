
using UnityEngine;

namespace OTG.CombatSystem
{
    public class OTGParticleController : MonoBehaviour 
    {
        [SerializeField] private ParticleSystem m_particleSystem;


        public void EmitParticle(int _amount)
        {
            m_particleSystem.Emit(_amount);
        }
    }
}
