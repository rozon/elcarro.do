﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ElCarro.Web.StringResource.Store {
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
    public class Store_SR {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Store_SR() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ElCarro.Web.StringResource.Store.Store_SR", typeof(Store_SR).Assembly);
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
        ///   Looks up a localized string similar to No se encontraron en la primera Fila(Row) las columnas:.
        /// </summary>
        public static string columns_are_not_valid {
            get {
                return ResourceManager.GetString("columns_are_not_valid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to El archivo tuvo algun error al subirse, Contacte al Administrador..
        /// </summary>
        public static string error_upload_excel {
            get {
                return ResourceManager.GetString("error_upload_excel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to El archivo subido no tiene un formato de &quot;Excel&quot; valido para nuestra plataforma..
        /// </summary>
        public static string format_error_excel {
            get {
                return ResourceManager.GetString("format_error_excel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Correo resultado excel subido..
        /// </summary>
        public static string subject_excel_upload {
            get {
                return ResourceManager.GetString("subject_excel_upload", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to El archivo se guardo exitosamente, recibira un mensaje al correo:.
        /// </summary>
        public static string success_upload_excel {
            get {
                return ResourceManager.GetString("success_upload_excel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to El archivo se guardo exitosamente..
        /// </summary>
        public static string success_upload_excel_without_email {
            get {
                return ResourceManager.GetString("success_upload_excel_without_email", resourceCulture);
            }
        }
    }
}