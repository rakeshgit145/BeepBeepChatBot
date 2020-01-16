using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BeepBotUnitTest.Models;

using Newtonsoft.Json;

namespace BeepBotUnitTest.Models
{
  public  class JsonReadTest
    {
        private string _filepath;
        public JsonReadTest(string filepath)
        {
            _filepath = filepath;
        }
       
        public MessagesList ParseJson2Model()
        {
            string jsonData;
            MessagesList testCaseChat = new MessagesList();
            try
            {
                //Json Data read
                using (StreamReader reader = new StreamReader(_filepath))
                {
                    jsonData = reader.ReadToEnd();
                }
                testCaseChat = JsonConvert.DeserializeObject<MessagesList>(jsonData);
            }
            catch (System.Exception exe)
            {
                //handle & log exception
            }
            return testCaseChat;
        }
    }
}
