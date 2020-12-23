using System.Collections;
using UnityEngine;

namespace OTG.CombatSystem
{
    public interface IDamagePayload
    {
        float GetStunTime();
        float GetDamage();
        Vector3 GetImpactForce();

    }
}