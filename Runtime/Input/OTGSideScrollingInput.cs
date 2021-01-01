
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
        private void OnEnable()
        {
            InitializeInputProfile();
            InitializeInputHandler();

        }
        private void OnDisable()
        {
            CleanupInputProfile();
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

            m_inputProfile.Dispose();
            m_inputProfile = null;
        }
        #endregion

        #region Input Callbacks
       private void OnMove(InputAction.CallbackContext _ctx)
        {

        }
        private void OnAttack(InputAction.CallbackContext _ctx)
        {

        }
        private void OnHeavyAttack(InputAction.CallbackContext _ctx)
        {

        }
        private void OnSpecialAttack(InputAction.CallbackContext _ctx)
        {

        }
        private void OnDefend(InputAction.CallbackContext _ctx)
        {

        }
        #endregion
    }
}
