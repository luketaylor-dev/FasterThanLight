using UnityEngine;

namespace FTL.Core.Services
{
    public class AIService : IAIService
    {
        public void AssignWeaponToAI(AI.EnemyAI ai, WeaponComponent weapon)
        {
            if (ai != null && weapon != null)
            {
                ai.SetWeapon(weapon);
            }
        }

        public void StartAI(AI.EnemyAI ai)
        {
            if (ai != null)
            {
                ai.enabled = true;
            }
        }

        public void StopAI(AI.EnemyAI ai)
        {
            if (ai != null)
            {
                ai.enabled = false;
            }
        }
    }
}
