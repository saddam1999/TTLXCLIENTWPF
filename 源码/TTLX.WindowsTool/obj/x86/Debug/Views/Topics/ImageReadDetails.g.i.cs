﻿#pragma checksum "..\..\..\..\..\Views\Topics\ImageReadDetails.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "80843A3AEBA5043A2C94F7C27CC7A0608AEF06E92E595FCD68332A7378D73626"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
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
using System.Windows.Interactivity;
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
using TTLX.WindowsTool.Common.Controls;
using TTLX.WindowsTool.Common.Core.ItemsControlDragDropBehavior;
using TTLX.WindowsTool.Controls;
using TTLX.WindowsTool.Controls.Common;
using TTLX.WindowsTool.Models;
using TTLX.WindowsTool.Views.Questions;
using TTLX.WindowsTool.Views.TopicContents;
using TTLX.WindowsTool.Views.Topics;
using TheArtOfDev.HtmlRenderer.WPF;


namespace TTLX.WindowsTool.Views.Topics {
    
    
    /// <summary>
    /// ImageReadDetails
    /// </summary>
    public partial class ImageReadDetails : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 48 "..\..\..\..\..\Views\Topics\ImageReadDetails.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image XImg;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\..\..\..\Views\Topics\ImageReadDetails.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button XBtnSelImg;
        
        #line default
        #line hidden
        
        
        #line 84 "..\..\..\..\..\Views\Topics\ImageReadDetails.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button XBtnAddQuestion;
        
        #line default
        #line hidden
        
        
        #line 95 "..\..\..\..\..\Views\Topics\ImageReadDetails.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox XLstQuestion;
        
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
            System.Uri resourceLocater = new System.Uri("/TTLX.WindowsTool;component/views/topics/imagereaddetails.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Views\Topics\ImageReadDetails.xaml"
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
            this.XImg = ((System.Windows.Controls.Image)(target));
            
            #line 50 "..\..\..\..\..\Views\Topics\ImageReadDetails.xaml"
            this.XImg.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.XSelectImg_OnClick);
            
            #line default
            #line hidden
            return;
            case 2:
            this.XBtnSelImg = ((System.Windows.Controls.Button)(target));
            
            #line 68 "..\..\..\..\..\Views\Topics\ImageReadDetails.xaml"
            this.XBtnSelImg.Click += new System.Windows.RoutedEventHandler(this.XSelectImg_OnClick);
            
            #line default
            #line hidden
            return;
            case 3:
            this.XBtnAddQuestion = ((System.Windows.Controls.Button)(target));
            
            #line 91 "..\..\..\..\..\Views\Topics\ImageReadDetails.xaml"
            this.XBtnAddQuestion.Click += new System.Windows.RoutedEventHandler(this.XBtnAddQuestion_OnClick);
            
            #line default
            #line hidden
            return;
            case 4:
            this.XLstQuestion = ((System.Windows.Controls.ListBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

