FileBookmark
============

### Overview
FileBookmark is a Windows application that works with Windows Explorer and allows you to bookmark files or folders. The idea was take from code editors, like Visual Studio, which allow you to easily drop bookmarks in your source code and then easily go back to them. This application tries to do the same thing, but work on the file system instead of source code. 
There are two parts to this, a Windows Explorer context menu plugin (FileBookmark.dll) and a Window File Selector (WinFileSelector.exe) which is a helper application that executes explorer.exe. FileBookmark.dll adds a Bookmark menu item with nine submenu bookmarks. When you right-click on a file or folder you will be able to use the bookmark menu to set a bookmark or jump to a bookmark. The bookmarks are stored in the registry at HKEY_CURRENT_USER\Software\FileBookmark. Additionally, there are nine Windows shortcuts created on the user’s Desktop. The shortcuts have hotkey Ctrl+Alt+1 to Ctrl+Alt+9 and executes the WinFileSelector.exe. The Window File Selector is used to read the bookmark from the registry and executes explorer.exe with a location so that the Windows Explorer will open to the bookmarked file or folder. So, first you right-click on a file or folder and select an unassigned bookmark, say the 5th bookmark. This sets the bookmark. Then you can use the hotkey of Ctrl+Alt+5 to open a new Windows Explorer window with the bookmarked file or folder selected. 

### Compiling
The code was compiled using Visual Studio 2013 Ultimate. You should be able to compile it with any version of Visual Studio 2013, with the exception of the Express versions. This project requires plugins and NuGet which probably won’t work in the Express versions of Visual Studio. Thus, I would recommend a real version of Visual Studio. 
Before you begin please download the [Visual Studio Installer Project](https://visualstudiogallery.msdn.microsoft.com/9abe329c-9bba-44a1-be59-0fbf6151054d). This will allow you to compile the Installation projects (SetupFileBookmark.vdproj). If you don’t want (or care) about the installer, you can skip this step, however the FileBookmarkSetup project will fail to load without it. I should also mention that the installer is targeted for an x64 computer. It shouldn’t be too hard to get it to work on an x86 computer, I just didn’t have one to test on and x86 computers seem to be getting rarer these days.     

The first time you compiler, you will need to be connected to the internet. There is a NuGet package that needs to be downloaded. After the initial compile you should be fine working offline.
There are four project in FileBookmark.sln:
-   **FileBookmark.csproj**: This contains the code for the Window Explorer context menu plugin. This uses ShellSharp which is download from NuGet. You can read more about it [here](http://www.codeproject.com/Articles/512956/NET-Shell-Extensions-Shell-Context-Menus).
-   **WinFileSelector.csproj**: This contains a small application that expects a number from 1-9, which specifies a bookmark. The bookmark path is retrieved from the registry and then explorer.exe is executes. Explorer.exe is executed to open a file window with the bookmarked file or folder selected.
-   **ShortcutCreator.csproj**: This is a helper application used by the install to create shortcut with a hotkey. This does use the Windows Script Host Object Model COM object which should be on your machine. 
-   **SetupFileBookmark.vdproj (FileBookmarkSetup in the Solution Explorer)**: This is the installer project which creates a x64 installer that you can use to install the Windows Explorer plugin, Desktop Shortcuts and the WinFileSelector. NOTE: This project is skipped during regular compiling. To build the installer right-click on the project in the Solution Explorer and click Build or Rebuild.

There is also a Tools directory which contains the following:
-   **InsertIcons.exe**: This is used in the post-build events of the WinFileSelector project to insert a list of icons into the build exe. This was written by [Einar Egilsson](http://stackoverflow.com/questions/8913018/adding-multiple-icons-win32-resource-to-net-application).
-   **srm.exe**: This is used by the installer to register the FileBookmark.dll plugin into the Windows Explorer. This was written by [Dave Kerr](http://www.codeproject.com/Articles/653780/NET-Shell-Extensions-Deploying-SharpShell-Servers).
 
### Running
It is recommended that you use the installer to run the application. However, for testing and debugging you might want to run and use the different pieces on their own.
FileBookmark.dll needs to be register before you can use it in Windows Explorer. There are several ways to install FileBookmark.dll, see the [Installing and Registering the Shell Extension](http://www.codeproject.com/Articles/512956/NET-Shell-Extensions-Shell-Context-Menus) section. To install FileBookmark.dll I recommend the following: 

    srm install server.dll –codebase

To uninstall, you can do the following:

    srm uninstall server.dll

To run WinFileSelector.exe, you can create a shortcut to it and pass in argument from 1 to 9 or you can run it from a command prompt by typing the following:
 WinFileSelector.exe 1
Note: If the number you pass into the parameter isn’t defined in the registry, the WinFileSelector.exe will run and immediately close without popping or opening explorer.exe. You might be tricked into thinking it isn’t working, but this is as designed. I didn’t want to have too many annoying message popups.

### Installing
To install the application simply run the FileBookmarkSetup.msi on an x64 windows machine. I have testing it on Windows 7 and Windows 8 machines.

