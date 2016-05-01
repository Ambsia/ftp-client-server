using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using FTPLibrary;

namespace FTPClient.model
{
    [ValueConversion(typeof(string), typeof(bool))]
    public class HeaderToImageConverter : IValueConverter
    {
        public static HeaderToImageConverter Instance = new HeaderToImageConverter();

        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((value as string).Contains(@"\"))
            {
                Uri uri = new Uri("pack://application:,,,/view/Images/diskdrive.png");
                BitmapImage source = new BitmapImage(uri);
                return source;
            }
            else
            {
                Uri uri = new Uri("pack://application:,,,/view/Images/folder.png");
                BitmapImage source = new BitmapImage(uri);
                return source;
            }
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Cannot convert back");
        }
    }


    public class HeaderToImageConverterRemote : HeaderToImageConverter
    {
        public new static HeaderToImageConverterRemote Instance = new HeaderToImageConverterRemote();

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value is Directory)
            {
                Uri uri = new Uri("pack://application:,,,/view/Images/folder.png");
                BitmapImage source = new BitmapImage(uri);
                return source;
            }
            else
            {
                Uri uri = new Uri("pack://application:,,,/view/Images/file.png");
                BitmapImage source = new BitmapImage(uri);
                return source;
            }
        }
    }
}