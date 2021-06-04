﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace ConvertTTML2SRT
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine(string.Format("Usage: {0} <ttml file>", System.AppDomain.CurrentDomain.FriendlyName));
                return;
            }

            if (!File.Exists(args[0]))
            {
                Console.WriteLine(string.Format("File '{0}' not found.", args[0]));
                return;
            }
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(args[0]);

            XElement doc = XElement.Load(new XmlNodeReader(xmlDocument.DocumentElement));
            doc.Descendants().Attributes().Where(a => a.IsNamespaceDeclaration).Remove();
            foreach (var elem in doc.Descendants())
            {
                elem.Name = elem.Name.LocalName;
            }

            // Strip file extension from cmdline argument
            string newfile = Path.GetFileNameWithoutExtension(args[0]) + ".srt";
            StreamWriter tw = new StreamWriter(Path.GetDirectoryName(args[0])+Path.DirectorySeparatorChar+newfile);
            int cnt = 1;
            foreach (XElement p in doc.Descendants("p"))
            {
                tw.WriteLine(cnt);
                string begin = p.Attribute("begin").Value.Replace('.', ',');            
                begin += new string('0', begin.Length - begin.IndexOf(',') - 2);
                
                string end = p.Attribute("end").Value.Replace('.', ',');                
                end += new string('0', end.Length - end.IndexOf(',') - 2);
                
                tw.WriteLine(string.Format("{0} --> {1}", begin, end));
                tw.WriteLine(String.Join("", p.Nodes()).Trim().Replace("<br />","\n"));                
                tw.WriteLine();
                cnt++;
            }
        }
    }
}
