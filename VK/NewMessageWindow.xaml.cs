using System;
using System.Windows;
using System.Windows.Media.Animation;
using VK.Utils;
using VKAPI.Model.LongPullMessageModel;

namespace VK
{
	/// <summary>
	/// Логика взаимодействия для NewMessageView.xaml
	/// </summary>
	public partial class NewMessageWindow : Window
	{
		LongPullMessageModel _longPullMessageModel;

		DoubleAnimation anim;
		int left;
		int top;
		DependencyProperty prop;
		int end;
		public NewMessageWindow(LongPullMessageModel longPullMessageModel)
		{
			_longPullMessageModel = longPullMessageModel;

			Message message = new Message();
			message.message = _longPullMessageModel.response.messages.items[0].body;
			message.Author = _longPullMessageModel.response.profiles[0].first_name + " " + _longPullMessageModel.response.profiles[0].last_name;
			message.Photo = _longPullMessageModel.response.profiles[0].photo_medium_rec;

			DataContext = message;

			InitializeComponent();
			TrayPos tpos = new TrayPos();
			tpos.getXY((int)this.Width, (int)this.Height, out top, out left, out prop, out end);
			this.Top = top;
			this.Left = left;
			anim = new DoubleAnimation(end, TimeSpan.FromSeconds(1));
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			AnimationClock clock = anim.CreateClock();
			this.ApplyAnimationClock(prop, clock);
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			this.DragMove();
		}
	}

	public class Message
	{
		public string Author { get; set; }
		public string message { get; set; }

		public string Photo { get; set; }
	}
}
