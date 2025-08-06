using NAudio.Wave.SampleProviders;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using System.Xml.Linq;
using System.Windows.Forms;

public class RadioStationsGenerator
{
    private int WheelPositionStart = 7500;
    private int WheelPositionIncrement = 100;
    private string StationFolderDirectory;//= "C:\\Los Santos RED Associated Projects\\Project Folders\\Radio Stations\\Generation Test\\Version1";
    public List<RadioStation> RadioStationList { get; private set; }
    public RadioStationsGenerator()
    {
    }

    public void GetListOfStations(string stationFolderDirectory, List<RadioStation> StoredStations)
    {

        StationFolderDirectory = stationFolderDirectory;


        RadioStationList = new List<RadioStation>();
        int currentWheelPosition = WheelPositionStart;
        var userDirs = Directory.GetDirectories(StationFolderDirectory);
        foreach (var dir in userDirs)
        {
            string stationName = Path.GetFileName(dir);// Path.GetDirectoryName(dir));
            string directory = dir;

            RadioStation toMake = new RadioStation(stationName, directory) { WheelPosition = currentWheelPosition };

            if (StoredStations != null)
            {
                RadioStation matchingStoredStation = StoredStations.FirstOrDefault(x => x.Name == stationName);
                if (matchingStoredStation != null)
                {
                    toMake.WheelPosition = matchingStoredStation.WheelPosition;
                    toMake.Genre = matchingStoredStation.Genre;
                    toMake.Flags = matchingStoredStation.Flags;
                    toMake.TextName = matchingStoredStation.TextName;
                }
            }
            RadioStationList.Add(toMake);
            currentWheelPosition += WheelPositionIncrement;
        }
    }

    public void Process(string trackIDstring)
    {


        int TrackID = 18500;//18500;


        if (int.TryParse(trackIDstring, out int parsedID))
        {
            TrackID = parsedID;
        }

        foreach (RadioStation radioStation in RadioStationList)
        {
            radioStation.Generate(TrackID);
            TrackID += radioStation.RadioTracks.Count();
        }

        CreateDat54File();
        CreateDat151File();
        CreateTrackIDFile();
        CreateGXT2File();


        MessageBox.Show("Generated Files. Copy to DLC pack to install","Success");
    }
    private void CreateGXT2File()
    {
        string gxt2File = StationFolderDirectory + "\\american.gxt2.txt";
        File.WriteAllText(gxt2File, "");
        foreach (RadioStation radioStation in RadioStationList)
        {
            WriteText($"{GetFullHash(radioStation.Name.ToLower())} = {radioStation.TextName}", gxt2File);
        }
    }

    private void CreateTrackIDFile()
    {
        string TrackIDFile = StationFolderDirectory + "\\trackid.txt";
        File.WriteAllText(TrackIDFile,"");


        



        foreach (RadioStation radioStation in RadioStationList)
        {
            foreach (RadioTrack radioTrack in radioStation.RadioTracks)
            {





                var tfile = TagLib.File.Create(radioTrack.LeftFileInfo.FullName);
                string title = tfile.Tag.Title;
                string artist = tfile.Tag.AlbumArtists.FirstOrDefault();





                WriteText($"{GetFullHash(radioTrack.TrackID + "a")} = {artist}", TrackIDFile);
                WriteText($"{GetFullHash(radioTrack.TrackID + "s")} = {title}", TrackIDFile);
            }
        }

        //uint cxHashArtist = GenHash(Encoding.ASCII.GetBytes("1602a"));
        //uint cxHashSong = GenHash(Encoding.ASCII.GetBytes("1602s"));

        //WriteText($"0x{string.Format("{0:x8}",cxHashArtist).ToUpper()} = 1602a", TrackIDFile);
        //WriteText($"0x{string.Format("{0:x8}", cxHashSong).ToUpper()} = 1602s", TrackIDFile);


    }
    public string GetFullHash(string toHash)
    {
        uint regularHash = GenHash(Encoding.ASCII.GetBytes(toHash));
        return $"0x{string.Format("{0:x8}", regularHash).ToUpper()}";
    }



