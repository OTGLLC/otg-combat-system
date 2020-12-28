
using UnityEngine;

namespace OTG.CombatSystem
{
    [CreateAssetMenu(menuName ="OTG/Test/TestTransition",fileName ="TestTransition")]
    public class TestTransition:OTGTransitionDecision
    {
        public override bool Decide(OTGCombatSMC _controller)
        {
            return true;
        }
    }
}
