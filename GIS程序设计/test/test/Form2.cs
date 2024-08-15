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
using ESRI.ArcGIS.Controls;//引用

namespace test
{
    public partial class frmProperty : Form
    {
       public IMapControl2 pMapControl;
        public IEnvelope pEnvelop;
        public frmProperty(IMapControl2 pFMapControl, IEnvelope pFEnvelop)
        {
            InitializeComponent();
            pMapControl = pFMapControl;
            pEnvelop = pFEnvelop;
        }



        public void SelectPropertyViaFeature()
        {
            treeView1.Nodes.Clear();
            for (int i = 0; i < pMapControl.Map.LayerCount; i++)
            {
                IFeatureLayer pFeatureLayer = (IFeatureLayer)pMapControl.Map.get_Layer(i);
                IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;

                ISpatialFilter pSpatialFilter = new SpatialFilterClass();
                pSpatialFilter.Geometry = pEnvelop;
                pSpatialFilter.GeometryField = pFeatureClass.ShapeFieldName;
                pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;

                IFields pFields = pFeatureClass.Fields;
                IFeatureCursor pFeatureCursor = pFeatureClass.Search(pSpatialFilter, false);

                TreeNode nodeParent;
                IFeature pFeature;
                pFeature = pFeatureCursor.NextFeature();

                nodeParent = treeView1.Nodes.Add(pFeatureLayer.Name.ToString());
                while (pFeature != null)
                {
                    TreeNode nodeSon;
                    for (int j = 0; j < pFields.FieldCount; j++)
                    {
                        string fldValue;
                        string fldName;
                        fldName = pFields.get_Field(j).Name;
                        if (fldName == "Shape")
                        {
                            fldValue = Convert.ToString(pFeature.Shape.GeometryType);
                        }
                        else
                            fldValue = Convert.ToString(pFeature.get_Value(j));
                        nodeSon = nodeParent.Nodes.Add(fldValue);
                    }
                    pMapControl.Map.SelectFeature(pFeatureLayer, pFeature);
                    pFeature = pFeatureCursor.NextFeature();
                }


            }
            IActiveView pActiveView;
            pActiveView = (IActiveView)pMapControl.Map;
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
        }
        public void frmProperty_Load(object sender, EventArgs e)
        {
            SelectPropertyViaFeature();
        }

        public frmProperty()
        {
            InitializeComponent();
        }


    }
}
