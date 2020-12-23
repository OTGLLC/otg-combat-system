
using UnityEngine;

namespace OTG.CombatSM.Core
{
    public class CombatHandler
    {
        #region Fields
        private CombatHandlerData m_handlerData;
        #endregion

        #region Properties
        public TwitchFighterCombatParams TwitchCombat { get; private set; }
        #endregion

        #region Public API
        public CombatHandler(HandlerDataGroup _dataGroup)
        {
            InitHandlerData(_dataGroup);
            InitializeIndividualParams(_dataGroup);
        }
        public void CleanupHandler()
        {
            Cleanup();
        }
        #endregion

        #region Utility
        private void InitializeIndividualParams(HandlerDataGroup _dataGroup)
        {
            TwitchCombat = new TwitchFighterCombatParams(_dataGroup);
        }
        private void InitHandlerData(HandlerDataGroup _dataGroup)
        {
            m_handlerData = _dataGroup.CombatsHandlerData;
        }
        private void Cleanup()
        {
            m_handlerData = null;
        }
        #endregion
    }
    public class TwitchFighterCombatParams
    {
        public delegate void OnHealthUpdate(int _current, int _max);
        public event OnHealthUpdate HealthUpdateEvent;

        public OTGCombatSMC NearestTarget { get; set; }
        public Vector3 NearestTargetPosition { get; set; }
        public CombatHandlerData Data { get; private set; }
        public int CurrentHealth { get; set; }
        public int CurrentEnergy { get; set; }
        public int CurrentPhysicalAttack { get; set; }
        public int CurrentEnergyAttack { get; set; }
        public int CurrentComboBreakPoint { get; set; }
        public int CurrentComboCount { get; set; }
        public int CombatLevel { get; set; }
        public int ComboLevel { get; set; }
        public bool IsDead { get; private set; }

        #region Public API
        public TwitchFighterCombatParams(HandlerDataGroup _datGroup)
        {
            Initialize( _datGroup);
        }

        public void CleanupParams()
        {
            NearestTarget = null;
        }
        public void ResetParams()
        {
            NearestTarget = null;
            NearestTargetPosition = Vector3.zero;
            Data = null;
        }
        
        public void RecieveDamagePayload(IDamagePayload _payload)
        {
            CurrentHealth -= (int)_payload.GetDamage();
            CheckifDead();
            UpdateHealth();
        }
        public void UpdateHealthBar()
        {
            UpdateHealth();
        }
        #endregion

        #region Utility
        private void Initialize(HandlerDataGroup _datGroup)
        {
            Data = _datGroup.CombatsHandlerData;
        }
       
        private void CheckifDead()
        {
            if (CurrentHealth <= 0)
                IsDead = true;
            else
                IsDead = false;
        }
        void UpdateHealth()
        {
            if(HealthUpdateEvent!= null)
            {
                HealthUpdateEvent(CurrentHealth, Data.MaxHealth);
            }
        }
        #endregion
    }
}