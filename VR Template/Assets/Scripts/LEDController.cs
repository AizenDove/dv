using System;
using System.IO.Ports;
using UnityEditor;
using UnityEngine;

public class LEDController : MonoBehaviour
{
    [SerializeField] string portName;
    SerialPort serialPort = new SerialPort(null, 11000); // Match with your Arduino's COM port
    void Start()
    {
        if (portName != null)
        {
            serialPort.PortName = portName;
        }
#if UNITY_EDITOR
        else
        {
            Debug.LogError("Lägg till namn på porten...");
            EditorApplication.isPlaying = false;
        }
#endif
        if (!serialPort.IsOpen)
        {
            string[] ports = SerialPort.GetPortNames();
            if (Array.Exists(ports, p => p == "COM4"))
            {
                serialPort.Open();
                serialPort.ReadTimeout = 100;
            }
#if UNITY_EDITOR
            else
            {
                Debug.LogError("Oops, PORT 4 är stängd. Vi avslutar...");
                EditorApplication.isPlaying = false;
            }
#endif
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
