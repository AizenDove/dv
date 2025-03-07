using System.IO.Ports;
using UnityEngine;

public class LEDController : MonoBehaviour
{
    SerialPort serialPort = new SerialPort("COM3", 11000); // Match with your Arduino's COM port

    void Start()
    {
        if (!serialPort.IsOpen)
        {
            serialPort.Open();
            serialPort.ReadTimeout = 100;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LEDTrigger"))
        {
            int ledIndex = int.Parse(other.gameObject.name);
            serialPort.WriteLine("ON" + ledIndex);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LEDTrigger"))
        {
            int ledIndex = int.Parse(other.gameObject.name);
            serialPort.WriteLine("OFF" + ledIndex);
        }
    }

    void OnApplicationQuit()
    {
        if (serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }
}
