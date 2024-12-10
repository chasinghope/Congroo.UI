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
"); 
            


        }
        
        public static void DoCreate(string template)
        {
            if (Selection.objects.Length > 0)
            {
                GameObject prefab = Selection.objects[0] as GameObject;
                if (prefab != null)
                {
                    string currentDir = ReflactionUtils.RunClassFunc<string>(typeof(ProjectWindowUtil), "GetActiveFolderPath");
                    var result = AddCodeSnippetToTemplate(template, prefab.name);
                    string scriptPath = Path.Combine(Application.dataPath, "..", currentDir, $"{prefab.name}.cs");
                    File.WriteAllText(scriptPath, result);
                    AssetDatabase.Refresh();
                    EditorApplication.delayCall += () => AttachScriptToPrefab(prefab);
                }
            }
        }

        private static void DelayedAttachScriptToPrefab()
        {
            // 脚本已经编译完成，执行挂载操作
            AttachScriptToPrefab(Selection.activeGameObject);
            EditorApplication.update -= DelayedAttachScriptToPrefab;
        }
        
        public static string AddCodeSnippetToTemplate(string template, string fileName)
        {
            var script = template;
            script = script.Replace("#SCRIPTNAME#", fileName);
            script = Regex.Replace(script, @"^ +\n", "\n", RegexOptions.Multiline);
            script = Regex.Replace(script, @"^ +\r\n", "\r\n", RegexOptions.Multiline);
            return script;
        }


        public static void AttachScriptToPrefab(GameObject prefabObject)
        {
            Debug.Log("AttachScriptToPrefab.");

            // 创建脚本实例并挂载到预制体上
            System.Type type = System.Type.GetType($"Game.UI.{prefabObject.name}");
            if (type == null)
            {
                Debug.Log($"预制体, 挂载空脚本.");
                return;
            }
            
            prefabObject.AddComponent(type);

            // 保存预制体并关闭预制体内容
            PrefabUtility.SaveAsPrefabAsset(prefabObject, AssetDatabase.GetAssetPath(prefabObject));
            PrefabUtility.UnloadPrefabContents(prefabObject);
        }
    }
}