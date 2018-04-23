using System.Collections;
using System.IO;
using System.Reflection;
using System.Windows.Controls;
using System.Xml.Serialization;
using DevExpress.Xpf.Collections;
using DevExpress.Xpf.PivotGrid;
using DevExpress.XtraPivotGrid.Data;

namespace DXPivotGrid_CustomFilterItemsSorting {
    public partial class MainPage : UserControl {
        string dataFileName = "DXPivotGrid_CustomFilterItemsSorting.nwind.xml";
        public MainPage() {
            InitializeComponent();

            // Parses an XML file and creates a collection of data items.
            Assembly assembly = Assembly.GetExecutingAssembly();
            Stream stream = assembly.GetManifestResourceStream(dataFileName);
            XmlSerializer s = new XmlSerializer(typeof(OrderData));
            object dataSource = s.Deserialize(stream);

            // Binds a pivot grid to this collection.
            pivotGridControl1.DataSource = dataSource;
        }
        private void pivotGridControl1_CustomFilterPopupItems(object sender, 
            PivotCustomFilterPopupItemsEventArgs e) {
            if (rbCaptionLength.IsChecked == true)
                ArrayList.Adapter((IList)e.Items).Sort(new FilterItemsComparer());
        }
    }
    public class FilterItemsComparer : IComparer {
        int IComparer.Compare(object x, object y) {
            if (!(x is PivotGridFilterItem) || !(y is PivotGridFilterItem)) return 0;
            PivotGridFilterItem item1 = (PivotGridFilterItem)x;
            PivotGridFilterItem item2 = (PivotGridFilterItem)y;
            if (item1.ToString().Length == item2.ToString().Length) return 0;
            if (item1.ToString().Length > item2.ToString().Length) return 1;
            return -1;
        }
    }
}
