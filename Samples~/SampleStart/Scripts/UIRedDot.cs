using System;
using Congroo.UI;
using Cysharp.Threading.Tasks;
using Samples.SampleStart;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class UIRedDotData : UIData
    {
    }

    [UILayer(EUILayer.Panel)]
    public class UIRedDot : UIComponent<UIRedDotData>
    {
        [SerializeField] private Button mBtnT1;
        [SerializeField] private Button mBtnT2;
        
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
            mBtnT1.onClick.AddListener(T1_OnClick);
            mBtnT2.onClick.AddListener(T2_OnClick);
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


        private void Update()
        {
            ReddotMgr.Ins.Update();
        }

        public int RedDotValue = 0;
        
        private void T1_OnClick()
        {
            Debug.Log("T1_OnClick");
            RedDotValue++;
            ReddotMgr.Ins.ChangeValue("System/Chat/Friend/Gift1", RedDotValue);
            ReddotMgr.Ins.ChangeValue("System/Chat/Friend/Gift2", RedDotValue);
        }
        
        private void T2_OnClick()
        {
            Debug.Log("T2_OnClick");
            RedDotValue--;
            ReddotMgr.Ins.ChangeValue(RedDotConst.Mail, RedDotValue);
        }
    }
}
