using HarmonyLib;
using SLZ.Bonelab;
using SLZ.Data;

namespace ArenaFix.HarmonyPatches
{
    [HarmonyPatch(typeof(Arena_GameController))]
    [HarmonyPatch(nameof(Arena_GameController.NPC_Registration))]
    // ReSharper disable once InconsistentNaming
    public static class Arena_GameController_NPC_Registration
    {
        [HarmonyPrefix]
        // ReSharper disable once UnusedMember.Local
        private static void Prefix(NPC_Data data)
        {
            // Spawns sometimes get stuck and are unable to enter the arena, the game kills them after 2
            // minutes anyway if they dont enter the arena, this just reduces that timer.
            EnemyProfile profile = data.enemyProfile;
            if (profile.enemyTitle != "OMNI PROJECTOR HAZMAT") // Omni projectors hang about up top sometimes
                profile.entranceTimeVal = 45f; // Original is 120f, which is way too long imo
        }
    }
}
