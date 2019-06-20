
using App.Views.需求部分;
using NewServices;
using Core.Enum;
using DevExpress.XtraGrid.Views.Grid;
using App.Helper;
using DevExpress.XtraSplashScreen;
using System;
using NewServices.Interfaces;

namespace App.Views
{
    public partial class CarFixRequestCtrl : ToolRequestCtrl
    {
        public CarFixRequestCtrl(IRequestService requestService, IManagementService managementService) : base(requestService, managementService)
        {
            SetCurrentRequestCategory(RequestCategoriesEnum.工程车维修);
        }

       
    }
}
