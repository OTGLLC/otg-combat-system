
using UnityEngine;

namespace OTG.CombatSystem
{
    public class InputHandler
    {
        #region Fields
        private InputHandlerData m_handlerData;
        #endregion

        #region Properties
        public Vector2 MovementVector { get; set; }
        public bool AttackInput { get; set; }
        public bool HardAttackInput { get; set; }
        public bool SpecialAttackInput { get; set; }
        public bool DefendInput { get; set; }
        public bool SwitchLaneUpInput { get; set; }
        public bool SwitchLaneDownInput { get; set; }
        #endregion

        #region Public API
        public InputHandler(HandlerDataGroup _dataGroup)
        {
            InitializeData(_dataGroup);
           
        }
        public void CleanupHandler()
        {
            Cleanup();
        }
        #endregion

        #region Utility
        private void InitializeData(HandlerDataGroup _group)
        {
            m_handlerData = _group.InputsHandlerData;
        }
        private void Cleanup()
        {
            m_handlerData = null;
        }
        #endregion

    }

}