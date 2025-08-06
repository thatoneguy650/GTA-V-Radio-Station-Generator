using NAudio.Wave.SampleProviders;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using TagLib.Mpeg;

namespace GTA_V_Radio_Station_Generator
{
    public partial class Form1 : Form
    {
        private RadioStationsGenerator RadioStationsGenerator;
        private List<RadioStation> StoredStations = new List<RadioStation>();
        private string StoredStationsPath = $"StoredStations.xml";

        public Form1()
        {
            InitializeComponent();
            AddVanillaExample();
            ReadStoredStations();
        }

        private void ReadStoredStations()
        {
            if (System.IO.File.Exists(StoredStationsPath))
            {

                StoredStations = Helper.DeserializeParams<RadioStation>(StoredStationsPath);
            }

        }

        private void AddVanillaExample()
        {

            List<RadioStation> VanillaList = new List<RadioStation>()
            {
                new RadioStation("RADIO_01_CLASS_ROCK", "Los Santos Rock Radio", 1000, 1, "0xAA800A54")
                ,new RadioStation("RADIO_02_POP", "Non-Stop-Pop FM", 2000, 3, "0xAA800A54")
                ,new RadioStation("RADIO_03_HIPHOP_NEW", "Radio Los Santos", 3000, 4, "0xAA800A62")
                ,new RadioStation("RADIO_04_PUNK", "Channel X", 4000, 6, "0xAA800A62")
                ,new RadioStation("RADIO_05_TALK_01", "West Coast Talk Radio", 5000, 7, "0xAA800955")
                ,new RadioStation("RADIO_06_COUNTRY", "Rebel Radio", 6000, 9, "0xAA800A56")
                ,new RadioStation("RADIO_07_DANCE_01", "Soulwax FM", 7000, 10, "0xAA800A0A")
                ,new RadioStation("RADIO_08_MEXICAN", "East Los FM", 8000, 11, "0xAA800A0A")
                ,new RadioStation("RADIO_09_HIPHOP_OLD", "West Coast Classics", 9000, 4, "0xAA800A66")
                ,new RadioStation("RADIO_11_TALK_02", "Blaine County Radio", 11000, 8, "0xAA800915")
                ,new RadioStation("RADIO_12_REGGAE", "Blue Ark", 12000, 12, "0xAA800A62")
                ,new RadioStation("RADIO_13_JAZZ", "Worldwide FM", 13000, 13, "0xAA80092A")
                ,new RadioStation("RADIO_14_DANCE_02", "FlyLo FM", 14000, 10, "0xAA80090A")
                ,new RadioStation("RADIO_15_MOTOWN", "The Lowdown 91.1", 15000, 14, "0xAA800A56")
                ,new RadioStation("RADIO_16_SILVERLAKE", "Radio Mirror Park", 16000, 1, "0xAA800A56")
                ,new RadioStation("RADIO_17_FUNK ", "Space 103.2", 17000, 14, "0xAA800A66")
                ,new RadioStation("RADIO_18_90S_ROCK ", "Vinewood Boulevard Radio", 18000, 1, "0xAA800A62")


            };

            dgvVanilla.Rows.Clear();
            var list = new BindingList<RadioStation>(VanillaList);
            dgvVanilla.DataSource = list;
            dgvVanilla.Columns["Directory"].Visible = false;
            dgvVanilla.ReadOnly = true;
            dgvVanilla.AllowUserToAddRows = false;
            dgvVanilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvVanilla.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvVanilla.AllowUserToDeleteRows = false;
            dgvVanilla.Refresh();



            dgvGenre.Rows.Clear();
            dgvGenre.Rows.Add("Rock", "1");
            dgvGenre.Rows.Add("Pop", "3");
            dgvGenre.Rows.Add("Hip-Hop", "4");
            dgvGenre.Rows.Add("Punk", "6");
            dgvGenre.Rows.Add("Talk Radio", "7");
            dgvGenre.Rows.Add("Country", "9");
            dgvGenre.Rows.Add("Electronica", "10");
            dgvGenre.Rows.Add("Latin", "11");
            dgvGenre.Rows.Add("Reggae", "12");
            dgvGenre.Rows.Add("World", "13");
            dgvGenre.Rows.Add("Funk/Soul", "14");


            dgvGenre.Refresh();



        }

        private void button1_Click(object sender, EventArgs e)
        {

            using (var fbd = new FolderBrowserDialog())
            {
                fbd.SelectedPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    string[] files = Directory.GetFiles(fbd.SelectedPath);
                    string folderName = Directory.GetFiles(fbd.SelectedPath).FirstOrDefault();
                    //System.Windows.Forms.MessageBox.Show("Files found: " + files.Length.ToString(), "Message");

                    RadioStationsGenerator = new RadioStationsGenerator();
                    RadioStationsGenerator.GetListOfStations(fbd.SelectedPath, StoredStations);
                    PopulateGrid();


                }
            }



            //CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            //dialog.InitialDirectory = "C:\\Users";
            //dialog.IsFolderPicker = true;
            //if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            //{
            //    RadioStationsGenerator = new RadioStationsGenerator();
            //    RadioStationsGenerator.GetListOfStations(dialog.FileName);
            //    PopulateGrid();
            //}
            //else
            //{

            //}
        }
        private void btnCreateStations_Click(object sender, EventArgs e)
        {
            if(RadioStationsGenerator == null)
            {
                return;
            }
            RadioStationsGenerator.Process(tbTrackID.Text);
            WriteStationsToFile();
        }
        private void WriteStationsToFile()
        {
            List<RadioStation> toWrite = new List<RadioStation>();
            toWrite.AddRange(RadioStationsGenerator.RadioStationList);
            foreach(RadioStation stat in StoredStations)
            {
                if(!RadioStationsGenerator.RadioStationList.Any(x=> x.Name == stat.Name))
                {
                    toWrite.Add(stat);
                }
            }
            Helper.SerializeParams(toWrite, StoredStationsPath);

        }
        private void PopulateGrid()
        {
            dgvRadioStations.Rows.Clear();
            var list = new BindingList<RadioStation>(RadioStationsGenerator.RadioStationList);
            dgvRadioStations.DataSource = list;
            dgvRadioStations.Columns["Directory"].Visible = false;
            dgvRadioStations.Columns["Name"].ReadOnly = true;
            dgvRadioStations.AllowUserToAddRows = false;
            dgvRadioStations.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvRadioStations.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvRadioStations.AllowUserToDeleteRows = false;
            dgvRadioStations.Refresh();
        }
    }
}
