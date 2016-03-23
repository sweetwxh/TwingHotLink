using System;
using System.Xml;

/*************************************************
 * 作者：吴兴华
 * 日期：2009-06-15
 * 版本：1.0.0
 * 说明：读取XML文件，目前满足业务需求
 * ***********************************************/

namespace TwingHotLink.Tools
{
    /// <summary>
    ///     从XML当中读取配置参数
    /// </summary>
    internal class ParamManager
    {
        private readonly XmlElement root;
        private readonly XmlDocument xmlDoc;

        /// <summary>
        ///     初始化配置读取
        /// </summary>
        /// <param name="XMLFileName">配置文件名称</param>
        public ParamManager(string XMLFileName)
        {
            xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load(XMLFileName);
                root = xmlDoc.DocumentElement;
            }
            catch
            {
                throw new Exception("配置文件丢失");
            }
        }

        public void Save(string XMLFileName)
        {
            xmlDoc.Save(XMLFileName);
        }

        /// <summary>
        ///     获取节点属性
        /// </summary>
        /// <param name="NodeName">节点名称，相对于根节点，如果需要取子节点数据，这可以写为(NodeName1/NodeName2)</param>
        /// <param name="AttributeName">属性名称</param>
        /// <returns>属性</returns>
        public string GetAttribute(string NodeName, string AttributeName)
        {
            var childNode = root.SelectSingleNode("//" + NodeName);
            if (childNode != null)
            {
                var attribute = childNode.Attributes[AttributeName];
                if (attribute != null)
                {
                    return attribute.Value;
                }
                throw new Exception("属性未发现");
            }
            throw new Exception("节点配置未发现");
        }

        /// <summary>
        ///     设置属性
        /// </summary>
        /// <param name="NodeName">节点名称，相对于根节点，如果需要取子节点数据，这可以写为(NodeName1/NodeName2)</param>
        /// <param name="AttributeName">属性名称</param>
        /// <param name="value">值</param>
        public void SetAttribute(string NodeName, string AttributeName, string value)
        {
            var childNode = root.SelectSingleNode("//" + NodeName);
            if (childNode != null)
            {
                var attribute = childNode.Attributes[AttributeName];
                if (attribute != null)
                {
                    attribute.Value = value;
                }
                else
                {
                    throw new Exception("属性未发现");
                }
            }
            else
            {
                throw new Exception("节点配置未发现");
            }
        }

        /// <summary>
        ///     获取节点当中的数据
        /// </summary>
        /// <param name="NodeName">节点名称，相对于根节点，如果需要取子节点数据，这可以写为(NodeName1/NodeName2)</param>
        /// <returns>节点数据</returns>
        public string GetNodeText(string NodeName)
        {
            var childNode = root.SelectSingleNode("//" + NodeName);
            if (childNode != null)
            {
                return childNode.InnerText;
            }
            throw new Exception("节点配置未发现");
        }

        /// <summary>
        ///     设置节点
        /// </summary>
        /// <param name="NodeName">节点名称，相对于根节点，如果需要取子节点数据，这可以写为(NodeName1/NodeName2)</param>
        /// <param name="value">值</param>
        public void SetNodeText(string NodeName, string value)
        {
            var childNode = root.SelectSingleNode("//" + NodeName);
            if (childNode != null)
            {
                childNode.InnerText = value;
            }
            else
            {
                throw new Exception("节点配置未发现");
            }
        }
    }
}