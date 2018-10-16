using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;


namespace DBMPlayer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            XDocument doc = new XDocument();
            doc.Add(
                new XElement("playlist",
                    new XAttribute("name", "CEFAM"),
                    new XElement("track",
                        new XAttribute("path", "cxxx")
                    )
                )
            );
            doc.Save("data/test.xml");
        }
    }
}
