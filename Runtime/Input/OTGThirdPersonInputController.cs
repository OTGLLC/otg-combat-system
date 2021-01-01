
using UnityEngine;
using UnityEngine.InputSystem;

namespace OTG.CombatSystem
{
    public class OTGThirdPersonInputController : MonoBehaviour
    {
        #region Fields
        private InputHandler m_inputHandler;
        #endregion

        #region Unity API
        private void OnEnable()
        {
            InitializeData();
        }
        private void OnDisable()
        {
            CleanupData();
        }
        #endregion

        #region Utility
        private void InitializeData()
        {
            m_inputHandler = GetComponent<OTGCombatSMC>().Handler_Input;
        }
        private void CleanupData()
        {
            m_inputHandler = null;
        }
        #endregion

        #region Input System Callbacks
        public void OnMove(InputAction.CallbackContext _ctx)
        {

        }
        public void OnLook(InputAction.CallbackContext _ctx)
        {

        }
        public void OnAttack(InputAction.CallbackContext _ctx)
        {

        }
        public void OnSpecial(InputAction.CallbackContext _ctx)
        {

        }
        public void OnSwitchWeapon(InputAction.CallbackContext _ctx)
        {

        }
        public void OnJump(InputAction.CallbackContext _ctx)
        {

        }
        #endregion
    }
}
