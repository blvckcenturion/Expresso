// Updated by XamlIntelliSenseFileGenerator 6/19/2022 10:51:39 PM
#pragma checksum "..\..\..\..\Pages\ProductPages\New.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "755075433DB2395773FE8F81513D3062B3238FD1581F22C01DCE8C199F3CE0A6"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using ExpressoWPF.Pages.ProductPages;
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


namespace ExpressoWPF.Pages.ProductPages
{


    /// <summary>
    /// New
    /// </summary>
    public partial class New : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector
    {


#line 32 "..\..\..\..\Pages\ProductPages\New.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtProductName;

#line default
#line hidden


#line 36 "..\..\..\..\Pages\ProductPages\New.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtProductBasePrice;

#line default
#line hidden


#line 40 "..\..\..\..\Pages\ProductPages\New.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtProductDescription;

#line default
#line hidden


#line 44 "..\..\..\..\Pages\ProductPages\New.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbCategories;

#line default
#line hidden


#line 51 "..\..\..\..\Pages\ProductPages\New.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnInsert;

#line default
#line hidden

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ExpressoWPF;component/pages/productpages/new.xaml", System.UriKind.Relative);

#line 1 "..\..\..\..\Pages\ProductPages\New.xaml"
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
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:

#line 12 "..\..\..\..\Pages\ProductPages\New.xaml"
                    ((ExpressoWPF.Pages.ProductPages.New)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Page_Loaded);

#line default
#line hidden
                    return;
                case 2:
                    this.txtProductName = ((System.Windows.Controls.TextBox)(target));
                    return;
                case 3:
                    this.txtProductBasePrice = ((System.Windows.Controls.TextBox)(target));
                    return;
                case 4:
                    this.txtProductDescription = ((System.Windows.Controls.TextBox)(target));
                    return;
                case 5:
                    this.cbCategories = ((System.Windows.Controls.ComboBox)(target));
                    return;
                case 6:
                    this.btnInsert = ((System.Windows.Controls.Button)(target));

#line 51 "..\..\..\..\Pages\ProductPages\New.xaml"
                    this.btnInsert.Click += new System.Windows.RoutedEventHandler(this.btnInsert_Click);

#line default
#line hidden
                    return;
            }
            this._contentLoaded = true;
        }

        internal System.Windows.Controls.DataGrid dgvData;
        internal System.Windows.Controls.TextBox txtProductSizeName;
    }
}

