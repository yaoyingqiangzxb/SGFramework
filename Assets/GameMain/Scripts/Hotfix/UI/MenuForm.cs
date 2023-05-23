using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Game
{


    public class MenuForm : UGuiForm
    {
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
        }
        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);

            Log.Debug("Hello");
        }
    }
}
