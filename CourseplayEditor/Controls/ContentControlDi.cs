using System;
using System.Windows;
using System.Windows.Controls;

namespace CourseplayEditor.Controls
{
    public class ContentControlDi : ContentControl
    {
        public static readonly DependencyProperty ServiceProviderProperty = DependencyProperty.Register(
            "ServiceProvider",
            typeof(IServiceProvider),
            typeof(ContentControlDi),
            new PropertyMetadata(default(IServiceProvider), ServiceProviderPropertyChanged)
        );

        private static void ServiceProviderPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is ContentControlDi control))
            {
                return;
            }

            if (control.ContentType == null || !(e.NewValue is IServiceProvider serviceProvider))
            {
                return;
            }
            
            control.Content = serviceProvider.GetService(control.ContentType);
        }

        public IServiceProvider ServiceProvider
        {
            get { return (IServiceProvider) GetValue(ServiceProviderProperty); }
            set { SetValue(ServiceProviderProperty, value); }
        }

        public static readonly DependencyProperty ContentTypeProperty = DependencyProperty.Register(
            "ContentType",
            typeof(Type),
            typeof(ContentControlDi),
            new PropertyMetadata(default(Type), ContentTypePropertyChanged)
        );

        private static void ContentTypePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is ContentControlDi control))
            {
                return;
            }

            if (!(e.NewValue is Type contentType) || control.ServiceProvider == null)
            {
                return;
            }

            control.Content = control.ServiceProvider.GetService(contentType);
        }

        public Type ContentType
        {
            get { return (Type) GetValue(ContentTypeProperty); }
            set { SetValue(ContentTypeProperty, value); }
        }

        public ContentControlDi()
        {
        }
    }
}
