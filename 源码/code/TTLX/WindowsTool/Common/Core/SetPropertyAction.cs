namespace TTLX.WindowsTool.Common.Core
{
    using System;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Interactivity;

    public class SetPropertyAction : TriggerAction<FrameworkElement>
    {
        public static readonly DependencyProperty PropertyNameProperty = DependencyProperty.Register("PropertyName", typeof(string), typeof(SetPropertyAction));
        public static readonly DependencyProperty PropertyValueProperty = DependencyProperty.Register("PropertyValue", typeof(object), typeof(SetPropertyAction));
        public static readonly DependencyProperty TargetObjectProperty = DependencyProperty.Register("TargetObject", typeof(object), typeof(SetPropertyAction));

        protected override void Invoke(object parameter)
        {
            object obj2 = this.TargetObject ?? base.AssociatedObject;
            obj2.GetType().GetProperty(this.PropertyName, BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).SetValue(obj2, this.PropertyValue, null);
        }

        public string PropertyName
        {
            get => 
                ((string) base.GetValue(PropertyNameProperty));
            set => 
                base.SetValue(PropertyNameProperty, value);
        }

        public object PropertyValue
        {
            get => 
                base.GetValue(PropertyValueProperty);
            set => 
                base.SetValue(PropertyValueProperty, value);
        }

        public object TargetObject
        {
            get => 
                base.GetValue(TargetObjectProperty);
            set => 
                base.SetValue(TargetObjectProperty, value);
        }
    }
}

