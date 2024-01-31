using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace CsvReader
{
    public partial class CsvReadForm : DevExpress.XtraEditors.XtraForm
    {
        public CsvReadForm()
        {
            InitializeComponent();
        }

        private void buttonEdit1_Click(object sender, EventArgs e)
        {
            readCsv2();
        }

        private void readCsv2()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "CSV Dosyaları|*.csv|Tüm Dosyalar|*.*";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = fileDialog.FileName;
                string[] Lines = File.ReadAllLines(filePath);
                string[] Fields;
                Fields = Lines[0].Split(new char[] { ',' });

                int Cols = Fields.GetLength(0);
                DataTable dt = new DataTable();
                
                for (int i = 0; i < Cols; i++)
                    dt.Columns.Add(ToTitleCase(Fields[i]), typeof(string));

                string ToTitleCase(string input)
                {
                    if (string.IsNullOrEmpty(input))
                    {
                        return input;
                    }

                    return char.ToUpper(input[0]) + input.Substring(1);
                }
                DataRow Row;
                for (int i = 1; i < Lines.GetLength(0); i++)
                {
                    Fields = Lines[i].Split(new char[] { ',' });
                    Row = dt.NewRow();
                    for (int f = 0; f < Cols; f++)
                        Row[f] = Fields[f];
                    dt.Rows.Add(Row);
                }
                gridControl1.DataSource = dt;
                if (dt.Rows.Count > 0)
                {
                    countLabelControl.Visible = true;
                    countLabelControl.Text = RowCount.ToString();
                }
            }
        }
        public int RowCount
        {
            get { return gridView1.RowCount; }
        }
    }
}
