
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace OTG.CombatSystem.Editor
{
    public static class OTGActionAndTransitionLibrary
    {
        public static List<OTGCombatAction> AvailableActions;
        public static List<OTGTransitionDecision> AvailableTransitions;


        static OTGActionAndTransitionLibrary()
        {
            AvailableActions = new List<OTGCombatAction>();
            AvailableTransitions = new List<OTGTransitionDecision>();
        }

    }
}

