namespace TTLX.WindowsTool.Common.Core
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public static class PasswordBoxAssistant
    {
        public static readonly DependencyProperty BoundPassword = DependencyProperty.RegisterAttached("BoundPassword", typeof(string), typeof(PasswordBoxAssistant), new PropertyMetadata(string.Empty, new PropertyChangedCallback(null, OnBoundPasswordChanged)));
        public static readonly DependencyProperty BindPassword = DependencyProperty.RegisterAttached("BindPassword", typeof(bool), typeof(PasswordBoxAssistant), new PropertyMetadata(false, new PropertyChangedCallback(null, OnBindPasswordChanged)));
        private static readonly DependencyProperty UpdatingPassword = DependencyProperty.RegisterAttached("UpdatingPassword", typeof(bool), typeof(PasswordBoxAssistant), new PropertyMetadata(false));

        public static bool GetBindPassword(DependencyObject dp) => 
            ((bool) dp.GetValue(BindPassword));

        public static string GetBoundPassword(DependencyObject dp) => 
            ((string) dp.GetValue(BoundPassword));

        private static bool GetUpdatingPassword(DependencyObject dp) => 
            ((bool) dp.GetValue(UpdatingPassword));

        private static void HandlePasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox dp = sender as PasswordBox;
            SetUpdatingPassword(dp, true);
            SetBoundPassword(dp, dp.Password);
            SetUpdatingPassword(dp, false);
        }

        private static void OnBindPasswordChanged(DependencyObject dp, DependencyPropertyChangedEventArgs e)
        {
            PasswordBox box = dp as PasswordBox;
            if (box != null)
            {
                bool flag = (bool) e.get_NewValue();
                if ((bool) e.get_OldValue())
                {
                    box.PasswordChanged -= new RoutedEventHandler(PasswordBoxAssistant.HandlePasswordChanged);
                }
                if (flag)
                {
                    box.PasswordChanged += new RoutedEventHandler(PasswordBoxAssistant.HandlePasswordChanged);
                }
            }
        }

        private static void OnBoundPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PasswordBox dp = d as PasswordBox;
            if ((d != null) && GetBindPassword(d))
            {
                dp.PasswordChanged -= new RoutedEventHandler(PasswordBoxAssistant.HandlePasswordChanged);
                string str = (string) e.get_NewValue();
                if (!GetUpdatingPassword(dp))
                {
                    dp.Password = str;
                }
                dp.PasswordChanged += new RoutedEventHandler(PasswordBoxAssistant.HandlePasswordChanged);
            }
        }

        public static void SetBindPassword(DependencyObject dp, bool value)
        {
            dp.SetValue(BindPassword, value);
        }

        public static void SetBoundPassword(DependencyObject dp, string value)
        {
            dp.SetValue(BoundPassword, value);
        }

        private static void SetUpdatingPassword(DependencyObject dp, bool value)
        {
            dp.SetValue(UpdatingPassword, value);
        }
    }
}

