// ***********************************************************************
// Copyright (c) 2007-2014 Charlie Poole
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ***********************************************************************
#if !PORTABLE
using System;

namespace NUnit.Framework.Internal
{
    /// <summary>
    /// PlatformHelper class is used by the PlatformAttribute class to 
    /// determine whether a platform is supported.
    /// </summary>
    public class PlatformHelper
    {
        private OSPlatform os;
        private RuntimeFramework rt;

        // Set whenever we fail to support a list of platforms
        private string reason = string.Empty;

        const string CommonOSPlatforms =
            "Win,Win32,Win32S,Win32NT,Win32Windows,WinCE,Win95,Win98,WinMe,NT3,NT4,NT5,NT6," +
            "Win2008Server,Win2008ServerR2,Win2012Server,Win2012ServerR2," +
            "Win2K,WinXP,Win2003Server,Vista,Win7,Windows7,Win8,Windows8,"+
            "Win8.1,Windows8.1,Win10,Windows10,WindowsServer,Unix,Linux";

        /// <summary>
        /// Comma-delimited list of all supported OS platform constants
        /// </summary>
#if NETCF
        public const string OSPlatforms = CommonOSPlatforms;
#else
        public const string OSPlatforms = CommonOSPlatforms + ",Xbox,MacOSX";
#endif
        
        /// <summary>
        /// Comma-delimited list of all supported Runtime platform constants
        /// </summary>
        public static readonly string RuntimePlatforms =
            "Net,NetCF,SSCLI,Rotor,Mono,MonoTouch";

        /// <summary>
        /// Default constructor uses the operating system and
        /// common language runtime of the system.
        /// </summary>
        public PlatformHelper()
        {
            this.os = OSPlatform.CurrentPlatform;
            this.rt = RuntimeFramework.CurrentFramework;
        }

        /// <summary>
        /// Construct a PlatformHelper for a particular operating
        /// system and common language runtime. Used in testing.
        /// </summary>
        /// <param name="os">OperatingSystem to be used</param>
        /// <param name="rt">RuntimeFramework to be used</param>
        public PlatformHelper( OSPlatform os, RuntimeFramework rt )
        {
            this.os = os;
            this.rt = rt;
        }

        /// <summary>
        /// Test to determine if one of a collection of platforms
        /// is being used currently.
        /// </summary>
        /// <param name="platforms"></param>
        /// <returns></returns>
        public bool IsPlatformSupported( string[] platforms )
        {
            foreach( string platform in platforms )
                if ( IsPlatformSupported( platform ) )
                    return true;

            return false;
        }

        /// <summary>
        /// Tests to determine if the current platform is supported
        /// based on a platform attribute.
        /// </summary>
        /// <param name="platformAttribute">The attribute to examine</param>
        /// <returns></returns>
        public bool IsPlatformSupported( PlatformAttribute platformAttribute )
        {
            string include = platformAttribute.Include;
            string exclude = platformAttribute.Exclude;

            return IsPlatformSupported(include, exclude);
        }

        /// <summary>
        /// Tests to determine if the current platform is supported
        /// based on a platform attribute.
        /// </summary>
        /// <param name="testCaseAttribute">The attribute to examine</param>
        /// <returns></returns>
        public bool IsPlatformSupported(TestCaseAttribute testCaseAttribute)
        {
            string include = testCaseAttribute.IncludePlatform;
            string exclude = testCaseAttribute.ExcludePlatform;

            return IsPlatformSupported(include, exclude);
        }

        private bool IsPlatformSupported(string include, string exclude)
        {
            try
            {
                if (include != null && !IsPlatformSupported(include))
                {
                    reason = string.Format("Only supported on {0}", include);
                    return false;
                }

                if (exclude != null && IsPlatformSupported(exclude))
                {
                    reason = string.Format("Not supported on {0}", exclude);
                    return false;
                }
            }
            catch (Exception ex)
            {
                reason = ex.Message;
                return false;
            }

            return true;
        }

