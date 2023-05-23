using GameFramework.DataTable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;
namespace Game
{
    public class ProcedureLauncher : ProcedureBase
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
            string dataTableAssetName = AssetUtility.GetDataTableAsset("UIForm", false);
            GameEntry.DataTable.LoadDataTable("UIForm", dataTableAssetName, this);

        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            // ����һ֡���л��� Splash չʾ����
            ChangeState<ProcedureMenu>(procedureOwner);
        }



    }
}