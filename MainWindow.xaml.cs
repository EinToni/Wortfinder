using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wortfinder
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly FieldGenerator fieldGenerator;
		private readonly LetterController letterController;
		private bool dragActive = false;
		public MainWindow()
		{
			InitializeComponent();
			letterController = new LetterController(LetterGrid, OutputText);
			fieldGenerator = new FieldGenerator(LetterGrid, letterController);
			fieldGenerator.InitializeField(3);
			LetterGrid.ShowGridLines = true;
		}

		public void MouseRelease(object sender, RoutedEventArgs e)
		{
			letterController.MouseRelease();
		}
	}
}
