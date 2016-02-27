using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKAPI;
using VKAPI.Model.DialogsModel;

namespace VK.Services
{
	/// <summary>
	/// отвечает за получение диалогов и их обновление, оповещает об изменении диалогов UI
	/// </summary>
	public class DialogService
	{
		//модель данных диалогов
		private DialogsModel _dialogModel;

		//для доступа к данным диалогов
		VkApi _vk = new VkApi();

		//событие на которое надо подписаться для оповещения что диалоги изменились и их нужно обновить в UI
		public delegate void MethodContainer();
		public event MethodContainer ChangeDialog;

		/// <summary>
		/// возвращает список диалогов
		/// </summary>
		/// <returns></returns>
		public DialogsModel GetDialog(int count, int offset = 0)
		{

			return null;
		}
		

		//класс так же получает ссылку на сервис messageService который будет оповещать о приходе нового сообщения
		//В ответ на это диалоги тоже должны будут отбновлены(Диалог в который пришло сообщение)


	}
}
