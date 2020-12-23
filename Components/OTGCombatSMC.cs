#pragma warning disable CS0649


using UnityEngine;

namespace OTG.CombatSM.Core
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CharacterController))]
    public class OTGCombatSMC : MonoBehaviour
    {
        #region Inspector Vars
        [SerializeField] private OTGGlobalCombatConfig m_globalConfig;
        [SerializeField] private HandlerDataGroup m_handlerDataGroup;
        [SerializeField] private OTGCombatState m_startingState;
        [SerializeField] private e_CombatantType m_combatantType;
        #endregion

        #region Fields
        [HideInInspector]
        [SerializeField]
        private OTGCombatState m_currentState;

        private OTGCombatState m_previousState;
        private bool m_flaggedForDespawn;
        #endregion


        #region Handlers
        public AnimationHandler Handler_Animation { get; private set; }
        public MovementHandler Handler_Movement { get; private set; }
        public InputHandler Handler_Input { get; private set; }
        public CollisionHandler Handler_Collision { get; private set; }
        public CombatHandler Handler_Combat { get; private set; }
        public VFXHandler Handler_VFX { get; private set; }
        public SFXHandler Handler_SFX { get; private set; }
        #endregion

        #region Unity API
        private void OnEnable()
        {
            m_flaggedForDespawn = false;
            InitializeHandlers();
        }
        private void Start()
        {
            ChangeState(m_startingState);
        }
        private void Update()
        {
            if (m_flaggedForDespawn)
            {
                Destroy(this.gameObject);
                return;
            }
                

            m_currentState.OnStateUpdate(this);
      
        }
        private void OnAnimatorMove()
        {
            m_currentState.OnStateAnimUpdate(this);
        }
        private void OnDisable()
        {
            CleanupHandlers();
            //OTGCombatantManager.UnsubscribeFromCombatantUpdateLoop(this);
        }
        #endregion

        #region Public API
        public void OnChangeStateRequested(OTGCombatState _newState, bool _usePrevious = false)
        {
            ChangeState(_newState, _usePrevious);
        }
        public void UpdateCombatant()
        {
            m_currentState.OnStateUpdate(this);
        }
        public void OnAnimationEvent(OTGAnimationEvent _event)
        {
            Handler_Animation.UpdateAnimationEvent(_event);
            Handler_VFX.OnAnimationEvent(_event);
            Handler_Collision.OnAnimationEvent(_event);
            Handler_SFX.OnAnimationEventUpdate(_event);
        }
        public void SetFlaggedForDespawn(bool _value)
        {
            m_flaggedForDespawn = _value;
        }
        #endregion

        #region Utility
 
        private void ChangeState(OTGCombatState _newState, bool _usePrevious = false)
        {
            if(_usePrevious && m_previousState != null)
            {
                m_currentState.OnStateExit(this);
                m_currentState = m_previousState;
                m_currentState.OnStateEnter(this, true);
                m_previousState = null;

                return;
            }


            if (m_currentState != null)
                m_currentState.OnStateExit(this);

            m_previousState = m_currentState;
            m_currentState = _newState;
            m_currentState.OnStateEnter(this);
            
        }
        private void InitializeHandlers()
        {
            Handler_Animation = new AnimationHandler(m_handlerDataGroup, GetComponent<Animator>());
            Handler_Movement = new MovementHandler(m_handlerDataGroup, GetComponent<CharacterController>(), GetComponent<Transform>(),m_globalConfig);
            Handler_Input = new InputHandler(m_handlerDataGroup);
            Handler_Collision = new CollisionHandler(m_handlerDataGroup, GetComponentInChildren<OTGHitColliderController>(), GetComponentsInChildren<OTGHurtColliderController>(), GetComponentInChildren<OTGTargetingController>(),m_globalConfig);
            Handler_Combat = new CombatHandler(m_handlerDataGroup);
            Handler_VFX = new VFXHandler(GetComponentsInChildren<OTGVFXController>());
            Handler_SFX = new SFXHandler(m_globalConfig.SoundHandleData,GetComponentsInChildren<OTGSoundFXController>());
        }
        private void CleanupHandlers()
        {
            Handler_Animation.CleanupHandler();
            Handler_Animation = null;

            Handler_Movement.CleanupHandler();
            Handler_Movement = null;

            Handler_Input.CleanupHandler();
            Handler_Input = null;

            Handler_Collision.CleanupHandler();
            Handler_Collision = null;

            Handler_Combat.CleanupHandler();
            Handler_Combat = null;

            Handler_VFX.CleanupHandler();
            Handler_VFX = null;

            Handler_SFX.CleanupHandler();
            Handler_SFX = null;
        }

        #endregion

        #region Unity Editor Only
#if UNITY_EDITOR
        public OTGCombatState CurrentState { get { return m_currentState; } }

#endif

        #endregion
    }
    public enum e_CombatantType
    {
        Player,
        Enemy,
        Prop,
        None
    }

  
}