        /// <summary>
        /// Test to determine if the a particular platform or comma-
        /// delimited set of platforms is in use.
        /// </summary>
        /// <param name="platform">Name of the platform or comma-separated list of platform ids</param>
        /// <returns>True if the platform is in use on the system</returns>
        public bool IsPlatformSupported( string platform )
        {
            if ( platform.IndexOf( ',' ) >= 0 )
                return IsPlatformSupported( platform.Split( new char[] { ',' } ) );

            string platformName = platform.Trim();
            bool isSupported = false;

//			string versionSpecification = null;
//
//			string[] parts = platformName.Split( new char[] { '-' } );
//			if ( parts.Length == 2 )
//			{
//				platformName = parts[0];
//				versionSpecification = parts[1];
//			}

            switch( platformName.ToUpper() )
            {
                case "WIN":
                case "WIN32":
                    isSupported = os.IsWindows;
                    break;
                case "WIN32S":
                    isSupported = os.IsWin32S;
                    break;
                case "WIN32WINDOWS":
                    isSupported = os.IsWin32Windows;
                    break;
                case "WIN32NT":
                    isSupported = os.IsWin32NT;
                    break;
                case "WINCE":
                    isSupported = os.IsWinCE;
                    break;
                case "WIN95":
                    isSupported = os.IsWin95;
                    break;
                case "WIN98":
                    isSupported = os.IsWin98;
                    break;
                case "WINME":
                    isSupported = os.IsWinME;
                    break;
                case "NT3":
                    isSupported = os.IsNT3;
                    break;
                case "NT4":
                    isSupported = os.IsNT4;
                    break;
                case "NT5":
                    isSupported = os.IsNT5;
                    break;
                case "WIN2K":
                    isSupported = os.IsWin2K;
                    break;
                case "WINXP":
                    isSupported = os.IsWinXP;
                    break;
                case "WIN2003SERVER":
                    isSupported = os.IsWin2003Server;
                    break;
                case "NT6":
                    isSupported = os.IsNT6;
                    break;
                case "VISTA":
                    isSupported = os.IsVista;
                    break;
                case "WIN2008SERVER":
                    isSupported = os.IsWin2008Server;
                    break;
                case "WIN2008SERVERR2":
                    isSupported = os.IsWin2008ServerR2;
                    break;
                case "WIN2012SERVER":
                    isSupported = os.IsWin2012ServerR1 || os.IsWin2012ServerR2;
                    break;
                case "WIN2012SERVERR2":
                    isSupported = os.IsWin2012ServerR2;
                    break;
                case "WIN7":
                case "WINDOWS7":
                    isSupported = os.IsWindows7;
                    break;
                case "WINDOWS8":
                case "WIN8":
                    isSupported = os.IsWindows8;
                    break;
                case "WINDOWS8.1":
                case "WIN8.1":
                    isSupported = os.IsWindows81;
                    break;
                case "WINDOWS10":
                case "WIN10":
                    isSupported = os.IsWindows10;
                    break;
                case "WINDOWSSERVER":
                    isSupported = os.IsWindowsServer10;
                    break;
                case "UNIX":
                case "LINUX":
                    isSupported = os.IsUnix;
                    break;
#if !NETCF
                case "XBOX":
                    isSupported = os.IsXbox;
                    break;
                case "MACOSX":
                    isSupported = os.IsMacOSX;
                    break;
#endif
                // These bitness tests relate to the process, not the OS.
                // We can't use Environment.Is64BitProcess because it's
                // only supported in NET 4.0 and higher.
                case "64-BIT":
                case "64-BIT-PROCESS":
                    isSupported = IntPtr.Size == 8;
                    break;
                case "32-BIT":
                case "32-BIT-PROCESS":
                    isSupported = IntPtr.Size == 4;
                    break;

#if NET_4_0 || NET_4_5
                // We only support bitness tests of the OS in .NET 4.0 and up
                case "64-BIT-OS":
                    isSupported = Environment.Is64BitOperatingSystem;
                    break;
                case "32-BIT-OS":
                    isSupported = !Environment.Is64BitOperatingSystem;
                    break;
#endif

                default:
                    isSupported = IsRuntimeSupported(platformName);
                    break;
            }

            if (!isSupported)
                this.reason = "Only supported on " + platform;

            return isSupported;
        }

        /// <summary>
        /// Return the last failure reason. Results are not
        /// defined if called before IsSupported( Attribute )
        /// is called.
        /// </summary>
        public string Reason
        {
            get { return reason; }
        }

        private bool IsRuntimeSupported(string platformName)
        {
            string versionSpecification = null;
            string[] parts = platformName.Split(new char[] { '-' });
            if (parts.Length == 2)
            {
                platformName = parts[0];
                versionSpecification = parts[1];
            }

            switch (platformName.ToUpper())
            {
                case "NET":
                    return IsRuntimeSupported(RuntimeType.Net, versionSpecification);

                case "NETCF":
                    return IsRuntimeSupported(RuntimeType.NetCF, versionSpecification);

                case "SSCLI":
                case "ROTOR":
                    return IsRuntimeSupported(RuntimeType.SSCLI, versionSpecification);

                case "MONO":
                    return IsRuntimeSupported(RuntimeType.Mono, versionSpecification);

                case "SL":
                case "SILVERLIGHT":
                    return IsRuntimeSupported(RuntimeType.Silverlight, versionSpecification);

                case "MONOTOUCH":
                    return IsRuntimeSupported(RuntimeType.MonoTouch, versionSpecification);

                default:
                    throw new ArgumentException("Invalid platform name", platformName);
            }
        }

        private bool IsRuntimeSupported(RuntimeType runtime, string versionSpecification)
        {
            Version version = versionSpecification == null
                ? RuntimeFramework.DefaultVersion
                : new Version(versionSpecification);

            RuntimeFramework target = new RuntimeFramework(runtime, version);

            return rt.Supports(target);
        }
    }
}
#endif