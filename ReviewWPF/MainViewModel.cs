using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ReviewWPF
{
    public class MainViewModel : Screen
    {
        //变色定时器
        private Timer ColorTimer = null;
        private int _clickCount = 1;
        private bool _isChange = true;
        private List<string> Colors = new List<string>() { "Yellow", "Blue", "Green", "Red", "HotPink", "Purple", "Orange" };

        private Visibility _homeVis;
        public Visibility HomeVis
        {
            get { return _homeVis; }
            set { _homeVis = value; NotifyOfPropertyChange(nameof(HomeVis)); }
        }

        private void InitData()
        {

        }

        #region 提示框
        public void ClickMe()
        {
            if (MessageBox.Show("点击了" + _clickCount + "次", "提示", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                MessageBox.Show("正确了", "提示");
            }
            _clickCount += 1;
        }
        #endregion

        #region 变色
        private string _btnContent = "点我变色";
        public string BtnContent
        {
            get { return _btnContent; }
            set
            {
                _btnContent = value;
                NotifyOfPropertyChange(nameof(BtnContent));
            }
        }

        private Visibility _colorVis = Visibility.Collapsed;
        public Visibility ColorVis
        {
            get { return _colorVis; }
            set { _colorVis = value; NotifyOfPropertyChange(nameof(ColorVis)); }
        }

        private string _bkColor = "white";
        public string BkColor
        {
            get { return _bkColor; }
            set
            {
                _bkColor = value;
                NotifyOfPropertyChange(nameof(BkColor));
            }
        }

        public void ChangeColor()
        {
            if (_isChange)
            {
                CloseAllGrid();
                ColorVis = Visibility.Visible;
                ColorTimer = new Timer(e => { ChColor(); }, null, 0, 2000);
                BtnContent = "停止";
            }
            else
            {
                BtnContent = "点我变色";
                BkColor = "white";
                ColorTimer.Dispose();
            }
            _isChange = !_isChange;
        }

        private void ChColor()
        {
            Random r = new Random();
            BkColor = Colors[r.Next(0, 6)];
        }
        #endregion

        #region 树列表
        private Visibility _treeVis = Visibility.Collapsed;
        /// <summary>
        /// 树列表显示
        /// </summary>
        public Visibility TreeVis
        {
            get { return _treeVis; }
            set
            {
                _treeVis = value;
                NotifyOfPropertyChange(nameof(TreeVis));
            }
        }

        private ObservableCollection<BaseTreeVM> _treeData;
        public ObservableCollection<BaseTreeVM> TreeData
        {
            get { return _treeData; }
            set { _treeData = value; NotifyOfPropertyChange(nameof(TreeData)); }
        }

        public void SetTree()
        {
            CloseAllGrid();
            TreeVis = Visibility.Visible;
            if (!_isChange)
                ChangeColor();
            BaseTreeVM vm = new BaseTreeVM() { DisplayText = "根节点", Children = new ObservableCollection<BaseTreeVM>() };
            for (int i = 1; i <= 10; i++)
            {
                BaseTreeVM cvm = new BaseTreeVM() { DisplayText = "子节点" + i, Children = new ObservableCollection<BaseTreeVM>() };
                BaseTreeVM ccvm = new BaseTreeVM() { DisplayText = "子子节点" + i };
                cvm.Children.Add(ccvm);
                vm.Children.Add(cvm);
            }
            TreeData = new ObservableCollection<BaseTreeVM>() { vm };
        }

        public void ClickNode(object sender, MouseButtonEventArgs e)
        {
            TextBlock tb = sender as TextBlock;
            BaseTreeVM vm = tb.DataContext as BaseTreeVM;
            if (e.LeftButton == MouseButtonState.Pressed && e.ClickCount == 2)
            {
                MessageBox.Show("双击了" + vm.DisplayText, "提示");
            }
        }
        #endregion

        #region 表格
        private Visibility _listVis = Visibility.Collapsed;
        /// <summary>
        /// 表格显示
        /// </summary>
        public Visibility ListVis
        {
            get { return _listVis; }
            set
            {
                _listVis = value;
                NotifyOfPropertyChange(nameof(ListVis));
            }
        }

        private BindableCollection<HumanEnt> _humanList;
        public BindableCollection<HumanEnt> HumanList
        {
            get { return _humanList; }
            set { _humanList = value; NotifyOfPropertyChange(nameof(HumanList)); }
        }

        public void SetDataGrid()
        {
            CloseAllGrid();
            ListVis = Visibility.Visible;
            if (!_isChange)
                ChangeColor();
            HumanList = new BindableCollection<HumanEnt>();
            List<string> names = new List<string>() { "刘一", "陈二", "张三", "李四", "王五", "赵六", "孙七", "周八", "吴九", "郑十" };
            Random r = new Random();
            for (int i = 1; i <= 5; i++)
            {
                HumanEnt human = new HumanEnt() { ID = i, Name = names[r.Next(0, 10)], Sex = r.Next(1, 3), Age = r.Next(18, 61), Phone = r.Next(139123, 139456).ToString(), Education = EducationEnum.本科, Email = "123@163.com", Birthday = DateTime.Now.AddDays(i - 1) };
                HumanList.Add(human);
            }
        }


        #region 表格操作弹框
        public void AddInfo()
        {
            EditViewModel vm = new EditViewModel(null, 1);
            var view = ViewLocator.LocateForModel(vm, null, null) as Window;
            if (view != null)
            {
                ViewModelBinder.Bind(vm, view, null);
                view.Owner = Application.Current.MainWindow;
                view.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                view.ShowDialog();
            }
        }

        public void EditInfo(HumanEnt ent)
        {
            EditViewModel vm = new EditViewModel(ent, 2);
            var view = ViewLocator.LocateForModel(vm, null, null) as Window;
            if (view != null)
            {
                ViewModelBinder.Bind(vm, view, null);
                view.Owner = Application.Current.MainWindow;
                view.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                view.ShowDialog();
            }
        }

        public void LookInfo(HumanEnt ent)
        {
            EditViewModel vm = new EditViewModel(ent, 3);
            var view = ViewLocator.LocateForModel(vm, null, null) as Window;
            if (view != null)
            {
                ViewModelBinder.Bind(vm, view, null);
                view.Owner = Application.Current.MainWindow;
                view.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                view.ShowDialog();
            }
        }

        public void DeleteInfo(HumanEnt ent)
        {
            if (MessageBox.Show("确定删除人员" + ent.Name + "?", "提示", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                HumanList.Remove(ent);
            }
        }
        #endregion
        #endregion

        #region 动画
        private Visibility _animationVis = Visibility.Collapsed;
        public Visibility AnimationVis
        {
            get { return _animationVis; }
            set { _animationVis = value; NotifyOfPropertyChange(nameof(AnimationVis)); }
        }

        public void AnimationBtn()
        {
            CloseAllGrid();
            AnimationVis = Visibility.Visible;
            if (!_isChange)
                ChangeColor();
        }
        #endregion

        /// <summary>
        /// 隐藏所有界面
        /// </summary>
        /// 创建人:刘俊  创建时间:2019/8/22 17:47
        private void CloseAllGrid()
        {
            HomeVis = Visibility.Collapsed;
            ColorVis = Visibility.Collapsed;
            TreeVis = Visibility.Collapsed;
            ListVis = Visibility.Collapsed;
            AnimationVis = Visibility.Collapsed;
        }

        public MainViewModel()
        {
            InitData();
        }
    }

    /// <summary>
    /// 树型数据VM
    /// </summary>
    /// 创建人:刘俊  创建时间:2019/8/22 9:58
    /// <seealso cref="Caliburn.Micro.Screen" />
    public class BaseTreeVM : Screen
    {
        private bool _isSelected;
        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                NotifyOfPropertyChange("IsSelected");

                if (_isSelected && this.OnSelected != null)
                {
                    this.OnSelected(this);
                }
            }
        }

        private bool _isChecked;
        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                NotifyOfPropertyChange("IsChecked");

                if (_isChecked && this.OnChecked != null)
                {
                    this.OnChecked(this);
                }
            }
        }

        private bool _showCheckBox;
        /// <summary>
        /// 是否显示选框
        /// </summary>
        public bool ShowCheckBox
        {
            get { return _showCheckBox; }
            set
            {
                _showCheckBox = value;
                NotifyOfPropertyChange("ShowCheckBox");
            }
        }


        private bool _isExpanded;
        /// <summary>
        /// 是否展开
        /// </summary>
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                _isExpanded = value;
                NotifyOfPropertyChange("IsExpanded");

                if (_isExpanded && _parent != null)
                {
                    _parent.IsExpanded = true;
                }
                if (_isExpanded && this.OnExpanded != null)
                {
                    this.OnExpanded(this);
                }
            }
        }

        private BaseTreeVM _parent;
        /// <summary>
        /// 父节点
        /// </summary>
        public new BaseTreeVM Parent
        {
            get { return _parent; }
            set
            {
                _parent = value;
                NotifyOfPropertyChange("Parent");
            }
        }

        private string _displayText;
        /// <summary>
        /// 节点展示的名称
        /// </summary>
        public string DisplayText
        {
            get { return _displayText; }
            set
            {
                _displayText = value;
                NotifyOfPropertyChange("DisplayText");
            }
        }

        private ObservableCollection<BaseTreeVM> _children = new ObservableCollection<BaseTreeVM>();
        /// <summary>
        /// 子节点集合
        /// </summary>
        public ObservableCollection<BaseTreeVM> Children
        {
            get { return _children; }
            set { _children = value; }
        }

        /// <summary>
        /// 节点展开的事件
        /// </summary>
        public event Action<BaseTreeVM> OnExpanded;

        /// <summary>
        /// 节点选中的事件
        /// </summary>
        public event Action<BaseTreeVM> OnSelected;

        /// 节点选择的事件
        /// </summary>
        public event Action<BaseTreeVM> OnChecked;

        private dynamic _current;
        /// <summary>
        /// 当前节点的对象
        /// </summary>
        public dynamic Current
        {
            get { return _current; }
            set
            {
                _current = value;
                NotifyOfPropertyChange("Current");
            }
        }
    }

    public class HumanEnt
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Sex { get; set; }
        public string Phone { get; set; }
        public EducationEnum Education { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
    }

    public enum EducationEnum
    {
        小学 = 1,
        初中,
        高中,
        本科,
        专科,
        硕士,
        博士
    }
}
