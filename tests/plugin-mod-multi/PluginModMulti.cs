using UnityEngine;

#if KSP_RETOOLED != UNITY_6_7
#error KSP_RETOOLED and UNITY_6_7 should always be defined together
#endif

public class PluginModMulti : MonoBehaviour
{
#if KSP_RETOOLED
    public const string Game = "Retooled";

    // Only compiles if Unity 6 and its packages are referenced
    public Unity.Mathematics.float3 position;
#else
    public const string Game = "Stock";
#endif

    private void Start()
    {
        Debug.Log("Built for " + Game + " on KSP " + Versioning.version_major + "." + Versioning.version_minor);
    }
}
