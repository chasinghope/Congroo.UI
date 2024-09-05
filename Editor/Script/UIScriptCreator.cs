using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace Congroo.UI.Editor
{
    public static class UIScriptCreator
    {
        [MenuItem("Assets/Create/CongrooUI/UI Component", false, 0)]
        public static void CreateUIComponent()
        {
            DoCreate(@"using System;
using UnityEngine;
using UnityEngine.UI;
using Congroo.UI;
using Cysharp.Threading.Tasks;

namespace Game.UI
{
    public class #SCRIPTNAME#Data : UIData
    {
    }

    [UILayer(EUILayer.Panel)]
    public class #SCRIPTNAME# : UIComponent<#SCRIPTNAME#Data>
    {
        #region UILifeCycle

        protected override UniTask OnCreate()
        {
            return UniTask.CompletedTask;
        }

        protected override UniTask OnRefresh()
        {
            return UniTask.CompletedTask;
        }

        protected override void OnBind()
        {

        }

        protected override void OnUnbind()
        {
        }

        protected override void OnShow()
        {
        }

        protected override void OnHide()
        {
        }

        protected override void OnDied()
        {
        }

        #endregion
    }
}
"); }
        
        public static void DoCreate(string template)
        {
            if (Selection.objects.Length > 0)
            {
                GameObject prefab = Selection.objects[0] as GameObject;
                if (prefab != null)
                {
                    string currentDir = ReflactionUtils.RunClassFunc<string>(typeof(ProjectWindowUtil), "GetActiveFolderPath");
                    var result = AddCodeSnippetToTemplate(template, prefab.name);
                    File.WriteAllText(Path.Combine(Application.dataPath, "..", currentDir, $"{prefab.name}.cs"), result);
                    AssetDatabase.Refresh();
                }
            }
        }
        
        public static string AddCodeSnippetToTemplate(string template, string fileName)
        {
            var script = template;
            script = script.Replace("#SCRIPTNAME#", fileName);
            script = Regex.Replace(script, @"^ +\n", "\n", RegexOptions.Multiline);
            script = Regex.Replace(script, @"^ +\r\n", "\r\n", RegexOptions.Multiline);
            return script;
        }
    }
}