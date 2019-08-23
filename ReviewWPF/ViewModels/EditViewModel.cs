﻿using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewWPF
{
    public class EditViewModel : Screen
    {
        private int _isAdd;

        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; NotifyOfPropertyChange(nameof(Title)); }
        }

        private HumanEnt _human;
        public HumanEnt Human
        {
            get { return _human; }
            set { _human = value; NotifyOfPropertyChange(nameof(Human)); }
        }

        public EditViewModel(HumanEnt ent, int isAdd)
        {
            _isAdd = isAdd;
            if (isAdd == 1)
                Title = "添加";
            else if (isAdd == 2)
                Title = "编辑";
            else
                Title = "查看";
            Human = ent;
        }

        public EditViewModel() { }
    }
}
