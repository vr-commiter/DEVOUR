using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using MyTrueGear;
using Opsive.UltimateCharacterController.Character.Abilities;
using Photon.Bolt;
using Rewired;
using System;
using UnityEngine;

namespace Devour_TrueGear;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BasePlugin
{
    internal static new ManualLogSource Log;
    private static string localPlayerName = null;
    private static TrueGearMod _TrueGear = null;

    public override void Load()
    {
        // Plugin startup logic
        Log = base.Log;

        Harmony.CreateAndPatchAll(typeof(Plugin));
        _TrueGear = new TrueGearMod();

        Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
    }


    [HarmonyPostfix, HarmonyPatch(typeof(NolanBehaviour), "OnStateChange")]
    private static void NolanBehaviour_OnStateChange_Postfix(NolanBehaviour __instance, GameObject obj, string stateName, bool active)
    {
        if (Vector3.Distance(obj.transform.position, Camera.main.transform.position) > 2f)
        {
            return;
        }
        Log.LogInfo("---------------------------------------------");
        Log.LogInfo("OnStateChange");
        Log.LogInfo(Vector3.Distance(obj.transform.position, Camera.main.transform.position));
        if (stateName == "Frying")
        {
            Log.LogInfo("FlashLightFryingTrigger");
            _TrueGear.Play("FlashLightFryingTrigger");
        }
        else if (stateName == "Crouch")
        {
            Log.LogInfo("Crouch");
            _TrueGear.Play("Crouch");
        }
        if (active)
        {
            if (stateName == "Fall")
            {
                Log.LogInfo("Jump");
                _TrueGear.Play("Jump");
            }
            else if (stateName == "DropObject")
            {
                Log.LogInfo("DropObject");
                _TrueGear.Play("DropObject");
            }
            else if (stateName.Contains("LongInteract"))
            {
                Log.LogInfo("StartInteract");
                _TrueGear.StartInteraction();
            }
            else if (stateName == "Poison")
            {
                Log.LogInfo("PlayerDamage");
                _TrueGear.Play("PlayerDamage");
            }
            else if (stateName == "Frying")
            {
                Log.LogInfo("StartFlashLightFryingTrigger");
                _TrueGear.StartFlashLightFrying();
            }
        }
        else
        {
            if (stateName == "Fall")
            {
                Log.LogInfo("OnLand");
                _TrueGear.Play("OnLand");
            }
            else if (stateName.Contains("LongInteract"))
            {
                Log.LogInfo("StopInteract");
                _TrueGear.StopInteraction();
            }
            else if (stateName == "Frying")
            {
                Log.LogInfo("StopFlashLightFryingTrigger");
                _TrueGear.StopFlashLightFrying();
            }

        }
        Log.LogInfo(obj.name);
        Log.LogInfo(__instance.name);
        Log.LogInfo(stateName);
        Log.LogInfo(active);
    }

    [HarmonyPostfix, HarmonyPatch(typeof(NolanBehaviour), "OnKnockoutEvent")]
    private static void NolanBehaviour_OnKnockoutEvent_Postfix(NolanBehaviour __instance, BoltEntity killedBy, BoltEntity player)
    {
        Log.LogInfo("---------------------------------------------");
        Log.LogInfo("OnKnockoutEvent");
        Log.LogInfo(__instance.name);
        Log.LogInfo(player.name);
        Log.LogInfo(killedBy.name);
        Log.LogInfo(Vector3.Distance(player.transform.position, Camera.main.transform.position));
        if (Vector3.Distance(player.transform.position, Camera.main.transform.position) > 2f)
        {
            return;
        }

        Log.LogInfo("Knockout");
        _TrueGear.Play("Knockout");
    }

