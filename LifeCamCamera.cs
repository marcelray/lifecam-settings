using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Video.DirectShow.Internals;

namespace CameraPrefsApp
{
	class LifeCamCamera
	{
		public VideoCaptureDevice Source;

		private IAMCameraControl cameraControls;
		private IAMVideoProcAmp videoProcAmp;

		private String _name;

		public LifeCamCamera(string name)
		{
			_name = name;
			String moniker = null;
			FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

			// Match specified camera name to device
			for (int i = 0, n = videoDevices.Count; i < n; i++)
			{
				if (name == videoDevices[i].Name)
				{
					moniker = videoDevices[i].MonikerString;
					break;
				}
			}

			if (moniker == null)
				return;
			//throw new Exception("Video device with name '" + name + "' not found.");

			Source = new VideoCaptureDevice(moniker);
			Source.DesiredFrameRate = 30;

			cameraControls = (IAMCameraControl)Source.SourceObject;
			videoProcAmp = (IAMVideoProcAmp)Source.SourceObject;
		}

		static public LifeCamCamera CreateFromPrefs(CameraPrefs prefs)
		{
			LifeCamCamera newCamera = new LifeCamCamera(prefs.Name);

			if (newCamera.Source == null)
				throw new Exception("Camera \"" + prefs.Name + "\" not found. Skipping.");

			FieldInfo[] fields = prefs.GetType().GetFields();

			Console.WriteLine("\nApplying \"" + prefs.Name + "\" settings...\n");

			foreach (FieldInfo field in fields)
			{
				try
				{
					newCamera.GetType().GetProperty(field.Name).SetValue(newCamera, field.GetValue(prefs), null);
					Console.WriteLine("  => " + field.Name + ": " + field.GetValue(prefs));
				}
				catch (Exception e)
				{
					continue;
				}
			}

			return newCamera;
		}

		public String Name
		{
			get { return _name; }
			set { }
		}

		/**
		 * Camera Control
		 */
		public int Focus
		{
			get { return Source.GetFocus(); }
			set { Source.SetFocus(value); }
		}

		public Boolean AutoFocus
		{
			get { return Source.GetAutoFocus(); }
			set { Source.SetAutoFocus(value); }
		}
		public void GetFocusRange(ref int minimum, ref int maximum, ref int step)
		{
			//int defaultValue;
			//Source.GetFocusRange(out minimum, out maximum, out step, out defaultValue);
		}

		public int Zoom
		{
			get { return Source.GetZoom(); }
			set { Source.SetZoom(value); }
		}
		public void GetZoomRange(out int minimum, out int maximum, out int step)
		{
			int defaultValue;
			Source.GetZoomRange(out minimum, out maximum, out step, out defaultValue);
		}



		public int Pan
		{
			get { return Source.GetPan(); }
			set { Source.SetPan(value); }
		}
		public void GetPanRange(ref int min, ref int max, ref int step, ref int def)
		{
			Source.GetPanRange(ref min, ref max, ref step, ref def);
		}

		public int Tilt
		{
			get { return Source.GetTilt(); }
			set { Source.SetTilt(value); }
		}
		public void GetTiltRange(ref int min, ref int max, ref int step, ref int def)
		{
			Source.GetTiltRange(ref min, ref max, ref step, ref def);
		}

		/**
		 * Video Settings
		 */
		public int Brightness
		{
			get
			{
				int value;
				VideoProcAmpFlags flags;
				return videoProcAmp.Get(VideoProcAmpProperty.Brightness, out value, out flags);
			}
			set
			{
				videoProcAmp.Set(VideoProcAmpProperty.Brightness, value, VideoProcAmpFlags.Manual);
			}
		}

		public int WhiteBalance
		{
			get
			{
				int value;
				VideoProcAmpFlags flags;
				return videoProcAmp.Get(VideoProcAmpProperty.WhiteBalance, out value, out flags);
			}
			set
			{
				videoProcAmp.Set(VideoProcAmpProperty.WhiteBalance, value, VideoProcAmpFlags.Manual);
			}
		}

		public int Saturation
		{
			get
			{
				int value;
				VideoProcAmpFlags flags;
				return videoProcAmp.Get(VideoProcAmpProperty.Saturation, out value, out flags);
			}
			set
			{
				videoProcAmp.Set(VideoProcAmpProperty.Saturation, value, VideoProcAmpFlags.Manual);
			}
		}

		public int Exposure
		{
			get
			{
				int value;
				CameraControlFlags flags;
				return cameraControls.Get(CameraControlProperty.Exposure, out value, out flags);
			}
			set
			{
				cameraControls.Set(CameraControlProperty.Exposure, value, CameraControlFlags.Manual);
			}
		}

		public int Contrast
		{
			get
			{
				int value;
				VideoProcAmpFlags flags;
				return videoProcAmp.Get(VideoProcAmpProperty.Contrast, out value, out flags);
			}
			set
			{
				videoProcAmp.Set(VideoProcAmpProperty.Contrast, value, VideoProcAmpFlags.Manual);
			}
		}
	}
}
