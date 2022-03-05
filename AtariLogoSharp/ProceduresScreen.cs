/* Atari Logo Sharp - implementation of Atari Logo in C#. Developed by 404 team. */

using System.Collections.Generic;
using System.Windows.Forms;

namespace AtariLogoSharp
{
    public partial class ProceduresScreen : Form
    {
        public ProceduresScreen()
        {
            InitializeComponent();
            listView.View = View.Details;
            listView.Columns.Add("Procedure", -2, HorizontalAlignment.Left);
            listView.Columns.Add("Parameters", -2, HorizontalAlignment.Left);
            this.TopMost = true;
        }

        /// <summary>
        /// Fills the list view with provided procedure names and corresponding parameters.
        /// </summary>
        /// <param name="procedureToParams">Procedure names and corresponding parameters to be filled in the list view.</param>
        public void FillProceduresList(Dictionary<string, List<string>> procedureToParams)
        {
            listView.Items.Clear();
            if (procedureToParams.Count != 0)
            {
                foreach (string key in procedureToParams.Keys)
                {
                    string[] results = { key, string.Join(" ", procedureToParams[key]) };
                    ListViewItem item = new ListViewItem(results);
                    listView.Items.Add(item);
                }
            }
        }
    }
}
