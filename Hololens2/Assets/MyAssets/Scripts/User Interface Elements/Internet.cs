using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    [AddComponentMenu("Scripts/Internet")]
    public class Internet : MonoBehaviour
{
    public void launchWebsite(string uri) {
        Debug.Log($"LaunchUri: Launching {uri}");

#if UNITY_WSA
            UnityEngine.WSA.Launcher.LaunchUri(uri, false);
#else
        Application.OpenURL(uri);
#endif

    }
}
