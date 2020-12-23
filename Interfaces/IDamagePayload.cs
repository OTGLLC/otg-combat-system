using System.Collections;
using UnityEngine;

namespace OTG.CombatSM.Core
{
    public interface IDamagePayload
    {
        float GetStunTime();
        float GetDamage();
        Vector3 GetImpactForce();

    }
}