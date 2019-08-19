using FourWays.Core.ViewModels;
using MvvmCross.Forms.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TipFormsCross.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TipView : MvxContentPage<TipViewModel>
    {
        public TipView()
        {
            InitializeComponent();
        }
    }
}