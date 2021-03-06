using Optical_View.Model;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using static Optical_View.Model.StaticViewMod;

namespace Optical_View.View.Control
{
    /// <summary>
    /// HeaderContainer.xaml 的交互逻辑
    /// </summary>
    public partial class PaddingContainer : UserControl
    {
        Optical_View.View.Control.MicrosoftEdgeView _Browser = null;

        //Optical_View.View.Control.HelixToolkitView _HelixToolkit = null;

        Optical_View.View.Control.Conversion3DView _Conversion3D = null;

        /// <summary>
        /// 当前用户控件继承看manwin的WS  （=-=）
        /// </summary>
        public static Window @this { get; private set; }
        public PaddingContainer()
        {
            InitializeComponent();

            #region 初始化用户控件 扔入公共控件类
            ConversionView.Assembly = _Conversion3D;
            Progress.Assembly = LoadProgressBar;
            #endregion

            @this = Main.Assembly;
            //加载条
            //LoadProgressBar.Visibility = Visibility.Visible;
            switch (ProjectMod.HistoricalProject.Type)
            {
                case "obj":
                    //_HelixToolkit = new HelixToolkitView();
                    //_HelixToolkit.Margin = new Thickness(31, 104, 2, 1);
                    //foreach (var item in Launch.HistoricalProject.Paths)
                    //{
                    //    Model3DGroup group = Load(item);
                    //    _HelixToolkit.HelixToolkit_ModelVisual3D.Content = group;
                    //}
                    _Browser = new MicrosoftEdgeView();
                    _Browser.Margin = new Thickness(31, 104, 2, 1);
                    Control.Children.Add(_Browser);
                    break;
                case "_folder":
                    _Browser = new MicrosoftEdgeView();
                    _Browser.Margin = new Thickness(31, 104, 2, 1);
                    Control.Children.Add(_Browser);
                    break;
                case "_extract":
                    _Conversion3D = new Conversion3DView();
                    _Conversion3D.HorizontalAlignment = HorizontalAlignment.Stretch;
                    //HorizontalAlignment = "Stretch" Height = "Auto"
                    Control.Children.Add(_Conversion3D);
                    break;

                default:
                    
                    break;


            }

            //LoadProgressBar.Visibility = Visibility.Hidden;

            #region logo

            using (var ms = new System.IO.MemoryStream(Properties.Resources.ov))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad; // here
                image.StreamSource = ms;
                image.EndInit();
                image.Freeze();
                ICON.Source = image;
            }
            #endregion
            #region 顶部栏设置
            ///鼠标移动
            TopBder.MouseLeftButtonDown += Grid_MouseDown;
            TopBder.MouseMove += new MouseEventHandler(TopBder_MouseMove);
            void TopBder_MouseMove(object sender, MouseEventArgs e)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    //处理win10 边框磁吸效果
                    if (@this != null && @this.WindowState == WindowState.Maximized)
                    {
                        // Gets the absolute mouse position, relative to screen
                        System.Windows.Point GetMousePos()
                        {
                            return PointToScreen(Mouse.GetPosition(this));
                        }
                        System.Windows.Point T = GetMousePos();
                        @this.Top = T.Y - (Mouse.GetPosition(this).Y);
                        @this.Left = @this.Left / 2;
                        @this.WindowState = WindowState.Normal;

                    }
                    if (@this != null) @this.DragMove();
                }
            }
            void Grid_MouseDown(object sender, MouseButtonEventArgs e)
            {

                //双击
                switch (e.ClickCount)
                {
                    case 1://单击
                        {

                            break;
                        }
                    case 2://双击
                        {
                            if (@this.WindowState == WindowState.Maximized)
                            {
                                @this.WindowState = WindowState.Normal;

                            }
                            else
                            {
                                @this.WindowState = WindowState.Maximized;
                            }


                            break;
                        }
                }
            }
            #endregion
            
        }
        public static string extractLoad(string path)
        {
            if (path == null)
            {
                return null;
            }
            var file = path;
            DirectoryInfo root = new DirectoryInfo(file);
            FileInfo[] dics = root.GetFiles();
            foreach (var f in dics)
            {
                switch (System.IO.Path.GetExtension(f.FullName).ToLower())
                {
                    case ".obj":

                        break;
                }
            }
            return null;
        }
        private void ICON_MouseUp(object sender, MouseButtonEventArgs e)
        {


        }
        //大标题点击事件
        private void MainTitle_MouseUp(object sender, MouseButtonEventArgs e)
        {

            #region 打开GITHUB网页
            string url = "https://github.com/light-come/Cesium-OpticalCore";
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;    //不使用shell启动
            p.StartInfo.RedirectStandardInput = true;//喊cmd接受标准输入
            p.StartInfo.RedirectStandardOutput = false;//不想听cmd讲话所以不要他输出
            p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            p.StartInfo.CreateNoWindow = true;//不显示窗口
            p.Start();
            //向cmd窗口发送输入信息 后面的&exit告诉cmd运行好之后就退出
            p.StandardInput.WriteLine("start " + url + "&exit");
            p.StandardInput.AutoFlush = true;
            p.WaitForExit();//等待程序执行完退出进程
            p.Close();
            #endregion
        }

        private void TitleLabel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //if (_Browser.webView != null && _Browser.webView.CoreWebView2 != null)
            //{
            //    TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            //    _Browser.webView.Source = new Uri("http://127.0.0.1:" + Model.Web_Server_Config.Web_Server_Port.ToString() + "/index.html?time:" + Convert.ToInt64(ts.TotalSeconds).ToString());
            //}
        }

    }
}
