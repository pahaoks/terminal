using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Bacodelib
{
    #region TaskKind
    /// <summary>
    /// виды задач
    /// </summary>
    public enum TaskKind
    {
        /// <summary>
        /// пустой
        /// </summary>
        None,
        /// <summary>
        /// приход
        /// </summary>
        In,
        /// <summary>
        /// отгрузка
        /// </summary>
        Out,
        /// <summary>
        /// переучет
        /// </summary>
        Recalc,
        /// <summary>
        /// присвоение шк
        /// </summary>
        Grab,
        /// <summary>
        /// проверка цен
        /// </summary>
        Check,
        /// <summary>
        /// проверка отгрузки
        /// </summary>
        CheckOut,
        /// <summary>
        /// оптовая отгрузка
        /// </summary>
        OutWholesale,
        /// <summary>
        /// отгрузка по грузоместам
        /// </summary>
        OutPack,
        /// <summary>
        /// приемка по грузоместам
        /// </summary>
        InPack,
        /// <summary>
        /// приемка от поставщика
        /// </summary>
        InSup,
        /// <summary>
        /// Сортировка CDP
        /// <summary>
        CDPSort
    } 
    #endregion

    #region KindNames
    /// <summary>
    /// Функции работы с типами заданий
    /// </summary>
    public static class KindNames
    {
        /// <summary>
        /// Название задания по типу.
        /// </summary>
        /// <param name="k">Тип</param>
        /// <returns></returns>
        public static string GetKindName(TaskKind k)
        {
            string ret = string.Empty;
            switch (k)
            {
                case TaskKind.In: ret = "Приёмка товара"; break;
                case TaskKind.Out: ret = "Отгрузка товара"; break;
                case TaskKind.Check: ret = "Проверка цен"; break;
                case TaskKind.Grab: ret = "Присв. нового ШК"; break;
                case TaskKind.Recalc: ret = "Переучёт"; break;
                case TaskKind.CheckOut: ret = "Проверка отгрузки"; break;
                case TaskKind.OutWholesale: ret = "Отгрузка.Опт"; break;
                case TaskKind.OutPack: ret = "Отгрузка по ГМ"; break;
                case TaskKind.InPack: ret = "Назначение МХ"; break;
                case TaskKind.InSup: ret = "Приемка от поставшика"; break;
                case TaskKind.CDPSort: ret = "CDP сортировка"; break;
                default: ret = string.Empty; break;
            }
            return ret;
        }
        /// <summary>
        /// Префикс имени файла по типу.
        /// </summary>
        /// <param name="kind">Тип</param>
        /// <returns></returns>
        public static string TaskPrefix(TaskKind kind)
        {
            string ret = string.Empty;
            switch (kind)
            {
                case TaskKind.In: ret = "pr"; break;
                case TaskKind.Out: ret = "ot"; break;
                case TaskKind.Recalc: ret = "in"; break;
                case TaskKind.Grab: ret = "sk"; break;
                case TaskKind.Check: ret = "pc"; break;
                case TaskKind.CheckOut: ret = "po"; break;
                case TaskKind.OutWholesale: ret = "oo"; break;
                case TaskKind.OutPack: ret = "op"; break;
                case TaskKind.InPack: ret = "ip"; break;
                case TaskKind.InSup: ret = "ps"; break;
                case TaskKind.CDPSort: ret = "cd"; break;
                default: ret = string.Empty; break;
            }
            return ret;
        }
    } 
    #endregion
    
    #region WaitEventArgs
    /// <summary>
    /// Аргументы события ожидания
    /// </summary>
    public class WaitEventArgs : EventArgs
    {
        #region Поля
        private string message;
        private bool is_err;
        public string Message
        {
            get { return message; }
            set { message = value; }
        }
        public bool Is_err
        {
            get { return is_err; }
        }
        #endregion

        #region Конструкторы
        public WaitEventArgs(string s)
        {
            message = s;
            is_err = false;
        }
        public WaitEventArgs(Exception e)
        {
            message = e.Message;
            is_err = true;
        } 
        #endregion
    } 
    #endregion

    #region Task
    /// <summary>
    /// Базовый класс для задач
    /// </summary>
    public class Task : IDisposable
    {
        #region Поля
        /// <summary>
        /// путь к файлам заданий
        /// </summary>
        protected static string TaskPath = "\\Storage Card";//"\\My Documents";

        protected DataTable table;
        protected DataTable addtable;
        protected FileInfo file_in;
        protected FileInfo file_out;
        private TaskKind kind = TaskKind.None;
        protected string taskname = "";
        protected bool needSave = false;

        protected string v_name = "";
        protected string v_code = "";
        protected string v_count = "";
        protected int v_gridrow = 0;

        public int number = 0;

        public event EventHandler<WaitEventArgs> WaitEvent;

        public bool NeedSave
        {
            get { return needSave; }
            set { needSave = value; }
        }

        public string vName
        {
            get { return v_name; }
        }
        public string vCode
        {
            get { return v_code; }
        }
        public string vCount
        {
            get { return v_count; }
        }
        public int vGridRow
        {
            get { return v_gridrow; }
            set { v_gridrow = value; }
        }

        public TaskKind Kind
        {
            get { return this.kind; }
        }
        public DataTable TaskData
        {
            get { return table; }
        }
        public DataTable OutData
        {
            get { return addtable; }
        }
        
        #endregion

        #region CreateTask
        /// <summary>
        /// конструктор задач по типу.
        /// </summary>
        /// <param name="kind">Тиа задачи</param>
        /// <returns></returns>
        public static Task CreateTask(TaskKind kind)
        {
            Task ret = null;
            switch (kind)
            {
                case TaskKind.In:
                    ret = new InputTask();
                    break;
                case TaskKind.Out:
                    ret = new OutputTask();
                    break;
                case TaskKind.Recalc:
                    ret = new RecalcTask();
                    break;
                case TaskKind.Grab:
                    ret = new GrabTask();
                    break;
                case TaskKind.Check:
                    ret = new CheckTask();
                    break;
                case TaskKind.CheckOut:
                    ret = new CheckOutputTask();
                    break;
                case TaskKind.OutWholesale:
                    ret = new OutputWholesaleTask();
                    break;
                case TaskKind.OutPack:
                    ret = new OutputPackTask();
                    break;
                case TaskKind.InPack:
                    ret = new InputPackTask();
                    break;
                case TaskKind.InSup:
                    ret = new InputFromSupplierTask();
                    break;
                case TaskKind.CDPSort:
                    ret = new CdpSortTask();
                    break;
            }
            return ret;
        } 
        #endregion

        #region конструктор Task
        protected Task(TaskKind kind)
        {
            this.kind = kind;
            switch (kind)
            {
                case TaskKind.In:
                    table = CreateInOutTable("t_main");
                    break;
                case TaskKind.Out:
                    table = CreateInOutTable("t_main");
                    break;
                case TaskKind.CheckOut:
                    table = CreateInOutTable("t_main");
                    table.Columns.Add("plan");
                    break;
                case TaskKind.Recalc:
                    table = CreateInOutTable("t_main");
                    addtable = CreateInOutTable("t_all");
                    break;
                case TaskKind.Grab:
                    table = new DataTable("t_main");
                    table.Columns.Add("number");
                    table.Columns.Add("good_code");
                    table.Columns.Add("name");
                    table.Columns.Add("code");
                    table.Columns.Add("vsk");
                    break;
                case TaskKind.Check:
                    table = new DataTable("t_main");
                    table.Columns.Add("number");
                    table.Columns.Add("good_code");
                    table.Columns.Add("name");
                    table.Columns.Add("code");
                    table.Columns.Add("code1");
                    table.Columns.Add("code2");
                    table.Columns.Add("code3");
                    table.Columns.Add("price");
                    table.Columns.Add("true");
                    table.Columns.Add("flag");
                    break;
                case TaskKind.OutWholesale:
                    table = CreateInOutTable("t_main");
                    table.Columns.Add("has_serial");
                    // таблица для sn
                    addtable = new DataTable("serial_numbers");
                    addtable.Columns.Add("good_code");
                    addtable.Columns.Add("sn");
                    break;
                case TaskKind.OutPack:
                    table = CreateInOutTable("t_main");
                    table.Columns.Add("pack"); 
                    break;
                case TaskKind.InPack:
                    table = CreateInOutTable("t_main");
                    table.Columns.Add("pack");
                    break;
                case TaskKind.InSup:
                    table = new DataTable("t_main");
                    table.Columns.Add("number");
                    table.Columns.Add("good_code");
                    table.Columns.Add("name");
                    table.Columns.Add("code");
                    table.Columns.Add("code1");
                    table.Columns.Add("code2");
                    table.Columns.Add("code3");
                    table.Columns.Add("plan");
                    table.Columns.Add("count");
                    table.Columns.Add("unused");
                    break;
                case TaskKind.CDPSort:
                    table = new DataTable("t_main");
                    table.Columns.Add("number");
                    table.Columns.Add("good_code");
                    table.Columns.Add("name");
                    table.Columns.Add("code");
                    table.Columns.Add("code1");
                    table.Columns.Add("code2");
                    table.Columns.Add("code3");
                    table.Columns.Add("plan");
                    table.Columns.Add("count");
                    table.Columns.Add("unused");
                    table.Columns.Add("driver");
                    table.Columns.Add("delivery_code");
                    break;
            }
        } 
        #endregion
        #region Load
        /// <summary>
        /// Загрузить данные из файла
        /// </summary>
        /// <param name="TaskName">Имя задания</param>
        public virtual void Load(string TaskName)
        {
            this.taskname = TaskName;
            string path_in = String.Format("{0}\\{1}{2}.csv", TaskPath, KindNames.TaskPrefix(kind), TaskName);
            string path_out = TaskPath + "\\" + KindNames.TaskPrefix(kind) + TaskName + "out.csv";
            //CreateFile(path_in);
            //CreateFile(path_out);
            file_in = new FileInfo(path_in);
            file_out = new FileInfo(path_out);
            switch (kind)
            {
                case TaskKind.In:
                case TaskKind.Out:
                case TaskKind.Grab:
                case TaskKind.Check:
                case TaskKind.CheckOut:
                case TaskKind.OutPack:
                case TaskKind.InSup:
                    if (ReadFile(table, file_out) < 1)
                        ReadFile(table, file_in);
                    break;
                case TaskKind.OutWholesale:
                    if (ReadFile(table, file_out) < 1)
                        ReadFile(table, file_in);
                    ReadFile(addtable, new FileInfo(TaskPath + "\\" + KindNames.TaskPrefix(kind) + TaskName + "sn.csv"));
                    break;
                case TaskKind.Recalc:
                    ReadFile(addtable, file_in);
                    ReadFile(table, file_out);
                    break;
                case TaskKind.CDPSort:
                    if (ReadFile(table, file_out) < 1)
                        ReadFile(table, file_in);
                    break;
            }
            OnWaitEvent(new WaitEventArgs(""));
            needSave = false;
        } 
        #endregion

        public static bool IsCartAvaliable()
        {
            DirectoryInfo f = new DirectoryInfo(TaskPath);
            return f.Exists;
        }

        protected bool SaveDataView(DataView dataView, string sufix)
        {
            if (!Task.IsCartAvaliable())
            {
                OnWaitEvent(new WaitEventArgs(new Exception("Нет карты памяти или она неотформатирована.")));
                return false;
            }

            FileInfo f = new FileInfo(TaskPath + "\\" + KindNames.TaskPrefix(kind) + taskname + sufix + ".csv");
            StreamWriter writer = f.CreateText();
            try
            {
                //for (int i = 0; i < table.Rows.Count; i++)
                int i = 0;
                foreach (DataRowView view in dataView)
                {
                    string output = "";
                    for (int j = 0; j < view.Row.Table.Columns.Count; j++)
                    {
                        if (j != 0) output += ";";
                        output += view[j].ToString();
                        //output += table.Rows[i][j].ToString();
                    }
                    writer.WriteLine(output);
                    //throw new Exception("my exception");
                    OnWaitEvent(new WaitEventArgs("Сохранение\n" + (++i).ToString()));
                }
                writer.Close();
                needSave = false;
                return true;
            }
            catch (Exception err)
            {
                OnWaitEvent(new WaitEventArgs(err));
                return false;
            }
        }
        
        #region Save
        /// <summary>
        /// Сохранить в файл
        /// </summary>
        public virtual bool Save()
        {
            return SaveDataView(new DataView(table), "out");
        } 
        #endregion
       
        #region GetTaskList
        /// <summary>
        /// Заполнить List списком заданий
        /// </summary>
        /// <param name="kind">Тип заданий</param>
        /// <param name="list">The list.</param>
        public static void GetTaskList(TaskKind kind, ListBox.ObjectCollection list)
        {
            string mask = KindNames.TaskPrefix(kind) + "*.*";
            DirectoryInfo dir = new DirectoryInfo(TaskPath);
            FileInfo[] files = dir.GetFiles(mask);

            for (int i = 0; i < files.Length; i++)
                if ((!files[i].Name.Trim().EndsWith("out.csv")) && (!files[i].Name.Trim().EndsWith("sn.csv")))
                    list.Add((files[i].Name.ToString().Split('.')[0]).Substring(2));

        } 
        #endregion

        #region GetInfo
        public virtual WareInfo GetInfo(int index)
        {
            return new WareInfo(table.Rows[index][8].ToString(), table.Rows[index][9].ToString(), table.Rows[index][2].ToString(), table.Rows[index][1].ToString());
        } 
        #endregion

        #region CreateInOutTable
        /// <summary>
        /// Инициализировать таблицу
        /// </summary>
        /// <param name="name">Имя таблицы</param>
        /// <returns></returns>
        protected DataTable CreateInOutTable(string name)
        {
            DataTable table = new DataTable(name);
            table.Columns.Add("number");
            table.Columns.Add("good_code");
            table.Columns.Add("name");
            table.Columns.Add("code");
            table.Columns.Add("code1");
            table.Columns.Add("code2");
            table.Columns.Add("code3");
            table.Columns.Add("count");
            table.Columns.Add("place"); // для места хранения
            table.Columns.Add("adr");   // для адреса
            return table;
        } 
        #endregion

        public void SortTable(int SortColumn)
        {

        }

        #region CreateFile
        private void CreateFile(string path)
        {
            if (!File.Exists(path))
            {
                FileStream fs = File.Create(path, 1024);
                fs.Close();
            }

        } 
        #endregion
        
        #region ReadFile
        /// <summary>
        /// Зачитать файл в таблицу
        /// </summary>
        /// <param name="table">Таблица</param>
        /// <param name="fi">Файл</param>
        /// <returns></returns>
        protected int ReadFile(DataTable table, FileInfo fi)
        {
            int i = 0;
            //string[] arr;
            if (File.Exists(fi.FullName))
            {
                string input;
                try
                {
                    StreamReader sr = fi.OpenText();
                    try
                    {
                        while ((input = sr.ReadLine()) != null)
                        {
                            //arr = input.Split(';');
                            string[] arr = input.Split(';');
                            table.Rows.Add(arr);
                            OnWaitEvent(new WaitEventArgs("Загрузка\n" + (i++).ToString()));
                        }
                    }
                    finally
                    {
                        sr.Close();
                    }
                }
                catch (Exception e)
                {
                    OnWaitEvent(new WaitEventArgs(e));
                    table.Rows.Clear();
                    i = 0;
                }
            }
            return i;
        }
        #endregion
        
        #region OnWaitEvent
        /// <summary>
        /// Raises the <see cref="E:WaitEvent"/> event.
        /// </summary>
        /// <param name="e">The <see cref="Bacodelib.WaitEventArgs"/> instance containing the event data.</param>
        private void OnWaitEvent(WaitEventArgs e)
        {
            EventHandler<WaitEventArgs> handler = WaitEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        } 
        #endregion
        
        #region FindByCol
        /// <summary>
        /// Поиск по колонке
        /// </summary>
        /// <param name="Col">Колонка</param>
        /// <param name="Value">Значение</param>
        /// <returns>номер строки</returns>
        protected int FindByCol(int Col, string Value)
        {
            int result = -1;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i][Col].ToString() == Value)
                {
                    result = i;
                    break;
                }
            }
            return result;
        } 
        #endregion
        
        #region FindBarCode
        /// <summary>
        /// Поиск штрихкода по всей таблице.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="Code">The code.</param>
        /// <returns></returns>
        protected virtual int FindBarCode(DataTable table, string Code)
        {
            int ret = -1;
            string code;
            if (Code.IndexOf("_v") > 0)
            {
                code = Code.Replace("_v", string.Empty);
                int i = 0;
                while (code[i] == '0')
                {
                    i++;
                }
                if (i > 0)
                {
                    code = code.Remove(0, i);
                }
            }
            else
            {
                code = Code;
            }

            for (int i = 0; i < table.Rows.Count; i++)
            {
                if ((table.Rows[i][1].ToString() == code) || (table.Rows[i][6].ToString().PadLeft(13, '0') == code) || (table.Rows[i][3].ToString().PadLeft(13, '0') == code) || (table.Rows[i][4].ToString().PadLeft(13, '0') == code) || (table.Rows[i][5].ToString().PadLeft(13, '0') == code))
                {
                    ret = i;
                    break;
                }
            }

            return ret;
        } 
        #endregion
        
        #region StrToFloat
        /// <summary>
        /// Преобразовать значение в float.
        /// </summary>
        /// <param name="value">Значение</param>
        /// <returns></returns>
        protected float StrToFloat(string value)
        {
            float result;
            if (value.Trim() == "")
                value = "0";
            try
            {
                value = value.Replace(".", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                value = value.Replace(",", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                result = float.Parse(value.Trim());
            }
            catch
            {
                MessageBox.Show("Ошибочное значение.\n" + value.Trim());
                result = 0;
            }
            return result;
        }

        protected float s2f(string value)
        {
            float result;
            if (value.Trim() == "")
                value = "0";
            try
            {
                value = value.Replace(".", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                value = value.Replace(",", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                result = float.Parse(value.Trim());
            }
            catch
            {
                result = 0;
            }
            return result;
        }

        #endregion
        
        #region IDisposable Members

        public void Dispose()
        {
            table.Dispose();
            if (addtable != null)
                addtable.Dispose();
        }

        #endregion
    } 
    #endregion

    public class InputTask : Task
    {
        public InputTask() : base(TaskKind.In)
        {
        }

        public bool ReadBarCode(string BarCode)
        {
            number = FindBarCode(table, BarCode);
            if (number > -1)
            {
                v_name = table.Rows[number][2].ToString();
                v_code = table.Rows[number][3].ToString();
                //v_count_doc = table.Rows[number][7].ToString();
            }
            else
            {
                Beeper.MessageBeep();
                MessageBox.Show("Товар не найден");
                v_name = "Неопределено";
                v_code = BarCode;
            }
            v_count = "1";
            return (number > -1);
        }
        public void CountEntered(string CountValue)
        {
            float count = StrToFloat(CountValue);
            if (number > -1)
            {
                table.Rows[number][7] = float.Parse(table.Rows[number][7].ToString()) + count;
                v_gridrow = number;
            }
            else
            {
                table.Rows.Add(taskname, "", v_name, v_code, "", "", "", count.ToString());
                v_gridrow = table.Rows.Count - 1;
            }
            needSave = true;
        }
    }

    public class OutputTask : Task
    {
        private string v_count_doc = "";

        public OutputTask()
            : base(TaskKind.Out)
        {
        }
        public string vCountDoc
        {
            get { return v_count_doc; }
        }
        public bool ReadBarCode(string BarCode)
        {
            number = v_gridrow = FindBarCode(table, BarCode);
            if (number > -1)
            {
                v_name = table.Rows[number][2].ToString();
                v_code = table.Rows[number][3].ToString();
                v_count_doc = table.Rows[number][7].ToString();
            }
            else
            {
                Beeper.MessageBeep();
                MessageBox.Show("Товар не найден");
                v_name = "Неопределено";
                v_code = BarCode;
                v_count_doc = "0";
            }
            v_count = "1";
            return (number > -1);
        }
        public void CountEntered(string CountValue)
        {
            float count = StrToFloat(CountValue);
            if (number > -1)
            {
                v_gridrow = number;
                if ((float.Parse(v_count_doc) - count) < 0)
                {
                    if (DialogResult.Yes == MessageBox.Show("Вы уверены, что хотите отгрузить больше товара чем в накладной?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                    {
                        table.Rows[number][7] = float.Parse(table.Rows[number][7].ToString()) - count;
                        needSave = true;
                        if (float.Parse(table.Rows[number][7].ToString()) <= 0)
                        {
                            MessageBox.Show("Отгрузка товара завершена");
                        }
                    }
                    else
                    {
                        v_gridrow = -1; // не надо grid
                    }
                }
                else
                {
                    table.Rows[number][7] = float.Parse(table.Rows[number][7].ToString()) - count;
                    needSave = true;
                    if (float.Parse(table.Rows[number][7].ToString()) <= 0)
                        MessageBox.Show("Отгрузка товара завершена");
                }
            }
            else
            {
                table.Rows.Add(taskname, "", v_name, v_code, "", "", "", count.ToString());
                needSave = true;
                v_gridrow = table.Rows.Count - 1;
            }

        }

        #region Save
        /// <summary>
        /// Сохранить в файл
        /// </summary>
        public override bool Save()
        {
            DataView dv = new DataView(table);
            dv.Sort = " count DESC";
            return SaveDataView(dv, "out");
        }
        #endregion

    }

    public class OutputWholesaleTask : Task
    {
        private string v_count_doc = "";
        private bool has_serial;
        private string goods_code;
        public bool HasSerial
        {
            get
            {
                return has_serial;
            }
        }

        public OutputWholesaleTask()
            : base(TaskKind.OutWholesale)
        {
        }
        public string vCountDoc
        {
            get { return v_count_doc; }
        }
        public void ReadBarCode(string BarCode)
        {
            number = v_gridrow = FindBarCode(table, BarCode);
            if (number > -1)
            {
                v_name = table.Rows[number][2].ToString();
                v_code = table.Rows[number][3].ToString();
                v_count_doc = table.Rows[number][7].ToString();
                goods_code = table.Rows[number][1].ToString();
                has_serial = table.Rows[number][10].ToString().StartsWith("1"); // 10
            }
            else
            {
                MessageBox.Show("Товар не найден");
                v_name = "Неопределено";
                v_code = BarCode;
                v_count_doc = "0";
                has_serial = false;
            }
            v_count = "1";
        }
        public void CountEntered(string CountValue)
        {
            float count = StrToFloat(CountValue);
            if (number > -1)
            {
                v_gridrow = number;
                if ((float.Parse(v_count_doc) - count) < 0)
                {
                    if (DialogResult.Yes == MessageBox.Show("Вы уверены, что хотите отгрузить больше товара чем в накладной?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                    {
                        table.Rows[number][7] = float.Parse(table.Rows[number][7].ToString()) - count;
                        needSave = true;
                        if (float.Parse(table.Rows[number][7].ToString()) <= 0)
                        {
                            MessageBox.Show("Отгрузка товара завершена");
                        }
                    }
                    else
                    {
                        v_gridrow = -1; // не надо grid
                    }
                }
                else
                {
                    table.Rows[number][7] = float.Parse(table.Rows[number][7].ToString()) - count;
                    needSave = true;
                    if (float.Parse(table.Rows[number][7].ToString()) <= 0)
                        MessageBox.Show("Отгрузка товара завершена");
                }
            }
            else
            {
                table.Rows.Add(taskname, "", v_name, v_code, "", "", "", count.ToString());
                needSave = true;
                v_gridrow = table.Rows.Count - 1;
            }

        }

        public void SerialNumberEntered(string SerialNumber)
        {
            if ((SerialNumber != String.Empty) && has_serial)
            {
                addtable.Rows.Add(goods_code, SerialNumber);
            }
            
        }

        #region Save
        /// <summary>
        /// Сохранить в файл
        /// </summary>
        public override bool Save()
        {
            DataView dv = new DataView(table);
            dv.Sort = " count DESC";
            SaveDataView(dv, "out");
            return SaveDataView(new DataView(addtable), "sn");
        }
        #endregion

    }

    public class CheckOutputTask : Task
    {
        private string v_count_doc = "";

        public CheckOutputTask()
            : base(TaskKind.CheckOut)
        {
        }
        public string vCountDoc
        {
            get { return v_count_doc; }
        }
        public void ReadBarCode(string BarCode)
        {
            number = v_gridrow = FindBarCode(table, BarCode);
            if (number > -1)
            {
                v_name = table.Rows[number][2].ToString();
                v_code = table.Rows[number][3].ToString();
                v_count_doc = table.Rows[number][7].ToString();
            }
            else
            {
                MessageBox.Show("Товар не найден");
                v_name = "Неопределено";
                v_code = BarCode;
                v_count_doc = "0";
            }
            v_count = "1";
        }
        public void CountEntered(string CountValue)
        {
            float count = StrToFloat(CountValue);
            if (number > -1)
            {
                v_gridrow = number;
                table.Rows[number][7] = float.Parse(table.Rows[number][7].ToString()) + count;
                needSave = true;
            }
            else
            {
                table.Rows.Add(taskname, "", v_name, v_code, "", "", "", count.ToString());
                needSave = true;
                v_gridrow = table.Rows.Count - 1;
            }

        }

        #region Save
        /// <summary>
        /// Сохранить в файл
        /// </summary>
        public override bool Save()
        {
            DataView dv = new DataView(table);
            dv.Sort = " count DESC";
            return SaveDataView(dv, "out");
        }
        #endregion

        public override WareInfo GetInfo(int index)
        {
            return new WareInfo(table.Rows[index][8].ToString(), table.Rows[index][9].ToString(), table.Rows[index][2].ToString(), table.Rows[index][1].ToString(), Convert.ToInt32(table.Rows[index][10]));
        } 


    }

    public class RecalcTask : Task
    {
        public String place = "";
        protected Dictionary<string, DataRow> addTableIndex;
        protected Dictionary<string, DataRow> tableIndex;

        private int w = 0;
        public RecalcTask() : base(TaskKind.Recalc)
        {
        }

        public bool ReadBarCode(string BarCode)
        {
            w = -1; // 1-найден, 0-найден в доп. таблице, -1 не найден
            v_name = "Неопределено";
            v_code = BarCode;

            if (addTableIndex == null)
                addTableIndex = Indexing(addtable);

            if (tableIndex == null)
                tableIndex = Indexing(table);

            try
            {
                number = FindBarcodeByIndex(tableIndex, table, BarCode);
                if (number > -1)
                {
                    v_name = table.Rows[number][2].ToString();
                    v_code = table.Rows[number][3].ToString();
                    place = table.Rows[number]["place"].ToString();
                    w = 1;
                }
                else
                {
                    number = FindBarcodeByIndex(addTableIndex, addtable, BarCode);
                    if (number > -1)
                    {
                        v_name = addtable.Rows[number][2].ToString();
                        v_code = addtable.Rows[number][3].ToString();
                        place = addtable.Rows[number]["place"].ToString();
                        w = 0;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }

            v_count = "1";
            return (w != -1);
        }

        public Dictionary<string, DataRow> Indexing(DataTable table)
        {
            Dictionary<string, DataRow> dict = new Dictionary<string,DataRow>();

            foreach (DataRow dr in table.Rows)
            {
                this.IndexingRow(dict, dr);
            }

            return dict;
        }

        private void IndexingRow(Dictionary<string, DataRow> dict, DataRow dr)
        {
            if (!dict.ContainsKey(dr["code"].ToString()))
            {
                dict.Add(dr["code"].ToString(), dr);
            }
            if (!dict.ContainsKey(dr["code1"].ToString()))
            {
                dict.Add(dr["code1"].ToString(), dr);
            }
            if (!dict.ContainsKey(dr["code2"].ToString()))
            {
                dict.Add(dr["code2"].ToString(), dr);
            }
            if (!dict.ContainsKey(dr["code3"].ToString()))
            {
                dict.Add(dr["code3"].ToString(), dr);
            }
        }

        public void IndexingTableData(DataRow row)
        {
            if (tableIndex == null)
                tableIndex = Indexing(table);

            IndexingRow(tableIndex, row);
        }

        public int FindBarcodeByIndex(Dictionary<string, DataRow> index, DataTable dt, string Code)
        {
            int ret = -1;
            DataRow row;
            string code;

            if (Code.IndexOf("_v") > 0)
            {
                code = Code.Replace("_v", string.Empty);
                int i = 0;
                while (code[i] == '0')
                {
                    i++;
                }
                if (i > 0)
                {
                    code = code.Remove(0, i);
                }
            }
            else
            {
                code = Code;
            }

            try
            {
                if (index.TryGetValue(code, out row))
                {
                    ret = dt.Rows.IndexOf(row);
                }
            }
            catch (Exception wx)
            {
                MessageBox.Show(wx.Message + "\n" + wx.StackTrace);
            }

            return ret;
        }

        public void CountEntered(string CountValue)
        {
            float count = StrToFloat(CountValue);
            if (w == 1)
            {
                table.Rows[number][7] = float.Parse(table.Rows[number][7].ToString()) + count;
                v_gridrow = number;
            }
            else if (w == 0)
            {
                addtable.Rows[number].BeginEdit();
                addtable.Rows[number]["count"] = count.ToString();
                addtable.Rows[number].EndEdit();
                table.ImportRow(addtable.Rows[number]);
                IndexingRow(tableIndex, table.Rows[table.Rows.Count - 1]);
                v_gridrow = table.Rows.Count - 1;
            }
            else
            {
                if (count > 0)
                {
                    table.Rows.Add(taskname, "", v_name, v_code, "", "", "", count.ToString());
                    IndexingRow(tableIndex, table.Rows[table.Rows.Count - 1]);
                    v_gridrow = table.Rows.Count - 1;
                }
            }
            needSave = true;
        }

    }
    public class GrabTask : Task
    {
        private string v_good_code = "";
        private bool v_checked = true;

        public bool V_checked
        {
            get { return v_checked; }
        }

        public GrabTask() : base(TaskKind.Grab)
        {
        }
        public string vGoodCode
        {
            get { return v_good_code; }
        }
        public int FindByCode(string Code)
        {
            number = FindByCol(1, Code);
            if (number > -1)
            {
                v_name = table.Rows[number][2].ToString();
                v_code = table.Rows[number][3].ToString();
                v_good_code = table.Rows[number][1].ToString();
                v_checked = (table.Rows[number][4].ToString() == "1");
            }
            else
            {
                MessageBox.Show("Товар с таким кодом не найден");
            }
            return ((number > -1) ? 1 : 0);
        }
        public int FindByWareCode(string WareCode)
        {
            number = FindByCol(3, WareCode);
            if (number > -1)
            {
                v_name = table.Rows[number][2].ToString();
                v_code = table.Rows[number][3].ToString();
                v_good_code = table.Rows[number][1].ToString();
                v_checked = (table.Rows[number][4].ToString() == "1");
            }
            else
            {
                MessageBox.Show("Товар с таким кодом не найден");
            }
            return ((number > -1) ? 1 : 0);
        }
        public int SetCode(string NewCode)
        {
            table.Rows[number][3] = NewCode;
            needSave = true;
            return number;
        }
        public int SetCheck(bool Value)
        {
            table.Rows[number][4] = Value ? "1" : "";
            needSave = true;
            return number;
        }
    }

    public class CheckTask : Task
    {
        private string v_flag = "";

        public CheckTask()
            : base(TaskKind.Check)
        {
        }

        public string vFlag
        {
            get { return v_flag; }
        }
        public void ReadBarCode(string BarCode)
        {
            v_gridrow = number = FindBarCode(table, BarCode);
            if (number > -1)
            {
                v_name = table.Rows[number][2].ToString();
                v_code = table.Rows[number][1].ToString();
                v_flag = table.Rows[number][9].ToString();
                v_count = table.Rows[number][7].ToString();
            }
            else
            {
                MessageBox.Show("Товар не найден");
            }
        }
        public void SetCheck(string value)
        {
            table.Rows[number][8] = value;
            needSave = true;
            v_gridrow = number;
        }
    }

    public class OutputPackTask : Task
    {
        private int pack;
        private int currentPack;

        public OutputPackTask()
            : base(TaskKind.OutPack)
        {
            currentPack = 0;
        }

        public int Pack
        {
            get { return pack; }
        }

        public int CurrentPack
        {
            get { return currentPack; }
        }

        public int IncCurrentPack()
        {
            return ++currentPack;
        }

        public override void Load(string TaskName)
        {
            this.taskname = TaskName;
            string path_in = String.Format("{0}\\{1}{2}.csv", TaskPath, KindNames.TaskPrefix(TaskKind.OutPack), TaskName);
            string path_out = TaskPath + "\\" + KindNames.TaskPrefix(TaskKind.OutPack) + TaskName + "out.csv";
            file_in = new FileInfo(path_in);
            file_out = new FileInfo(path_out);
            currentPack = 1;
            if (ReadFile(table, file_out) < 1)
            {
                var tmptable = CreateInOutTable("tmp_main");
                ReadFile(tmptable, file_in);
                foreach (DataRow row in tmptable.Rows)
                {
                    int count = Convert.ToInt32(row["count"]);
                    for(int i = 0; i < count; i++)
                    {
                        row["count"] = "1";
                        var rr = new List<object>(row.ItemArray);
                        rr.Add("");
                        table.Rows.Add(rr.ToArray());
                    }
                }
            }
            else
            {
                foreach (DataRow row in table.Rows)
                {
                    if (!string.IsNullOrEmpty(row["pack"].ToString()))
                    {
                        currentPack = Math.Max(CurrentPack, Convert.ToInt32(row["pack"]));
                    }
                }
                
            }
        }

        public bool ReadBarCode(string BarCode)
        {
            number = v_gridrow = FindBarCode(table, BarCode);
            if (number > -1)
            {
                v_name = table.Rows[number][2].ToString();
                v_code = table.Rows[number][3].ToString();
                pack = CurrentPack;
            }
            else
            {
                //Beeper.MessageBeep();
                int otherRowWithThisBarCode = base.FindBarCode(table, BarCode);
                if (otherRowWithThisBarCode > -1)
                {
                    v_name = table.Rows[otherRowWithThisBarCode]["name"].ToString();
                    number = v_gridrow = otherRowWithThisBarCode;
                }
                else
                {
                    MessageBox.Show("Товар не найден");
                    v_name = "Неопределено";
                }
                v_code = BarCode;
                pack = CurrentPack;
            }
            return (number > -1);
        }
        public void CountEntered(string pack, bool editmode)
        {
            if (editmode)
            {
                table.Rows[v_gridrow]["pack"] = pack;
                table.Rows[number][7] = string.IsNullOrEmpty(pack) ? "1" : "0";
                needSave = true;
            }
            else
            {
                if (number > -1)
                {
                    var left = GetNotMarked(table.Rows[number]["good_code"].ToString());
                    v_gridrow = number;

                    if (((left - 1) < 0) /*&& string.IsNullOrEmpty(table.Rows[number]["pack"].ToString())*/)
                    {
                        if (DialogResult.Yes == MessageBox.Show("Вы уверены, что хотите отгрузить больше товара чем в накладной?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                        {
                            var newrow = table.Rows.Add(table.Rows[number].ItemArray);
                            newrow[7] = "0";
                            newrow["pack"] = pack;
                            needSave = true;

                            if ((left - 1) <= 0)
                            {
                                MessageBox.Show("Отгрузка товара завершена");
                            }
                        }
                        else
                        {
                            v_gridrow = -1; // не надо grid
                        }
                    }
                    else
                    {
                        table.Rows[number][7] = "0";
                        table.Rows[number]["pack"] = pack;
                        needSave = true;
                        if ((left - 1) <= 0)
                            MessageBox.Show("Отгрузка товара завершена");
                    }
                }
                else
                {
                    table.Rows.Add(taskname, "", v_name, v_code, "", "", "", 1, pack);
                    needSave = true;
                    v_gridrow = table.Rows.Count - 1;
                }
            }
            if (!string.IsNullOrEmpty(pack))
            {
                currentPack = Int32.Parse(pack);
            }
        }

        protected virtual int FindBarCode(DataTable table, string Code)
        {
            int ret = -1;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (((table.Rows[i][6].ToString().PadLeft(13, '0') == Code) 
                        || (table.Rows[i][3].ToString().PadLeft(13, '0') == Code) 
                        || (table.Rows[i][4].ToString().PadLeft(13, '0') == Code) 
                        || (table.Rows[i][5].ToString().PadLeft(13, '0') == Code))
                    && string.IsNullOrEmpty(table.Rows[i]["pack"].ToString()) )
                {
                    ret = i;
                    break;
                }
            }
            return ret;
        } 

        private int GetNotMarked(string goodCode)
        {
            int result = 0;
            foreach (DataRow row in table.Rows)
            {
                if((row["good_code"].ToString() == goodCode) && (string.IsNullOrEmpty(row["pack"].ToString())))
                {
                    result++;
                }
            }
            return result;
        }

        #region Save
        /// <summary>
        /// Сохранить в файл
        /// </summary>
        public override bool Save()
        {
            DataView dv = new DataView(table);
            dv.Sort = " count DESC";
            return SaveDataView(dv, "out");
        }
        #endregion

    }

    public class InputPackTask : Task
    {
        private string pack;
        private string currentPack;

        public InputPackTask()
            : base(TaskKind.InPack)
        {
            currentPack = string.Empty;
        }

        public string Pack
        {
            get { return pack; }
        }

        public string CurrentPack
        {
            get { return currentPack; }
            //set { currentPack = value; }
        }


        public override void Load(string TaskName)
        {
            this.taskname = TaskName;
            string path_in = String.Format("{0}\\{1}{2}.csv", TaskPath, KindNames.TaskPrefix(TaskKind.InPack), TaskName);
            string path_out = TaskPath + "\\" + KindNames.TaskPrefix(TaskKind.InPack) + TaskName + "out.csv";
            file_in = new FileInfo(path_in);
            file_out = new FileInfo(path_out);
            currentPack = string.Empty;
            if (ReadFile(table, file_out) < 1)
            {
                ReadFile(table, file_in);
                foreach (DataRow row in table.Rows)
                {
                    row["pack"] = string.Empty;
                }
            }
            else
            {
                foreach (DataRow row in table.Rows)
                {
                    if (!string.IsNullOrEmpty(row["pack"].ToString()))
                    {
                        currentPack = row["pack"].ToString();
                    }
                }

            }
            if (string.IsNullOrEmpty(currentPack))
                currentPack = "1";
        }

        private bool IsPackCode(string barCode)
        {
            int dotCount = 0;
            foreach (var c in barCode)
                if (c == '.')
                    dotCount++;
            return dotCount == 3;
        }

        public bool ReadBarCode(string BarCode)
        {
            if (IsPackCode(BarCode))
            {
                currentPack = BarCode.Trim();
                number = v_gridrow = - 1;
                return false;
            }


            if (string.IsNullOrEmpty(currentPack))
            {
                MessageBox.Show("Не указано место");
                number = v_gridrow = -1;
                return false;
            }

            number = v_gridrow = FindBarCode(table, BarCode);
            if (number > -1)
            {
                v_name = table.Rows[number][2].ToString();
                v_code = table.Rows[number][3].ToString();
                if (!string.IsNullOrEmpty(table.Rows[number]["pack"].ToString()))
                {
                    if (DialogResult.Yes == MessageBox.Show("Вы уверены, что хотите установить новое место хранение?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                    {
                        //currentPack = table.Rows[number][8].ToString();
                    }
                    else
                    {
                        number = v_gridrow = -1;
                        return false;
                    }
                }
                pack = CurrentPack;
            }
            else
            {
                //Beeper.MessageBeep();
                v_code = BarCode;
                pack = CurrentPack;
            }

            return (number > -1);

        }
        public void CountEntered(string pack, bool editmode)
        {
            if (editmode)
            {
                table.Rows[v_gridrow]["pack"] = pack;
                table.Rows[number][7] = string.IsNullOrEmpty(pack) ? "1" : "0";
                needSave = true;
            }
            else
            {
                if (number > -1)
                {
                        table.Rows[number][7] = "0";
                        table.Rows[number]["pack"] = pack;
                        needSave = true;
                }
                else
                {
                    table.Rows.Add(taskname, "", v_name, v_code, "", "", "", 1, pack);
                    needSave = true;
                    v_gridrow = table.Rows.Count - 1;
                }
            }

            if (!string.IsNullOrEmpty(pack))
            {
                currentPack = pack;
            }
        }

        private int GetNotMarked(string goodCode)
        {
            int result = 0;
            foreach (DataRow row in table.Rows)
            {
                if ((row["good_code"].ToString() == goodCode) && (string.IsNullOrEmpty(row["pack"].ToString())))
                {
                    result++;
                }
            }
            return result;
        }

        #region Save
        /// <summary>
        /// Сохранить в файл
        /// </summary>
        public override bool Save()
        {
            DataView dv = new DataView(table);
            dv.Sort = " count DESC";
            return SaveDataView(dv, "out");
        }
        #endregion

    }

    public class WareInfo
    {
        #region Поля
        private string place;
        public string Place
        {
            get { return place; }
        }

        private string adr;
        public string Adr
        {
            get { return adr; }
        }

        private string name;
        public string Name
        {
            get { return name; }
        }

        private string code;
        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        private int planOut; // для CheckOutput
        public int PlanOut
        {
            get
            {
                return planOut;
            }
            set
            {
                planOut = value;
            }
        }
        #endregion

        public WareInfo(string Place, string Adr, string Name, string Code)
        {
            this.adr = Adr;
            this.place = Place;
            this.name = Name;
            this.code = Code;
        }
        public WareInfo(string Place, string Adr, string Name, string Code, int PlanOut)
            : this( Place, Adr, Name, Code)
        {
            this.planOut = PlanOut;
        }

    
    }

    public class InputFromSupplierTask : Task
    {
        public InputFromSupplierTask()
            : base(TaskKind.InSup)
        {
        }
        public bool ReadBarCode(string BarCode)
        {
            number = FindBarCode(table, BarCode);
            if (number > -1)
            {
                v_name = table.Rows[number][2].ToString();
                v_code = table.Rows[number][3].ToString();
                //v_count_doc = table.Rows[number][7].ToString();
            }
            else
            {
                Beeper.MessageBeep();
                MessageBox.Show("Товар не найден");
                v_name = "Неопределено";
                v_code = BarCode;
            }
            v_count = "1";
            return (number > -1);
        }
        public void CountEntered(string CountValue)
        {
            float count = StrToFloat(CountValue);
            if (number > -1)
            {
                table.Rows[number][8] = s2f(table.Rows[number][8].ToString()) + count;
                v_gridrow = number;
            }
            else
            {
            }
            needSave = true;
        }
    }

    public class CdpSortTask : Task
    {
        public string DeliveryCode;
        public string Driver;

        public CdpSortTask()
            : base(TaskKind.CDPSort)
        {
        }

        #region FindBarCode
        /// <summary>
        /// Поиск штрихкода по всей таблице.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="Code">The code.</param>
        /// <returns></returns>
        protected override int FindBarCode(DataTable table, string Code)
        {
            int ret = -1;
            string code;
            if (Code.IndexOf("_v") > 0)
            {
                code = Code.Replace("_v", string.Empty);
                int i = 0;
                while (code[i] == '0')
                {
                    i++;
                }
                if (i > 0)
                {
                    code = code.Remove(0, i);
                }
            }
            else
            {
                code = Code;
            }

            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (((table.Rows[i][1].ToString() == code) 
                    || (table.Rows[i][6].ToString().PadLeft(13, '0') == code) 
                    || (table.Rows[i][3].ToString().PadLeft(13, '0') == code) 
                    || (table.Rows[i][4].ToString().PadLeft(13, '0') == code) 
                    || (table.Rows[i][5].ToString().PadLeft(13, '0') == code))
                    && (DeliveryCode != null && table.Rows[i][11].ToString() == DeliveryCode))
                {
                    ret = i;
                    break;
                }
            }
            return ret;
        }
        #endregion

        public int FindDeliveryCode(string deliveryBarCode)
        {
            int ret = -1;

            for (int i = 0; i < table.Rows.Count; i++)
            {
                if ((table.Rows[i][11].ToString() == deliveryBarCode))
                {
                    ret = i;
                    break;
                }
            }
            return ret;
        }

        public bool ReadDeliveryBarCode(string deliveryBarCode)
        {
            number = FindDeliveryCode(deliveryBarCode);

            if (number > -1)
            {
                DeliveryCode = deliveryBarCode;
                Driver = table.Rows[number][10].ToString();
                return true;
            }
            else
            {
                Beeper.MessageBeep();
                MessageBox.Show("Доставка не найдена");
                v_name = "Неопределено";
                v_code = deliveryBarCode;
                return false;
            }

        }

        public bool ReadBarCode(string BarCode)
        {
            int qtyFact, qtyPlan;
            number = FindBarCode(table, BarCode);

            if (number > -1)
            {
                v_name = table.Rows[number][2].ToString();
                v_code = table.Rows[number][1].ToString();
                Driver = table.Rows[number][10].ToString();
                try
                {
                    qtyFact = int.Parse(table.Rows[number][8].ToString());
                    qtyPlan = int.Parse(table.Rows[number][7].ToString());

                    if (qtyPlan > qtyFact)
                    {
                        qtyFact += 1;
                        table.Rows[number][8] = (qtyFact).ToString();

                        if (qtyPlan == qtyFact)
                        {
                            table.Rows[number].Delete();
                        }
                    }
                    else
                    {
                        if (qtyPlan == qtyFact)
                        {
                            table.Rows[number].Delete();
                        }
                    }
                    v_count = qtyFact.ToString();
                    needSave = true;
                }
                catch
                {
                    MessageBox.Show("Не указано количество план, или количество факт");
                }

                //v_count_doc = table.Rows[number][7].ToString();
            }
            else
            {
                Beeper.MessageBeep();
                MessageBox.Show(string.Format("Не найден товар {1} по доставке {0}", DeliveryCode, BarCode));
                v_name = "Неопределено";
                v_code = BarCode;
            }

            return (number > -1);
        }

        public void CountEntered(string CountValue)
        {
            float count = StrToFloat(CountValue);
            if (number > -1)
            {
                table.Rows[number][8] = s2f(table.Rows[number][8].ToString()) + count;
                v_gridrow = number;
            }
            else
            {
                table.Rows.Add(taskname, "", v_name, v_code, "", "", "", count.ToString());
                v_gridrow = table.Rows.Count - 1;
            }
            needSave = true;
        }
    }
}
