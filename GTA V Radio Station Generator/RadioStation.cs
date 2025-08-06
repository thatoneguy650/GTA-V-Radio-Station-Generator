using NAudio.Wave.SampleProviders;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;


public class RadioStation
{
    private DirectoryInfo DirectoryInfo;


    public RadioStation() { }

    public RadioStation(string name, string directory)
    {
        Name = name;
        Directory = directory;
        TextName = name;
    }



    public RadioStation(string name, string textName, int wheelPosition, int genre, string flags)
    {
        Name = name;
        TextName = textName;
        WheelPosition = wheelPosition;
        Genre = genre;
        Flags = flags;
    }



    public string Name { get; set; }

    [XmlIgnore]
    public string Directory { get; set; }


    [XmlIgnore]
    public List<RadioTrack> RadioTracks { get; set; }
    public string TextName { get; set; }
    public int WheelPosition { get; set; }
    public int Genre { get; set; } = 7;
    public string Flags { get; set; } = "0xAAA80955";

    public void Generate(int TrackID)
    {
        DirectoryInfo = new DirectoryInfo(Directory);
        //foreach (FileInfo fi in d.GetFiles())
        //{
        //    if(fi.Extension == ".mp3")
        //    {
        //        ConvertToWAV(fi.FullName);
        //    }
        //}

        ProcessFiles();



        //int TrackID = 2100;
        //RadioTracks = new List<RadioTrack>();
        //foreach (FileInfo fi in DirectoryInfo.GetFiles().Where(x=> x.Extension == ".wav"))
        //{
        //    RadioTrack radioTrack = new RadioTrack(fi, TrackID);
        //    radioTrack.Process();
        //    RadioTracks.Add(radioTrack);
        //    TrackID++;
        //}







        RadioTracks = new List<RadioTrack>();
        var trackFolders = System.IO.Directory.GetDirectories(Directory);
        foreach (var trackDir in trackFolders)
        {
            DirectoryInfo TrackFolderDirectoryInfo = new DirectoryInfo(trackDir);
            FileInfo leftFileInfo = TrackFolderDirectoryInfo.GetFiles().Where(x => x.Extension == ".wav" && x.Name.Contains("_left")).FirstOrDefault();
            FileInfo rightFileInfo = TrackFolderDirectoryInfo.GetFiles().Where(x => x.Extension == ".wav" && x.Name.Contains("_right")).FirstOrDefault();

            //FileInfo existingOAC = DirectoryInfo.GetFiles().Where(x => x.Name.Contains(trackDir + ".oac") && x.Extension == ".xml").FirstOrDefault();
            //int trackIDToUse = TrackID;
            //if(existingOAC != null)
            //{

            //}



            RadioTrack radioTrack = new RadioTrack(leftFileInfo, rightFileInfo, TrackID, DirectoryInfo);
            radioTrack.Process();
            RadioTracks.Add(radioTrack);
            TrackID++;
        }





        //foreach (FileInfo leftFileInfo in DirectoryInfo.GetFiles().Where(x => x.Extension == ".wav" && x.Name.Contains("_left")))
        //{
        //    FileInfo rightFileInfo = new FileInfo(leftFileInfo.FullName.Replace("_left","_right"));
        //    RadioTrack radioTrack = new RadioTrack(leftFileInfo, rightFileInfo, TrackID);
        //    radioTrack.Process();
        //    RadioTracks.Add(radioTrack);
        //    TrackID++;
        //}


        int i = 9;
        i = 0;
    }

    private void ProcessFiles()
    {
        foreach (FileInfo fi in DirectoryInfo.GetFiles().Where(x => x.Extension == ".wav"))
        {
            if(fi.Name.Contains("_left") || fi.Name.Contains("_right"))
            {
                continue;
            }
            if(fi.Name.Contains("-2"))
            {
                string newName = Path.GetFileNameWithoutExtension(fi.Name);
                string folderName = newName.Replace("-2", "");
                newName = newName.Replace("-2", "_left");

                System.IO.Directory.CreateDirectory(fi.Directory + "\\" + folderName);
                string newPath = fi.Directory + "\\" + folderName + "\\" + newName + fi.Extension;
                System.IO.File.Move(fi.FullName, newPath);
            }
            else
            {
                string newName = Path.GetFileNameWithoutExtension(fi.Name);
                string folderName = newName;
                newName += "_right";
                System.IO.Directory.CreateDirectory(fi.Directory + "\\" + folderName);
                string newPath = fi.Directory + "\\" + folderName + "\\" + newName + fi.Extension;
                System.IO.File.Move(fi.FullName, newPath);
            }
        }
    }

    private void ConvertToWAV(string filePath)
    {
        using (var reader = new Mp3FileReader(filePath))
        {

            string outputPath = Path.GetDirectoryName(filePath) + "\\" + Path.GetFileNameWithoutExtension(filePath) + ".wav";

            WaveFileWriter.CreateWaveFile(outputPath, reader);
        }
    }

}

