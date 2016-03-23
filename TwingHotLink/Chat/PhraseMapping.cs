using System;
using System.Collections.Generic;
using System.Xml;

namespace TwingHotLink.Chat
{
    internal class PhraseMapping
    {
        private readonly XmlElement root;
        private readonly XmlDocument xmlDoc;

        public PhraseMapping()
        {
            xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load("phrase.xml");
                root = xmlDoc.DocumentElement;
                GetPhraseList();
            }
            catch
            {
                throw new Exception("配置文件丢失");
            }
        }

        public SortedDictionary<string, string> PhraseList { get; private set; }

        private void GetPhraseList()
        {
            PhraseList = new SortedDictionary<string, string>();
            if (root.HasChildNodes)
            {
                for (var i = 0; i < root.ChildNodes.Count; i++)
                {
                    var hotkey = root.ChildNodes[i].Attributes["Hotkey"].Value;
                    var phrase = root.ChildNodes[i].InnerText;
                    PhraseList.Add(hotkey, phrase);
                }
            }
        }

        public void AddPhrase(string hotKey, string phrase)
        {
            var newNode = xmlDoc.CreateElement("Phrase");
            newNode.SetAttribute("Hotkey", hotKey);
            newNode.InnerText = phrase;
            root.AppendChild(newNode);

            //更新列表
            PhraseList.Add(hotKey, phrase);
        }

        public void RemovePhrase(string hotKey)
        {
            var toRemove = root.SelectSingleNode("//Phrase[@Hotkey='" + hotKey + "']");
            if (toRemove != null)
            {
                root.RemoveChild(toRemove);
                PhraseList.Remove(hotKey);
            }
        }

        public void UpdataHotkey(string oldHotkey, string newHotkey)
        {
            var toChange = root.SelectSingleNode("//Phrase[@Hotkey='" + oldHotkey + "']");
            var el = (XmlElement) toChange;
            el.SetAttribute("Hotkey", newHotkey);
            GetPhraseList();
        }

        public void UpdatePhrase(string oldHotkey, string newPhrase)
        {
            var toChange = root.SelectSingleNode("//Phrase[@Hotkey='" + oldHotkey + "']");
            var el = (XmlElement) toChange;
            el.InnerText = newPhrase;
            PhraseList[oldHotkey] = newPhrase;
        }

        public void Save()
        {
            xmlDoc.Save("phrase.xml");
        }
    }
}