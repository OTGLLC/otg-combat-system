#pragma warning disable CS649

using UnityEngine;

namespace OTG.CombatSM.Core
{
    
    [System.Serializable]
    public class CombatHandlerData
    {
        [SerializeField] private int m_maxHealth;
        public int MaxHealth { get { return m_maxHealth; } }
        [SerializeField] private int m_maxEnergy;
        public int MaxEnergy { get { return m_maxEnergy; } }
        [SerializeField] private int m_basePhysicalAttack;
        public int BasePhysicalAttack { get { return m_basePhysicalAttack; } }
        [SerializeField] private int m_baseEnergyAttack;
        public int BaseEnergyAttack { get { return m_basePhysicalAttack; } }
        [SerializeField] private int m_maxComboLevel;
        public int MaxComboLevel { get { return m_maxComboLevel; } }
        
    }
}