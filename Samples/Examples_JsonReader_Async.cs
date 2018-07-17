using System;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace BeatThat.Serializers.Examples
{

    /// <summary>
    /// Another simple example just uses async read call
    /// </summary>
    public class Examples_JsonReader_Async : MonoBehaviour
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

        public void ResetInput()
        {
            m_jsonInput.text = INITAL_JSON;
        }

#if NET_4_6
        private void Start()
        {
            m_jsonInput.onValueChanged.AddListener(this.HandleJson);
            ResetInput();
        }

        private async void HandleJson(string json)
        {
            var reader = new JsonReader<Data>(); // in real apps, usually share a static instance
            using (var s = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                try
                {
                    var data = await reader.ReadAsync(s);

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
#else 
        void Start()
        {
            Debug.LogError("You must enable .NET4x to use async");
        }
#endif

    }

}
