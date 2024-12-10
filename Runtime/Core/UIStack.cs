
using System;
using System.Collections.Generic;

namespace Congroo.UI
{
    

    public class UIStack<T>
    {
        private List<T> items = new List<T>();

        // Push 方法用于向栈中添加元素
        public void Push(T item)
        {
            items.Add(item);
        }

        // Pop 方法用于移除并返回栈顶元素
        // 如果栈为空，则抛出 InvalidOperationException 异常
        public T Pop()
        {
            if (items.Count == 0)
            {
                throw new InvalidOperationException("The stack is empty.");
            }
            T topItem = items[items.Count - 1];
            items.RemoveAt(items.Count - 1);
            return topItem;
        }

        // Peek 方法用于返回栈顶元素但不移除它
        // 如果栈为空，则抛出 InvalidOperationException 异常
        public T Peek()
        {
            if (items.Count == 0)
            {
                throw new InvalidOperationException("The stack is empty.");
            }
            return items[items.Count - 1];
        }

        // IsEmpty 方法用于检查栈是否为空
        public bool IsEmpty()
        {
            return items.Count == 0;
        }

        // Count 属性用于获取栈中的元素数量
        public int Count
        {
            get { return items.Count; }
        }

        public int FindIndex(Predicate<T> match)
        {
            return items.FindIndex(match);
        }
        
        
        public int FindLastIndex(Predicate<T> match)
        {
            return items.FindLastIndex(match);
        }

        public void RemoveIndex(int index)
        {
            items.RemoveAt(index);
        }

        public T GetIndex(int index)
        {
            return items[index];
        }
    }

}