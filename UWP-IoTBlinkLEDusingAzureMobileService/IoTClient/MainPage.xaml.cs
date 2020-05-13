using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace IoTClient
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
        IMobileServiceTable<powercontrol> userTable = App.MobileService.GetTable<powercontrol>();

        private async void btnOn_Click(object sender, RoutedEventArgs e)
        {
                List<powercontrol> datalist = await App.MobileService.GetTable<powercontrol>().ToListAsync();
                powercontrol obj = new powercontrol();
                foreach (var x in datalist)
                {
                    obj.id = x.id;
                    obj.status = "on";

                }
                await userTable.UpdateAsync(obj);
            
               
        }

        private async void btnOff_Click(object sender, RoutedEventArgs e)
        {
            List<powercontrol> datalist = await App.MobileService.GetTable<powercontrol>().ToListAsync();
            powercontrol obj = new powercontrol();
            foreach (var x in datalist)
            {
                obj.id = x.id;
             obj.status = "off";

            }
            await userTable.UpdateAsync(obj);
        }

        private async void btnReset_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                powercontrol obj1 = new powercontrol();

                obj1.status = "on";

                await userTable.InsertAsync(obj1);
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
