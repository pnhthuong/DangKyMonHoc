﻿#pragma checksum "..\..\..\..\WindowQL\QuanLyNienKhoa.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "305D65C24B390BDCE7022F7584F0689EBB74E1DD"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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
using Wpf_DangKyMonHoc.WindowQL;


namespace Wpf_DangKyMonHoc.WindowQL {
    
    
    /// <summary>
    /// QuanLyNienKhoa
    /// </summary>
    public partial class QuanLyNienKhoa : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 26 "..\..\..\..\WindowQL\QuanLyNienKhoa.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView list_NienKhoa;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\..\WindowQL\QuanLyNienKhoa.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txt_mank;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\..\WindowQL\QuanLyNienKhoa.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txt_tennk;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\..\WindowQL\QuanLyNienKhoa.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmb_ctdt;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.9.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Wpf_DangKyMonHoc;component/windowql/quanlynienkhoa.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\WindowQL\QuanLyNienKhoa.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.9.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.list_NienKhoa = ((System.Windows.Controls.ListView)(target));
            
            #line 26 "..\..\..\..\WindowQL\QuanLyNienKhoa.xaml"
            this.list_NienKhoa.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.list_NienKhoa_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.txt_mank = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.txt_tennk = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.cmb_ctdt = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            
            #line 67 "..\..\..\..\WindowQL\QuanLyNienKhoa.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btn_them);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 69 "..\..\..\..\WindowQL\QuanLyNienKhoa.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btn_sua);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 71 "..\..\..\..\WindowQL\QuanLyNienKhoa.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btn_xoa);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 73 "..\..\..\..\WindowQL\QuanLyNienKhoa.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btn_lamMoi);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 75 "..\..\..\..\WindowQL\QuanLyNienKhoa.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btn_thoat);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

