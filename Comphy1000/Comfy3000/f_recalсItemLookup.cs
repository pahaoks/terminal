using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Bacodelib;
using System.Data;

namespace Comfy3000
{
    public partial class f_recalcItemLookup : Form
    {
        #region Переменные

        private DataTable TableIn { get; set; }
        private DataTable TableOut { get; set; }
        private DataTable DataGridTable { get; set; }

        private DataView InView;
        private DataView OutView;

        private String GoodCodeColumn = "good_code";
        private String NameColumn = "name";
        private String CountColumn = "count";
        private String PlaceColumn = "place";
        private String CodeColumn = "code";
        private String Code1Column = "code1";
        private String Code2Column = "code2";
        private String Code3Column = "code3";

        private Task CurrentTask;

        public bool NeedSave { get; set; }

        public int CurrentRowNum { get; set; }

        #endregion

        #region Конструкторы

        public f_recalcItemLookup()
        {
            InitializeComponent();
            Scanner.Instance.OnReadBarCode += OnReadBarCode;
        }

        public f_recalcItemLookup(Task task, String defaultGoodCode)
            : this()
        {
            CurrentTask = task;
            TableIn = task.OutData;
            TableOut = task.TaskData;
            InView = new DataView(TableIn);
            OutView = new DataView(TableOut);

            codeTextBox.Text = defaultGoodCode;
            codeTextBox.SelectAll();
            codeTextBox.Focus();
        }

        #endregion

        #region Обработчики событий

        private void findButton_Click(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.CloseInternal();
        }

        private void dataGrid1_CurrentCellChanged(object sender, EventArgs e)
        {
            editButton.Enabled = true;
        }

        private void dataGrid1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    ApplyFilter();
                    break;
                case Keys.Escape:
                    this.Close();
                    break;
            }
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panelCountTextBox.Focus();
        }

        private void closePanelButton_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void panelButton_Click(object sender, EventArgs e)
        {
            try
            {
                string goodCode = DataGridTable.Rows[dataGrid1.CurrentRowIndex][GoodCodeColumn].ToString();
                DataRow[] dataRowsOut = TableOut.Select(string.Format("{0} = '{1}'", GoodCodeColumn, goodCode));
                DataRow[] dataRowsIn;

                //MessageBox.Show(goodCode);

                if (dataRowsOut == null)
                {
                    MessageBox.Show("DataRowsOut не инициализирован");
                    return;
                }

                if (dataRowsOut.Length > 0)
                {
                    //MessageBox.Show("Начинаем апдейтить строку");
                    dataRowsOut[0].BeginEdit();
                    dataRowsOut[0][CountColumn] = panelCountTextBox.Text;
                    dataRowsOut[0].EndEdit();
                    CurrentRowNum = TableOut.Rows.IndexOf(dataRowsOut[0]);
                    NeedSave = true;
                }
                else
                {
                    //MessageBox.Show("");
                    dataRowsIn = TableIn.Select(string.Format("{0} = '{1}'", GoodCodeColumn, goodCode));
                    if (dataRowsIn == null)
                    {
                        MessageBox.Show("DataRowsIn не инициализирован");
                        return;
                    }

                    if (dataRowsIn.Length > 0)
                    {
                        //MessageBox.Show("Начинаем вставку в аут");
                        dataRowsIn[0][CountColumn] = panelCountTextBox.Text;
                        TableOut.ImportRow(dataRowsIn[0]);
                        RecalcTask recalcTask = (RecalcTask)CurrentTask;
                        recalcTask.IndexingTableData(TableOut.Rows[TableOut.Rows.Count - 1]);
                        NeedSave = true;
                        CurrentRowNum = TableOut.Rows.Count - 1;
                    }
                    else
                    {
                        MessageBox.Show("DataRowsIn не найдена исходная запись");
                    }
                }

                TableOut.AcceptChanges();
                dataGrid1.Update();
                ApplyFilter();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            panel1.Visible = false;
            panelCountTextBox.Text = "";
            this.CloseInternal();
        }

        private void f_recalcItemLookup_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    ApplyFilter();
                    break;
                case Keys.Escape:
                    this.CloseInternal();
                    break;
            }
        }

        private void panelCountTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    panelButton_Click(panelCountTextBox, new EventArgs());
                    break;
                case Keys.Escape:
                    this.CloseInternal();
                    break;
            }
        }

        protected void OnReadBarCode(string Code, string OriginalCode)
        {
            codeTextBox.Text = Code;
            codeTextBox.SelectAll();
            codeTextBox.Focus();
        }

        private void codeTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    ApplyFilter();
                    break;
                case Keys.Escape:
                    this.CloseInternal();
                    break;
            }
        }

        #endregion

        #region Вспомогательные методы

        /// <summary>
        /// Сформировать таблицу с отфильтрованными даннымы
        /// </summary>
        /// <param name="dataView"></param>
        /// <returns></returns>
        private DataTable LookupRow(DataView dataView)
        {
            string rowFilter = "";
            DataTable ret = null;

            try
            {
                if (string.Empty != codeTextBox.Text)
                    rowFilter += string.Format(@"({0} like '%{1}%') 
                                                or ({2} = '{1}' or {3} = '{1}' or {4} = '{1}' or {5} = '{1}')
                                                or ({6} like '%{1}%')", 
                                                GoodCodeColumn, 
                                                codeTextBox.Text,
                                                CodeColumn,
                                                Code1Column,
                                                Code2Column,
                                                Code3Column,
                                                NameColumn);

                dataView.RowFilter = rowFilter;

                ret = dataView.ToTable("gridTable", false, GoodCodeColumn, NameColumn, PlaceColumn, CountColumn);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return ret;
        }

        /// <summary>
        /// Применить фильтр
        /// </summary>
        private void ApplyFilter()
        {
            DataGridTable = LookupRow(OutView);
            if (DataGridTable.Rows.Count == 0)
                DataGridTable = LookupRow(InView);
            dataGrid1.DataSource = DataGridTable;
            dataGrid1.Focus();
        }

        public void CloseInternal()
        {
            Scanner.Instance.OnReadBarCode -= OnReadBarCode;
            this.Close();
        }

        #endregion


    }
}