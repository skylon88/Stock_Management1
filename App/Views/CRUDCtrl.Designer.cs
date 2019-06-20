namespace App.Views
{
    partial class CRUDCtrl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CRUDCtrl));
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            //((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Buttons.Append.Enabled = false;
            this.gridControl1.EmbeddedNavigator.Buttons.Append.ImageIndex = 0;
            this.gridControl1.EmbeddedNavigator.Buttons.CancelEdit.ImageIndex = 2;
            this.gridControl1.EmbeddedNavigator.Buttons.Edit.ImageIndex = 3;
            this.gridControl1.EmbeddedNavigator.Buttons.EndEdit.ImageIndex = 1;
            this.gridControl1.EmbeddedNavigator.Buttons.First.ImageIndex = 4;
            this.gridControl1.EmbeddedNavigator.Buttons.ImageList = this.imageCollection1;
            this.gridControl1.EmbeddedNavigator.Buttons.Last.ImageIndex = 5;
            this.gridControl1.EmbeddedNavigator.Buttons.Next.ImageIndex = 8;
            this.gridControl1.EmbeddedNavigator.Buttons.NextPage.ImageIndex = 6;
            this.gridControl1.EmbeddedNavigator.Buttons.Prev.ImageIndex = 9;
            this.gridControl1.EmbeddedNavigator.Buttons.PrevPage.ImageIndex = 7;
            this.gridControl1.EmbeddedNavigator.Buttons.Remove.ImageIndex = 10;
            this.gridControl1.EmbeddedNavigator.ButtonClick += new DevExpress.XtraEditors.NavigatorButtonClickEventHandler(this.gridControl1_EmbeddedNavigator_ButtonClick);
            this.gridControl1.UseEmbeddedNavigator = true;

            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.InsertGalleryImage("add_16x16.png", "images/actions/add_16x16.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/actions/add_16x16.png"), 0);
            this.imageCollection1.Images.SetKeyName(0, "add_16x16.png");
            this.imageCollection1.InsertGalleryImage("apply_16x16.png", "images/actions/apply_16x16.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/actions/apply_16x16.png"), 1);
            this.imageCollection1.Images.SetKeyName(1, "apply_16x16.png");
            this.imageCollection1.InsertGalleryImage("cancel_16x16.png", "images/actions/cancel_16x16.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/actions/cancel_16x16.png"), 2);
            this.imageCollection1.Images.SetKeyName(2, "cancel_16x16.png");
            this.imageCollection1.InsertGalleryImage("pencolor_16x16.png", "images/richedit/pencolor_16x16.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/richedit/pencolor_16x16.png"), 3);
            this.imageCollection1.Images.SetKeyName(3, "pencolor_16x16.png");
            this.imageCollection1.InsertGalleryImage("doublefirst_16x16.png", "images/arrows/doublefirst_16x16.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/arrows/doublefirst_16x16.png"), 4);
            this.imageCollection1.Images.SetKeyName(4, "doublefirst_16x16.png");
            this.imageCollection1.InsertGalleryImage("doublelast_16x16.png", "images/arrows/doublelast_16x16.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/arrows/doublelast_16x16.png"), 5);
            this.imageCollection1.Images.SetKeyName(5, "doublelast_16x16.png");
            this.imageCollection1.InsertGalleryImage("doublenext_16x16.png", "images/arrows/doublenext_16x16.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/arrows/doublenext_16x16.png"), 6);
            this.imageCollection1.Images.SetKeyName(6, "doublenext_16x16.png");
            this.imageCollection1.InsertGalleryImage("doubleprev_16x16.png", "images/arrows/doubleprev_16x16.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/arrows/doubleprev_16x16.png"), 7);
            this.imageCollection1.Images.SetKeyName(7, "doubleprev_16x16.png");
            this.imageCollection1.InsertGalleryImage("next_16x16.png", "images/arrows/next_16x16.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/arrows/next_16x16.png"), 8);
            this.imageCollection1.Images.SetKeyName(8, "next_16x16.png");
            this.imageCollection1.InsertGalleryImage("prev_16x16.png", "images/arrows/prev_16x16.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/arrows/prev_16x16.png"), 9);
            this.imageCollection1.Images.SetKeyName(9, "prev_16x16.png");
            this.imageCollection1.InsertGalleryImage("trash_16x16.png", "images/actions/trash_16x16.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/actions/trash_16x16.png"), 10);
            this.imageCollection1.Images.SetKeyName(10, "trash_16x16.png");

            // 
            // CRUDCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl1);
            this.Size = new System.Drawing.Size(1030, 402);
            //((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        public DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.Utils.ImageCollection imageCollection1;
    }
}
