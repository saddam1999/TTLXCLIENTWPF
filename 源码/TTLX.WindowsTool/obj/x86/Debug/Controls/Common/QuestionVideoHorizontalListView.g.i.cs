﻿#pragma checksum "..\..\..\..\..\Controls\Common\QuestionVideoHorizontalListView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "A94EF0218752F501DFF83A58651E1533B71B6D65396B5631B7C9D0FA85872379"
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
using TTLX.WindowsTool.Controls.Common;
using TTLX.WindowsTool.Views.Questions;


namespace TTLX.WindowsTool.Controls.Common {
    
    
    /// <summary>
    /// QuestionVideoHorizontalListView
    /// </summary>
    public partial class QuestionVideoHorizontalListView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 25 "..\..\..\..\..\Controls\Common\QuestionVideoHorizontalListView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button XBtnLast;
        
        #line default
        #line hidden
        
        
        #line 82 "..\..\..\..\..\Controls\Common\QuestionVideoHorizontalListView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button XBtnNext;
        
        #line default
        #line hidden
        
        
        #line 148 "..\..\..\..\..\Controls\Common\QuestionVideoHorizontalListView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ContentControl XCon;
        
        #line default
        #line hidden
        
        
        #line 170 "..\..\..\..\..\Controls\Common\QuestionVideoHorizontalListView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ScrollBar XBar;
        
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
            System.Uri resourceLocater = new System.Uri("/TTLX.WindowsTool;component/controls/common/questionvideohorizontallistview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Controls\Common\QuestionVideoHorizontalListView.xaml"
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
            this.XBtnLast = ((System.Windows.Controls.Button)(target));
            
            #line 28 "..\..\..\..\..\Controls\Common\QuestionVideoHorizontalListView.xaml"
            this.XBtnLast.Click += new System.Windows.RoutedEventHandler(this.XBtnLast_OnClick);
            
            #line default
            #line hidden
            
            #line 29 "..\..\..\..\..\Controls\Common\QuestionVideoHorizontalListView.xaml"
            this.XBtnLast.MouseWheel += new System.Windows.Input.MouseWheelEventHandler(this.OnMouseWheel);
            
            #line default
            #line hidden
            return;
            case 2:
            this.XBtnNext = ((System.Windows.Controls.Button)(target));
            
            #line 85 "..\..\..\..\..\Controls\Common\QuestionVideoHorizontalListView.xaml"
            this.XBtnNext.Click += new System.Windows.RoutedEventHandler(this.XBtnNext_OnClick);
            
            #line default
            #line hidden
            
            #line 86 "..\..\..\..\..\Controls\Common\QuestionVideoHorizontalListView.xaml"
            this.XBtnNext.MouseWheel += new System.Windows.Input.MouseWheelEventHandler(this.OnMouseWheel);
            
            #line default
            #line hidden
            return;
            case 3:
            this.XCon = ((System.Windows.Controls.ContentControl)(target));
            return;
            case 4:
            this.XBar = ((System.Windows.Controls.Primitives.ScrollBar)(target));
            
            #line 180 "..\..\..\..\..\Controls\Common\QuestionVideoHorizontalListView.xaml"
            this.XBar.Scroll += new System.Windows.Controls.Primitives.ScrollEventHandler(this.XBar_OnScroll);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

