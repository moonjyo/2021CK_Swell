using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SoundData : MonoBehaviour
{
    private string StrSfxData;
    private string StrBgmData;


    [HideInInspector]
    public float SfxData;
    [HideInInspector]
    public float BgmData;

    string line;
  public  string ReadCsv(string filePath) //수정필요 
    {
        FileInfo fileInfo = new FileInfo(filePath);
        string value = "";

   
        if (fileInfo.Exists)
        {
            
            StreamReader reader = new StreamReader(filePath);
            List<string> columns = new List<string>();

            while ((line = reader.ReadLine()) != null)
            {
                Debug.Log(line);

                columns.Add(line);
            }

            for(int i =1;  i < columns.Count; ++i)
            {
                string[] row = columns[i].Split(new char[] { ',' });

                StrSfxData = row[0];
                StrBgmData = row[1];
                SfxData  = float.Parse(StrSfxData);
                BgmData = float.Parse(StrBgmData); 
            }

           
            reader.Close();
        }

        else
            value = "파일이 없습니다.";

        return value;
    }


    public void SaveSfxData(float Sfxvalue)
    {
        StrSfxData = Sfxvalue.ToString();
    }
    public void SaveBgmData(float BgmValue)
    {
        StrBgmData = BgmValue.ToString();
    }

    public void WriteData()
    {
        using (var writer = new CsvFileWriter("Data/SoundText.csv"))
        {
            List<string> columns = new List<string>() { "SFX", "BGM" };// making Index Row
            writer.WriteRow(columns);
            columns.Clear();

            columns.Add(StrSfxData); // sfxdata
            columns.Add(StrBgmData); // bgmdata
            writer.WriteRow(columns);
            columns.Clear();
        }
    }
 

}
