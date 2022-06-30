using Habby.Guild;
using UnityEditor;
using UnityEngine;

public class TIMEditorTool : Editor
{
    [UnityEditor.Callbacks.DidReloadScripts]
    private static void OnScriptReload()
    {
        if (GuildSDKManager.IsHaveInstance)
        {
            if (GuildSDKManager.InteractiveModule != null)
            {
                GuildSDKManager.InteractiveModule.UInitIM();
            }
            
        }
    }
}