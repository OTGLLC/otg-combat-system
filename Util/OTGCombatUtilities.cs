


namespace OTG.CombatSM.Core
{
    public enum E_NewCombatStateTemplate
    {
        AttackTemplate,
        IdleTemplate,
        DashTemplate,
        HitStunTemplate,
        HitStopTemplate,
        KnockdownTemplate,
        KnockBackTemplate
    }


    public static class OTGCombatUtilities
    {
       
        public const string OTGRoot = "OTG/";
        public const string CombatStateMachinePath = OTGRoot + "CombatStateMachine/";
        public const string CombatStatePath = CombatStateMachinePath + "CombatState";
        public const string DataGroupPath = CombatStateMachinePath + "DataGroups/";

        public const string CombatActionPath = CombatStateMachinePath + "Actions/";
        public const string CombatTransitionDecisionPath = CombatStateMachinePath + "TransitionDecisions/";
    }
}