using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

using Microsoft.Win32;

namespace CameraPrefsApp
{
	class Program
	{
		private static Configuration config;

		static void Main(string[] args)
		{
			Console.WriteLine("\nReading CameraPrefs.xml...");

			String configPath = "CameraPrefs.xml";
			if (args.Length >= 1)
				configPath = args[0];

			config = loadAndParseConfig(configPath);
			applyCameraSettings(config.Cameras);
			applyTrueColorSetting(config.TrueColorEnabled);
		}

		private static Configuration loadAndParseConfig(String pPath)
		{
			try
			{
				return Configuration.Deserialize(pPath);
			}
			catch (IOException pException)
			{
				TextWriter errorWriter = Console.Error;
				errorWriter.WriteLine("\n  ERROR: Config not found at \"" + pPath + "\"");
				return null;
			}
		}

		private static void applyTrueColorSetting(Boolean pValue)
		{
			Console.WriteLine("\nApplying TrueColor setting to registry...");
			RegistryKey hkcu = Registry.CurrentUser;
			hkcu = hkcu.OpenSubKey("Software\\Microsoft\\LifeCam", true);
			try
			{
				hkcu.SetValue("TrueColorOff", pValue ? 0 : 1);
			}
			catch (Exception pException)
			{
				Console.WriteLine("\n  ERROR: LifeCam entry not found. Is the LifeCam software installed?");
			}
		}

		private static void applyCameraSettings(CameraPrefs[] pCameraPrefsList)
		{
			foreach (CameraPrefs cameraPrefs in pCameraPrefsList)
			{
				try
				{
					LifeCamCamera camera = LifeCamCamera.CreateFromPrefs(cameraPrefs);
				}
				catch (Exception pException)
				{
					Console.WriteLine("\n  ERROR: " + pException.Message);
					continue;
				}

			}
		}

	}
}
