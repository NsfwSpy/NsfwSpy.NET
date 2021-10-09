using NsfwSpyNS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NsfwSpyNS.App
{
    public partial class Form1 : Form
    {
        private NsfwSpy _nsfwSpy;

        public Form1()
        {
            InitializeComponent();
            _nsfwSpy = new NsfwSpy();
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            var dialogResult = openFileDialog1.ShowDialog();
            if (dialogResult != DialogResult.OK) return;

            var fileLocation = openFileDialog1.FileName;
            txtFileLocation.Text = fileLocation;
        }

        private async void txtFileLocation_TextChangedAsync(object sender, EventArgs e)
        {
            var fileLocation = txtFileLocation.Text;
            await LoadImage(fileLocation);
        }

        private async Task LoadImage(string fileLocation)
        {
            if (File.Exists(fileLocation))
            {
                pictureBox1.Image = Image.FromFile(fileLocation);

                var result = await Task.Run(() => _nsfwSpy.ClassifyImage(fileLocation));
                SetResults(result);
            }
            else if (Uri.IsWellFormedUriString(fileLocation, UriKind.Absolute))
            {
                try
                {
                    var webClient = new WebClient();
                    var stream = webClient.OpenRead(fileLocation);
                    var image = Image.FromStream(stream);
                    pictureBox1.Image = image;

                    var result = await Task.Run(() => _nsfwSpy.ClassifyImage(new Uri(fileLocation)));
                    SetResults(result);
                }
                catch (Exception) { }
            }
        }

        private void SetResults(NsfwSpyResult result)
        {
            var resultsDictionary = result.ToDictionary();

            lblScore1.Text = resultsDictionary.Keys.ElementAt(0);
            lblScore2.Text = resultsDictionary.Keys.ElementAt(1);
            lblScore3.Text = resultsDictionary.Keys.ElementAt(2);
            lblScore4.Text = resultsDictionary.Keys.ElementAt(3);

            lblValue1.Text = resultsDictionary.Values.ElementAt(0).ToString();
            lblValue2.Text = resultsDictionary.Values.ElementAt(1).ToString();
            lblValue3.Text = resultsDictionary.Values.ElementAt(2).ToString();
            lblValue4.Text = resultsDictionary.Values.ElementAt(3).ToString();
        }
    }
}
