using System;
using HarmonyLib;
using SLZ.AI;
using SLZ.Bonelab;

namespace ArenaFix.HarmonyPatches
{
    [HarmonyPatch(typeof(Arena_GameController))]
    [HarmonyPatch(nameof(Arena_GameController.Start))]
    // ReSharper disable once InconsistentNaming
    public static class Arena_GameController_Start
    {
        [HarmonyPostfix]
        // ReSharper disable once InconsistentNaming
        // ReSharper disable once UnusedMember.Local
        private static void Postfix(Arena_GameController __instance)
        {
            // If an NPC gets thrown out of the arena, it just kinda sits there
            GenGameControl_Trigger arenaBounds = __instance.NPCEntranceTrigger;
            arenaBounds.OnNPC_ProxyExit += (Action<TriggerRefProxy>)(triggerRefProxy =>
            {
                // if an npc leaves the arena and doesnt get back in, then kill it
                triggerRefProxy._aiManager.StartArenaEntranceTimer(45f);
            });
        }
    }
}
