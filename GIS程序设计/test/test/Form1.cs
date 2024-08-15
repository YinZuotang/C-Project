using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.ADF;




namespace test
{
    public partial class Form1 : Form
    {
        public IMapControl2 pMapControl;
        public IToolbarControl2 pToolBarControl;
        public ITOCControl2 pTocControl;
        public bool toolSelected = false;


        public Form1()
        {
            InitializeComponent();
        }
        private IMapControl3 m_mapControl = null;
        private string m_mapDocumentName = string.Empty;
        //private IContainer components yinzuotang;
        private void Form1_Load(object sender, EventArgs e)
        {
            m_mapControl = (IMapControl3)axMapControl1.Object;

            menuSaveDoc.Enabled = false;
            pMapControl = (IMapControl2)axMapControl1.Object;
            pTocControl = (ITOCControl2)axTOCControl1.Object;
            pToolBarControl = (IToolbarControl2)axToolbarControl1.Object;
            pToolBarControl.SetBuddyControl(pMapControl);
            pTocControl.SetBuddyControl(pMapControl);
            CreateToolBarItem();

        }

        /*private void Form1_Load(object sender, EventArgs e)
        {
           //yinzuotang
        }*/
        private void CreateToolBarItem()
        {
            pToolBarControl.AddItem("esriControls.ControlsOpenDocCommand", -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            pToolBarControl.AddItem("esriControls.ControlsAddDataCommand", -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);

            pToolBarControl.AddItem("esriControls.ControlsMapZoomInTool", -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            pToolBarControl.AddItem("esriControls.ControlsMapZoomOutTool", -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            pToolBarControl.AddItem("esriControls.ControlsMapZoomInFixedCommand", -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            pToolBarControl.AddItem("esriControls.ControlsMapZoomOutFixedCommand", -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            pToolBarControl.AddItem("esriControls.ControlsMapPanTool", -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            pToolBarControl.AddItem("esriControls.ControlsMapFullExtentCommand", -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            pToolBarControl.AddItem("esriControls.ControlsMapZoomToLastExtentBackCommand", -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            pToolBarControl.AddItem("esriControls.ControlsMapZoomToLastExtentForwardCommand", -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            pToolBarControl.AddItem("esriControls.ControlsSelectFeaturesTool", -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            pToolBarControl.AddItem("esriControls.ControlsClearSelectionCommand", -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            pToolBarControl.AddItem("esriControls.ControlsSelectTool", -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            pToolBarControl.AddItem("esriControls.ControlsMapIdentifyTool", -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            pToolBarControl.AddItem("esriControls.ControlsMapFindCommand", -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            pToolBarControl.AddItem("esriControls.ControlsMapMeasureTool", -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);

        }



        private void axToolbarControl1_OnMouseDown(object sender, IToolbarControlEvents_OnMouseDownEvent e)
        {
            {

            }
        }

        private void axMapControl1_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            if (e.button == 1 && toolSelected == true)
            {
                IEnvelope pEnvelop = pMapControl.TrackRectangle();
                pMapControl.Map.ClearSelection();
                frmProperty fProperty = new frmProperty(pMapControl, pEnvelop);
                fProperty.Show();
            }

        }

        private void PropertyViaFeature_Click(object sender, EventArgs e)
        {
            toolSelected = true;
        }

        private void PropertyViaFeature_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void PropertyViaFeature_Click_1(object sender, EventArgs e)
        {
            toolSelected = true;
        }

        private void menuOpenDoc_Click(object sender, EventArgs e)
        {
            ICommand command = new ControlsOpenDocCommandClass();

            command.OnCreate(m_mapControl.Object);

            command.OnClick();
        }

        private void menuSaveDoc_Click(object sender, EventArgs e)
        {
            if (m_mapControl.CheckMxFile(m_mapDocumentName))
            {
                IMapDocument mapDoc = new MapDocumentClass();
                mapDoc.Open(m_mapDocumentName, string.Empty);

                if (mapDoc.get_IsReadOnly(m_mapDocumentName))
                {
                    MessageBox.Show("Map document is read only!");
                    mapDoc.Close();
                    return;
                }
            }
}

        

        private void menuSaveAsDoc_Click(object sender, EventArgs e)
        {
            ICommand command = new ControlsSaveAsDocCommandClass();
            command.OnCreate(m_mapControl.Object);
            command.OnClick();


        }

        private void menuExitApp_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /*private void 属性ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 属性ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

        }*/
    }
}
