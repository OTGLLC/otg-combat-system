
using UnityEngine;
using UnityEngine.InputSystem;

namespace OTG.CombatSystem
{
    public class OTGSideScrollingInput : MonoBehaviour
    {
        #region Fields
        private SideScrollInputProfile m_inputProfile;
        private InputHandler m_inputHandler;
        #endregion

        #region Unity API
  
        private void Start()
        {
            //InitializeInputProfile();
            InitializeInputHandler();
        }
        private void OnDisable()
        {
            //CleanupInputProfile();
            CleanupInputHandler();
        }
        #endregion

        #region Utility
        private void InitializeInputHandler()
        {
            m_inputHandler = GetComponent<OTGCombatSMC>().Handler_Input;
        }
        private void CleanupInputHandler()
        {
            m_inputHandler = null;
        }
        private void InitializeInputProfile()
        {
            m_inputProfile = new SideScrollInputProfile();
          
            m_inputProfile.Player.Move.started += OnMove;
            m_inputProfile.Player.Move.performed += OnMove;
            m_inputProfile.Player.Move.canceled += OnMove;

            m_inputProfile.Player.Attack.started += OnAttack;
            m_inputProfile.Player.Attack.performed += OnAttack;
            m_inputProfile.Player.Attack.canceled += OnAttack;

            m_inputProfile.Player.HeavyAttack.started += OnHeavyAttack;
            m_inputProfile.Player.HeavyAttack.performed += OnHeavyAttack;
            m_inputProfile.Player.HeavyAttack.canceled += OnHeavyAttack;

            m_inputProfile.Player.Special.started += OnSpecialAttack;
            m_inputProfile.Player.Special.performed += OnSpecialAttack;
            m_inputProfile.Player.Special.canceled += OnSpecialAttack;

            m_inputProfile.Player.Defend.started += OnDefend;
            m_inputProfile.Player.Defend.performed += OnDefend;
            m_inputProfile.Player.Defend.canceled += OnDefend;

            m_inputProfile.Player.SwitchLaneDown.started += OnSwitchLanesDown;
            m_inputProfile.Player.SwitchLaneDown.performed += OnSwitchLanesDown;
            m_inputProfile.Player.SwitchLaneDown.canceled += OnSwitchLanesDown;

            m_inputProfile.Player.SwitchLaneUp.started += OnSwitchLaneUp;
            m_inputProfile.Player.SwitchLaneUp.performed += OnSwitchLaneUp;
            m_inputProfile.Player.SwitchLaneUp.canceled += OnSwitchLaneUp;
        }

        private void CleanupInputProfile()
        {
            m_inputProfile.Player.Move.started -= OnMove;
            m_inputProfile.Player.Move.performed -= OnMove;
            m_inputProfile.Player.Move.canceled -= OnMove;

            m_inputProfile.Player.Attack.started -= OnAttack;
            m_inputProfile.Player.Attack.performed -= OnAttack;
            m_inputProfile.Player.Attack.canceled -= OnAttack;

            m_inputProfile.Player.HeavyAttack.started -= OnHeavyAttack;
            m_inputProfile.Player.HeavyAttack.performed -= OnHeavyAttack;
            m_inputProfile.Player.HeavyAttack.canceled -= OnHeavyAttack;

            m_inputProfile.Player.Special.started -= OnSpecialAttack;
            m_inputProfile.Player.Special.performed -= OnSpecialAttack;
            m_inputProfile.Player.Special.canceled -= OnSpecialAttack;

            m_inputProfile.Player.Defend.started -= OnDefend;
            m_inputProfile.Player.Defend.performed -= OnDefend;
            m_inputProfile.Player.Defend.canceled -= OnDefend;

            m_inputProfile.Player.SwitchLaneDown.started -= OnSwitchLanesDown;
            m_inputProfile.Player.SwitchLaneDown.performed -= OnSwitchLanesDown;
            m_inputProfile.Player.SwitchLaneDown.canceled -= OnSwitchLanesDown;

            m_inputProfile.Player.SwitchLaneUp.started -= OnSwitchLaneUp;
            m_inputProfile.Player.SwitchLaneUp.performed -= OnSwitchLaneUp;
            m_inputProfile.Player.SwitchLaneUp.canceled -= OnSwitchLaneUp;

            m_inputProfile.Dispose();
            m_inputProfile = null;
        }
        #endregion

        #region Input Callbacks
       public void OnMove(InputAction.CallbackContext _ctx)
        {
            switch(_ctx.phase)
            {
                case InputActionPhase.Canceled:
                    m_inputHandler.MovementVector = _ctx.ReadValue<Vector2>();
                    break;
                case InputActionPhase.Performed:
                    m_inputHandler.MovementVector = _ctx.ReadValue<Vector2>();
                    Debug.Log(m_inputHandler.MovementVector);
                    break;
            }
        }
        public void OnAttack(InputAction.CallbackContext _ctx)
        {

        }
        public void OnHeavyAttack(InputAction.CallbackContext _ctx)
        {

        }
        public void OnSpecialAttack(InputAction.CallbackContext _ctx)
        {

        }
        public void OnDefend(InputAction.CallbackContext _ctx)
        {

        }
        public void OnSwitchLaneUp(InputAction.CallbackContext _ctx)
        {
            switch(_ctx.phase)
            {
                case InputActionPhase.Performed:
                    m_inputHandler.SwitchLaneUpInput = true;
                    break;
                case InputActionPhase.Canceled:
                    m_inputHandler.SwitchLaneUpInput = false;
                    break;
            }
            
        }
        public void OnSwitchLanesDown(InputAction.CallbackContext _ctx)
        {

        }
        #endregion
    }
}
