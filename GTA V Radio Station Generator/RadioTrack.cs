using NAudio.Wave.SampleProviders;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


public class RadioTrack
{
    public RadioTrack(FileInfo fileInfo, int trackID, DirectoryInfo radioStationDirectory)
    {
        OriginalFileInfo = fileInfo;
        TrackName = Path.GetFileNameWithoutExtension(fileInfo.Name);
        TrackID = trackID;
        RadioStationDirectory = radioStationDirectory;
    }


    public RadioTrack(FileInfo leftFileInfo, FileInfo rightFileInfo, int trackID, DirectoryInfo radioStationDirectory)
    {
        LeftFileInfo = leftFileInfo;
        RightFileInfo = rightFileInfo;
        TrackName = Path.GetFileNameWithoutExtension(leftFileInfo.Name.Replace("_left",""));
        TrackID = trackID;
        RadioStationDirectory = radioStationDirectory;
    }


    public string TrackName { get; private set; }


    public DirectoryInfo RadioStationDirectory { get; set; }

    public FileInfo OriginalFileInfo { get; set; }
    public FileInfo LeftFileInfo { get; private set; }
    public FileInfo RightFileInfo { get; private set; }
    public long Samples { get; private set; }
    public int SampleRate { get; private set; }
    public int TrackID { get; set; }
    public double SecondsLong { get; set; }

    public XDocument AWCXML { get; set; }
    public string LeftFileName { get; private set; }
    public string LeftFileNameWithExtension { get; private set; }
    public string RightFileName { get; private set; }
    public string RightFileNameWithExtension { get; private set; }

    public void Process()
    {
        //CreateMonoChannels(OriginalFileInfo.FullName);

        MoveToFolder();

        GetSamples();
        BuildAWCXML();
    }

    private void MoveToFolder()
    {
        
    }

    private void GetSamples()
    {
        WaveFileReader test = new WaveFileReader(LeftFileInfo.FullName);
        SampleRate = test.WaveFormat.SampleRate;
        Samples = test.SampleCount;
        SecondsLong = test.TotalTime.TotalSeconds * 1000;
    }

    private void CreateMonoChannels(string stereoFilePath)
    {

        string MonoDirectoryOutput = Path.GetDirectoryName(stereoFilePath) + "\\" + TrackName;
        Directory.CreateDirectory(MonoDirectoryOutput);

        string outputFilePath = Path.GetDirectoryName(stereoFilePath) + "\\" + Path.GetFileNameWithoutExtension(stereoFilePath);

        string outputFilePath_Left = MonoDirectoryOutput + "\\" + TrackName + "_left.wav";
        string outputFilePath_Right = MonoDirectoryOutput + "\\" + TrackName + "_right.wav";//outputFilePath + "_right.wav";

        //ConvertToMono(stereoFilePath, outputFilePath_Left);
        //ConvertToMono(stereoFilePath, outputFilePath_Right);
        //return;





        using (var inputReader = new AudioFileReader(stereoFilePath))
        {
            // convert our stereo ISampleProvider to mono
            StereoToMonoSampleProvider leftMono = new StereoToMonoSampleProvider(inputReader);
            leftMono.LeftVolume = 1.0f; // discard the left channel
            leftMono.RightVolume = 0.0f; // keep the right channel

            WaveFileWriter.CreateWaveFile16(outputFilePath_Left, leftMono);
            LeftFileInfo = new FileInfo(outputFilePath_Left);
        }



        using (var inputReader2 = new AudioFileReader(stereoFilePath))
        {
            StereoToMonoSampleProvider rightMono = new StereoToMonoSampleProvider(inputReader2);
            rightMono.LeftVolume = 0.0f; // discard the left channel
            rightMono.RightVolume = 1.0f; // keep the right channel

            WaveFileWriter.CreateWaveFile16(outputFilePath_Right, rightMono);
            RightFileInfo = new FileInfo(outputFilePath_Right);
        }

    }


    private void ConvertToMono(string sourceFileName, string destFileName)
    {
        var monoFormat = new WaveFormat(44100, 1);
        using (var waveFileReader = new WaveFileReader(sourceFileName))
        {
            var floatTo16Provider = new WaveFloatTo16Provider(waveFileReader);
            using (var provider = new WaveFormatConversionProvider(monoFormat, floatTo16Provider))
            {
                WaveFileWriter.CreateWaveFile(destFileName, provider);
            }
        }

    }
    private void BuildAWCXML()
    {
        LeftFileName = Path.GetFileNameWithoutExtension(LeftFileInfo.FullName);
        LeftFileNameWithExtension = LeftFileInfo.Name;
       
        RightFileName = Path.GetFileNameWithoutExtension(RightFileInfo.FullName);
        RightFileNameWithExtension = RightFileInfo.Name;



        AWCXML = XDocument.Parse($"<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n<AudioWaveContainer>\r\n <Version value=\"1\" />\r\n <ChunkIndices value=\"True\" />\r\n <MultiChannel value=\"True\" />\r\n <Streams>\r\n  <Item>\r\n   <Name />\r\n   <Chunks>\r\n    <Item>\r\n     <Type>streamformat</Type>\r\n     <BlockSize value=\"524288\" />\r\n    </Item>\r\n    <Item>\r\n     <Type>data</Type>\r\n    </Item>\r\n    <Item>\r\n     <Type>seektable</Type>\r\n    </Item>\r\n   </Chunks>\r\n  </Item>\r\n  <Item>\r\n   <Name>{LeftFileName}</Name>\r\n   <FileName>{LeftFileNameWithExtension}</FileName>\r\n   <StreamFormat>\r\n    <Codec>ADPCM</Codec>\r\n    <Samples value=\"{Samples}\" />\r\n    <SampleRate value=\"{SampleRate}\" />\r\n    <Headroom value=\"113\" />\r\n   </StreamFormat>\r\n   <Chunks>\r\n      <Item>\r\n     <Type>markers</Type>\r\n\t\t<Markers>\r\n\t\t<Item>\r\n\t\t\t<Name>trackid</Name>\r\n\t\t\t<Value value=\"{TrackID}\" />\r\n\t\t\t<SampleOffset value=\"0\" />\r\n\t\t</Item>\r\n\t\t</Markers>\r\n\t</Item>\r\n\t</Chunks>\r\n  </Item>\r\n  <Item>\r\n   <Name>{RightFileName}</Name>\r\n   <FileName>{RightFileNameWithExtension}</FileName>\r\n   <StreamFormat>\r\n    <Codec>ADPCM</Codec>\r\n    <Samples value=\"{Samples}\" />\r\n    <SampleRate value=\"{SampleRate}\" />\r\n    <Headroom value=\"112\" />\r\n   </StreamFormat>\r\n  </Item>\r\n </Streams>\r\n</AudioWaveContainer>\r\n");


        string savePath = RadioStationDirectory.FullName + "\\" + TrackName + ".awc.xml";

        AWCXML.Save(savePath);


    }

}