    [HarmonyPostfix, HarmonyPatch(typeof(NolanBehaviour), "OnPlayerPickedUpEvent")]
    private static void NolanBehaviour_OnPlayerPickedUpEvent_Postfix(NolanBehaviour __instance, BoltEntity killedBy, BoltEntity player, bool inHidingSpot)
    {
        if (Vector3.Distance(player.transform.position, Camera.main.transform.position) > 2f)
        {
            return;
        }
        Log.LogInfo("---------------------------------------------");
        Log.LogInfo("CatchPlayer");
        _TrueGear.Play("CatchPlayer");
        Log.LogInfo(__instance.name);
        Log.LogInfo(killedBy.name);
        Log.LogInfo(player.name);
        Log.LogInfo(inHidingSpot);
        Log.LogInfo(Vector3.Distance(player.transform.position, Camera.main.transform.position));
    }

    [HarmonyPostfix, HarmonyPatch(typeof(NolanBehaviour), "StartHeartbeat")]
    private static void NolanBehaviour_StartHeartbeat_Postfix(NolanBehaviour __instance)
    {
        if (Vector3.Distance(__instance.transform.position, Camera.main.transform.position) > 2f)
        {
            return;
        }
        Log.LogInfo("---------------------------------------------");
        Log.LogInfo("StartHeartbeat");
        _TrueGear.StartHeartBeat();
        Log.LogInfo(__instance.name);
        Log.LogInfo(Vector3.Distance(__instance.transform.position, Camera.main.transform.position));
    }

    [HarmonyPostfix, HarmonyPatch(typeof(NolanBehaviour), "StopHeartbeat")]
    private static void NolanBehaviour_StopHeartbeat_Postfix(NolanBehaviour __instance)
    {
        if (Vector3.Distance(__instance.transform.position, Camera.main.transform.position) > 3.5f)
        {
            return;
        }
        Log.LogInfo("---------------------------------------------");
        Log.LogInfo("StopHeartbeat");
        _TrueGear.StopHeartBeat();
        Log.LogInfo(__instance.name);
        Log.LogInfo(Vector3.Distance(__instance.transform.position, Camera.main.transform.position));
    }

    [HarmonyPostfix, HarmonyPatch(typeof(NolanBehaviour), "ActivateCarryObject")]
    private static void NolanBehaviour_ActivateCarryObject_Postfix(NolanBehaviour __instance)
    {
        if (Vector3.Distance(__instance.transform.position, Camera.main.transform.position) > 2f)
        {
            return;
        }
        Log.LogInfo("---------------------------------------------");
        Log.LogInfo("HoldObject");
        _TrueGear.Play("HoldObject");
        Log.LogInfo(__instance.name);
        Log.LogInfo(Vector3.Distance(__instance.transform.position, Camera.main.transform.position));
    }

    [HarmonyPostfix, HarmonyPatch(typeof(NolanBehaviour), "Attached")]
    private static void NolanBehaviour_Attached_Postfix(NolanBehaviour __instance)
    {
        Log.LogInfo("---------------------------------------------");
        Log.LogInfo("Attached");
        if (Vector3.Distance(__instance.transform.position, Camera.main.transform.position) < 2f)
        {
            Log.LogInfo("PlayerSpawn");
            _TrueGear.Play("PlayerSpawn");

            localPlayerName = __instance.name;
        }
        Log.LogInfo(Vector3.Distance(__instance.transform.position, Camera.main.transform.position));
    }

    [HarmonyPrefix, HarmonyPatch(typeof(NolanBehaviour), "Detached")]
    private static void NolanBehaviour_Detached_Prefix(NolanBehaviour __instance)
    {
        if (__instance.name == localPlayerName)
        {
            Log.LogInfo("---------------------------------------------");
            Log.LogInfo("Detached");
            Log.LogInfo(__instance.name);
            Log.LogInfo(Vector3.Distance(__instance.transform.position, Camera.main.transform.position));
            localPlayerName = null;
        }
    }


    [HarmonyPostfix, HarmonyPatch(typeof(GameUI), "PauseGame")]
    private static void GameUI_PauseGame_Postfix(GameUI __instance)
    {
        Log.LogInfo("---------------------------------------------");
        Log.LogInfo("PauseGame");
        _TrueGear.Pause();
    }

