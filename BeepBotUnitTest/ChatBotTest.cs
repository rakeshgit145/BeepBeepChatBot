using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using System.Net.Http;
using BeepBotUnitTest.Models;
using BeepBot;

namespace BeepBotUnitTest
{
    [TestClass]
    public class ChatBotTest
    {
        static string _ChatBotMocDataPath = @".\TestJson\ChatBot.json";
        /// <summary>
        /// Below Method Is For Mock TestMethod.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        [DynamicData(nameof(GetChatData), DynamicDataSourceType.Method)]
        public void ChatBotTestMethod(Comment comment)
        {
            string replyMessage = null;
            Program objBeepBot = new Program();
            replyMessage = Program.ChatQuestionnaires(comment.Id, comment.Message);
            Assert.AreEqual(comment.CompairMessage, replyMessage);
        }

        /// <summary>
        /// Below Method Is For Getting  Data From Json File & Return Parsed Data To above TestMethod.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> GetChatData()
        {
            var testChatDataList = new JsonReadTest(_ChatBotMocDataPath).ParseJson2Model().Comment;
            foreach (var testData in testChatDataList)
            {
                yield return new object[] {
                       testData
                };
            }
        }
    }
}
