using MahApps.Metro.Controls;

namespace Yatzee
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : MetroWindow
  {
    public MainWindow()
    {
      InitializeComponent();
      this.DataContext = new ViewModel();
    }
  }
}
