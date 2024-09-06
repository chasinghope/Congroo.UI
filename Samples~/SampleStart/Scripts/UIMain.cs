using Congroo.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class UIMainData : UIData
    {
    }

    [UILayer(EUILayer.Panel)]
    public class UIMain : UIComponent<UIMainData>
    {
        [SerializeField] private Button mBtnStartGame;
        [SerializeField] private Button mBtnQuitGame;
        
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
            mBtnStartGame.onClick.AddListener(StartGame_OnClick);
            mBtnQuitGame.onClick.AddListener(QuitGame_OnClick);
        }

        protected override void OnUnbind()
        {
            mBtnStartGame.onClick.RemoveListener(StartGame_OnClick);
            mBtnQuitGame.onClick.RemoveListener(QuitGame_OnClick);
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
        
        
        


        private void StartGame_OnClick()
        {
            Debug.Log("Start Game");
            UIMgr.Ins.Open<UIRedDot>();
        }
        
        private void QuitGame_OnClick()
        {
            Debug.Log("Quit Game");
        }
    }
}
