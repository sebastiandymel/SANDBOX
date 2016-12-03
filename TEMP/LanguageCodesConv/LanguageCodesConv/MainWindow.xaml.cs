using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Xml.Linq;

namespace LanguageCodesConv
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private readonly List<LanguageDefinition> languages = new List<LanguageDefinition>();

        private void OnConvertClick(object sender, RoutedEventArgs e)
        {
            this.languages.Clear();
            var resourceCodes = Assembly.GetExecutingAssembly().GetManifestResourceStream("LanguageCodesConv.codes.csv");
            using (var reader = new StreamReader(resourceCodes))
            {
                int index = 0;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    if (index > 0)
                    {
                        var delimited = line.Split(';');

                        var entry = new LanguageDefinition
                        {
                            LanguageFamily = delimited[0],
                            LanguageName = delimited[1],
                            NativeName = delimited[2],
                            TwoLetterCodeISO6391 = delimited[3],
                            ThreeLetterCodeISO6392T = delimited[4],
                            ThreeLetterCodeISO6392B = delimited[5]
                        };
                        if (entry.LanguageName.Contains("?????"))
                        {
                            entry.LanguageName = null;
                        }
                        if (entry.NativeName.Contains("?????"))
                        {
                            entry.NativeName = null;
                        }
                        this.languages.Add(entry);
                    }
                    index++;
                }
            }

            if (!string.IsNullOrEmpty(txtInput.Text))
            {
                XDocument xmlDoc = XDocument.Parse(txtInput.Text);
                var items = xmlDoc.Root.Elements(XName.Get("Language"));
                var notFount = new List<string>();
                foreach (var item in items)
                {
                    var threeLetterCode = item.Attribute(XName.Get("ThreeLetterCode")).Value;

                    string code = null;

                    var language = this.languages.FirstOrDefault(l => l.ThreeLetterCodeISO6392B == threeLetterCode || l.ThreeLetterCodeISO6392T == threeLetterCode);
                    code = language?.TwoLetterCodeISO6391;

                    if (code != null)
                    {
                        txtOutPut.Text += item.ToString().Replace($"ThreeLetterCode=\"{threeLetterCode}", $"ThreeLetterCode =\"{code}");
                    }                    
                    else
                    {
                        notFount.Add(item.ToString());
                    }

                    txtOutPut.Text += Environment.NewLine;
                }


                if (notFount.Count > 0)
                {
                    txtOutPut.Text += Environment.NewLine;
                    txtOutPut.Text += Environment.NewLine;
                    
                    txtOutPut.Text += "NOT FOUND";

                    txtOutPut.Text += Environment.NewLine;
                    txtOutPut.Text += Environment.NewLine;

                    notFount.ForEach(c => txtOutPut.Text += c + Environment.NewLine);
                }
                
            }
        }
    }

    public class LanguageDefinition
    {
        public string LanguageFamily { get; set; }
        public string NativeName { get; set; }
        public string LanguageName { get; set; }
        public string ThreeLetterCodeISO6392T { get; set; }
        public string ThreeLetterCodeISO6392B { get; set; }
        public string TwoLetterCodeISO6391 { get; set; }
    }
}
