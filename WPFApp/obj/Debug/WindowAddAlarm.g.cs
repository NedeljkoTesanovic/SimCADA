#pragma checksum "..\..\WindowAddAlarm.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "09D87AAF5BC96D61FF26F8BDB85EB4C8DD0E1B945185338D764EFF296482211E"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
using WPFApp;


namespace WPFApp {
    
    
    /// <summary>
    /// WindowAddAlarm
    /// </summary>
    public partial class WindowAddAlarm : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\WindowAddAlarm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txbx_Name;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\WindowAddAlarm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txbx_Msg;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\WindowAddAlarm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txbx_Threshold;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\WindowAddAlarm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Confirm;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\WindowAddAlarm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox chbx_ActiveHigh;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\WindowAddAlarm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Reset;
        
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
            System.Uri resourceLocater = new System.Uri("/WPFApp;component/windowaddalarm.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\WindowAddAlarm.xaml"
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
            this.txbx_Name = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.txbx_Msg = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.txbx_Threshold = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.btn_Confirm = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\WindowAddAlarm.xaml"
            this.btn_Confirm.Click += new System.Windows.RoutedEventHandler(this.Btn_Confirm_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.chbx_ActiveHigh = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 6:
            this.btn_Reset = ((System.Windows.Controls.Button)(target));
            
            #line 15 "..\..\WindowAddAlarm.xaml"
            this.btn_Reset.Click += new System.Windows.RoutedEventHandler(this.Btn_Reset_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

