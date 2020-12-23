#pragma warning disable CS0649

using UnityEngine;

namespace OTG.CombatSM.Core
{
    [CreateAssetMenu(menuName = OTGCombatUtilities.DataGroupPath+ "HandlerDataGroup", fileName ="HandlerDataGroup")]
    public class HandlerDataGroup : ScriptableObject
    {
        #region Inspector Vars
        [SerializeField] private AnimationHandlerData m_animHandlerData;
        [SerializeField] private MovementHandlerData m_moveHandlerData;
        [SerializeField] private InputHandlerData m_inputHandlerData;
        [SerializeField] private CollisionsHandlerData m_collisionHandlerData;
        [SerializeField] private CombatHandlerData m_combatHandlerData;
        #endregion

        #region Properties
        public AnimationHandlerData AnimHandlerData { get { return m_animHandlerData; } }
        public MovementHandlerData MoveHandlerData { get { return m_moveHandlerData; } }
        public InputHandlerData InputsHandlerData { get { return m_inputHandlerData; } }
        public CollisionsHandlerData CollisionHandlerData { get { return m_collisionHandlerData; } }
        public CombatHandlerData CombatsHandlerData { get { return m_combatHandlerData; } }
        #endregion
    }
}