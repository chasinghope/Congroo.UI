using System;
using UnityEngine;

namespace Congroo.UI.SampleStart
{
    public class SampleStartInit : MonoBehaviour
    {
        private void Start()
        {
            UIMgr.Ins.Initialize();
        }
    }
}