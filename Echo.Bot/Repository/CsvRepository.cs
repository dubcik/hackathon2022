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
			using TextFieldParser dataParser = new(path)
			{
				TextFieldType = FieldType.Delimited,
				Delimiters = new string[] { "," }
			};

			while (!dataParser.EndOfData)
			{
				var fields = dataParser.ReadFields();

				Add(fields.FirstOrDefault().ToLower(), fields.LastOrDefault().ToLower());
			}
		}
	}
}
