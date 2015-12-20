using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;

namespace AOKT4
{
    class Program
    {
        static SerialPort serialPort;

        static void Main(string[] args)
        {

           // newThread thOne = new newThread();

            int portSpeed, dataBits;
            string parity, stopBeats, port;
            string message = "", username = "";

            Console.Write("Порт: ");
            port = Console.ReadLine();
            Console.Write("Скорость передачи: ");
            portSpeed = Convert.ToInt32(Console.ReadLine());
            Console.Write("Паритет: ");
            parity = Console.ReadLine();
            Console.Write("Биты данных: ");
            dataBits = Convert.ToInt32(Console.ReadLine());
            Console.Write("Стоп биты: ");
            stopBeats = Console.ReadLine();
            Console.Write("Ваше имя: ");
            username = Console.ReadLine();
            initPort(port, portSpeed, parity, stopBeats, dataBits);
            while (message != "/exit")
            {
                Console.Write("Вы: ");
                message = Console.ReadLine();
                serialPort.WriteLine(username + ": " + message);
            };
        }

        static string getData()
        {
            string data = serialPort.ReadLine();
            return data.Trim();
        }

        static void initPort(string sPort, int sSpeed, string sParity, string sStopBeats, int sDataBits)
        {
            serialPort = new SerialPort(sPort, sSpeed, (Parity)Enum.Parse(typeof(Parity), sParity), sDataBits,
                (StopBits)Enum.Parse(typeof(StopBits), sStopBeats));
            serialPort.Open();
            serialPort.ErrorReceived += new SerialErrorReceivedEventHandler(serialPort_ErrorReceived);
            serialPort.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived);
            Console.WriteLine("\nПорт открыт!\n");
        }

        static void serialPort_SendData(byte[] data)
        {
            serialPort.RtsEnable = true;
            serialPort.Write(data, 0, data.Length);
            Thread.Sleep(100);
            serialPort.RtsEnable = false;
        }

        static void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string data = serialPort.ReadLine();
            Thread.Sleep(100);
            Console.Write("\r" + data);
            Console.Write("\nВы: ");
        }

        static private void serialPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}