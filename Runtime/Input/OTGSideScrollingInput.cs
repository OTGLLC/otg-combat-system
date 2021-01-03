
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace OTG.CombatSystem
{
    public class OTGSideScrollingInput : MonoBehaviour
    {
        #region Fields
        private SideScrollInputProfile m_inputProfile;
        private InputHandler m_inputHandler;
        private WaitForSeconds m_actionDegredation;
        private float m_degredationTime = 0.2f;
        #endregion

        #region Coroutines
        private Coroutine m_attack_co;
        private Coroutine m_specialAttack_co;
        private Coroutine m_hardAttack_co;
        private Coroutine m_defend_co;
        private Coroutine m_switchLanesUp_co;
        private Coroutine m_switchLanesDown_co;

        #endregion

        #region Unity API
        private void OnEnable()
        {
            InitializeCoroutines();
        }

        private void Start()
        {
            //InitializeInputProfile();
            InitializeInputHandler();
        }
        private void OnDisable()
        {
            //CleanupInputProfile();
            CleanupInputHandler();
            CleanupCoroutines();
        }
        #endregion

        #region Utility
        private void InitializeCoroutines()
        {
            m_actionDegredation = new WaitForSeconds(m_degredationTime);
        }
        private void CleanupCoroutines()
        {
            StopAllCoroutines();
            m_attack_co = null;
            m_defend_co = null;
            m_hardAttack_co = null;
            m_specialAttack_co = null;
            m_switchLanesUp_co = null;
            m_switchLanesDown_co = null;
            m_actionDegredation = null;
        }
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
        private void StartActionCoroutine(ref Coroutine _target,IEnumerator _coroutine,InputAction.CallbackContext _ctx)
        {
            if (_ctx.phase != InputActionPhase.Performed)
                return;

            if (_target != null)
            {
                StopCoroutine(_target);
                _target = null;
            }

            _target = StartCoroutine(_coroutine);
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
            StartActionCoroutine(ref m_attack_co, Attack_CO(), _ctx);
        }
        public void OnHeavyAttack(InputAction.CallbackContext _ctx)
        {
            StartActionCoroutine(ref m_hardAttack_co, HardAttack_CO(), _ctx);
        }
        public void OnSpecialAttack(InputAction.CallbackContext _ctx)
        {
            StartActionCoroutine(ref m_specialAttack_co, SpecialAttack_CO(), _ctx);
        }
        public void OnDefend(InputAction.CallbackContext _ctx)
        {
            StartActionCoroutine(ref m_defend_co, Defend_CO(), _ctx);
        }
        public void OnSwitchLaneUp(InputAction.CallbackContext _ctx)
        {
            StartActionCoroutine(ref m_switchLanesUp_co, SwitchLanesUp_CO(), _ctx);
        }
        public void OnSwitchLanesDown(InputAction.CallbackContext _ctx)
        {
            StartActionCoroutine(ref m_switchLanesDown_co, SwitchLanesDown_CO(), _ctx);
        }
        #endregion

        #region Coroutines
        private IEnumerator Attack_CO()
        {
            m_inputHandler.AttackInput = true;
            yield return m_actionDegredation;
            m_inputHandler.AttackInput = false;
        }
        private IEnumerator HardAttack_CO()
        {
            m_inputHandler.HardAttackInput = true;
            yield return m_actionDegredation;
            m_inputHandler.HardAttackInput = false;
        }
        private IEnumerator SpecialAttack_CO()
        {
            m_inputHandler.SpecialAttackInput = true;
            yield return m_actionDegredation;
            m_inputHandler.SpecialAttackInput = false;
        }
        private IEnumerator Defend_CO()
        {
            m_inputHandler.DefendInput = true;
            yield return m_actionDegredation;
            m_inputHandler.DefendInput = false;
        }
        private IEnumerator SwitchLanesUp_CO()
        {
            m_inputHandler.SwitchLaneUpInput = true;
            yield return m_actionDegredation;
            m_inputHandler.SwitchLaneUpInput = false;
        }
        private IEnumerator SwitchLanesDown_CO()
        {
            m_inputHandler.SwitchLaneDownInput = true;
            yield return m_actionDegredation;
            m_inputHandler.SwitchLaneDownInput = false;
        }

        #endregion
    }
}
