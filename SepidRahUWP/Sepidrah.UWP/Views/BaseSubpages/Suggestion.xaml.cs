using Sepidrah.UWP.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Sepidrah.UWP.Views.BaseSubpages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Suggestion : Page
    {
        public ObservableCollection<NameValueItem> Items { get; set; }
        public Suggestion()
        {
            this.InitializeComponent();
            Items = new ObservableCollection<NameValueItem>();
            Items.Add(new NameValueItem() {Name="پارک آزادگان",Value="2 ساعت" });
            Items.Add(new NameValueItem() { Name = "پارک جمشیدیه", Value = "1 ساعت" });
        }
    }
}
