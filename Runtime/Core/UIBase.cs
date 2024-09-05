using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Congroo.UI
{
    public enum UIStatus
    {
        None,
        Creating,
        Showing,
        Hiding,
        Destroying,
    }
    
    [RequireComponent(typeof(RequireComponent))]
    [DisallowMultipleComponent]
    public abstract class UIBase : MonoBehaviour
    {
        /// <summary>
        /// �Ƿ��Զ�����
        /// </summary>
        public bool AutoDestroy = true;
        public UIStatus Status = UIStatus.None;
        
        public UIData DataReference;
        
        protected internal UniTask InnerOnCreate() => OnCreate();
        protected internal UniTask InnerOnRefresh() => OnRefresh();
        protected internal void InnerOnBind() => OnBind();
        protected internal void InnerOnUnbind() => OnUnbind();
        protected internal void InnerOnShow() => OnShow();
        protected internal void InnerOnHide() => OnHide();
        protected internal void InnerOnDied() => OnDied();
        
        
        /// <summary>
        /// ����ʱ���ã�����������ִֻ��һ��
        /// </summary>
        protected virtual UniTask OnCreate() => UniTask.CompletedTask;
    
        /// <summary>
        /// ˢ��ʱ����
        /// </summary>
        protected virtual UniTask OnRefresh() => UniTask.CompletedTask;
    
        /// <summary>
        /// ���¼�
        /// </summary>
        protected virtual void OnBind() { }
    
        /// <summary>
        /// ����¼�
        /// </summary>
        protected virtual void OnUnbind() { }
    
        /// <summary>
        /// ��ʾʱ����
        /// </summary>
        protected virtual void OnShow() { }
    
        /// <summary>
        /// ����ʱ����
        /// </summary>
        protected virtual void OnHide() { }
    
        /// <summary>
        /// ����ʱ���ã�����������ִֻ��һ��
        /// </summary>
        protected virtual void OnDied() { }
    }
}
