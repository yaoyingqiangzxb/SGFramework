using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;
namespace Game
{
    public class ProcedureMenu : ProcedureBase
    {
        public override bool UseNativeDialog
        {
            get
            {
                return true;
            }
        }

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
            GameEntry.UI.OpenUIForm(UIFormId.MenuForm, procedureOwner);

        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
        }



    }
}