using UnityEditor;
using UnityEngine;

namespace Kogane.Internal
{
    [InitializeOnLoad]
    internal static class IL2CPPMenuItem
    {
        private const string MENU_ITEM_NAME = "Kogane/IL2CPP";

        static IL2CPPMenuItem()
        {
            EditorApplication.delayCall += () => UpdateChecked();
        }

        [MenuItem( MENU_ITEM_NAME )]
        private static void Change()
        {
            var newBackend = IsIL2CPP()
                    ? ScriptingImplementation.Mono2x
                    : ScriptingImplementation.IL2CPP
                ;

            var buildTarget      = EditorUserBuildSettings.activeBuildTarget;
            var buildTargetGroup = BuildPipeline.GetBuildTargetGroup( buildTarget );

            PlayerSettings.SetScriptingBackend( buildTargetGroup, newBackend );

            foreach ( var editorWindow in Resources.FindObjectsOfTypeAll<EditorWindow>() )
            {
                editorWindow.Repaint();
            }

            UpdateChecked();
        }

        private static void UpdateChecked()
        {
            Menu.SetChecked( MENU_ITEM_NAME, IsIL2CPP() );
        }

        private static bool IsIL2CPP()
        {
            var buildTarget      = EditorUserBuildSettings.activeBuildTarget;
            var buildTargetGroup = BuildPipeline.GetBuildTargetGroup( buildTarget );
            var currentBackend   = PlayerSettings.GetScriptingBackend( buildTargetGroup );

            return currentBackend == ScriptingImplementation.IL2CPP;
        }
    }
}