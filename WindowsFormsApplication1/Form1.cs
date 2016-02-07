using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml.Serialization;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        const string FileName = "D:\\OneDrive\\Dev\\Temp\\state.xml";
        Klasa okno;
        int procent;
        public Form1()
        {
            InitializeComponent();
            backgroundWorker3.WorkerReportsProgress = true;
            backgroundWorker3.WorkerSupportsCancellation = true;
            backgroundWorker3.RunWorkerAsync();
            okno = new Klasa();
        }



        private void backgroundWorker3_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            for (int i = 1; i <= 10; i++)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    MessageBox.Show("Koniec");
                    break;
                }
                else
                {
                    System.Threading.Thread.Sleep(5000);
                    worker.ReportProgress(i*10);
                }

            }
            
        }

        private void backgroundWorker3_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int x;
            x = textBox2.SelectionStart;
            textBox2.Text = textBox2.Text + "Joker";
            textBox2.SelectionStart = x;
            progressBar1.Value = e.ProgressPercentage;
            procent = e.ProgressPercentage;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            backgroundWorker3.CancelAsync();
            okno.procent = procent;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            okno.okno = textBox2.Text;
            okno.procent = progressBar1.Value;

            //Stream TestFileStream = File.Create(FileName);
            //BinaryFormatter serializer = new BinaryFormatter();
            //serializer.Serialize(TestFileStream, okno);
            //TestFileStream.Close();   


            var writer = new System.Xml.Serialization.XmlSerializer(typeof(Klasa));
            var wfile = new System.IO.StreamWriter(FileName);
            writer.Serialize(wfile, okno);
            wfile.Close();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists(FileName))
            {
                //Stream TestFileStream = File.OpenRead(FileName);
                //BinaryFormatter deserializer = new BinaryFormatter();
                //okno = (Klasa)deserializer.Deserialize(TestFileStream);
                //TestFileStream.Close();


                System.Xml.Serialization.XmlSerializer reader =
                    new System.Xml.Serialization.XmlSerializer(typeof(Klasa));
                System.IO.StreamReader file = new System.IO.StreamReader(
                    FileName);
                okno = (Klasa)reader.Deserialize(file);
                file.Close();

                textBox2.Text = okno.okno;
                progressBar1.Value = okno.procent;
            }
        }
    }
}
