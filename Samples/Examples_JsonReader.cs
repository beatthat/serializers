using System;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace BeatThat.Serializers.Examples
{
    [Serializable]
    public struct Data
    {
        public string name;
        public string type;
        public bool success;
    }

    public class Examples_JsonReader : MonoBehaviour
    {
        public const string INITAL_JSON =
@"{
    ""name"":""my name"",
    ""type"":""my type"",
    ""success"": true 
}";


        public InputField m_jsonInput;

        public Text m_name;
        public Text m_type;
        public Toggle m_success;

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
#if NET_4_6
                    var data = reader.ReadOne(s); // TODO: switch to async
#else
                    var data = reader.ReadOne(s);
#endif


                    m_name.text = data.name ?? "";
                    m_type.text = data.type ?? "";
                    m_success.isOn = data.success;
                }
                catch (Exception e)
                {
                    Debug.LogWarning("invalid json");
                }

            }
        }

    }

}
