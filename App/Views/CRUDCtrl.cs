using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;

namespace App.Views
{
    public partial class CRUDCtrl : XtraUserControl
    {
        public CRUDCtrl()
        {
            InitializeComponent();
        }
        public GridControl GetGridControl()
        {
            return gridControl1;
        }

        public virtual void gridControl1_EmbeddedNavigator_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {

        }

    }
}
