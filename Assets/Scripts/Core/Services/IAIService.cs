using UnityEngine;
using FTL.Core.Components;

namespace FTL.Core.Services
{
    public interface IAIService
    {
        void AssignWeaponToAI(AI.EnemyAI ai, WeaponComponent weapon);
        void StartAI(AI.EnemyAI ai);
        void StopAI(AI.EnemyAI ai);
    }
}
