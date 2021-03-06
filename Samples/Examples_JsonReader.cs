﻿using System;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace BeatThat.Serializers.Examples
{
   

    public class Examples_JsonReader : MonoBehaviour
    {
        [Serializable]
        public struct Data
        {
            public string name;
            public string type;
            public bool success;
        }

        public const string INITAL_JSON =
@"{
    ""name"":""my name"",
    ""type"":""my type"",
    ""success"": true 
}";


        public InputField m_jsonInput;

        public Text m_name;
        public Text m_type;
        public Text m_success;

        private void Start()
        {
            m_jsonInput.onValueChanged.AddListener(this.HandleJson);
            ResetInput();
        }

        public void ResetInput()
        {
            m_jsonInput.text = INITAL_JSON;
        }


        private void HandleJson(string json)
        {
            var reader = new JsonReader<Data>(); // in real apps, usually share a static instance
            using (var s = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                try
                {
                    var data = reader.ReadOne(s);

                    m_name.text = data.name ?? "";
                    m_type.text = data.type ?? "";
                    m_success.text = data.success.ToString();
                }
                catch (Exception e)
                {
                    Debug.LogWarning("invalid json: " + e.Message);
                }

            }
        }

    }

}
