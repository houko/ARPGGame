using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.Windows.Markup;
using System.IO;
using System.Xml;


namespace MyFriends
{

    public enum DisplayStyle { GrowShrink = 1, ThreeDimension };
   

    /// <summary>
    /// Interaction logic for MainInterfaceWindow.xaml
    /// </summary>
    public partial class MainInterfaceWindow : Window
    {

        private enum CurrentDisplayOption { AddIsShown = 1, AddIsHidden, ViewIsShown, ViewIsHidden };
        private CurrentDisplayOption currentDisplayOption;
        private DisplayStyle currentDisplayStyle;
        private ItemsControl items3d;
        private Storyboard Grow_Hide_AddUserControlStoryBoard;
        private Storyboard Grow_Show_ViewUsersControlStoryBoard;
        private Storyboard Shrink_Show_AddUserControlStoryBoard;
        private Storyboard Shrink_Hide_ViewUsersControlStoryBoard;
        private AddNewFriendControl addfriendsControl3D = null;
        private Point topleft;


        public MainInterfaceWindow()
        {
            InitializeComponent();
            CreateAlphaGrowArea(true);//创建缩放显示区域
            btnViewAllFriends.IsEnabled = true;//启用查看联系人按钮
            btnAddFriend.IsEnabled = false;//关闭添加联系人按钮
        }




        private void CreateAlphaGrowArea(bool initialCall)
        {
            //初始化添加联系人控件
            addFriendsControl.ReInitialise();
            //设置当前的显示风格
            currentDisplayStyle = DisplayStyle.GrowShrink;
            mainGrid.Children.Clear();//首先清除Grid中的控件
            //如果GridOuter中存在gridHolder,先从GridOuter移除
            if (GridOuter.Children.Contains(gridHolder))
                GridOuter.Children.Remove(gridHolder);
            //在mainGrid控件集合中添加gridHolder控件
            if (!mainGrid.Children.Contains(gridHolder))
                mainGrid.Children.Add(gridHolder);
            //让gridHolder控件得以显示
            gridHolder.Visibility = Visibility.Visible;
            //获取故事板并为故事板关联Completed事件
            Grow_Hide_AddUserControlStoryBoard =
                this.TryFindResource("GrowAndHideAddUserControl") as Storyboard;
            Grow_Hide_AddUserControlStoryBoard.Completed +=
                new EventHandler(Grow_Hide_AddUserControl_Completed);
            Grow_Show_ViewUsersControlStoryBoard =
                this.TryFindResource("GrowAndShowViewUsersControl") as Storyboard;
            Shrink_Show_AddUserControlStoryBoard =
                this.TryFindResource("ShrinkAndShowAddUserControl") as Storyboard;
            Shrink_Show_AddUserControlStoryBoard.Completed +=
                new EventHandler(Shrink_Show_AddUserControl_Completed);
            Shrink_Hide_ViewUsersControlStoryBoard =
                this.TryFindResource("ShrinkAndHideViewUsersControl") as Storyboard;
            if (initialCall)//如果己经初始化，则将当前显示选项设置为己显示
                currentDisplayOption = CurrentDisplayOption.AddIsShown;
        }





        //创建三维动画区域
        private void Create3Area()
        {
            currentDisplayStyle = DisplayStyle.ThreeDimension;//设置当前显示风格
            //将控件移动到Grid中。
            if (mainGrid.Children.Contains(gridHolder))
                mainGrid.Children.Remove(gridHolder);
            if (!GridOuter.Children.Contains(gridHolder))
                GridOuter.Children.Add(gridHolder);
            gridHolder.Visibility = Visibility.Collapsed;//折叠控件
            //取消关联委拖，在3D模式时将不使用动画
            if (Grow_Hide_AddUserControlStoryBoard != null)
                Grow_Hide_AddUserControlStoryBoard.Completed -=
                    //移除故事板Completed事件
                    new EventHandler(Grow_Hide_AddUserControl_Completed);
            if (Shrink_Show_AddUserControlStoryBoard != null)
                Shrink_Show_AddUserControlStoryBoard.Completed -= 
                    //移除故事板Completed事件
                    new EventHandler(Shrink_Show_AddUserControl_Completed);
            //现在创建3D动画区域
            items3d = new ItemsControl();//实例化一个ItemsControl控件
            //设置控件的对齐依赖属性
            items3d.SetValue(Grid.HorizontalAlignmentProperty, HorizontalAlignment.Center);
            items3d.SetValue(Grid.VerticalAlignmentProperty, VerticalAlignment.Center);
            //设置控件的ItemTemplate模板为资源flipItemTemplate模板
            items3d.ItemTemplate = this.TryFindResource("flipItemTemplate") as DataTemplate;
            //随便添加一项
            items3d.Items.Add("dont care");
            mainGrid.Children.Clear();//清除容器Grid
            mainGrid.Children.Add(items3d);//将ItemsControl控件添加进去
        }

