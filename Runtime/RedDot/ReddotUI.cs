using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Congroo.UI
{
    public class ReddotUI : MonoBehaviour,IPointerClickHandler
    {
        public string Path;
        public List<string> Args = new List<string>();
        [SerializeField] private GameObject mDot;
        [SerializeField] private Text mTCount;
        [SerializeField] private bool mIsShowCnt = true;


        private string mRealPath;
        
        private void Awake()
        {
            mDot.gameObject.SetActive(false);
            mTCount.gameObject.SetActive(mIsShowCnt);
        }

        private void OnEnable()
        {
            mRealPath = Path;
            if (Args != null && Args.Count > 0)
            {
                if (Args.Count == 1)
                {
                    mRealPath = string.Format(Path, Args[0]);
                }
                else if (Args.Count == 2)
                {
                    mRealPath = string.Format(Path, Args[0], Args[1]);
                }
                else if (Args.Count == 3)
                {
                    mRealPath = string.Format(Path, Args[0], Args[1], Args[2]);
                }
                else
                {
                    throw new Exception("ReddotUI args count is too much!");
                }
            }
            TreeNode node = ReddotMgr.Ins.AddListener(mRealPath, ReddotCallback);
#if UNITY_EDITOR
            gameObject.name = string.Format("[Reddot]={0}", node.FullPath);
#endif
        }
    
        private void ReddotCallback(int value)
        {
            // Debug.Log("红点刷新，路径:" + Path + ",当前帧数:" + Time.frameCount + ",值:" + value);
            mDot.SetActive(value > 0);
            if (mIsShowCnt)
            {
                if (mTCount != null)
                {
                    mTCount.text = value.ToString();
                }
            }
        }
    
        public void OnPointerClick(PointerEventData eventData)
        {
            int value = ReddotMgr.Ins.GetValue(mRealPath);
    
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                ReddotMgr.Ins.ChangeValue(mRealPath, value + 1);
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                ReddotMgr.Ins.ChangeValue(mRealPath, Mathf.Clamp(value - 1,0, value));
            }
        }
    }
}

