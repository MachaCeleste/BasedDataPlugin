using HarmonyLib;
using Newtonsoft.Json;
using System;
using System.Text;

[HarmonyPatch]
public class DatabasePatch
{
    [HarmonyPatch(typeof(Database), "GetContenido")]
    class GetContenidoPatch
    {
        static void Postfix(ref string __result)
        {
            if (!string.IsNullOrEmpty(__result))
            {
                __result = Encoding.UTF8.GetString(Convert.FromBase64String(__result));
            }
        }
    }

    [HarmonyPatch(typeof(Database), "SetContenido")]
    class SetContenidoPatch
    {
        static void Prefix(ref FileSystem.Archivo file, ref string contenido, ref string prevID)
        {
            contenido = Convert.ToBase64String(Encoding.UTF8.GetBytes(contenido));
        }
    }

    [HarmonyPatch(typeof(Database), "AddLibraryContent")]
    class AddLibraryContentPatch
    {
        static void Prefix(ref string ID, ref string contenido)
        {
            contenido = Convert.ToBase64String(Encoding.UTF8.GetBytes(contenido));
        }
    }
}