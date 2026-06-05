using BepInEx;
using HarmonyLib;
using UnityEngine;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

[BepInPlugin(ModGUID, ModName, ModVersion)]
public class Plugin : BaseUnityPlugin
{
	public const string ModGUID = "zopthemop.betterstaffoffrostvisibility";
	public const string ModName = "Better Staff of Frost Visibility";
	public const string ModVersion = "1.0.0";
	public const string ModDescription = "Improves visibility while using Staff of Frost by reducing the size of its effects";

    private void Awake()
    {
        Harmony harmony = new(ModGUID);
        harmony.PatchAll();
    }
	
	[HarmonyPatch(typeof(ZNetScene), "Awake")]
	public static class ZNetSceneAwakePatch
	{
		private static void Postfix(ZNetScene __instance)
		{
			GameObject prefab = __instance.GetPrefab("fx_iceshard_launch");
			if (prefab == null)
				return;

			// Make the cloud around the staff tiny
			foreach (Renderer renderer in prefab.GetComponentsInChildren<Renderer>(true))
			{
				renderer.transform.localScale *= 0.10f;
			}
		}
	}

	[HarmonyPatch(typeof(ZNetScene), "Awake")]
	public static class ScaleIceShardHitEffectPatch
	{
		private static void Postfix(ZNetScene __instance)
		{
			GameObject prefab = __instance.GetPrefab("fx_iceshard_hit");
			if (prefab == null)
				return;

			// Make the clouds around the projectile hits smaller
			foreach (Renderer renderer in prefab.GetComponentsInChildren<Renderer>(true))
			{
				renderer.transform.localScale *= 0.15f;
			}
		}
	}
}
