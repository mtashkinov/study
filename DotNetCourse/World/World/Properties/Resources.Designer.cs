﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace World.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("World.Properties.Resources", typeof(Resources).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Sunday is a holiday.
        /// </summary>
        public static string DoNotWorkOnSunday {
            get {
                return ResourceManager.GetString("DoNotWorkOnSunday", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to How many humans do you want to create?.
        /// </summary>
        public static string HumansNum {
            get {
                return ResourceManager.GetString("HumansNum", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to It isn&apos;t a valid humans number.
        /// </summary>
        public static string InvalidHumansNum {
            get {
                return ResourceManager.GetString("InvalidHumansNum", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Total money saved to {0} file.
        /// </summary>
        public static string MoneySaved {
            get {
                return ResourceManager.GetString("MoneySaved", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This is God Console! Create your humans here..
        /// </summary>
        public static string Welcome {
            get {
                return ResourceManager.GetString("Welcome", resourceCulture);
            }
        }
    }
}
