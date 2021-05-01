using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueParser : MonoBehaviour
{
    public Dialogue[] Parse(string _CSVFileName)
    {
       List<Dialogue> dialougeList = new List<Dialogue>(); //대사 리스트 생성 
       string DataPath =  "Data/" + _CSVFileName;
       TextAsset csvData  =  Resources.Load<TextAsset>(DataPath);
       string[] data = csvData.text.Split(new char[] {'\n'}); // 횡렬로 자르기 
       

        for(int i = 1;  i < data.Length - 1;) //마지막 데이터가 "" 불러오지는거때문에 임시적으로 막음 
        {
            string[] row = data[i].Split(new char[]{','}); // 또 다시 ,으로 자르기 

            Dialogue dialogue = new Dialogue();

            dialogue.name = row[1];

            List<string> contextList = new List<string>();

            if (row.Length == 6) // Texture L , R 이 있는경우 
            {
                dialogue.TextureL = row[3];
                dialogue.TextureR = row[4];
                row[5] = row[5].Replace("\r", ""); // \r삭제 마지막데이터는 \r이 무조건있음
                dialogue.CurrentTurn = row[5];
            }

            do
            {
                contextList.Add(row[2]);
                if (++i < data.Length -1)
                {
                    row = data[i].Split(new char[] {','});
                }
                else
                {
                    break;
                }
            } while (row[0].ToString() == "");


          

            dialogue.context = contextList.ToArray();

            dialougeList.Add(dialogue);
        }

        return dialougeList.ToArray();
    }



}
