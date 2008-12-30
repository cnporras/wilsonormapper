//**************************************************************//
// Paul Wilson -- www.WilsonDotNet.com -- Paul@WilsonDotNet.com //
// Feel free to use and modify -- just leave these credit lines //
// I also always appreciate any other public credit you provide //
//**************************************************************//

using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information
[assembly: AssemblyTitle("Wilson.ORMapper")]
[assembly: AssemblyDescription("An O/R mapper for .Net")]
#if DEBUG && DOTNETV2
[assembly: AssemblyConfiguration("Debug:DotNetV2 Build")]
#elif DEBUG
[assembly: AssemblyConfiguration("Debug:DotNetV1 Build")]
#elif DOTNETV2
[assembly: AssemblyConfiguration("Release:DotNetV2 Build")]
#else
[assembly: AssemblyConfiguration("Release:DotNetV1 Build")]
#endif
[assembly: AssemblyCompany("http://code.google.com/p/wilsonormapper/")]
[assembly: AssemblyProduct("WilsonORMapper")]
[assembly: AssemblyCopyright("Copyright 2003-2006 Paul Wilson")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Version Information
[assembly: AssemblyVersion("4.2.2.1")]
[assembly: AssemblyFileVersion("4.2.2.1")]
[assembly: CLSCompliant(true)]
[assembly: Guid("164a12ab-a08d-4b78-9207-373baecc23ad")]

// Signing Information
[assembly: AssemblyDelaySign(false)]
[assembly: AssemblyKeyFile("")]
[assembly: AssemblyKeyName("")]
