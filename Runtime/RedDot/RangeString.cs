using System;
using System.Text;


namespace Congroo.UI
{
    /// <summary>
    /// 范围字符串
    /// 表示在Source字符串中，从StartIndex到EndIndex范围的字符构成的字符串
    /// </summary>
    public struct RangeString : IEquatable<RangeString>
    {
        /// <summary>
        /// 源字符串
        /// </summary>
        private string mSource;

        /// <summary>
        /// 开始索引
        /// </summary>
        private int mStartIndex;

        /// <summary>
        /// 结束范围
        /// </summary>
        private int mEndIndex;

        /// <summary>
        /// 长度
        /// </summary>
        private int mLength;

        /// <summary>
        /// 源字符串是否为Null或Empty
        /// </summary>
        private bool mIsSourceNullOrEmpty;

        /// <summary>
        /// 哈希码
        /// </summary>
        private int mHashCode;



        public RangeString(string source, int startIndex, int endIndex)
        {
            mSource = source;
            mStartIndex = startIndex;
            mEndIndex = endIndex;
            mLength = endIndex - startIndex + 1;
            mIsSourceNullOrEmpty = string.IsNullOrEmpty(source);
            mHashCode = 0;
        }

        public bool Equals(RangeString other)
        {

            bool isOtherNullOrEmpty = string.IsNullOrEmpty(other.mSource);

            if (mIsSourceNullOrEmpty && isOtherNullOrEmpty)
            {
                return true;
            }

            if (mIsSourceNullOrEmpty || isOtherNullOrEmpty)
            {
                return false;
            }

            if (mLength != other.mLength)
            {
                return false;
            }

            for (int i = mStartIndex, j = other.mStartIndex; i <= mEndIndex; i++, j++)
            {
                if (mSource[i] != other.mSource[j])
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            if (mHashCode == 0 && !mIsSourceNullOrEmpty)
            {
                for (int i = mStartIndex; i <= mEndIndex; i++)
                {
                    //有效地减少哈希冲突的概率
                    mHashCode = 31 * mHashCode + mSource[i];
                }
            }

            return mHashCode;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = mStartIndex; i <= mEndIndex; i++)
            {
                sb.Append(mSource[i]);
            }
            return sb.ToString();
        }
    }
}

