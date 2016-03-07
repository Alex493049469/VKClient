using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace VK.Services
{
	//Супер глобальный класс который имеет доступ ко всем сервисам
	public class ManagerService : Singleton<ManagerService>
	{
		//получает различные события : набор текста, приход нового сообщения и тд
		private EventsService _eventService;

		//отвечает за получение и обновление диалогов
		private DialogService _dialogService;

		private ManagerService()
		{ }

		public void StartManagerService()
		{
			_eventService = new EventsService();
			_dialogService = new DialogService(_eventService);
		}


		public DialogService DialogService
		{
			get { return _dialogService; }
			set { _dialogService = value; }
		}

		public EventsService EventService
		{
			get { return _eventService; }
			set { _eventService = value; }
		}
	}
}
