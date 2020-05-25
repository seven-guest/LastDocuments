using System;
using System.Collections.Generic;
using System.ComponentModel;            // Для компонента GridView
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenResetDocs;
using Microsoft.Win32;

namespace OpenResetDocs
{
    public struct ColumnsParams
    {
        public int opt_1;      // ->
        public int opt_2;      // Name
        public int opt_3;      // Path
        public int opt_4;      // Ext
        public int opt_5;      // Date
        public int opt_6;      // Form->With
        public int opt_7;      // Form->Heigth

        public void SetWithToReg(int[] opttions)//(int w1, int w2, int w3, int w4, int w5, int w6)
        {
            opt_1 = opttions[0];
            opt_2 = opttions[1];
            opt_3 = opttions[2];
            opt_4 = opttions[3];
            opt_5 = opttions[4];
            opt_6 = opttions[5];
            opt_7 = opttions[6];
          
            RegistryKey reg = Registry.CurrentUser.OpenSubKey("Software\\OpenResetDocs\\Settings", true);

            if (reg == null)
            {
                reg = Registry.CurrentUser.CreateSubKey("Software\\OpenResetDocs\\Settings");             
            }
            reg.SetValue("with_1", opt_1);
            reg.SetValue("With_name", opt_2);
            reg.SetValue("With_path", opt_3);
            reg.SetValue("With_ext", opt_4);
            reg.SetValue("With_date", opt_5);
            reg.SetValue("With_form", opt_6);
            reg.SetValue("Height_form", opt_7);

            reg.Close();
        }

        public bool GetWithFromReg()
        {
            RegistryKey reg = Registry.CurrentUser.OpenSubKey("Software\\OpenResetDocs\\Settings", true);

            if (reg == null)
                return false;
                        
            opt_1 = (int)reg.GetValue("with_1");
            opt_2 = (int)reg.GetValue("With_name");
            opt_3 = (int)reg.GetValue("With_path");
            opt_4 = (int)reg.GetValue("With_ext");
            opt_5 = (int)reg.GetValue("With_date");
            opt_6 = (int)reg.GetValue("With_form");
            opt_7 = (int)reg.GetValue("Height_form");
            reg.Close();

            return true;
        }
    }    
    //------------------------------------------------------------- class Form1 --
    public partial class MainForm : Form
	{
#region Поля класса
		Documents documents;            // Объект для работы с документами
        int indexDoc;                   // Индекс в таблице текущего документа
        ColumnsParams columns;          // Для запоминания ширины колонок        
#endregion
		public MainForm()
        { 
            InitializeComponent();            
        }
        //===================================================================   
        /// <summary>
        /// Программа выводит данные о программе, с помощью которой
        /// возможно открыть данный файл
        /// </summary>
        private void GetProgData(string Key)
        {
            string Path;
            if(documents.progs.TryGetValue(Key, out Path))
            {                
                lPath.Text = "Программа: " + Path;
                lExt.Text = "Расширение:  " + Key;
            }
			else
			{
				lPath.Text = "Программа: ";
				lExt.Text = "Расширение:  ";
			}
        }
        //===================================================================
        /// <summary>
        /// Загрузка приложения
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {           
            dGrid.Columns.Add("Name", "Документ");
            dGrid.Columns.Add("Path", "Путь");
            dGrid.Columns.Add("Ext", "Расширение");
            dGrid.Columns.Add("Date", "Последнее открытие");
            dGrid.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            documents = new Documents();
            foreach (Docs doc in documents.docs)
            {
                dGrid.Rows.Add(doc.Name, doc.Path, doc.Ext, doc.OpenTime);
            }            
            dGrid.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            dGrid.Sort(dGrid.Columns["Date"], ListSortDirection.Descending);

            // Чтение информации о программе в строку состояния
            if(dGrid.RowCount > 0)
            { GetProgData(dGrid["Ext", 0].Value.ToString()); }

            // Установка курсора на первой строке
            if (dGrid.RowCount != 0)
            { dGrid.CurrentCell = dGrid["Name", 0]; }

            // Установка размеров столбцов
            if (columns.GetWithFromReg())
            {
                //dGrid.Columns[0].Width = columns.opt_1;
                dGrid.RowHeadersWidth = columns.opt_1;
                dGrid.Columns["Name"].Width = columns.opt_2;
                dGrid.Columns["Path"].Width = columns.opt_3;
                dGrid.Columns["Ext"].Width = columns.opt_4;
                dGrid.Columns["Date"].Width = columns.opt_5;
                this.Width = columns.opt_6;
                this.Height = columns.opt_7;
            }
        }
        //===================================================================

