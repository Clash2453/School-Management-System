﻿using OOPStage3Library.Classes.Controls;
using OOPStage3Library.Classes.Events;
using OOPStage3Library.Classes.Users;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace OOPStage3.Views
{
    public partial class DayControl : UserControl
    {
        private Event _customEvent;
        private EventControls _eventControls = new();
        private readonly User _user;
        private int _month, _year;
        public DayControl(User user, int month, int year)
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint |
                    ControlStyles.AllPaintingInWmPaint |
                    ControlStyles.OptimizedDoubleBuffer, true);
            int day = Convert.ToInt32(labelCustomEvent.Text);
            this.ForeColor = Color.White;
            this.BackColor = Color.FromArgb(100, 0, 0, 0);
            _user = user;
            _year = year;
            _month = month;
            if (_eventControls.EventExists(day, month, year))
            {
                var firstEvent = _eventControls.GetEvent(int.Parse(labelCustomEvent.Text), month, year).First();
                this.BackColor = Color.FromArgb(100, firstEvent.GetColor());
            }
            label.Text = "";

        }

        public void Days(int numday)
        {
            labelCustomEvent.Text = numday + "";
            if (_customEvent != null)
                label.Text = _user.GetBaseInfo()[0];
        }
        private void DayControl_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            EventCreationForm eventFillForm = new EventCreationForm(int.Parse(labelCustomEvent.Text), _eventControls, _user);
            eventFillForm.FormClosed += (s, e) =>
            {
                if (_eventControls.GetEvent(int.Parse(labelCustomEvent.Text), _month, _year).Count() != 0)
                {

                    this.BackColor = eventFillForm.BackColor;
                    label.Text = _user.GetBaseInfo()[0];
                }
            };  
            eventFillForm.ShowDialog();
        }
    }
}