    public static uint GenHash(byte[] data)
    {
        uint h = 0;
        for (uint i = 0; i < data.Length; i++)
        {
            h += data[i];
            h += (h << 10);
            h ^= (h >> 6);
        }
        h += (h << 3);
        h ^= (h >> 11);
        h += (h << 15);
        return h;
    }
    private void WriteText(String TextToLog, string OutputFilePath)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(TextToLog + System.Environment.NewLine);
        File.AppendAllText(OutputFilePath, sb.ToString());
        sb.Clear();
    }

    private void CreateDat151File()
    {
        string header = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n<Dat151>\r\n <Version value=\"7516460\" />\r\n <Items>\r\n  <Item type=\"RadioStationList\" ntOffset=\"0\">\r\n   <Name>radio_stations_dlc</Name>\r\n   <Stations>";
        foreach(RadioStation radioStation in RadioStationList)
        {
            header += $"    <Item>{radioStation.Name.ToLower()}</Item>";
        }
        header += "   </Stations>\r\n  </Item>";


        int wheelPosition = 7500;

        foreach (RadioStation radioStation in RadioStationList)
        {
            //header += $"  <Item type=\"RadioStationSettings\" ntOffset=\"122\">\r\n   <Name>{radioStation.Name.ToLower()}</Name>\r\n   <Flags value=\"0xAAA80955\" />\r\n   <WheelPosition value=\"{radioStation.WheelPosition}\" />\r\n   <Genre value=\"7\" />\r\n   <AmbientRadioVol value=\"0\" />\r\n   <RadioName>{radioStation.Name}</RadioName>\r\n   <TrackList>\r\n    <Item>{radioStation.Name.ToLower()}_tracks</Item>\r\n   </TrackList>\r\n  </Item>\r\n  <Item type=\"RadioStationTrackList\" ntOffset=\"124\">\r\n   <Name>{radioStation.Name.ToLower()}_tracks</Name>\r\n   <Unk00 value=\"0xAAAAAAA0\" />\r\n   <TrackType value=\"2\" />\r\n   <Unk01 value=\"0\" />\r\n   <Unk02 value=\"0\" />\r\n   <Unk03 value=\"10\" />\r\n   <Tracks>";
            header += $"  <Item type=\"RadioStationSettings\" ntOffset=\"122\">\r\n   <Name>{radioStation.Name.ToLower()}</Name>\r\n   <Flags value=\"{radioStation.Flags}\" />\r\n   <WheelPosition value=\"{radioStation.WheelPosition}\" />\r\n   <Genre value=\"{radioStation.Genre}\" />\r\n   <AmbientRadioVol value=\"0\" />\r\n   <RadioName>{radioStation.Name}</RadioName>\r\n   <TrackList>\r\n    <Item>{radioStation.Name.ToLower()}_tracks</Item>\r\n   </TrackList>\r\n  </Item>\r\n  <Item type=\"RadioStationTrackList\" ntOffset=\"124\">\r\n   <Name>{radioStation.Name.ToLower()}_tracks</Name>\r\n   <Unk00 value=\"0xAAAAAAA0\" />\r\n   <TrackType value=\"2\" />\r\n   <Unk01 value=\"0\" />\r\n   <Unk02 value=\"0\" />\r\n   <Unk03 value=\"10\" />\r\n   <Tracks>";

            foreach (RadioTrack radioTrack in radioStation.RadioTracks)
            {
                header += $"    <Item>\r\n     <Context />\r\n     <SoundRef>dlc_{radioTrack.TrackName}</SoundRef>\r\n    </Item>";
            }
            wheelPosition += 100;


            header += "   </Tracks>\r\n  </Item>\r\n";

        }

        header += " </Items>\r\n</Dat151>";





        XDocument DAT54 = XDocument.Parse(header);

        string savePath = StationFolderDirectory + "\\dlcgreskfers_game.dat151.rel.xml";

        DAT54.Save(savePath);

    }

    private void CreateDat54File()
    {

        string header = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n<Dat54>\r\n <Version value=\"7314721\" />\r\n <ContainerPaths>";


        foreach (RadioStation radioStation in RadioStationList)
        {
            foreach(RadioTrack radioTrack in radioStation.RadioTracks)
            {
                header += $"  <Item>DLC_RADIO\\{radioTrack.TrackName}</Item>\r\n";
            }
        }
        string bodyText = header + " </ContainerPaths>\r\n <Items>";


        foreach (RadioStation radioStation in RadioStationList)
        {
            foreach (RadioTrack radioTrack in radioStation.RadioTracks)
            {
               
                string trackText = $"  <Item type=\"StreamingSound\">\r\n   <Name>dlc_{radioTrack.TrackName}</Name>\r\n   <Header>\r\n    <Flags value=\"0x0180C001\" />\r\n    <Flags2 value=\"0xAA90AAAA\" />\r\n    <DopplerFactor value=\"0\" />\r\n    <Category>hash_45EB536F</Category>\r\n    <SpeakerMask value=\"0\" />\r\n    <EffectRoute value=\"0\" />\r\n   </Header>\r\n   <Duration value=\"{Math.Round(radioTrack.SecondsLong,0)}\" />\r\n   <ChildSounds>\r\n    <Item>{radioTrack.LeftFileName}</Item>\r\n    <Item>{radioTrack.RightFileName}</Item>\r\n   </ChildSounds>\r\n  </Item>\r\n  <Item type=\"SimpleSound\">\r\n   <Name>{radioTrack.LeftFileName}</Name>\r\n   <Header>\r\n    <Flags value=\"0x00800040\" />\r\n    <Pan value=\"307\" />\r\n    <SpeakerMask value=\"0\" />\r\n   </Header>\r\n   <ContainerName>dlc_radio/{radioTrack.TrackName}</ContainerName>\r\n   <FileName>{radioTrack.LeftFileName}</FileName>\r\n   <WaveSlotIndex value=\"0\" />\r\n  </Item>\r\n  <Item type=\"SimpleSound\">\r\n   <Name>{radioTrack.RightFileName}</Name>\r\n   <Header>\r\n    <Flags value=\"0x00800040\" />\r\n    <Pan value=\"53\" />\r\n    <SpeakerMask value=\"0\" />\r\n   </Header>\r\n   <ContainerName>dlc_radio/{radioTrack.TrackName}</ContainerName>\r\n   <FileName>{radioTrack.RightFileName}</FileName>\r\n   <WaveSlotIndex value=\"0\" />\r\n  </Item>";

                bodyText += trackText;
            }
        }
        bodyText += " </Items>\r\n</Dat54>\r\n";



        XDocument DAT54 = XDocument.Parse(bodyText);

        string savePath = StationFolderDirectory + "\\dlcgreskfers_sounds.dat54.rel.xml";

        DAT54.Save(savePath);

    }
}

