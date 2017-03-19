using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using Microsoft.Win32;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;

namespace SymHelper
{
    public class Inventory
    {
        public Processor[] MyProcessor;
        public Video[] MyVideo;
        public Memory[] MyMemory;
        public HardDisk[] MyHardDisk;
        public Windows[] MyWindows;
        public Keyboard[] MyKeyboard;
        public Mouse[] MyMouse;
        public Monitor[] MyMonitor;
        //public Floppy[] MyFloppy;
        public CDROM[] MyCDROM;
        public Sound[] MySound;
        public Network[] MyNetwork;
        public Motheboard[] MyMotheboard;
        public Soft[] MySoft;

        public string IdProcessor
        {
            get
            {
                if (this.MyProcessor != null)
                    return this.MyProcessor[0].Id;
                else
                    return "noIdProcessor";
            }
        }

        public class Processor
        {
            public string Id;
            public string Name;
            public string Caption;
            public string Manufacturer;
            public string Speed;
            public string BusSpeed;
            public string CacheSize;
            public string NumberOfCores;
            public string NumberOfThreads;
            public string Bit;
            public string SocketType;

            public Processor[] GetDevice()
            {
                List<Processor> arrayProcessor = new List<Processor>();
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    Processor tempProcessor = new Processor();
                    tempProcessor.Id = queryObj["ProcessorId"] != null ? queryObj["ProcessorId"].ToString() : "Nodata";
                    tempProcessor.Name = queryObj["Name"] != null ? queryObj["Name"].ToString() : "Nodata";
                    tempProcessor.Caption = queryObj["Caption"] != null ? queryObj["Caption"].ToString() : "Nodata";
                    tempProcessor.Manufacturer = queryObj["Manufacturer"] != null ? queryObj["Manufacturer"].ToString() : "Nodata";
                    tempProcessor.Speed = queryObj["MaxClockSpeed"] != null ? queryObj["MaxClockSpeed"].ToString() : "Nodata";
                    tempProcessor.BusSpeed = queryObj["ExtClock"] != null ? (Convert.ToDouble(queryObj["ExtClock"]) * 4).ToString() : "Nodata";
                    tempProcessor.CacheSize = queryObj["L2CacheSize"] != null ? queryObj["L2CacheSize"].ToString() : "Nodata";
                    tempProcessor.NumberOfCores = queryObj["NumberOfCores"] != null ? queryObj["NumberOfCores"].ToString() : "Nodata";
                    tempProcessor.NumberOfThreads = queryObj["NumberOfLogicalProcessors"] != null ? queryObj["NumberOfLogicalProcessors"].ToString() : "Nodata";
                    tempProcessor.Bit = queryObj["DataWidth"] != null ? queryObj["DataWidth"].ToString() : "Nodata";
                    tempProcessor.SocketType = queryObj["SocketDesignation"] != null ? queryObj["SocketDesignation"].ToString() : "nodata";
                    arrayProcessor.Add(tempProcessor);
                }
                return arrayProcessor.ToArray();
            }
        }

        public class Video
        {
            public string VideoID;
            public string Name;
            public string Caption;
            public string DriverVersion;
            public string VideoProcessor;
            public string TypeDAC;
            public string Memory;

