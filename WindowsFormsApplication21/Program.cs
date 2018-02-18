using System;
using System.Security.Cryptography.Xml;
using System.Security.Cryptography;
using System.Xml;
using System.Xml.Linq;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace WindowsFormsApplication21
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

    }
    static public class UseDia
    {
        static public string Pyt()
        {
            OpenFileDialog qq = new OpenFileDialog();
            qq.ShowDialog();
            return qq.FileName;
        }
        static public XmlElement Poisk(string tag)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Pyt());
            XmlNodeList dsaw = doc.GetElementsByTagName(tag);
            XmlElement ele = (XmlElement)dsaw[0];
            return ele;
        }
        static public XmlElement Poisk(XmlDocument xml,string tag)
        {
            XmlNodeList dsaw = xml.GetElementsByTagName(tag);
            return null;
        }
    }
    class qq
    {//Реализовать можно как базовый класс

        protected XmlDocument Polych(string pyt)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(pyt);
            return xml;
        }
        protected bool REad (XmlDocument doc)
        {

            
            SignedXml signedXml = new SignedXml(doc);
            
            XmlNodeList nodeList = doc.GetElementsByTagName("Signature");

            // Load the signature node.
            signedXml.LoadXml((XmlElement)nodeList[0]);
            XmlElement xmlElement = doc.DocumentElement;
            DSA ke = DSA.Create();
            foreach(XmlNode xnode in xmlElement)
            {
                if(xnode.Name == "KEy")
                {
                    ke.FromXmlString(xnode.InnerText);
                    break;
                }
            }
            
            return signedXml.CheckSignature(ke);

        }
    }
    class SIx: qq
    {
        public void Proverka(string pyt,TextBox box)
        {
            XmlDocument xml = Polych(pyt);
            bool flag = REad( xml);
            if (flag == true)
                box.Text = "Proverka proidena";
            else
                box.Text = "Ne proidena";
        }
    }
    public class Five
    {
        public void WriteDSA(string pyt, TextBox textBox, TextBox textBoxName)
        {
            textBox.Text = pyt;
            XmlDocument qq = new XmlDocument();
            qq.Load(pyt);
            CreateCrypto(pyt, ref qq, textBoxName);
           // qq.Save(pyt);
            
        }
        private void CreateCrypto(string pyt,ref XmlDocument doc, TextBox textBoxName)
        {
            XmlTextReader gf = new XmlTextReader(pyt);
            string buff = doc.OuterXml;
            int j = 0;
            //Проверка есть ли уже криптография
            for (int i = 0; i < buff.Length; i++)
            {
                string sig = "Signature";
                char a = buff[i];
                char b = sig[j];
                if (buff[i] == sig[j])
                {
                    string str = "";
                    for (int k = 0; k < sig.Length; k++)
                    {
                        str += buff[i + k];
                    }
                    //j++;
                    if (str == sig)
                    {
                        MessageBox.Show("Ключ уже создан");
                        goto BB;
                    }
                }
            }
            DSA Key = DSA.Create();
            string xmls = Key.ToXmlString(false);
            XmlElement xmlElement = doc.CreateElement("KEy");
            xmlElement.InnerText = xmls;
            doc.DocumentElement.AppendChild(xmlElement);
            dris.Key = Key;
            SignedXml signedXml = new SignedXml(doc); 
            signedXml.SigningKey = Key;
            Reference reference = new Reference();
            reference.Uri = "";
            XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
            reference.AddTransform(env);
            signedXml.AddReference(reference);
            signedXml.ComputeSignature();
            XmlElement xmlDigitalSignature = signedXml.GetXml();
            doc.DocumentElement.AppendChild(doc.ImportNode(xmlDigitalSignature, true));

            if (doc.FirstChild is XmlDeclaration)
            {
                doc.RemoveChild(doc.FirstChild);
            }
            gf.Close();
            XmlTextWriter xmltw = new XmlTextWriter(pyt, System.Text.Encoding.UTF8);
            doc.WriteTo(xmltw);
            xmltw.Close();
            BB:;
            if(gf.ReadState != ReadState.Closed)
            {
                gf.Close();
            }

            //XmlDocument doc = new XmlDocument();
            //doc.PreserveWhitespace = false;
            //XmlTextReader gf = new XmlTextReader(pyt);
            //doc.Load(gf);
            //string buff = doc.OuterXml;
            //int j = 0;
            ////Проверка есть ли уже криптография
            //for(int i =0;i<buff.Length;i++)
            //{
            //    string sig = "Signature";
            //    char a = buff[i];
            //    char b = sig[j];
            //    if(buff[i]==sig[j])
            //    {
            //        string str = "";
            //        for(int k=0;k<sig.Length;k++)
            //        {
            //            str += buff[i + k];
            //        }
            //        //j++;
            //        if(str == sig)
            //        {
            //            MessageBox.Show("Ключ уже создан");
            //            goto BB;
            //        }
            //    }
            //}
            //DSA Key = DSA.Create();
            //dris.Key = Key;
            //SignedXml signedXml = new SignedXml(doc);
            //signedXml.SigningKey = Key;
            //Signature XMLSignature = signedXml.Signature;
            //Reference reference = new Reference("");
            //XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
            //reference.AddTransform(env);
            //XMLSignature.SignedInfo.AddReference(reference);
            //KeyInfo keyInfo = new KeyInfo();
            //keyInfo.AddClause(new DSAKeyValue((DSA)Key));
            //XMLSignature.KeyInfo = keyInfo;
            //signedXml.ComputeSignature();
            //XmlElement xmlDigitalSignature = signedXml.GetXml();
            //doc.DocumentElement.AppendChild(doc.ImportNode(xmlDigitalSignature, true));
            ////DSA dfs = DSA.Create();
            ////dfs.FromXmlString("");
            //XmlElement ssdw = doc.CreateElement("Key", Key.SignatureAlgorithm);
            //doc.DocumentElement.AppendChild(ssdw);
            //if (doc.FirstChild is XmlDeclaration)
            //{
            //    doc.RemoveChild(doc.FirstChild);
            //}
            //gf.Close();
            ////doc.Save(pyt);
            //XmlTextWriter xmltw = new XmlTextWriter(pyt, System.Text.Encoding.UTF8);
            //doc.WriteTo(xmltw);
            //xmltw.Close();
            //BB:;
            ////


        }

    }
    public class dour
    {
        public void drisna(int kydaPerem,int whyPerem)
        {
            bool flagdlakyda = false, flagdlawhy = false;
            XmlDocument XDoc = new XmlDocument();
            XmlNode ro = XDoc.DocumentElement;
            XmlNode ro1 = XDoc.DocumentElement;
            XDoc.Load(dris.pytik);
            //XDoc.Re
            XmlReader Xre = XmlReader.Create(dris.pytik);
            // Слишком тупое решение
            string xbufkyda = null, xbuf1why = null, xqkyda = null, xq1why = null;
            while (Xre.Read() == true)
            {
                if (flagdlakyda)
                {
                    xbufkyda = Xre.Name;
                    flagdlakyda = false;
                }
                else if (flagdlawhy)
                {
                    xbuf1why = Xre.Name;
                    flagdlawhy = false;
                }
                /////////////////////////////////
                if (Xre.Name == dris.mes[kydaPerem])
                {
                    flagdlakyda = true;
                    xqkyda = Xre.ReadOuterXml();
                }
                else if (Xre.Name == dris.mes[whyPerem])
                {
                    xq1why = Xre.ReadOuterXml();
                    flagdlawhy = true;
                }
            }
            Xre.Close();
            string rew = XDoc.OuterXml;
            XmlNode qq = XDoc.DocumentElement;
            XmlElement Ele = qq[dris.mes[kydaPerem]];
            XmlNode xmlnode = XDoc.GetElementsByTagName(dris.mes[whyPerem])[0];
            qq.InsertAfter(Ele, xmlnode);
            Ele = qq[dris.mes[whyPerem]];
            xmlnode = XDoc.GetElementsByTagName(dris.mes[kydaPerem+1])[0];
            qq.InsertBefore(Ele, xmlnode);
            XmlDocument sve = new XmlDocument();
            sve.LoadXml(qq.OuterXml);
            sve.Save(dris.pytik);
        }

    }
    public class three// : two
    {
        public void drisna(string pyt)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(dris.pytik);
            /////////////////////
            XmlReader xread = XmlReader.Create(pyt);
            while(xread.Read() == true)
            {
                if (xread.NodeType == XmlNodeType.Element)
                {
                    if(xread.Name== "Оценка")
                    {
                        goto End;
                    }
                }
            }
            xread.Close();
            /////////////////////
            XmlElement basedoc = doc.DocumentElement;
            XmlElement elem = doc.CreateElement("Оценка");
            elem.InnerText = dris.value;
            basedoc.AppendChild(elem);
            doc.Save(dris.pytik);
        End:;
        }
    }
    public class two
    {
        public void drisna(string pyt)
        {
            XmlReader dd = XmlReader.Create(pyt);
            drisnadop(ref dd);
            dd.Close();
        }
        private void drisnadop(ref XmlReader dd)
        {
            //Нужна дабы узнать в какой элемент массива записать данные
            int ii = 0;
            bool buf = false;
            string name = null;
            while (dd.Read() == true)
            {
                if (dd.NodeType == XmlNodeType.Text && buf == true)
                {
                    dris.mass[ii] = dd.ReadContentAsString();
                    buf = false;
                }
                else if (dd.NodeType == XmlNodeType.Element)
                {
                    for (int i = 0; i < dris.mes.Length; i++)
                    {
                        if (dd.Name == dris.mes[i])
                        {
                            name = dd.Name;
                            buf = true;
                            ii = i;
                            break;
                        }
                    }
                }
            }

        }
    }
    public class first
    {
        public virtual void drisna(string kydaWrite)
        {
           // string[] mes = { "фамилия", "имя", "отчество", "дата_рождения", "специальность", "группа", "курс", "номер_студенческого_билета" };
            XDocument xdoc = new XDocument();
            XElement bes = new XElement("LABA");
            XElement[] buff = new XElement[dris.mes.Length];
            for(int i=0;i<dris.mes.Length;i++)
            {
                buff[i] = new XElement(dris.mes[i], dris.mass[i]);
            }
            bes.Add(buff);
            xdoc.Add(bes);
            xdoc.Save(kydaWrite);
        }
        
    }
    public class seven
    {
        public void Шифрование(string tag)
        {
            //Алгоритм шифрования 3DES
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();

            XmlElement xml = UseDia.Poisk(tag);
            byte [] узел = Encoding.UTF8.GetBytes(xml.InnerText);
            MemoryStream ms = new MemoryStream();
            //Сохраним вектор и ключ
            var IV = tripleDES.IV;
            var Key = tripleDES.Key;
            CryptoStream cs = new CryptoStream(ms, tripleDES.CreateEncryptor(Key, IV), CryptoStreamMode.Write);
            cs.Write(узел, 0, узел.Length);
            cs.Close();
        }
    }
    public static class dris
    {
        public static DSA Key;
        public static string[] mes = { "фамилия", "имя", "отчество", "дата_рождения", "специальность", "группа", "курс", "номер_студака" };
        public static string[] mass = new string[mes.Length];
        public static string value = "0";
        public static string pytik = null;
    }
}
