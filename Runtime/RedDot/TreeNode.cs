﻿using System;
using System.Collections.Generic;


namespace Congroo.UI
{
    /// <summary>
    /// 树节点
    /// </summary>
    public class TreeNode
    {

        /// <summary>
        /// 子节点
        /// </summary>
        private Dictionary<RangeString, TreeNode> mChildren;

        /// <summary>
        /// 节点值改变回调
        /// </summary>
        private Action<int> mChangeCallback;

        /// <summary>
        /// 完整路径
        /// </summary>
        private string mFullPath;

        /// <summary>
        /// 节点名
        /// </summary>
        public string Name { get; private set; }


        /// <summary>
        /// 完整路径
        /// </summary>
        public string FullPath
        {
            get
            {
                if (string.IsNullOrEmpty(mFullPath))
                {
                    if (Parent == null || Parent == ReddotMgr.Ins.Root)
                    {
                        mFullPath = Name;
                    }
                    else
                    {
                        mFullPath = Parent.FullPath + ReddotMgr.Ins.SplitChar + Name;
                    }
                }

                return mFullPath;
            }
        }

        /// <summary>
        /// 节点值
        /// </summary>
        public int Value { get; private set; }


        /// <summary>
        /// 父节点
        /// </summary>
        public TreeNode Parent { get; private set; }

        /// <summary>
        /// 子节点
        /// </summary>
        public Dictionary<RangeString, TreeNode>.ValueCollection Children => mChildren?.Values;


        /// <summary>
        /// 子节点数量
        /// </summary>
        public int ChildrenCount
        {
            get
            {
                if (mChildren == null)
                {
                    return 0;
                }

                int sum = mChildren.Count;
                foreach (TreeNode node in mChildren.Values)
                {
                    sum += node.ChildrenCount;
                }
                return sum;
            }
        }

        public TreeNode(string name)
        {
            Name = name;
            Value = 0;
            mChangeCallback = null;
        }

        public TreeNode(string name, TreeNode parent) : this(name)
        {
            Parent = parent;
        }

        /// <summary>
        /// 添加节点值监听
        /// </summary>
        public void AddListener(Action<int> callback)
        {
            mChangeCallback += callback;
        }

        /// <summary>
        /// 移除节点值监听
        /// </summary>
        public void RemoveListener(Action<int> callback)
        {
            mChangeCallback -= callback;
        }

        /// <summary>
        /// 移除所有节点值监听
        /// </summary>
        public void RemoveAllListener()
        {
            mChangeCallback = null;
        }

        /// <summary>
        /// 改变节点值（使用传入的新值，只能在叶子节点上调用）
        /// </summary>
        public void ChangeValue(int newValue)
        {
            if (mChildren != null && mChildren.Count != 0)
            {
                throw new Exception("不允许直接改变非叶子节点的值：" + FullPath);
            }

            InternalChangeValue(newValue);
        }

        /// <summary>
        /// 改变节点值（根据子节点值计算新值，只对非叶子节点有效）
        /// </summary>
        public void ChangeValue()
        {
            int sum = 0;

            if (mChildren != null && mChildren.Count != 0)
            {
                foreach (KeyValuePair<RangeString, TreeNode> child in mChildren)
                {
                    sum += child.Value.Value;
                }
            }

            InternalChangeValue(sum);
        }

        /// <summary>
        /// 获取子节点，如果不存在则添加
        /// </summary>
        public TreeNode GetOrAddChild(RangeString key)
        {
            TreeNode child = GetChild(key);
            if (child == null)
            {
                child = AddChild(key);
            }
            return child;
        }

        /// <summary>
        /// 获取子节点
        /// </summary>
        public TreeNode GetChild(RangeString key)
        {

            if (mChildren == null)
            {
                return null;
            }

            mChildren.TryGetValue(key, out TreeNode child);
            return child;
        }

        /// <summary>
        /// 添加子节点
        /// </summary>
        public TreeNode AddChild(RangeString key)
        {
            if (mChildren == null)
            {
                mChildren = new Dictionary<RangeString, TreeNode>();
            }
            else if (mChildren.ContainsKey(key))
            {
                throw new Exception("子节点添加失败，不允许重复添加：" + FullPath);
            }

            TreeNode child = new TreeNode(key.ToString(), this);
            mChildren.Add(key, child);
            ReddotMgr.Ins.NodeNumChangeCallback?.Invoke();
            return child;
        }

        /// <summary>
        /// 移除子节点
        /// </summary>
        public bool RemoveChild(RangeString key)
        {
            if (mChildren == null || mChildren.Count == 0)
            {
                return false;
            }

            TreeNode child = GetChild(key);

            if (child != null)
            {
                //子节点被删除 需要进行一次父节点刷新
                ReddotMgr.Ins.MarkDirtyNode(this);

                mChildren.Remove(key);

                ReddotMgr.Ins.NodeNumChangeCallback?.Invoke();

                return true;
            }

            return false;
        }

        /// <summary>
        /// 移除所有子节点
        /// </summary>
        public void RemoveAllChild()
        {
            if (mChildren == null || mChildren.Count == 0)
            {
                return;
            }

            mChildren.Clear();
            ReddotMgr.Ins.MarkDirtyNode(this);
            ReddotMgr.Ins.NodeNumChangeCallback?.Invoke();
        }

        public override string ToString()
        {
            return FullPath;
        }

        /// <summary>
        /// 改变节点值
        /// </summary>
        private void InternalChangeValue(int newValue)
        {
            if (Value == newValue)
            {
                return;
            }

            Value = newValue;
            mChangeCallback?.Invoke(newValue);
            ReddotMgr.Ins.NodeValueChangeCallback?.Invoke(this, Value);

            //标记父节点为脏节点
            ReddotMgr.Ins.MarkDirtyNode(Parent);
        }
    }

}