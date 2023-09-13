using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;

namespace Photo_Manager.Views
{
    /// <summary>
    /// Interaction logic for AddDirectoryView.xaml
    /// </summary>
    /// 
    public class jsonfile 
    {
        public string dir { get; set; }
    }

    public partial class AddDirectoryView : System.Windows.Controls.UserControl
    {

        public AddDirectoryView()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
        }

        private void AddDirectory_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string _path = @".\date.json";

            if (!File.Exists(_path))File.CreateText(_path).Close();

            var _newdir = newDirectoryName.Text.ToString();

            if (Directory.Exists(_newdir))
            {
                var jsonData = System.IO.File.ReadAllText(_path);
                var dirList = JsonConvert.DeserializeObject<List<jsonfile>>(jsonData) ?? new List<jsonfile>();

                if (dirList.Find(x => x.dir == _newdir) == null)
                {

                    dirList.Add(new jsonfile()
                    {
                        dir = _newdir,
                    });

                    jsonData = JsonConvert.SerializeObject(dirList);
                    System.IO.File.WriteAllText(_path, jsonData);
                }
                else 
                {
                    var errorview = new ErrorView("Podana ścieżka jest już zapisana");
                    errorview.ShowDialog();
                }
            }
            else
            {
                var errorview = new ErrorView("Podana ścieżka nie istnieje");
                errorview.ShowDialog();
            }

        }

        private void btnChooseDirectory_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                newDirectoryName.Text = folderBrowserDialog.SelectedPath;
            }
        }
    }
}
