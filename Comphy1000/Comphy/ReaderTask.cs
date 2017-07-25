using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Comphy
{
    public enum TaskKind
    {
        None,
        In,
        Out,
        Recalc
    }

    class ReaderTask
    {
        private string TaskDirectory = "";
        private TaskKind currentTaskKind = TaskKind.None;
        /*
         * временная
         */
        public TaskKind TaskKindByInt( int Value )
        {
            switch (Value)
            {
                case 0: return TaskKind.In; break;
                case 1: return TaskKind.Out; break;
                case 2: return TaskKind.Recalc; break;
                default: return TaskKind.None; break;
            }
        }
        public ReaderTask(string TaskDirertory)
        {
            this.TaskDirectory = TaskDirectory;
        }
        public Object[] GetTaskList( System.Collections.IList list )
        {
            Object[] result = {};
            DirectoryInfo dir = new DirectoryInfo(TaskDirectory);
            string mask = "";
            switch (currentTaskKind)
            {
                case TaskKind.In: mask = "pr*.csv"; break;
                case TaskKind.Out: mask = "ot*.csv"; break;
                case TaskKind.Recalc: mask = "in*.csv"; ; break;
                default: break;
            }
            FileInfo[] files = dir.GetFiles(mask);
            for (int i = 0; i < files.Length; i++)
                if (files[i].Name.ToString()[files[i].Name.Length - 5] != 't')
                    result.SetValue(files[i].Name.ToString().Split('.')[0], result.Length-1);
            return result;
        }
        public TaskKind CurrentTaskKind
        {
            get { return currentTaskKind; }
            set { currentTaskKind = value; }
        }

    }
}
