﻿#pragma checksum "..\..\..\..\WindowQL\QuanLyChucVu.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "96845E9279EAA8ECAA84745BDDF9E7B3D1CEF4A6"
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
    /// QuanLyChucVu
    /// </summary>
    public partial class QuanLyChucVu : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 29 "..\..\..\..\WindowQL\QuanLyChucVu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView listChucVu;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\..\WindowQL\QuanLyChucVu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txt_macv;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\..\WindowQL\QuanLyChucVu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txt_tencv;
        
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
            System.Uri resourceLocater = new System.Uri("/Wpf_DangKyMonHoc;component/windowql/quanlychucvu.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\WindowQL\QuanLyChucVu.xaml"
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
            
            #line 17 "..\..\..\..\WindowQL\QuanLyChucVu.xaml"
            ((Wpf_DangKyMonHoc.WindowQL.QuanLyChucVu)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.listChucVu = ((System.Windows.Controls.ListView)(target));
            
            #line 29 "..\..\..\..\WindowQL\QuanLyChucVu.xaml"
            this.listChucVu.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.listChucVu_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.txt_macv = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.txt_tencv = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            
            #line 61 "..\..\..\..\WindowQL\QuanLyChucVu.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btn_themChucVu);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 63 "..\..\..\..\WindowQL\QuanLyChucVu.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btn_suaChucVu);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 65 "..\..\..\..\WindowQL\QuanLyChucVu.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btn_xoaChucVu);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 67 "..\..\..\..\WindowQL\QuanLyChucVu.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btn_lammoi);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 69 "..\..\..\..\WindowQL\QuanLyChucVu.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btn_thoat);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
