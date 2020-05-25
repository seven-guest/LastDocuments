//-------------------- Класс для работы с документами ----
// Класс Documents реализует работу по хранению данных об используемых документах
// для удобства их дальнейшего запуска без реализации
// поиска их в системе со стороны пользователя.
//                      Copyright (c) by Romanov Alexey
//---------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;                      // Работа с StringBuilder
using System.Text.RegularExpressions;   // Работа с регулярными выражениями
using Microsoft.Win32;                  // Работа с реестром
using System.Diagnostics;               // Работа с Process
using System.Windows.Forms;             // Работа с MessageBox

namespace OpenResetDocs
{
    //-----------------------------------------------------struct Docs --
    public struct Docs:IComparable
    {
        public string Name;         // Имя файла
        public string Path;         // Путь к файлу
        public string Ext;          // Расширение файла
        public DateTime OpenTime;   // Время последнее открытия файла

        /// <summary>
        /// Конструктор
        /// </summary>
        public Docs(string path, string ext)
        {
            this.Name = GetDocName(path);
            this.Path = path;
            this.Ext = ext;
            this.OpenTime = DateTime.Now;
        }
        //============================================================
        /// <summary>
        /// Конструктор
        /// </summary>
        public Docs(string path)
        {
            this.Name = GetDocName(path);
            this.Path = path;
            this.Ext = GetDocExt(path);
            this.OpenTime = DateTime.Now;
        }
        //============================================================
        /// <summary>
        /// Получение имени файла при заданном пути
        /// </summary>
        public static string GetDocName(string path)
        {
            string name;
            string patt = @"^.*\\";             // file.ext
            string patt2 = "[^\"]+";//@"[-_№@~? 0-9a-zA-Zа-яА-Я]+";
            name = (Regex.Replace(path, patt, "")).Trim();
            Regex reg = new Regex(patt2);
            Match match = reg.Match(name);
            name = match.Value;

            return name;
        }
        //==============================================================
        /// <summary>
        /// Получение расширения файла при заданном пути
        /// </summary>
        public static string GetDocExt(string path)
        {
            //string ext;
            string patt = @".\w+$";
            Regex reg = new Regex(patt);
            Match match = reg.Match(path);

            return match.Value;
        }
        //==============================================================

        public int CompareTo(object obj)
        {
            Docs _file = (Docs)obj;
            int result;

            if (this.OpenTime < _file.OpenTime)
                result = -1;
            else if (this.OpenTime > _file.OpenTime)
                result = 1;
            else
                result = 0;

            return result;
        }
    }
    //end Docs ==========================================================    
    //-----------------------------------------------------class Documents --
    class Documents
    {
#region Member fills
        private List<Docs> _docs;       // Список документов
        public List<Docs> docs
        {
            get { return _docs; }
            private set { _docs = value; }
        }