        //---------------------------------------------------------------------void Searching() --
        /// <summary>
        /// Ф-ия реализации алгоритма поиска по тексту таблицы
        /// </summary>
        /// <param name="searchText">Искомое значение текста</param>
        /// <param name="FromBegin">Флаг поиска от начала документа</param>
        private void Searching(string searchText, bool FromBegin = false)
        {
            int Row = dGrid.CurrentRow.Index + 1;
            bool Match = false;                                     // Флаг нахождения совпадения

            if (FromBegin)
            {
                Row = 0;
            }

            for (; Row < dGrid.Rows.Count; Row++)
            {
                if ((dGrid["Name", Row].Value.ToString()).IndexOf(tTextSearch.Text, StringComparison.CurrentCultureIgnoreCase) != -1)
                {
                    dGrid.CurrentCell = dGrid["Name", Row];
                    dGrid.Focus();

                    Match = true;

                    break;
                }
            }

            if (!Match)
            {
                DialogResult dlg = MessageBox.Show("Совпадений не найдено. Начать поиск с начала?", "Поиск", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dlg == System.Windows.Forms.DialogResult.OK)
                {
                    dGrid.CurrentCell = dGrid["Name", 0];
                    Searching(searchText, true);
                }
            }
        }
        //=====================================================================
        //---------------------------------------------------------------------
        /// <summary>
        /// Добавление нового документа
        /// </summary>
        private void tbAdd_Click(object sender, EventArgs e)
        {            
            OpenFileDialog dlg = new OpenFileDialog();
            DialogResult dRes = DialogResult.Cancel;
            do 
            {
                dRes = DialogResult.Cancel;
                dlg.DefaultExt = "*.*";
                dlg.Filter = "Все файлы|*.*";

                if (DialogResult.OK == dlg.ShowDialog())
                {
                    if (dlg.FileName != "")
                    {
                        if (!AddDoc_funtion(dlg.FileName))
                        {                            
                            for (int i = 0; i < dGrid.RowCount; i++)
                            {
                                if ((string)dGrid["Path", i].Value == dlg.FileName)      //??????????? Доделать
                                {
                                    //dGrid["Path", i].;
                                    //MessageBox.Show("Такой документ уже находится в списке");
                                    dRes = MessageBox.Show("Такой документ уже находится в списке", "Совпадение",
                                                    MessageBoxButtons.RetryCancel, MessageBoxIcon.Question);
                                    break;
                                }
                            }
                        }
                    }
                }
            } 
            while(dRes == DialogResult.Retry);
        }
        //===================================================================
        //----------------------------------------------------AddDoc_funtion() --
        private bool AddDoc_funtion(string fileName)
        {
            Docs dc = new Docs(fileName);
            if (documents.AddNewDocument(dc))
            {
                dGrid.Rows.Add(dc.Name, dc.Path, dc.Ext, dc.OpenTime);
                
                // Открытие документа, если стоит флажок
                if (cbOpen.Checked)
                {
                    documents.OpenDocument(dc.Path, dc.Ext);
                }
                dGrid.Sort(dGrid.Columns["Date"], ListSortDirection.Descending);
                dGrid.CurrentCell = dGrid["Name", 0];       // Установка курсора
                GetProgData(dGrid["Ext", dGrid.CurrentRow.Index].Value.ToString());

                return true;
            }
            return false;
        }
        //====================================================================
        /// <summary>
        /// Открытие документа
        /// </summary>
        private void dGrid_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)       // если не первый служебный столбец
            {
                string filePath = (string)dGrid["Path", e.RowIndex].Value;
                string fileExt = (string)dGrid["Ext", e.RowIndex].Value;

				OpenDocResult(filePath, fileExt, e.RowIndex);
            }
        }
        //===================================================================
        /// <summary>
        /// Закрытие приложения
        /// </summary>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            documents.SaveDocList();
        }
        //===================================================================
        /// <summary>
        /// Кнопка "Удалить документ"
        /// </summary>
        private void tbDel_Click(object sender, EventArgs e)
        {
            if (!documents.DeleteDocument((string)dGrid["Path", indexDoc].Value))
            {
                MessageBox.Show("Нет такого документа в списке.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            dGrid.Rows.RemoveAt(indexDoc);

            if (dGrid.RowCount != 0)
            { GetProgData(dGrid["Ext", indexDoc].Value.ToString()); }
            else
            {
                lPath.Text = "Программа: ";
                lExt.Text = "Расширение:  ";
            }
        }
        //===================================================================
        /// <summary>
        /// Событие при активации к-л ячейки таблицы
        /// </summary>
        private void dGrid_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            indexDoc = e.RowIndex;
        }
		//===================================================================
        /// <summary>
        /// Кнопка "Сохранить ширину столбцов"
        /// </summary>
        private void bFix_Click(object sender, EventArgs e)
        {
            int[] optt = new int[7];
            optt[0] = dGrid.RowHeadersWidth;  //dGrid.Columns[0].Width;
            optt[1] = dGrid.Columns["Name"].Width;
            optt[2] = dGrid.Columns["Path"].Width;
            optt[3] = dGrid.Columns["Ext"].Width;
            optt[4] = dGrid.Columns["Date"].Width;
            optt[5] = this.Width;
            optt[6] = this.Height;
            columns.SetWithToReg(optt);
            //columns.SetWithToReg(dGrid.Columns[0].Width, dGrid.Columns["Name"].Width,
            //        dGrid.Columns["Path"].Width, dGrid.Columns["Ext"].Width, 
            //        dGrid.Columns["Date"].Width, this.Width);
        }
		//===================================================================
		//----------------------------------------------- dGrid_CellMouseClick() --
        private void dGrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
			if (e.RowIndex != -1)
			{
				indexDoc = e.RowIndex;
				
				//if (bLeftButtonOk)
				if(e.Button == MouseButtons.Left)
				{ GetProgData((string)dGrid["Ext", e.RowIndex].Value); }
				else if ((MouseButtons)e.Button == MouseButtons.Right)
				{
					popMenu.Show(Cursor.Position);			// Показать контекстное меню
				}
			}			
        }
		//===================================================================
		//-------------------------------------------- dGrid_MouseDown(object, MouseEventArgs) --
        private void dGrid_MouseDown(object sender, MouseEventArgs e)
        {
			//if (e.Button == MouseButtons.Left)
			//{
			//	bLeftButtonOk = true;
			//}
			//else
			//	bLeftButtonOk = false;
        }
        //===================================================================
		//----------------------------------------------------------- popOpen_Click() --
		private void popOpen_Click(object sender, EventArgs e)
		{
			string filePath = (string)dGrid["Path", indexDoc].Value;
			string fileExt = (string)dGrid["Ext", indexDoc].Value;

			OpenDocResult(filePath, fileExt, indexDoc);
		}
        //===================================================================        
		private void OpenDocResult(string filePath, string fileExt, int rowIndex)
		{
			short openResult = documents.OpenDocument(filePath, fileExt);
			switch (openResult)
			{
				case 1:
					dGrid["Date", rowIndex].Value = DateTime.Now;
					//dGrid.Sort(dGrid.SortedColumn, ListSortDirection.Descending);
					break;

				case 0:
					MessageBox.Show("Нет программы, которая смогла бы открыть данный файл.", "Открытие файла", 
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					break;

				case -1:
                    MessageBox.Show("Отсутствует связь программы с данным расширением.", "Открытие файла", 
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					break;
			}
		}
        //=================================================================
        /// <summary>
        /// Открыть с помощью другой программы
        /// </summary>
        private void toolOpenWith_Click(object sender, EventArgs e)
        {
            string filePath = (string)dGrid["Path", indexDoc].Value;
            //string fileExt = (string)dGrid["Ext", indexDoc].Value;

            OpenFileDialog dlgOpen = new OpenFileDialog();
            dlgOpen.DefaultExt = "*.exe";
            dlgOpen.Filter = "Приложение|*.exe";

            if (DialogResult.OK == dlgOpen.ShowDialog())
            {
                if (System.IO.File.Exists(dlgOpen.FileName))
                {
                    if (documents.OpenDocumentWith(filePath, dlgOpen.FileName))
                    {
                        dGrid["Date", indexDoc].Value = DateTime.Now;
                        dGrid.Sort(dGrid.SortedColumn, ListSortDirection.Descending);
                    }
                    else
                    {
                        MessageBox.Show("Выбраннам программа не смогла открыть файл!", "Открыть с помощью...",
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("По заданному пути файл не существует!", "Открыть с помощью...",
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
        //================================================================
        /// <summary>
        /// Обработка события нажатия клавиш клавиатуры
        /// </summary>
        private void dGrid_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:               {
                    int index = Math.Min(((DataGridView)sender).CurrentRow.Index + 1, ((DataGridView)sender).RowCount - 1);
                    GetProgData(dGrid["Ext", index].Value.ToString());
                    break;
                }
                case Keys.Up:
                {
                    int index = Math.Max(((DataGridView)sender).CurrentRow.Index - 1, 0);
                    GetProgData(dGrid["Ext", index].Value.ToString());
                    break;
                }
                case Keys.Return:
                {
                    string filePath = (string)dGrid["Path", ((DataGridView)sender).CurrentRow.Index].Value;
                    string fileExt = (string)dGrid["Ext", ((DataGridView)sender).CurrentRow.Index].Value;

                    OpenDocResult(filePath, fileExt, ((DataGridView)sender).CurrentRow.Index);
                    e.Handled = true;

                    break;
                }
                case Keys.F3:
                {
                    tSearch_Click(tTextSearch.Text, new EventArgs());

                    break;
                }
                case Keys.Delete:
                {
                    if (e.Control)
                    {
                        if (dGrid.Rows.Count != 0)
                        {
                            string dialogText = string.Format("Удалить из списка файл \"{0}\"", (string)dGrid["Name", ((DataGridView)sender).CurrentRow.Index].Value);
                            DialogResult dRes = MessageBox.Show(dialogText, "Удаление файла",
                                            MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                            if (dRes == System.Windows.Forms.DialogResult.OK)
                            {
                                tbDel_Click(this, null);
                            }
                        }
                    }

                    break;
                } 

            }
        }
        //==================================================================
        //--------------------------------------------------------------dGrid_DragDrop() --
        private void dGrid_DragDrop(object sender, DragEventArgs e)
        {
            this.Activate();
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] fileList = (string[])e.Data.GetData(DataFormats.FileDrop);
                //List<string> BadFileList = new List<string>();
                StringBuilder BadFileList = new StringBuilder();
                foreach (string stDoc in fileList)
                {
                    if (!AddDoc_funtion(stDoc))
                    {
                        BadFileList.AppendLine(stDoc);
                    }
                }

                if(BadFileList.Length != 0)
                {
                    MessageBox.Show(BadFileList.ToString(), "Совпадения", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                GC.Collect();
            }
        }
        //===================================================================
        //-------------------------------------------------------------------dGrid_DragOver() --
        private void dGrid_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }       
        //==================================================================        

        //---------------------------------------------------------------------void bAbout_Click() --
        // Кнопка "Программ инфо"
        private void bAbout_Click(object sender, EventArgs e)
        {
            About.AboutForm fAbout = new About.AboutForm();
            fAbout.ShowDialog();
        }        
        //=====================================================================

        //---------------------------------------------------------------------void tSearch_Click() --
        // Кнопка "Поиск по полю Документ"
        private void tSearch_Click(object sender, EventArgs e)
        {
            if (tTextSearch.Text.Length != 0)
            {
                Searching(tTextSearch.Text);                
            }
        }
        //=====================================================================

        //---------------------------------------------------------------------void MainForm_KeyDown() --
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F3)
            {
                tSearch_Click(tTextSearch.Text, new EventArgs());
            }
        }        
        //=====================================================================

        //---------------------------------------------------------------------
        private void tTextSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                tSearch_Click(tTextSearch.Text, new EventArgs());
            }
        }
        //=====================================================================
    } // end_class
}
