﻿#pragma checksum "C:\Users\Admin\source\repos\Assigment\Assigment\Pages\HomePage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "407318130D93EE6A4EDD1A40E35DDC9F7ABDC46826C6291FD6AF12B7817619D1"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Assigment.Pages
{
    partial class HomePage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.685")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // Pages\HomePage.xaml line 12
                {
                    global::Windows.UI.Xaml.Controls.NavigationView element2 = (global::Windows.UI.Xaml.Controls.NavigationView)(target);
                    ((global::Windows.UI.Xaml.Controls.NavigationView)element2).ItemInvoked += this.NavigationView_ItemInvoked;
                }
                break;
            case 3: // Pages\HomePage.xaml line 26
                {
                    this.Content = (global::Windows.UI.Xaml.Controls.Frame)(target);
                }
                break;
            case 4: // Pages\HomePage.xaml line 38
                {
                    this.avatar = (global::Windows.UI.Xaml.Controls.PersonPicture)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.685")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}
