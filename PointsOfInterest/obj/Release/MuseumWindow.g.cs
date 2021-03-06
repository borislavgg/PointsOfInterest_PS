﻿#pragma checksum "..\..\MuseumWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "E303D1EC81F554CA07002A5D79910E22E105620E"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using PointsOfInterest;
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


namespace PointsOfInterest {
    
    
    /// <summary>
    /// MuseumWindow
    /// </summary>
    public partial class MuseumWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\MuseumWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image MusImg;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\MuseumWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal PointsOfInterest.Rating MusRate;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\MuseumWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button RateBtn;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\MuseumWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label MusDes;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\MuseumWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label MusName;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\MuseumWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal PointsOfInterest.Rating AverageRate;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\MuseumWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox CommentVal;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\MuseumWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DeleteRateBtn;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\MuseumWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label YourRateLabel;
        
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
            System.Uri resourceLocater = new System.Uri("/PointsOfInterest;component/museumwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MuseumWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
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
            this.MusImg = ((System.Windows.Controls.Image)(target));
            return;
            case 2:
            this.MusRate = ((PointsOfInterest.Rating)(target));
            return;
            case 3:
            this.RateBtn = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\MuseumWindow.xaml"
            this.RateBtn.Click += new System.Windows.RoutedEventHandler(this.AddRate_Button);
            
            #line default
            #line hidden
            return;
            case 4:
            this.MusDes = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.MusName = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.AverageRate = ((PointsOfInterest.Rating)(target));
            return;
            case 7:
            this.CommentVal = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            
            #line 20 "..\..\MuseumWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddComment_Button);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 21 "..\..\MuseumWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ViewComments_Button);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 22 "..\..\MuseumWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.BackToMuseums_Button);
            
            #line default
            #line hidden
            return;
            case 11:
            this.DeleteRateBtn = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\MuseumWindow.xaml"
            this.DeleteRateBtn.Click += new System.Windows.RoutedEventHandler(this.DeleteRateBtn_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.YourRateLabel = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

