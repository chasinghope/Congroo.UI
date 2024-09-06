using System;
using System.Collections.Generic;
using System.Net.Mime;
using Cysharp.Threading.Tasks;
using Game.UI;
using UnityEngine;
using Object = UnityEngine.Object;
namespace Congroo.UI.SampleStart
{
    public class SampleStartInit : MonoBehaviour
    {
        private void Start()
        {
            UIMgr.Ins.Initialize();

            UIMgr.Ins.OnAssetRequest += Test_OnAssetRequestHandler;
            UIMgr.Ins.OnAssetRelease += Test_OnAssetReleaseHandler;

            UIMgr.Ins.Open<UIMain>();
        }
        
        #region UI资源
        
        public Dictionary<Type, Object> mPrefabResDict = new ();
        public const string UI_PATH = "UI/";
        
        private void Test_OnAssetReleaseHandler(Type rType)
        {
            if (mPrefabResDict.TryGetValue(rType, out Object res))
            {
                // Resources.UnloadAsset(res);
                mPrefabResDict.Remove(rType);
            }
        }

        private async UniTask<GameObject> Test_OnAssetRequestHandler(Type rType)
        {
            string path = UI_PATH + rType.Name;
            ResourceRequest request = Resources.LoadAsync<GameObject>(path);
            var res = await request;
            GameObject resPrefab = request.asset as GameObject;
            mPrefabResDict[rType] = request.asset;
            return resPrefab;
        }
        
        #endregion
        
    }
}