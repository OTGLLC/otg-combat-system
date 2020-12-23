#pragma warning disable CS0649


using System.Collections.Generic;
using UnityEngine;
 

namespace OTG.CombatSM.Core
{
    [CreateAssetMenu(menuName =OTGCombatUtilities.CombatStatePath,fileName ="OTGCombatState")]
    public class OTGCombatState : ScriptableObject
    {
        #region Fields

        #endregion

        #region Inspector Vars
        [SerializeField] private OTGCombatAnimation m_combatAnim;
        [SerializeField] private OTGCombatAction[] m_onEnterActions;
        [SerializeField] private OTGCombatAction[] m_onUpdateActions;
        [SerializeField] private OTGCombatAction[] m_animUpdateActions;
        [SerializeField] private OTGCombatAction[] m_onExitActions;
        [SerializeField] private CombatStateTransition[] m_stateTransitions;
        #endregion

        #region Properties
        public ushort ID { get; private set; }
        #endregion

        #region Fields
        private float m_stateTime;
        private bool m_stateReEntered;
        #endregion


        #region Public API
        public void AssignID(ushort _id)
        {
            ID = _id;
        }
        public void OnStateEnter(OTGCombatSMC _controller, bool _reentry = false)
        {
            m_stateReEntered = _reentry;
            SetAnimationData(_controller);
            SetHurtColliderData(_controller);
            SetHitColliderData(_controller);
            PerformActions(m_onEnterActions, _controller);
            SetSoundFXData(_controller);

            if (_reentry)
            {
                return;
            }

            ResetStateTime();
            UpdateHandlerStateTime(_controller);
            PlayAnimation(_controller);
            
        }
        public void OnStateUpdate(OTGCombatSMC _controller)
        {
            UpdateHandlerStateTime(_controller);
            PerformActions(m_onUpdateActions, _controller);
            EvaluateTransitions(_controller);
            IncrementStateTime();
            
        }
        public void OnStateAnimUpdate(OTGCombatSMC _controller)
        {
            PerformActions(m_animUpdateActions, _controller);
        }
        public void OnStateExit(OTGCombatSMC _controller)
        {
           
            PerformActions(m_onExitActions, _controller);
            m_stateReEntered = false;
        }
        #endregion

        #region Utility
        private void PerformActions(OTGCombatAction[] _actions, OTGCombatSMC _controller)
        {
            for(int i = 0; i < _actions.Length; i++)
            {
                _actions[i].Act(_controller);
            }
        }
        private void PlayAnimation(OTGCombatSMC _controller)
        {
            if (m_combatAnim == null || m_combatAnim.AnimClip == null)
                return;

            _controller.Handler_Animation.PlayAnimation(m_combatAnim.AnimClip);

        }
        private void EvaluateTransitions(OTGCombatSMC _controller)
        {
            for(int i = 0; i < m_stateTransitions.Length; i++)
            {
                m_stateTransitions[i].MakeDecision(_controller);
            }
        }
        private void SetHitColliderData(OTGCombatSMC _controller)
        {
            CombatAnimHitCollisionData data = m_combatAnim.HitCollisionData;
            _controller.Handler_Collision.HitCollider.OnDataUpdate(data);
        }
        private void SetHurtColliderData(OTGCombatSMC _controller)
        {
            CombatAnimHurtCollisionData data = m_combatAnim.HurtCollisionData;
            _controller.Handler_Collision.SetHurtColliderData(data);
        }
        private void ResetStateTime()
        {
            m_stateTime = 0;
        }
        private void IncrementStateTime()
        {
            m_stateTime += Time.deltaTime;
        }
        private void UpdateHandlerStateTime(OTGCombatSMC _controller)
        {
            _controller.Handler_Animation.UpdateStateTime(m_stateTime);
        }
        private void SetAnimationData(OTGCombatSMC _controller)
        {
            if (m_combatAnim.AnimClip == null)
                return;

            _controller.Handler_Animation.UpdateAnimData(m_combatAnim.AnimData);
        }
       
        private void SetSoundFXData(OTGCombatSMC _controller)
        {
            if (m_combatAnim.AnimClip == null)
                return;
            _controller.Handler_SFX.SetData(m_combatAnim.SoundFX);
        }
        #endregion

        #region Unity Editor Only
#if UNITY_EDITOR
        public float StateTime { get { return m_stateTime; } }
        public bool StateReEntered { get { return m_stateReEntered; } }
#endif
        #endregion

    }

    [System.Serializable]
    public struct CombatStateTransition
    {
        [SerializeField] private OTGCombatState m_nextState;
        [SerializeField] private OTGTransitionDecision[] m_decisions;
        [SerializeField] private bool m_usePreviousState;

        public void MakeDecision(OTGCombatSMC _controller)
        {
            for(int i = 0; i < m_decisions.Length; i++)
            {
                if (!m_decisions[i].Decide(_controller))
                    return;
            }
            _controller.OnChangeStateRequested(m_nextState,m_usePreviousState);
        }
    }

}