        private Dictionary<string, string> _progs;       // ключ - расширение, значение - путь к программе
        public Dictionary<string, string> progs
        {
            get { return _progs; }
            private set { _progs = value; }
        }
        private string fileName;        // Имя файла с документами
        #endregion
#region Public sectioin - class fills
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Documents()
        {
            fileName = "DocsList.txt";
            docs = new List<Docs>();
            progs = new Dictionary<string, string>();
            ReadDocList();                  // Прочитать файл с документами
            FillProgList();                 // Заполнение списка программ
        }
        /// <summary>
        /// Конструктор
        /// </summary>
        public Documents(string fName)
        {
            fileName = fName;
            docs = new List<Docs>();
            progs = new Dictionary<string, string>();
            ReadDocList();
            FillProgList();                 // Заполнение списка программ
        }
        //===============================================================
        /// <summary>
        /// Открыть документ из списка
        /// </summary>
        public short OpenDocument(string Doc, string Ext)
        {
            short Ok = -1;							// -1 - нет расширения в списке
            if (progs.ContainsKey(Ext))				//  0 - нет программы в списке
            {										//  1 - файл открыт
                string doc = "\"" + Doc + "\"";
                string prg = progs[Ext];

                if (prg != null && prg != "")
                {

                    //Process newProc = Process.Start(progs[Ext], doc);	// можно (doc);
                    Process newProc = Process.Start(prg, doc);	// можно (doc);
                    if (newProc != null)
                    {
                        int ind = GetDocIndex(Doc);
                        if (ind != -1)
                        {
                            docs[ind] = new Docs(Doc, Ext);
                        }
                        Ok = 1;
                    }
                    else
                    { Ok = 0; }
                }
                else
                {
                    Ok = 0;
                }
            }
			//else
			//{ MessageBox.Show("Нет программы, которая смогла бы открыть данный файл."); }
            return Ok;
        }
        //========================================================================
        /// <summary>
        /// Открыть файл с помощью программы, переданной в качестве параметра
        /// </summary>
        public bool OpenDocumentWith(string Doc, string Prog)
        {
            bool Ok = false;
            string OpenDoc = "\"" + Doc + "\"";
            Process OpenProg = Process.Start(Prog, OpenDoc);

            if (OpenProg != null)
            {
                Ok = true;
                int ind = GetDocIndex(Doc);

                if(ind != -1)
                { docs[ind] = new Docs(Doc); }
            }
            return Ok;
        }
        //===============================================================
        /// <summary>
        /// Добавить новый документ
        /// </summary>
        public bool AddNewDocument(Docs doc)
        {
            //bool bOk = true;
            foreach (Docs dc in docs)
            {
                if (dc.Path == doc.Path)
                {
                    return false;
                }
            }
            docs.Add(doc);
            return true;
        }
        //===============================================================
        /// <summary>
        /// Удалить документ из списка
        /// </summary>
        public bool DeleteDocument(string path)
        {
            if (docs.Count != 0)
            {
                int ind = -1;                    // порядковый номер документа в списке
                int i = 0;                      // индекс массива
                foreach (Docs dc in docs)
                {
                    if (path == dc.Path)
                    {
                        ind = i;
                        break;
                    }
                    i++;
                }
                if (ind != -1)
                {
                    docs.RemoveAt(ind);
                    return true;
                }
            }
            return false;
        }
        //===============================================================
        /// <summary>
        /// Сохранить список файлов
        /// </summary>
        public void SaveDocList()
        {
            if (docs.Count != 0)
            {
                FileStream fil = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                StreamWriter stream = new StreamWriter(fil);
                using (stream)
                {
                    foreach (Docs dc in docs)
                    {
                        StringBuilder str = new StringBuilder();
                        str.Append(dc.Name + ";");
                        str.Append(dc.Path + ";");
                        str.Append(dc.Ext + ";");
                        str.Append(dc.OpenTime);
                        stream.WriteLine(str.ToString());
                    }
                }
            }
            else
            { File.Delete(fileName); }
        }
        //===============================================================
#endregion
#region Private section - Some methods must be rewrite to there own static class
        /// <summary>
        /// Отделить путь к программе от полной строки из реестра
        /// </summary>
        private string GetPathFromString(string str)
        {
            if (str.Length != 0)
            {
                string patt = "(^\"[^\"]*\")|(^[^\"]*)";
                Regex reg = new Regex(patt);
                Match match = reg.Match(str);
                string temp = match.Value;

                patt = "[^\"]+\\.[^ \"]+";//"[^\"]+";//".\\$";
                reg = new Regex(patt);
                match = reg.Match(temp);
                return match.Value;
            }
            else
            {
                return "";
            }
        }
        //===============================================================
        //------------------------------------------------------ GetDocIndex(string) --
        // Получение индекса документа из списка по заданному пути
        private int GetDocIndex(string path)
        {
            int index = 0;
            foreach (Docs st in docs)
            {
                if (st.Path == path)
                {
                    return index;
                }
                index++;
            }
            return -1;
        }
        //================================================================
        /// <summary>
        /// Разбиение строки из файла в структуру Docs
        /// </summary>
        private Docs SplitString(string str)
        {
            Docs doc = new Docs();
            string patt = @"^[^;]+|[^;]+|[^;]+$";
            Regex reg = new Regex(patt);
            MatchCollection coll = reg.Matches(str);

            doc.Name = coll[0].Value;
            doc.Path = coll[1].Value;
            doc.Ext = coll[2].Value;
            try
            {
                DateTime.TryParse(coll[3].Value, out doc.OpenTime);
            }
            //catch (Exception e)
            catch (Exception)
            {
                doc.OpenTime = DateTime.Now;
            }
            //foreach (Match match in coll)
            //{            
            //    //doc.Name = match.Groups["Name"].Value;
            //    //doc.Path = match.Groups["Path"].Value;
            //    //doc.Ext = match.Groups["Ext"].Value;
            //    //DateTime.TryParse(match.Groups["Date"].Value, out doc.OpenTime);
            //}
            return doc;
        }
        //===============================================================
        /// <summary>
        /// Заполнение списка программ данными из реестра
        /// </summary>
        private void FillProgList()
        {
            RegistryKey rKey = Registry.ClassesRoot;

            //rKey = rKey.OpenSubKey("Photoshop.Image.11\\shell\\open\\command");
            //string par = (string)rKey.GetValue("");
            //par = GetPathFromString(par);

            string[] Keys = rKey.GetSubKeyNames();  // избыточность, для точности - еще одна поверка
            rKey.Close();
            string patt = @"^\.[0-9a-zA-Zа-яА-Я_]+";
            Regex reg = new Regex(patt);
            Match match;
            for (int i = 0; i < Keys.Length; i++)
            {
                match = reg.Match(Keys[i]);
                if (match.Value != null && match.Value != "")
                {
                    // считываем значение программы открытия расширения
                    rKey = Registry.ClassesRoot.OpenSubKey(Keys[i]);
                    string sProg = (string)rKey.GetValue("");       // (По умолчанию)
                    rKey.Close();
                    // Получаем путь к программе из реестра
                    //if (sProg != null && sProg != "")
                    if (sProg != null || sProg != "")
                    {
                        sProg = sProg + @"\shell\open\command";
                        rKey = Registry.ClassesRoot.OpenSubKey(sProg);
                        if (rKey != null)       //Если такой ключ существует
                        {
                            sProg = (string)rKey.GetValue("");          // (По умолчанию)
                            rKey.Close();

                            if (sProg != null)
                            {
                                sProg = GetPathFromString(sProg);
                            }
                            else
                            {
                                sProg = "";
                            }
                        }
                        else                    // Если такой ключ не существует
                            sProg = "";
                    }
                    else
                    { sProg = ""; }
                    progs.Add(Keys[i], sProg);
                }
            }
            GC.Collect();
            FillProgListByUserChoice();
        }
        //===============================================================
        /// <summary>
        /// Внесение изменений в список программ считыванием пользовательских
        /// настроек связи типов файлов с программами из реестра
        /// </summary>
        private void FillProgListByUserChoice()
        {
            const string RegPath = @"Software\Microsoft\Windows\CurrentVersion\Explorer\FileExts";

            RegistryKey regKey = Registry.CurrentUser.OpenSubKey(RegPath);
            if (regKey != null)
            {
                string[] strSubKeysNames = new string[regKey.SubKeyCount];
                strSubKeysNames = regKey.GetSubKeyNames();

                foreach (string str in strSubKeysNames)
                {
                    // Есть ли в заданном ключе подключ UserChoice
                    RegistryKey rkRegSubKey = regKey.OpenSubKey(str + "\\UserChoice");
                    if(rkRegSubKey != null)
                    {                     
                        // Считываем значение поля ProgID и открываем этот ключ в HKCR\'ProgID'\shell\open\command
                        string strSubKey_ProgID = (string)rkRegSubKey.GetValue("ProgID");
                        rkRegSubKey = Registry.ClassesRoot.OpenSubKey(strSubKey_ProgID + "\\shell\\open\\command");

                        if (rkRegSubKey != null)
                        {
                            //string strSubKeyValue = rkRegSubKey.GetValue("").ToString();        // Переменная 'по умолчанию'
                            string strSubKeyValue = (string)rkRegSubKey.GetValue("");        // Переменная 'по умолчанию'
                            
                            if (strSubKeyValue != null && strSubKeyValue != "" && progs.ContainsKey(str))
                            {
                                progs[str] = GetPathFromString(strSubKeyValue);
                            }
                            rkRegSubKey.Close();
                        }
                    }
                }
                regKey.Close();
            }
        }
        //===============================================================
        /// <summary>
        /// Прочитать файл с документами
        /// </summary>
        private void ReadDocList()
        {
            if (fileName != "" && fileName != null)
            {
                if (File.Exists(fileName))
                {
                    FileStream fil = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    StreamReader stream = new StreamReader(fil);

                    using (stream)
                    {
                        string getLine;
                        while ((getLine = stream.ReadLine()) != null)
                        {
                            if (!docs.Equals(getLine) && getLine != "")   // Если такой файл уже есть в списке
                            {
                                docs.Add(SplitString(getLine));
                            }
                        }
                        GC.Collect();
                    }
                    fil.Close();
                }
            }
            else
            { fileName = "DocsList.txt"; }
        }
        //==============================================================
        #endregion 
    }
}
