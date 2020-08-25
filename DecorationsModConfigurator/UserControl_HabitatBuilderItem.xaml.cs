using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace DecorationsModConfigurator
{
    /// <summary>
    /// Logique d'interaction pour UserControl_HabitatBuilderItem.xaml
    /// </summary>
    public partial class UserControl_HabitatBuilderItem : UserControl, INotifyPropertyChanged
    {
        public static readonly DependencyProperty ItemEnabledProperty = DependencyProperty.Register("ItemEnabled", typeof(bool), typeof(UserControl_HabitatBuilderItem), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsRender)); //, new PropertyChangedCallback(ItemEnabledChanged)));
        public static readonly DependencyProperty ItemNameProperty = DependencyProperty.Register("ItemName", typeof(string), typeof(UserControl_HabitatBuilderItem), new FrameworkPropertyMetadata("Unknown", FrameworkPropertyMetadataOptions.AffectsRender)); //, new PropertyChangedCallback(ItemNameChanged)));

        public bool ItemEnabled { get => (bool)GetValue(ItemEnabledProperty); set => SetValue(ItemEnabledProperty, value); }
        public string ItemName { get => (string)GetValue(ItemNameProperty); set => SetValue(ItemNameProperty, value); }

        public UserControl_HabitatBuilderItem()
        {
            InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;
        }

        public void RefreshGUI() => OnPropertyChanged("");

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
