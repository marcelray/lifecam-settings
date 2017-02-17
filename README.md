# Microsoft LifeCam Webcam Settings Tool

This is a little utility for forcing Microsoft LifeCam webcam settings. It's a command-line tool that reads in settings via XML—admittedly not the most elegant solution, but it worked great for our needs. Hopefully it will help you, too.

I have personally tested it with the following cameras:

- Microsoft LifeCam Studio
- Microsoft LifeCam Cinema

After compiling, to use the tool: 

1. Create a shortcut to CameraPrefs.exe in your Start Menu's Startup folder.
1. Open CameraPrefs.xml in Wordpad or another text editor (Notepad has difficulties with the line endings).
1. Open the Microsoft LifeCam tool from your Start Menu.
1. Click the little white right arrow/triangle along the right border of the application to expand the settings panel.
1. Click the gear icon at the top.
1. Note the name listed in the Select Webcam drop down (e.g. "Microsoft LifeCam Cinema")
1. Scroll down and click the Properties… button under Image Adjustments.
1. Adjust the camera settings to your preference.
1. Go back to the CameraPrefs.xml document.
1. Set the name attribute in the <camera> tag to the name noted in step 7.
1. Modify the rest of the properties in the XML to match the values you set in the Properties dialog in the LifeCam tool.

A couple notes:

- This was last verified as working with Windows 7 and a Microsoft LifeCam Studio camera. Support beyond that cannot be guaranteed.
- Requires the .NET 3.5 runtime. If you're running Windows Vista or Windows 7, you should be fine.
- Higher zoom levels don't seem to take effect. For the LifeCam Studio, it seems to ignore values above 60.
- Setting auto exposure doesn't seem to work by itself. To enable (or disable) this feature, you need to set the trueColorEnabled tag's value to true (on) or false (off).
- Note I am not a .NET developer, so it may be a bit rough around the edges, but feel free to do with this source what you will.

For more details on this issue, including others' workarounds for some of the above limitations, please visit this Microsoft Community page: https://answers.microsoft.com/en-us/windows/forum/windows_7-hardware/is-it-possible-to-save-auto-focus-auto-color/e6b812e3-5e52-e011-8dfc-68b599b31bf5
