using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Echo.Bot.Repository
{
	internal class CsvRepository : Dictionary<string, string>
	{
		const string path = @"Resources\dbdump.csv";

		internal CsvRepository()
		{
			Add("default", "Hmmm... Honestly I don't know how to answer that.");
			using TextFieldParser dataParser = new(path)
			{
				TextFieldType = FieldType.Delimited,
				Delimiters = new string[] { "," }
			};

			while (!dataParser.EndOfData)
			{
				var fields = dataParser.ReadFields();

				Add(fields.FirstOrDefault(), fields[1].Replace("\\n", Environment.NewLine));
			}
		}
	}
}
