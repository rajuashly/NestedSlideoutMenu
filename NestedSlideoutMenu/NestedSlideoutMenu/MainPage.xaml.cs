using NestedSlideoutMenu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NestedSlideoutMenu
{

    public partial class MainPage : MasterDetailPage
    {

        public List<MasterPageItem> menuList { get; set; }
        public static MainPage mainPage = null;
        string MenuSelected = "Main";

        public MainPage()
        {
            InitializeComponent();
            mainPage = this;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    menuContent.Icon = "slideout.png";
                    break;
                case Device.Android:
                    menuContent.Icon = "";
                    break;
            }

            Username.Text = "Employee Name";
            // Detail = new SwipeTabbedHomePage();
            Detail = new NavigationPage((new Home())) { BackgroundColor = Color.White, BarTextColor = Color.White };
            // if (Device.Idiom != TargetIdiom.Tablet)
            //  {
            IsPresented = false;
            //}
            //else
            //{
            //    IsPresented = true;
            //}
        }

        protected override async void OnAppearing()
        {
            lblVersion.Text = "v" + 1.0 + " © Nested Menu ";

            lblCompanyName.Text = "Company Name";
            ImgCompanyLogo.Source = "A.jpg";

            await CreateMenu();

        }

        public async Task CreateMenu()
        {
            MenuSelected = "Main";
            InvalidateMeasure();

            menuList = new List<MasterPageItem>();
            menuList.Clear();
            menuList.Add(new MasterPageItem() { Title = "Home", Icon = "home.png", TargetType = typeof(Home) });
            menuList.Add(new MasterPageItem() { Title = "SecondMenu", Icon = "noticeboard.png", TargetType = typeof(Home) });
            menuList.Add(new MasterPageItem() { Title = "ThirdMenu", Icon = "leaverequest.png", TargetType = typeof(Home), Count = "8" });
            menuList.Add(new MasterPageItem() { Title = "Page Four", Icon = "noticeboard.png", TargetType = typeof(PageFour) });
            menuList.Add(new MasterPageItem() { Title = "Logout", Icon = "logout.png", TargetType = typeof(LoginPage) });

            // Setting our list to be ItemSource for ListView in MainPage.xaml
            navigationDrawerList.ItemsSource = menuList;
            // Initial navigation, this can be used for our home page    
        }

        public void CreateSecondMenu()
        {
            MenuSelected = "SecondMenu";

            InvalidateMeasure();
            menuList = new List<MasterPageItem>();
            menuList.Clear();

            menuList.Add(new MasterPageItem() { Title = "2.1", Icon = "checkin.png", TargetType = typeof(Home) });
            menuList.Add(new MasterPageItem() { Title = "2.2", Icon = "employeeleavebalance.png", TargetType = typeof(Home) });
            menuList.Add(new MasterPageItem() { Title = "2.3", Icon = "checkinreport.png", TargetType = typeof(Home) });
            menuList.Add(new MasterPageItem() { Title = "Back To Main Menu", Icon = "backburger.png", BorderColor = Color.Transparent, BorderThickness = 0 });
            navigationDrawerList.ItemsSource = menuList;
        }

        public void CreateThirdceMenu()
        {
            MenuSelected = "ThirdMenu";
            InvalidateMeasure();
            menuList = new List<MasterPageItem>();
            menuList.Clear();

            menuList.Add(new MasterPageItem() { Title = "3.1", Icon = "leaverequest.png", TargetType = typeof(Home) });
            menuList.Add(new MasterPageItem() { Title = "3.2", Icon = "noticeboard.png", TargetType = typeof(Home), Count = "'5" });
            menuList.Add(new MasterPageItem() { Title = "3.3", Icon = "policy.png",  TargetType = typeof(Home), Count = "8" });
            menuList.Add(new MasterPageItem() { Title = "3.4", Icon = "employees.png", TargetType = typeof(Home) });
            menuList.Add(new MasterPageItem() { Title = "Back To Main Menu", Icon = "backburger.png", BorderColor = Color.Transparent, BorderThickness = 0 });
            navigationDrawerList.ItemsSource = menuList;
        }

        private async void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var item = (MasterPageItem)e.SelectedItem;

            switch (item.Title)
            {
                case "ThirdMenu":
                    CreateThirdceMenu();
                    IsPresented = true;
                    break;
                case "SecondMenu":
                    CreateSecondMenu();
                    IsPresented = true;
                    break;
                case "Back To Main Menu":
                    CreateMenu();
                    IsPresented = true;
                    break;
                case "Logout":
                    await App.Current.MainPage.Navigation.PushModalAsync(new LoginPage());
                    break;
                default:
                    Type page = item.TargetType;
                    Detail = new NavigationPage((Page)Activator.CreateInstance(page)) { BackgroundColor = Color.White, BarTextColor = Color.White };
                    IsPresented = false;
                    break;
            }
        }


        public void NavigateToDetail(Type page)
        {
            Detail = new NavigationPage((Page)Activator.CreateInstance(page)) { BackgroundColor = Color.White, BarTextColor = Color.White };
        }
    }
}
