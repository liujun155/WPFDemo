using Caliburn.Micro;
using MySql.Data.MySqlClient;
using ReviewWPF.DataOperate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ReviewWPF
{
    public class EditViewModel : Screen
    {
        private int _isAdd;

        private bool _isEdit;
        public bool IsEdit
        {
            get { return _isEdit; }
            set
            {
                _isEdit = value;
                NotifyOfPropertyChange(nameof(IsEdit));
            }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; NotifyOfPropertyChange(nameof(Title)); }
        }

        private string _selSex;
        public string SelSex
        {
            get { return _selSex; }
            set { _selSex = value; NotifyOfPropertyChange(nameof(SelSex)); }
        }

        private List<string> _sexs = new List<string>() { "男", "女" };
        public List<string> SexList
        {
            get { return _sexs; }
            set { _sexs = value; NotifyOfPropertyChange(nameof(SexList)); }
        }

        private HumanEnt _human = new HumanEnt();
        public HumanEnt Human
        {
            get { return _human; }
            set { _human = value; NotifyOfPropertyChange(nameof(Human)); }
        }

        public void BtnOK()
        {
            if (string.IsNullOrEmpty(Human.Name) || string.IsNullOrWhiteSpace(Human.Name))
            {
                MessageBox.Show("请填写姓名", "提示");
                return;
            }
            if (Human.Age == null)
            {
                MessageBox.Show("请填写年龄", "提示");
                return;
            }
            if (SelSex == null)
            {
                MessageBox.Show("请选择性别", "提示");
                return;
            }
            if (SelSex == "男")
                Human.Sex = 1;
            else
                Human.Sex = 2;
            if (_isAdd == 1)
            {
                if (HumanService.InsertHuman(Human))
                {
                    MessageBox.Show("添加成功", "提示");
                    this.TryClose();
                }
                else
                    MessageBox.Show("添加失败", "提示");
            }
            else if(_isAdd == 2)
            {
                if (HumanService.UpdateHuman(Human))
                {
                    MessageBox.Show("修改成功", "提示");
                    this.TryClose();
                }
                else
                    MessageBox.Show("修改失败", "提示");
            }
        }

        public void BtnCancel()
        {
            this.TryClose();
        }

        public EditViewModel(HumanEnt ent, int isAdd)
        {
            _isAdd = isAdd;
            IsEdit = true;
            if (isAdd == 1)
                Title = "添加";
            else if (isAdd == 2)
                Title = "编辑";
            else
            {
                Title = "查看";
                IsEdit = false;
            }
            if (ent != null)
            {
                Human = ent;
                SelSex = SexList[Human.Sex - 1];
            }
        }

        public EditViewModel() { }
    }
}
