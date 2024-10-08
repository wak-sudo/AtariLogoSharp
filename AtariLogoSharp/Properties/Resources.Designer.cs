﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AtariLogoSharp.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("AtariLogoSharp.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to === Atari Logo Interpreter ===
        ///
        ///Developed by 404 team for Motorola Science Cup 2021/2022. 
        ///Written by Wojciech Kieloch (wak-sudo) in C#..
        /// </summary>
        internal static string AboutInfo {
            get {
                return ResourceManager.GetString("AboutInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Icon similar to (Icon).
        /// </summary>
        internal static System.Drawing.Icon AtariLogoSharp {
            get {
                object obj = ResourceManager.GetObject("AtariLogoSharp", resourceCulture);
                return ((System.Drawing.Icon)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to == User Manual ==
        ///
        ///=== Basic commands ===
        ///
        ///CS - remove drawings / clean up the board.
        ///HT - hide the turtle icon.
        ///ST - show the turtle icon.
        ///RT angle - turn the turtle by the specified angle (in degrees) to the right.
        ///LT angle - turn the turtle by the specified angle (in degrees) to the left.
        ///FD steps - move the turtle forward by the given number of steps.
        ///PU - the moving turtle does not leave a trace.
        ///PD - the moving turtle leaves a trace
        ///BK steps - move the turtle backwards by the given number  [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string AtariLogoUserManualEng {
            get {
                return ResourceManager.GetString("AtariLogoUserManualEng", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap TurtleArt {
            get {
                object obj = ResourceManager.GetObject("TurtleArt", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
    }
}
