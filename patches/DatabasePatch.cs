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
                try
                {
                    JsonConvert.DeserializeObject<Library>(__result);
                }
                catch (Exception ex)
                {
                    __result = Encoding.UTF8.GetString(Convert.FromBase64String(__result));
                }
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
}