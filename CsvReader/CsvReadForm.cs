using DevExpress.XtraEditors;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace CsvReader
{
    public partial class CsvReadForm : XtraForm
    {
        #region constructor
        public CsvReadForm()
        {
            InitializeComponent();
        }
        #endregion

        #region properties
        public int RowCount
        {
            get { return csvGridView.RowCount; }
        }
        #endregion

        #region events
        private void buttonEdit1_Click(object sender, EventArgs e)
        {
            readCsv();
        }
        #endregion

        #region private methods
        private void readCsv()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "CSV Dosyaları|*.csv|Tüm Dosyalar|*.*";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {

                string filePath = fileDialog.FileName;
                string[] Lines = File.ReadAllLines(filePath);

                DataTable dt = new DataTable();

                //for (int i = 1; i <= 6; i++)
                //{
                dt.Columns.Add($"Değişken Numarası", typeof(string));
                dt.Columns.Add($"Ürün Anahtarı", typeof(string));
                dt.Columns.Add($"Ürün Açıklaması", typeof(string));
                dt.Columns.Add($"Yükleme Tarihi", typeof(string));
                dt.Columns.Add($"Unknown", typeof(string));
                dt.Columns.Add($"Unknown2", typeof(string));
                //}

                foreach (string line in Lines)
                {
                    string[] fields = line.Split(',', ';');

                    while (fields.Length < 6)
                    {
                        Array.Resize(ref fields, fields.Length + 1);
                        fields[fields.Length - 1] = string.Empty;
                    }

                    dt.Rows.Add(fields);
                }


                csvGridControl.DataSource = dt;
                if (dt.Rows.Count > 0)
                {
                    countLabelControl.Visible = true;
                    countLabelControl.Text = RowCount.ToString();
                }
            }
        }
        #endregion

    }
}