        void AddNewFriendControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            addfriendsControl3D = sender as AddNewFriendControl;//获取对象引用
            addfriendsControl3D.ReInitialise();//重新初始化最后一次的联系人内容
            //获取ViewAllUsersControl有些技巧，
            //需要在DataTemplate中查找，意味着需要遍历视觉树
            DependencyObject item = null;
            //因为DataItem中只有一个Item，作为一个技巧方法来应用自定义的3D模板
            foreach (object dataitem in items3d.Items)
            {
                //获取ItemsControl中的UIElement
                item = items3d.ItemContainerGenerator.
                    ContainerFromItem(dataitem);
                int count = VisualTreeHelper.GetChildrenCount(item);
                for (int i = 0; i < count; i++)
                {
                    DependencyObject itemFetched =
                        VisualTreeHelper.GetChild(item, i);
                    //查找Grid，找出哪一个允许找查相关的宿主
                    //ViewAllUsersControl的ContentPresenter
                    if (itemFetched is Grid)
                    {
                        //查找backContent中的ViewAllUsersControl,
                        //设置其高度并重新绑定数据
                        ContentPresenter cp = (itemFetched as Grid).
                            FindName("backContent") as ContentPresenter;
                        DataTemplate myDataTemplate = cp.ContentTemplate;
                        ViewAllUsersControl viewUsers = (ViewAllUsersControl)
                            myDataTemplate.FindName("viewFriendsControl3d", cp);
                        viewUsers.Height = (sender as AddNewFriendControl).Height;
                        viewUsers.DataBind();
                        return;
                    }
                }
            }
        }




        //查看联系人按钮单击事件处理代码
        void btnViewAllFriends_Click(object sender, RoutedEventArgs e)
        {
            viewAllUsersControl.Visibility = Visibility.Visible;//显示查看控件
            Grow_Hide_AddUserControlStoryBoard.Begin(this);//开始动画
            Grow_Show_ViewUsersControlStoryBoard.Begin(this);//开始动画
            btnAddFriend.IsEnabled = true;//启用添加按钮
            btnViewAllFriends.IsEnabled = false;//启用查看按扭
            viewAllUsersControl.DataBind();//绑定联系人数据
        }
        //添加联系人按钮单击事件处理代码
        void btnAddFriend_Click(object sender, RoutedEventArgs e)
        {
            addFriendsControl.Visibility = Visibility.Visible;//显示添加控件
            viewAllUsersControl.Visibility = Visibility.Visible;//显示查看控件
            Shrink_Show_AddUserControlStoryBoard.Begin(this);//开始动画
            Shrink_Hide_ViewUsersControlStoryBoard.Begin(this);//开始动画
            btnAddFriend.IsEnabled = false;//禁用添加按钮
            btnViewAllFriends.IsEnabled = true;//启用查看按钮
        }

        void Shrink_Show_AddUserControl_Completed(object sender, EventArgs e)
        {
            addFriendsControl.Visibility = Visibility.Visible;//显示添加联系人
            viewAllUsersControl.Visibility = Visibility.Hidden;//隐藏查看联系人
            currentDisplayOption = CurrentDisplayOption.AddIsShown;//设置显示状态

        }
        void Grow_Hide_AddUserControl_Completed(object sender, EventArgs e)
        {
            addFriendsControl.Visibility = Visibility.Hidden;//隐藏添加联系人
            viewAllUsersControl.Visibility = Visibility.Visible;//显示查看联系人
            currentDisplayOption = CurrentDisplayOption.ViewIsShown;//设置显示状态            
        }

        private void btnOptions_Click(object sender, RoutedEventArgs e)
        {
            switch (currentDisplayOption)
            {
                case CurrentDisplayOption.AddIsShown:
                    topleft = //根据当前的显示状态设置控件的初始位置
                        addFriendsControl.PointToScreen(new Point(0, 0));
                    break;
                case CurrentDisplayOption.ViewIsShown:
                    topleft = //如果当前查看联系人为显状态，则将其定位到左上角
                        viewAllUsersControl.PointToScreen(new Point(0, 0));
                    break;
            }
            OptionsWindow optionsWin = new OptionsWindow();//选项窗口
            optionsWin.Owner = this;
            optionsWin.CurrentDisplayStyle = currentDisplayStyle;//设置选项的当前显示风格
            optionsWin.WindowStartupLocation =
                WindowStartupLocation.CenterScreen;//窗口位置
            optionsWin.ShowDialog();//显示选项按钮
            //获取定义在应用程序属性中的显示风格
            DisplayStyle newDisplayStle = (DisplayStyle)Application.
                Current.Properties["SelectedDisplayStyle"];
            bool showingAddControl = false;//控件显示布尔值
            if (newDisplayStle != currentDisplayStyle)
            {//判断新的风格与当前风格是否一致
                if (newDisplayStle.Equals(DisplayStyle.ThreeDimension))
                {//如果新的风格为三维风格
                    Create3Area();//用于创建三维动画区域
                    btnViewAllFriends.IsEnabled = false;//禁用查看按钮
                    btnAddFriend.IsEnabled = false;//禁用添加按扭
                }
                else
                {//如果是动画风格
                    CreateAlphaGrowArea(false);//初始化动画方法
                    switch (currentDisplayOption)
                    {//根据当前的显示状态设置控件的可见性与按钮的可用性
                        case CurrentDisplayOption.AddIsShown:
                            viewAllUsersControl.Visibility = Visibility.Visible;
                            showingAddControl = true;
                            break;
                        case CurrentDisplayOption.ViewIsShown:
                            showingAddControl = false;
                            viewAllUsersControl.Visibility = Visibility.Visible;
                            break;
                    }
                    //根据是否显示设置按钮的启用或禁用
                    btnViewAllFriends.IsEnabled = showingAddControl;
                    btnAddFriend.IsEnabled = !showingAddControl;
                }
            }
        }

        private void addFriendsControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void viewAllUsersControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
