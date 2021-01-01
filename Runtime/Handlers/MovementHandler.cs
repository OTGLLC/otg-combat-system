
using UnityEngine;

namespace OTG.CombatSystem
{
    public class MovementHandler
    {

        #region Properties
        public Transform Comp_Transform { get; private set; }
        public Transform Comp_CharModel_Transform { get; private set; }
        public MovementHandlerData Data { get; private set; }
        public CharacterController Comp_CharacterControl { get; private set; }
        public OTGGlobalCombatConfig GlobalCombatConfig { get; private set; }
        #endregion

        #region Parameters
        public float Speed_DesiredXaxis;
        public float Speed_DesiredYaxis;
        public float Speed_DesiredZaxis;

        public float Speed_CurrentXaxis;
        public float Speed_CurrentYaxis;
        public float Speed_CurrentZaxis;


        #endregion

        #region Public API
        public MovementHandler(HandlerDataGroup _dataGroup, CharacterController _charControl, Transform _trans,Transform _characterModelTrans ,OTGGlobalCombatConfig _globalConfig)
        {
            Data = _dataGroup.MoveHandlerData;
            Comp_CharacterControl = _charControl;
            Comp_CharModel_Transform = _characterModelTrans;
            Comp_Transform = _trans;
            GlobalCombatConfig = _globalConfig;
        }
        public void CleanupHandler()
        {
            Cleanup();
        }
        

        #endregion

        #region Utility
        private void Cleanup()
        {
            Data = null;
            Comp_CharacterControl = null;
            Comp_CharModel_Transform = null;
            Comp_Transform = null;
            GlobalCombatConfig = null;
        }

        #endregion
    }
}