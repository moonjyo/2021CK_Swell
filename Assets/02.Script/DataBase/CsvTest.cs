using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CsvTest : MonoBehaviour {
	void Start () {
		using (var writer = new CsvFileWriter("Data/SoundText.csv"))
		{
			List<string> columns = new List<string>(){"SFX", "BGM"};// making Index Row
			writer.WriteRow(columns);
			columns.Clear();

			columns.Add("1"); // Name
			columns.Add("1"); // Level
			writer.WriteRow(columns);
		}
	}
}
