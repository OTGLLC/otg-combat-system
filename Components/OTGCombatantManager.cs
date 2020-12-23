

using System.Collections.Generic;
using UnityEngine;

namespace OTG.CombatSM.Core
{
    public class OTGCombatantManager : MonoBehaviour
    {
        #region Fields
        private static OTGCombatantManager m_instance;
        private List<OTGCombatSMC> m_combatants = new List<OTGCombatSMC>();
        private int m_combatantCount = 0;
        #endregion
        
        #region Unity API
        private void Awake()
        {
            CreateInstance(); 
        }
        private void Update()
        {
            UpdateAllCombatants();
        }
        #endregion

        #region Utility
        private void CreateInstance()
        {
            if(m_instance == null)
            {
                m_instance = this;
                return;
            }
           
        }
        private void SubscribeToCombatantsList(OTGCombatSMC _requester)
        {
            if (!m_combatants.Contains(_requester))
                m_combatants.Add(_requester);

            m_combatantCount = m_combatants.Count;
        }
        private void UnsubscribeFromCombatantsList(OTGCombatSMC _requester)
        {
            m_combatants.Remove(_requester);

            m_combatantCount = m_combatants.Count;
        }
        private void UpdateAllCombatants()
        {
            for(int i = 0; i < m_combatantCount; i++ )
            {
                m_combatants[i].UpdateCombatant();
            }
        }
        #endregion

        #region Public API
        public static ushort GetCombatantID(OTGCombatSMC _requestingController)
        {
            ushort newID = 0;


            return newID;
        }
        public static void SubscribeToCombatantUpdateLoop(OTGCombatSMC _combatant)
        {
            m_instance.SubscribeToCombatantsList(_combatant);
        }
        public static void UnsubscribeFromCombatantUpdateLoop(OTGCombatSMC _combatant)
        {
            m_instance.UnsubscribeFromCombatantsList(_combatant);
        }
        #endregion

    }
}