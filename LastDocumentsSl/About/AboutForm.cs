using System;
using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace AccEditDB.About
{
    public partial class AboutForm : Form
    {
        //---------------------------------------------------------------------
        public AboutForm()
        {
            InitializeComponent();

            Version progVer = null;
            string progName = "";
            string Copyright = "";
            string Company = "";
            string Description = "";
                        
            StringBuilder str = new StringBuilder();
            //AssemblyName assData = Assembly.GetExecutingAssembly().GetName();
            Assembly assemblyData = Assembly.GetExecutingAssembly();

            progVer = assemblyData.GetName().Version;

            //Type attributeType = typeof(AssemblyCopyrightAttribute);
            
            object[] obj = assemblyData.GetCustomAttributes(false);
                        
            foreach (object item in obj)
            {
                if (item.GetType() == typeof(AssemblyTitleAttribute))
                {
                    progName = ((AssemblyTitleAttribute)item).Title;
                }

                if (item.GetType() == typeof(AssemblyCopyrightAttribute))
                {
                    Copyright = ((AssemblyCopyrightAttribute)item).Copyright;
                }

                if (item.GetType() == typeof(AssemblyCompanyAttribute))
                {
                    Company = ((AssemblyCompanyAttribute)item).Company;
                }

                if (item.GetType() == typeof(AssemblyDescriptionAttribute))
                {
                    Description = ((AssemblyDescriptionAttribute)item).Description;
                }
            }

            str.AppendLine(string.Format("{0}", progName));
            str.AppendLine(string.Format("Версия продукта {0}.{1}.{2}", progVer.Major, progVer.Minor, progVer.Build));
            str.AppendLine(string.Format("{0}", Company));
            str.AppendLine(string.Format("Разработчик: {0}", Copyright));
            //str.AppendLine(string.Format("Описание: {0}", Description));
            
            
            //str.AppendLine(string.Format("Name: {0}", assData.Name));
            //str.AppendLine(string.Format("EscapedCodeBase: {0}", assData.EscapedCodeBase));
            //str.AppendLine(string.Format("CultureInfo: {0}", assData.CultureInfo));
            //str.AppendLine(string.Format("CodeBase: {0}", assData.CodeBase));

            //FileInfo fInfo = new FileInfo(Assembly.GetEntryAssembly().Location);
            
            //str.AppendLine(string.Format("Дата создания: {0}", fInfo.CreationTime));
            //str.AppendLine(string.Format("Дата модификации: {0}", fInfo.LastWriteTime));
            
            



            //str.Append("FullName: " + curAssembly.FullName + "\n");
            

            lText.Text = str.ToString();
        }
        //=====================================================================

        //---------------------------------------------------------------------void bOk_Click() --
        private void bOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //=====================================================================
    }
}