    [HarmonyPostfix, HarmonyPatch(typeof(GameUI), "UnpauseGame")]
    private static void GameUI_UnpauseGame_Postfix(GameUI __instance)
    {
        Log.LogInfo("---------------------------------------------");
        Log.LogInfo("UnpauseGame");
        _TrueGear.UnPause();
    }


    [HarmonyPostfix, HarmonyPatch(typeof(NolanBehaviour), "OnDemonHoldingPlayer")]
    private static void NolanBehaviour_OnDemonHoldingPlayer_Postfix(NolanBehaviour __instance, BoltEntity player)
    {
        if(Vector3.Distance(player.transform.position, Camera.main.transform.position) > 2f)
        {
            return;
        }
        Log.LogInfo("---------------------------------------------");
        Log.LogInfo("DemonHoldingPlayer");
        _TrueGear.Play("DemonHoldingPlayer");
        Log.LogInfo(__instance.name);
        Log.LogInfo(player.name);
        Log.LogInfo(Vector3.Distance(player.transform.position, Camera.main.transform.position));
    }


    [HarmonyPrefix, HarmonyPatch(typeof(Interact), "DoInteract")]
    private static void Interact_DoInteract_Prefix(Interact __instance)
    {
        if (Vector3.Distance(__instance.m_GameObject.transform.position, Camera.main.transform.position) > 2f)
        {
            return;
        }
        try
        {
            if (__instance.m_Interactable == null)
            {
                return;
            }
            Log.LogInfo("---------------------------------------------");
            Log.LogInfo("DoInteract");
            string interactItemName = __instance.m_Interactable.gameObject.name;
            if (interactItemName.Contains("Key") || interactItemName.Contains("SM_") || interactItemName.Contains("Rose") || interactItemName.Contains("CherryBlossom") || interactItemName.Contains("Patch"))
            {
                Log.LogInfo("CollectItem");
                _TrueGear.Play("CollectItem");
            }
            else if (interactItemName.ToLower().Contains("door") || interactItemName.ToLower() == "openleft" || interactItemName.ToLower() == "openright")
            {
                Log.LogInfo("DoorOpened");
                _TrueGear.Play("DoorOpened");
            }
            else if (interactItemName.Contains("Note"))
            {
                Log.LogInfo("ReadNote");
                _TrueGear.Play("ReadNote");
            }
            Log.LogInfo(__instance.m_Interactable.gameObject.name);
            Log.LogInfo(__instance.m_GameObject.name);
            Log.LogInfo(Vector3.Distance(__instance.m_GameObject.transform.position, Camera.main.transform.position));
        }
        catch { }
    }


    [HarmonyPostfix, HarmonyPatch(typeof(UseBattery), "AbilityStarted")]
    private static void UseBattery_AbilityStarted_Postfix(UseBattery __instance)
    {
        if (Vector3.Distance(__instance.behaviour.transform.position, Camera.main.transform.position) > 2f)
        {
            return;
        }
        Log.LogInfo("---------------------------------------------");
        Log.LogInfo("BatteryUsed");
        _TrueGear.Play("BatteryUsed");
        Log.LogInfo(__instance.behaviour.name);
        Log.LogInfo(Vector3.Distance(__instance.behaviour.transform.position, Camera.main.transform.position));
    }


    [HarmonyPostfix, HarmonyPatch(typeof(NolanBehaviour), "OnReviveEvent")]
    private static void NolanBehaviour_OnReviveEvent_Postfix(NolanBehaviour __instance, BoltEntity player, BoltEntity reviver)
    {
        Log.LogInfo("---------------------------------------------");
        Log.LogInfo("OnReviveEvent");
        Log.LogInfo(player.name);
        Log.LogInfo(reviver.name);
        Log.LogInfo(Vector3.Distance(player.transform.position, Camera.main.transform.position));
        if (Vector3.Distance(player.transform.position, Camera.main.transform.position) > 2f)
        {
            return;
        }
        Log.LogInfo("Revive");
        _TrueGear.Play("Revive");
    }



}
