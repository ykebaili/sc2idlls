using System;
using System.Collections.Generic;
using System.Text;
using System.Management;
using System.Collections.ObjectModel;

namespace sc2i.common
{

	public static class CUtilDrives
	{
		private static string ParseSerialFromDeviceID(string deviceId)
		{
			string[] splitDeviceId = deviceId.Split('\\');
			string[] serialArray;
			string serial;
			int arrayLen = splitDeviceId.Length - 1;

			serialArray = splitDeviceId[arrayLen].Split('&');
			serial = serialArray[0];
			serial = serial.Replace("_", "");

			return serial;
		}

		public static ReadOnlyCollection<CDriveInfo> GetPhysicalDrives()
		{
			List<CDriveInfo> drives = new List<CDriveInfo>();
			ManagementClass clsDiskDrive = new ManagementClass("Win32_DiskDrive");
			ManagementObjectCollection disks = clsDiskDrive.GetInstances();
			ManagementClass clsLogicalDiskToPartition = new ManagementClass("Win32_LogicalDiskToPartition");
			ManagementObjectCollection logicalDisksToPartition = clsLogicalDiskToPartition.GetInstances();

			foreach (ManagementObject disk in disks)
			{

				object oPNPDeviceID = disk.GetPropertyValue("PNPDeviceID");
				if (oPNPDeviceID == null)
					continue;
				object oDeviceID = disk.GetPropertyValue("DeviceID");
				if (oDeviceID == null)
					continue;

				string strDeviceID = oDeviceID.ToString().Trim().ToUpper();
				int nIdxPhysicalDrive = strDeviceID.IndexOf("PHYSICALDRIVE");
				if (nIdxPhysicalDrive == -1)
					continue;
				nIdxPhysicalDrive += "PHYSICALDRIVE".Length;
				int nIdxDrive = -1;
				try
				{
					nIdxDrive = int.Parse(strDeviceID.Substring(nIdxPhysicalDrive));
				}
				catch
				{
					continue;
				}
				List<char> lettres = new List<char>();
				foreach (ManagementObject DiskToLogical in logicalDisksToPartition)
				{
					try
					{
						string strAntecedent = DiskToLogical.GetPropertyValue("Antecedent").ToString().Trim().ToUpper();

						if (strAntecedent.IndexOf("DISK #" + nIdxDrive.ToString()) != -1)
						{
							string strDependent = DiskToLogical.GetPropertyValue("Dependent").ToString().Trim().ToUpper();
							lettres.Add(strDependent.Substring(strDependent.IndexOf("DEVICEID=\"") + "DEVICEID=\"".Length, 1).ToCharArray()[0]);
						}
					}
					catch
					{
					}
				}


				CDriveInfo drive = new CDriveInfo();
				drive.LettresPartitions = lettres.AsReadOnly();
				drive.ID = ParseSerialFromDeviceID(oPNPDeviceID.ToString());

				object oCaption = disk.GetPropertyValue("Caption");
				if (oCaption != null)
					drive.Caption = oCaption.ToString();

				//Model
				object oModel = disk.GetPropertyValue("Model");
				if (oModel != null)
					drive.Model = oModel.ToString();

				//Interface
				object oInterface = disk.GetPropertyValue("InterfaceType");
				if (oInterface != null)
					drive.TypeInterface = oInterface.ToString();

				drives.Add(drive);

			}
			return drives.AsReadOnly();
		}
	}

	public class CDriveInfo
	{
		public CDriveInfo()
		{

		}

		private string m_strId = "";
		public string ID
		{
			get
			{
				return m_strId;
			}
			set
			{
				m_strId = value;
			}
		}
		private string m_strModel = "";
		public string Model
		{
			get
			{
				return m_strModel;
			}
			set
			{
				m_strModel = value;
			}
		}
		private string m_strTypeInterface = "";
		public string TypeInterface
		{
			get
			{
				return m_strTypeInterface;
			}
			set
			{
				m_strTypeInterface = value;
			}
		}

		private string m_strCaption = "";
		public string Caption
		{
			get
			{
				return m_strCaption;
			}
			set
			{
				m_strCaption = value;
			}
		}

		private List<char> m_letters = new List<char>();
		public ReadOnlyCollection<char> LettresPartitions
		{
			get
			{
				return m_letters.AsReadOnly();
			}
			set
			{
				m_letters = new List<char>(value);
			}
		}

	}
}
