﻿#pragma checksum "..\..\Passwort.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "C8BB9A6B5D863D06533228301743EB04652EAB9AB7BFFDB16F5DD83B8A730496"
//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.42000
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

using Frische_Seiten;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Frische_Seiten {
    
    
    /// <summary>
    /// Passwort
    /// </summary>
    public partial class Passwort : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\Passwort.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border Backborder;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\Passwort.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label bel;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\Passwort.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label frage;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\Passwort.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox antwort;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\Passwort.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label pal;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\Passwort.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Benutzername;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\Passwort.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox passwort;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\Passwort.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button loginbutton;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\Passwort.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button neuerpasswort;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\Passwort.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Hilfe;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Frische_Seiten;component/passwort.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Passwort.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Backborder = ((System.Windows.Controls.Border)(target));
            return;
            case 2:
            this.bel = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.frage = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.antwort = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.pal = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.Benutzername = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.passwort = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 8:
            this.loginbutton = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\Passwort.xaml"
            this.loginbutton.Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.neuerpasswort = ((System.Windows.Controls.Button)(target));
            
            #line 32 "..\..\Passwort.xaml"
            this.neuerpasswort.Click += new System.Windows.RoutedEventHandler(this.neuerpasswort_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.Hilfe = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\Passwort.xaml"
            this.Hilfe.Click += new System.Windows.RoutedEventHandler(this.Hilfe_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
