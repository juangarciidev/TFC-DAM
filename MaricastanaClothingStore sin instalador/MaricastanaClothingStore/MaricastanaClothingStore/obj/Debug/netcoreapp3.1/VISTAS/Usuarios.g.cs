﻿#pragma checksum "..\..\..\..\VISTAS\Usuarios.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "083B1A7CC7E8453FCFB82D45FE6DE7C9338D1DD2"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using MaricastanaClothingStore.VISTAS;
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


namespace MaricastanaClothingStore.VISTAS {
    
    
    /// <summary>
    /// Usuarios
    /// </summary>
    public partial class Usuarios : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 282 "..\..\..\..\VISTAS\Usuarios.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid GridUsuarios;
        
        #line default
        #line hidden
        
        
        #line 328 "..\..\..\..\VISTAS\Usuarios.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbBusqueda;
        
        #line default
        #line hidden
        
        
        #line 345 "..\..\..\..\VISTAS\Usuarios.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnAgregarUsuario;
        
        #line default
        #line hidden
        
        
        #line 360 "..\..\..\..\VISTAS\Usuarios.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnGenerarInforme;
        
        #line default
        #line hidden
        
        
        #line 373 "..\..\..\..\VISTAS\Usuarios.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid GridDatos;
        
        #line default
        #line hidden
        
        
        #line 439 "..\..\..\..\VISTAS\Usuarios.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Frame FrameUsuarios;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.4.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/MaricastanaClothingStore;component/vistas/usuarios.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\VISTAS\Usuarios.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.4.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.GridUsuarios = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.tbBusqueda = ((System.Windows.Controls.TextBox)(target));
            
            #line 334 "..\..\..\..\VISTAS\Usuarios.xaml"
            this.tbBusqueda.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.tbBusqueda_TextChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.BtnAgregarUsuario = ((System.Windows.Controls.Button)(target));
            
            #line 346 "..\..\..\..\VISTAS\Usuarios.xaml"
            this.BtnAgregarUsuario.Click += new System.Windows.RoutedEventHandler(this.BtnAgregarUsuario_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.BtnGenerarInforme = ((System.Windows.Controls.Button)(target));
            
            #line 361 "..\..\..\..\VISTAS\Usuarios.xaml"
            this.BtnGenerarInforme.Click += new System.Windows.RoutedEventHandler(this.BtnGenerarInforme_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.GridDatos = ((System.Windows.Controls.DataGrid)(target));
            
            #line 385 "..\..\..\..\VISTAS\Usuarios.xaml"
            this.GridDatos.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.GridDatos_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 9:
            this.FrameUsuarios = ((System.Windows.Controls.Frame)(target));
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.4.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 6:
            
            #line 398 "..\..\..\..\VISTAS\Usuarios.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.BtnConsultar_Click);
            
            #line default
            #line hidden
            break;
            case 7:
            
            #line 405 "..\..\..\..\VISTAS\Usuarios.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.BtnModificar_Click);
            
            #line default
            #line hidden
            break;
            case 8:
            
            #line 412 "..\..\..\..\VISTAS\Usuarios.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.BtnEliminar_Click);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

