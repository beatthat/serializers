using BeatThat.Pools;
using System;
using System.IO;
using UnityEngine;

namespace BeatThat.Serializers
{
    /// <summary>
    /// Reads/writes objects to/from json.
    /// </summary>
    public class JsonReader<T> : ReaderBase<T> 
	{
		override public T ReadOne(Stream s)
		{
			string json = null;
			using(var r = new StreamReader(s)) {
				json = r.ReadToEnd();
			}
			return JsonToItem(json);
		}

		override public T ReadOne(Stream s, ref T toObject)
		{
			string json = null;
			using(var r = new StreamReader(s)) {
				json = r.ReadToEnd();
			}
			return JsonToItem(json, ref toObject);
		}

		override public T[] ReadArray(Stream s)
		{
			string json = null;
			using(var r = new StreamReader(s)) {
				json = r.ReadToEnd();
			}
			return JsonToArray(json);
		}

		virtual public T[] JsonToArray(string json)
		{
			string newJson = "{ \"array\": " + json + "}";
			var wrapper = JsonUtility.FromJson<JsonArrayWrapper<T>>(newJson);
			return wrapper.array;
		}

		virtual public T JsonToItem(string json)
		{
			return JsonUtility.FromJson<T>(json);
		}

		virtual public T JsonToItem(string json, ref T toObject)
		{
			JsonUtility.FromJsonOverwrite(json, toObject);
			return toObject;
		}

	}

	public abstract class JsonReader<T, DtoType> : ReaderBase<T> 
	{
		override public T ReadOne(Stream s)
		{
			string json = null;
			using(var r = new StreamReader(s)) {
				json = r.ReadToEnd();
			}

			if(typeof(DtoType).IsValueType) {
				return TempDTOToItem(JsonUtility.FromJson<DtoType>(json));
			}

			DtoType tempDTO = default(DtoType);
			try { 
				tempDTO = StaticObjectPool<DtoType>.Get();
				JsonUtility.FromJsonOverwrite(json, tempDTO);
				return TempDTOToItem(tempDTO);
			}
			finally {
				StaticObjectPool<DtoType>.Return(tempDTO);
			}
		}

		public override T ReadOne (Stream s, ref T toObject)
		{
			string json = null;
			using(var r = new StreamReader(s)) {
				json = r.ReadToEnd();
			}

			DtoType tempDTO = default(DtoType);
			if(typeof(DtoType).IsValueType) {
				tempDTO = JsonUtility.FromJson<DtoType>(json);
				OverwriteItem(tempDTO, toObject);
				return toObject;
			}

			try { 
				tempDTO = StaticObjectPool<DtoType>.Get();
				JsonUtility.FromJsonOverwrite(json, tempDTO);
				OverwriteItem(tempDTO, toObject);
				return toObject;
			}
			finally {
				StaticObjectPool<DtoType>.Return(tempDTO);
			}
		}
			
		override public T[] ReadArray(Stream s)
		{
			string json = null;
			using(var r = new StreamReader(s)) {
				json = r.ReadToEnd();
			}
			var dtoArray = JsonToDTOArray(json);
			using(var list = ListPool<T>.Get()) {
				foreach(var dto in dtoArray) {
					list.Add(TempDTOToItem(dto));
				}
				return list.ToArray();
			}
		}

		virtual public DtoType[] JsonToDTOArray(string json)
		{
			string newJson = "{ \"array\": " + json + "}";
			var wrapper = JsonUtility.FromJson<JsonArrayWrapper<DtoType>>(newJson);
			return wrapper.array;
		}
			
		/// <summary>
		/// IMPORTANT NOTE:
		/// The DTO implementation of JsonReader<T,DTO> treats DTO objects as poolable/disposable.
		/// Once this function exits, the dto param will become immediately invalid.
		/// So when creating T, you MUST NOT keep a reference to the dto.
		/// Instead just copy the values you need.
		/// </summary>
		/// <returns>The to item.</returns>
		/// <param name="dto">a temporty DTO represenation of the object</param>
		abstract protected T TempDTOToItem(DtoType tempDTO);

		protected void OverwriteItem(DtoType tempDTO, T item)
		{
			var overwrite = item as Overwritable<DtoType>;
			if(overwrite != null) {
				overwrite.Overwrite(tempDTO);
				return;
			}

			TempDTOToItem(tempDTO, item);
		}

		virtual protected void TempDTOToItem(DtoType tempDTO, T item) 
		{
			throw new NotImplementedException("either T must implement Overwritable<DtoType> or you must implement this method");
		}


	}


	[Serializable]
	public struct JsonArrayWrapper<T>
	{
		public T[] array;
	}
}


