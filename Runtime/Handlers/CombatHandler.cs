using System.Collections.Generic;
using UnityEngine;

namespace OTG.CombatSystem
{
    public class CombatHandler
    {
        #region Fields
        private CombatHandlerData m_handlerData;
        private Dictionary<OTGWeaponID, OTGWeapon> m_weaponLookup;
        #endregion

        #region Properties
        public OTGWeapon[] AvailableWeapons { get; private set; }
        #endregion

        #region Public API
        public CombatHandler(HandlerDataGroup _dataGroup, OTGWeapon[] _weapons)
        {
            InitHandlerData(_dataGroup);
            InitializeWeapons(_weapons);
        }
        public void CleanupHandler()
        {
            Cleanup();
            CleanupWeapons();
        }
        #endregion

        #region Utility
        
        private void InitHandlerData(HandlerDataGroup _dataGroup)
        {
            m_handlerData = _dataGroup.CombatsHandlerData;
        }
        private void Cleanup()
        {
            m_handlerData = null;
        }
        private void InitializeWeapons(OTGWeapon[] _weapons)
        {
            AvailableWeapons = _weapons;
            m_weaponLookup = new Dictionary<OTGWeaponID, OTGWeapon>();
            for(int i = 0; i < AvailableWeapons.Length; i++)
            {
                if (!m_weaponLookup.ContainsKey(AvailableWeapons[i].ID))
                    m_weaponLookup.Add(AvailableWeapons[i].ID, AvailableWeapons[i]);
            }
        }
        private void CleanupWeapons()
        {
            m_weaponLookup.Clear();
            m_weaponLookup = null;
            AvailableWeapons = null;
        }
        #endregion
    }

}