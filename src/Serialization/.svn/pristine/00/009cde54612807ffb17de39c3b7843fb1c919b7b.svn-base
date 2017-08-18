using System;


namespace BeatThat.Serialization
{
	/// <summary>
	/// Reads array-of messages in the form: [ 1", 2" ]
	/// 
	/// NOTE: this is ONLY FOR READING ARRAYS. Does NOT support the Reader<string>::ReadOn(Stream)
	/// </summary>
	public class JsonIdArrayReader : JsonReader<string>
	{
		public static JsonIdArrayReader SHARED = new JsonIdArrayReader();

		override public string[] JsonToArray(string json)
		{
			var a = base.JsonToArray(json);

			if(a.Length > 0) {
				var s = a[0];
				if(!char.IsDigit(s[0])) {
					throw new System.NotSupportedException("Invalid format " + json);
				}
			}

			return a;
		}

	}
}