            public Video[] GetDevice()
            {
                List<Video> arrayVideo = new List<Video>();
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_VideoController");
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    Video tempVideo = new Video();
                    tempVideo.VideoID = queryObj["PNPDeviceID"] != null ? queryObj["PNPDeviceID"].ToString() : "Nodata";
                    tempVideo.Name = queryObj["Name"] != null ? queryObj["Name"].ToString() : "Nodata";
                    tempVideo.Caption = queryObj["Caption"] != null ? queryObj["Caption"].ToString() : "Nodata";
                    tempVideo.DriverVersion = queryObj["DriverVersion"] != null ? queryObj["DriverVersion"].ToString() : "Nodata";
                    tempVideo.VideoProcessor = queryObj["VideoProcessor"] != null ? queryObj["VideoProcessor"].ToString() : "Nodata";
                    tempVideo.TypeDAC = queryObj["AdapterDACType"] != null ? queryObj["AdapterDACType"].ToString() : "Nodata";
                    tempVideo.Memory = queryObj["AdapterRAM"] != null ? (Convert.ToDouble(queryObj["AdapterRAM"]) / 1048576).ToString() : "Nodata";
                    arrayVideo.Add(tempVideo);
                }
                return arrayVideo.ToArray();
            }
        }

        public class Memory
        {
            public string SerialNumber;
            public string BankLabel;
            public string PartNumber;
            public string Capacity;
            public string Speed;

            public Memory[] GetDevice()
            {
                List<Memory> arrayMemory = new List<Memory>();
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PhysicalMemory");
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    Memory tempMemory = new Memory();
                    tempMemory.SerialNumber = queryObj["SerialNumber"] != null ? queryObj["SerialNumber"].ToString() : "Nodata";
                    tempMemory.BankLabel = queryObj["BankLabel"] != null ? queryObj["BankLabel"].ToString() : "Nodata";
                    tempMemory.PartNumber = queryObj["PartNumber"] != null ? queryObj["PartNumber"].ToString() : "Nodata";
                    tempMemory.Capacity = queryObj["Capacity"] != null ? (Convert.ToDouble(queryObj["Capacity"]) / 1048576).ToString() : "Nodata";
                    tempMemory.Speed = queryObj["Speed"] != null ? queryObj["Speed"].ToString() : "Nodata";
                    arrayMemory.Add(tempMemory);
                }
                return arrayMemory.ToArray();
            }
        }

        public class HardDisk
        {
            //public class PartitionHardDisk
            //{
            //    public string Caption;
            //    public string Size;
            //    public string Type;
            //}

            public string SerialNumber;
            public string Caption;
            public string Size;
            public string PartitionValue;
            //public PartitionHardDisk[] Partition;

            public HardDisk[] GetDevice()
            {
                List<HardDisk> arrayHardDisk = new List<HardDisk>();
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DiskDrive");
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    HardDisk tempHardDisk = new HardDisk();
                    tempHardDisk.SerialNumber = queryObj["SerialNumber"] != null ? queryObj["SerialNumber"].ToString() : "Nodata";
                    tempHardDisk.Caption = queryObj["Caption"] != null ? queryObj["Caption"].ToString() : "Nodata";
                    tempHardDisk.Size = queryObj["Size"] != null ? (Convert.ToDouble(queryObj["Size"]) / 1073741824).ToString() : "Nodata";
                    tempHardDisk.PartitionValue = queryObj["Partitions"] != null ? queryObj["Partitions"].ToString() : "Nodata";

                    /*
                    try
                    {
                        ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DiskPartition");

                        foreach (ManagementObject queryObj in searcher.Get())
                        {
                            Console.WriteLine("Caption: {0}", queryObj["Caption"]);
                            Console.WriteLine("Size     : {0}", (long.Parse(queryObj["Size"].ToString()) / 1073741824).ToString());
                            Console.WriteLine("Type: {0}", queryObj["Type"]);
                        }
                    }
                    catch (ManagementException e)
                    {
                        ;
                    }
                     */

                    arrayHardDisk.Add(tempHardDisk);
                }
                return arrayHardDisk.ToArray();
            }
        }

        public class Keyboard
        {
            public string KeyboardID;
            public string Caption;

            public Keyboard[] GetDevice()
            {
                List<Keyboard> arrayKeyboard = new List<Keyboard>();
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Keyboard");
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    Keyboard tempKeyboard = new Keyboard();
                    KeyboardID = queryObj["PNPDeviceID"] != null ? queryObj["PNPDeviceID"].ToString() : "Nodata";
                    Caption = queryObj["Caption"] != null ? queryObj["Caption"].ToString() : "Nodata";
                    arrayKeyboard.Add(tempKeyboard);
                }
                return arrayKeyboard.ToArray();
            }
        }

        public class Mouse
        {
            public string MouseID;
            public string Caption;

            public Mouse[] GetDevice()
            {
                List<Mouse> arrayMouse = new List<Mouse>();
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PointingDevice");
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    Mouse tempMouse = new Mouse();
                    MouseID = queryObj["PNPDeviceID"] != null ? queryObj["PNPDeviceID"].ToString() : "Nodata";
                    Caption = queryObj["Caption"] != null ? queryObj["Caption"].ToString() : "Nodata";
                    arrayMouse.Add(tempMouse);
                }
                return arrayMouse.ToArray();
            }
        }

        public class Monitor
        {
            public string MonitorID;
            public string Name;
            public string Manufacturer;

            public Monitor[] GetDevice()
            {
                List<Monitor> arrayMonitor = new List<Monitor>();
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DesktopMonitor");
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    Monitor tempMonitor = new Monitor();
                    tempMonitor.MonitorID = queryObj["PNPDeviceID"] != null ? queryObj["PNPDeviceID"].ToString() : "Nodata";
                    tempMonitor.Name = queryObj["Name"] != null ? queryObj["Name"].ToString() : "Nodata";
                    tempMonitor.Manufacturer = queryObj["MonitorManufacturer"] != null ? queryObj["MonitorManufacturer"].ToString() : "Nodata";
                    arrayMonitor.ToArray();
                }
                return arrayMonitor.ToArray();
            }
        }

    /*    public class Floppy
        {
            public string FloppyID;
            public string Caption;

            public Floppy[] GetDevice()
            {
                List<Floppy> arrayFloppy = new List<Floppy>();
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_FloppyDrive");
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    Floppy tempFloppy = new Floppy();
                    tempFloppy.FloppyID = queryObj["PNPDeviceID"] != null ? queryObj["PNPDeviceID"].ToString() : "Nodata";
                    tempFloppy.Caption = queryObj["Caption"] != null ? queryObj["Caption"].ToString() : "Nodata";
                    arrayFloppy.Add(tempFloppy);
                }
                return arrayFloppy.ToArray();
            }
        }
        */
        public class CDROM
        {
            public string CDROMID;
            public string Name;

            public CDROM[] GetDevice()
            {
                List<CDROM> arrayCDROM = new List<CDROM>();
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_CDROMDrive");
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    CDROM tempCDROM = new CDROM();
                    tempCDROM.CDROMID = queryObj["PNPDeviceID"] != null ? queryObj["PNPDeviceID"].ToString() : "Nodata";
                    tempCDROM.Name = queryObj["Name"] != null ? queryObj["Name"].ToString() : "Nodata";
                    arrayCDROM.Add(tempCDROM);
                }
                return arrayCDROM.ToArray();
            }
        }

        public class Sound
        {
            public string SoundID;
            public string Name;
            public string Manufacturer;

            public Sound[] GetDevice()
            {
                List<Sound> arraySound = new List<Sound>();
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_SoundDevice");
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    Sound tempSound = new Sound();
                    tempSound.SoundID = queryObj["PNPDeviceID"] != null ? queryObj["PNPDeviceID"].ToString() : "Nodata";
                    tempSound.Name = queryObj["Name"] != null ? queryObj["Name"].ToString() : "Nodata";
                    tempSound.Manufacturer = queryObj["Manufacturer"] != null ? queryObj["Manufacturer"].ToString() : "Nodata";
                    arraySound.Add(tempSound);
                }
                return arraySound.ToArray();
            }
        }

        public class Network
        {
            public string NetworkID;
            public string MACAdress;
            public string Name;
            public string Manufacturer;

            public Network[] GetDevice()
            {
                List<Network> arrayNetwork = new List<Network>();
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_NetworkAdapter");
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    if (queryObj["MACAddress"] != null)
                    {
                        Network tempNetwork = new Network();
                        tempNetwork.NetworkID = queryObj["PNPDeviceID"] != null ? queryObj["PNPDeviceID"].ToString() : "Nodata";
                        tempNetwork.MACAdress = queryObj["MACAddress"] != null ? queryObj["MACAddress"].ToString() : "Nodata";
                        tempNetwork.Name = queryObj["Name"] != null ? queryObj["Name"].ToString() : "Nodata";
                        tempNetwork.Manufacturer = queryObj["Manufacturer"] != null ? queryObj["Manufacturer"].ToString() : "Nodata";
                        arrayNetwork.Add(tempNetwork);
                    }
                }
                return arrayNetwork.ToArray();
            }
        }

        public class Motheboard
        {
            public string Name;
            public string Manufacturer;
            public string SerialNumber;

            public Motheboard[] GetDevice()
            {
                List<Motheboard> arrayMotheboard = new List<Motheboard>();
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BaseBoard");
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    Motheboard tempMotheboard = new Motheboard();
                    tempMotheboard.Name = queryObj["Name"] != null ? queryObj["Name"].ToString() : "Nodata";
                    tempMotheboard.Manufacturer = queryObj["Manufacturer"] != null ? queryObj["Manufacturer"].ToString() : "Nodata";
                    tempMotheboard.SerialNumber = queryObj["SerialNumber"] != null ? queryObj["SerialNumber"].ToString() : "Nodata";
                    arrayMotheboard.Add(tempMotheboard);
                }
                return arrayMotheboard.ToArray();
            }
        }

        //ДОДЕЛАТЬ ТОЧКА НЕТ - чтобы показывал версию .net
        public class Windows
        {
            public string ProductName;
            public string CSDVersion;
            public string Publisher;
            public string InstallDate;
            public string CurrentBuild;

            public Windows[] GetDevice()
            {
                List<Windows> arrayWindows = new List<Windows>();
                using (RegistryKey rk = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion"))
                {
                    try
                    {
                        Windows tempWindows = new Windows();
                        if (!(rk.GetValue("ProductName") == null))
                            tempWindows.ProductName = rk.GetValue("ProductName").ToString();
                        if (!(rk.GetValue("CSDVersion") == null))
                            tempWindows.CSDVersion = rk.GetValue("CSDVersion").ToString();
                        if (!(rk.GetValue("Publisher") == null))
                            tempWindows.Publisher = rk.GetValue("Publisher").ToString();
                        if (!(rk.GetValue("InstallDate") == null))
                            tempWindows.InstallDate = rk.GetValue("InstallDate").ToString();
                        if (!(rk.GetValue("CurrentBuild") == null))
                            tempWindows.CurrentBuild = rk.GetValue("CurrentBuild").ToString();
                        arrayWindows.Add(tempWindows);
                    }
                    catch (Exception e)
                    {
                        ;
                    }
                }
                return arrayWindows.ToArray();
            }
        }

        //подумать с реестром
        public class Soft
        {
            public string Caption;
            public string Version;
            public string Vendor;
            public string InstallDate;
            public string InstallLocation;


            //диспетчер показывает  чято у меня 111 прог
            public Soft[] GetDevice()
            {
                return this.GetDeviceReg();
            }

            //нашел 113 прог - делал через WMI
            public Soft[] GetDeviceWMI()
            {
                List<Soft> arraySoft = new List<Soft>();
                try
                {
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Product");
                    foreach (ManagementObject queryObj in searcher.Get())
                    {
                        Soft tempSoft = new Soft();
                        tempSoft.Caption = queryObj["Caption"].ToString();
                        tempSoft.Version = queryObj["Version"].ToString();
                        tempSoft.Vendor = queryObj["Vendor"].ToString();
                        tempSoft.InstallDate = queryObj["InstallDate"].ToString();
                        tempSoft.InstallLocation = queryObj["InstallLocation"].ToString();
                        arraySoft.Add(tempSoft);
                    }
                }
                catch (ManagementException e)
                {
                    ;
                }
                return arraySoft.ToArray();
            }

            //нашел 188 прог - делал через реестр
            public Soft[] GetDeviceReg()
            {
                List<Soft> arraySoft = new List<Soft>();
                //А если система x64, то будет еще одна ветка HKLM\Software\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall
                using (RegistryKey rk = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall"))
                {
                    foreach (string skName in rk.GetSubKeyNames())
                    {
                        using (RegistryKey sk = rk.OpenSubKey(skName))
                        {
                            try
                            {
                                /*
                                if (!(sk.GetValue("DisplayName") == null))
                                {
                                    Console.WriteLine("-----------------" + i++);
                                    if (sk.GetValue("InstallLocation") == null)
                                        Console.WriteLine(sk.GetValue("DisplayName") + " - Install path not known\n");
                                    else
                                        Console.WriteLine(sk.GetValue("DisplayName") + " - " + sk.GetValue("InstallLocation") + "\n");
                                }
                                 */

                                Soft tempSoft = new Soft();
                                if (!(sk.GetValue("DisplayName") == null))
                                {
                                    tempSoft.Caption = sk.GetValue("DisplayName").ToString();
                                    if (!(sk.GetValue("DisplayVersion") == null))
                                        tempSoft.Version = sk.GetValue("DisplayVersion").ToString();
                                    if (!(sk.GetValue("Publisher") == null))
                                        tempSoft.Vendor = sk.GetValue("Publisher").ToString();
                                    if (!(sk.GetValue("InstallDate") == null))
                                        tempSoft.InstallDate = sk.GetValue("InstallDate").ToString();
                                    if (!(sk.GetValue("InstallLocation") == null))
                                        tempSoft.InstallLocation = sk.GetValue("InstallLocation").ToString();
                                    arraySoft.Add(tempSoft);
                                }
                            }
                            catch (Exception e)
                            {
                                ;
                            }
                        }
                    }
                }
                return arraySoft.ToArray();
            }
        }

        public void StartInventory()
        {
            this.MyProcessor = new Processor().GetDevice();
            this.MyVideo = new Video().GetDevice();
            this.MyMemory = new Memory().GetDevice();
            this.MyHardDisk = new HardDisk().GetDevice();
            this.MyKeyboard = new Keyboard().GetDevice();
            this.MyMouse = new Mouse().GetDevice();
            this.MyMonitor = new Monitor().GetDevice();
      //    this.MyFloppy = new Floppy().GetDevice();
            this.MyCDROM = new CDROM().GetDevice();
            this.MySound = new Sound().GetDevice();
            this.MyNetwork = new Network().GetDevice();
            this.MyMotheboard = new Motheboard().GetDevice();
            this.MyWindows = new Windows().GetDevice();
            this.MySoft = new Soft().GetDevice();
        }

        public static string GetStringXMLFile(Inventory comp)
        {
         /*   XmlSerializer serializer = new XmlSerializer(typeof(Inventory));
            MemoryStream stream = new MemoryStream();
            serializer.Serialize(stream, comp);
            stream.Position = 0;
            TextReader reader = new StreamReader(stream);
            return reader.ReadToEnd();
*/
            XmlSerializer serializer = new XmlSerializer(typeof(Inventory));
            FileStream fs = new FileStream("persons.xml", FileMode.OpenOrCreate);
            MemoryStream stream = new MemoryStream();
            //  serializer.Serialize(stream, comp);
            serializer.Serialize(fs, comp);
          //  stream.Position = 0;
           TextReader reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        public static Inventory GetObjectXMLFile(string comp)
        {
            XmlSerializer xmlser = new XmlSerializer(typeof(Inventory));
            MemoryStream filestream = new MemoryStream(Encoding.UTF8.GetBytes(comp));
            return (Inventory)xmlser.Deserialize(filestream);
        }

    }
}
